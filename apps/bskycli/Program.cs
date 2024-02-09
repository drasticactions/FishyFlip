// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using DotMake.CommandLine;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#if DEBUG
var loggerFactory = LoggerFactory.Create(
    builder => builder
        .AddConsole()
        .AddDebug()
        .SetMinimumLevel(LogLevel.Debug));
#else
var loggerFactory = LoggerFactory.Create(
    builder => builder
        .AddDebug()
        .SetMinimumLevel(LogLevel.Debug));
#endif

var logger = loggerFactory.CreateLogger<RootCommand>();

Cli.Ext.ConfigureServices(service =>
{
    service.AddSingleton(loggerFactory);
});

try
{
    logger.LogDebug("Console Arguments: bskycli" + string.Join(" ", args));
    await Cli.RunAsync<RootCommand>(args);
}
catch (Exception e)
{
    logger.LogError(e, "An error occurred.");
}

/// <summary>
/// Root CLI command.
/// </summary>
[CliCommand(Description = "bskycli", ShortFormAutoGenerate = false)]
public class RootCommand
{
    /// <summary>
    /// Post Command.
    /// </summary>
    [CliCommand(Description = "Post a message with images")]
    public class ImagePostCommand(ILoggerFactory loggerFactory) : CredentialCommandBase(loggerFactory)
    {
        /// <summary>
        /// Gets or sets the message to post.
        /// </summary>
        [CliOption(Description = "Text to post", Required = false)]
        public string Text { get; set; } = string.Empty;

        [CliOption(
            Description = "Images to post. Max of 4.",
            AllowMultipleArgumentsPerToken = true,
            ValidationRules = CliValidationRules.ExistingFile)]
        public required IEnumerable<FileInfo> Images { get; set; }

        [CliOption(Description = "Alt Text for images. Max of 4. Text is mapped to images in order of entry.", AllowMultipleArgumentsPerToken = true, Required = false)]
        public IEnumerable<string>? AltText { get; set; }

        [CliOption(Description = "Languages used in post. Can use two or four letter ISO (Ex. 'th' 'en-US')", AllowMultipleArgumentsPerToken = true, Required = false)]
        public IEnumerable<string>? Languages { get; set; }

        /// <summary>
        /// Run the command.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override async Task RunAsync()
        {
            var logger = loggerFactory.CreateLogger<ImagePostCommand>();
            if (!this.Images.Any())
            {
                throw new Exception("Images are required.");
            }

            this.Languages ??= new List<string>();

            var parsedText = MarkdownLinkParser.ParseMarkdown(this.Text ?? string.Empty);
            var facets = MarkdownLinkParser.GenerateFacets(parsedText);
            var images = this.Images?.Take(4).ToList() ?? throw new Exception("No images provided.");
            var alts = this.AltText?.Take(4).ToList() ?? new List<string>();

            var imageEmbeds = new List<ImageEmbed>();
            var fileDetector = new FileContentTypeDetector(logger);

            // Verify the images before we upload
            var contentTypes = new List<string>();
            foreach (var image in images)
            {
                await using var fileStream = image.OpenRead();
                var contentType = fileDetector.GetContentType(fileStream);
                if (contentType == "unsupported")
                {
                    throw new Exception($"Unsupported file type for {image.FullName}");
                }

                contentTypes.Add(contentType);
            }

            if (contentTypes.Any(x => x != "image/jpeg" && x != "image/png" && x != "image/gif"))
            {
                throw new Exception("Unsupported file type.");
            }

            // Log in and upload the images.
            await base.RunAsync();
            ArgumentNullException.ThrowIfNull(this.ATProtocol);

            for (var i = 0; i < this.Images.Count(); i++)
            {
                var image = images.ElementAt(i);
                var altText = alts.ElementAtOrDefault(i) ?? string.Empty;
                var contentType = contentTypes.ElementAt(i);
                await using var fileStream = image.OpenRead();
                var content = new StreamContent(fileStream);
                content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                var blobResult = (await this.ATProtocol!.Repo.UploadBlobAsync(content)).HandleResult();
                logger.LogDebug($"Uploaded {image.Name} to {blobResult.Blob.Ref}");
                var img = blobResult.Blob.ToImage() ?? throw new Exception($"Failed to convert blob {image.Name} to image.");
                var imgEmbed = new ImageEmbed(img, altText);
                logger.LogDebug($"{imgEmbed.Image!.Ref} {imgEmbed.Alt}");
                imageEmbeds.Add(imgEmbed);
            }

            var postResult = (await this.ATProtocol!.Repo.CreatePostAsync(
                parsedText?.ModifiedString ?? string.Empty,
                facets,
                langs: this.Languages.ToArray(),
                embed: new ImagesEmbed(imageEmbeds.ToArray()))).HandleResult();
            logger.LogInformation($"Post Created: {postResult.Uri} {postResult.Cid}");
            var url = $"https://bsky.app/profile/{postResult.Uri!.Did}/post/{postResult.Uri!.Pathname.Split("/").Last()}";
            Console.WriteLine($"Post: {postResult.Uri}");
            Console.WriteLine($"Post URL: {url}");
        }
    }

    /// <summary>
    /// Post Command.
    /// </summary>
    [CliCommand(Description = "Post a message")]
    public class PostCommand(ILoggerFactory loggerFactory) : CredentialCommandBase(loggerFactory)
    {
        /// <summary>
        /// Gets or sets the message to post.
        /// </summary>
        [CliOption(Description = "Text to post")]
        public required string Text { get; set; }

        /// <summary>
        /// Gets or sets the message to the link card.
        /// </summary>
        [CliOption(Description = "Link Card Url. Embed into the post as external.", Required = false)]
        public string LinkCardUrl { get; set; } = string.Empty;

        [CliOption(Description = "Languages used in post. Can use two or four letter ISO (Ex. 'th' 'en-US')", AllowMultipleArgumentsPerToken = true, Required = false)]
        public IEnumerable<string>? Languages { get; set; }

        /// <summary>
        /// Run the command.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override async Task RunAsync()
        {
            var logger = loggerFactory.CreateLogger<PostCommand>();
            if (string.IsNullOrEmpty(this.Text))
            {
                throw new Exception("Text is required.");
            }

            this.Languages ??= new List<string>();

            var parsedText = MarkdownLinkParser.ParseMarkdown(this.Text);
            var facets = MarkdownLinkParser.GenerateFacets(parsedText);

            if (parsedText?.ModifiedString == null)
            {
                throw new Exception("Failed to parse text.");
            }

            await base.RunAsync();
            ArgumentNullException.ThrowIfNull(this.ATProtocol);

            ExternalEmbed? externalEmbed = null;
            if (!string.IsNullOrEmpty(this.LinkCardUrl))
            {
                var parser = new OpenGraphParser(this.ATProtocol!, logger);
                externalEmbed = await parser.GenerateExternalEmbed(this.LinkCardUrl);
            }

            var postResult = (await this.ATProtocol!.Repo.CreatePostAsync(
                parsedText.ModifiedString,
                facets,
                externalEmbed,
                langs: this.Languages.ToArray())).HandleResult();
            logger.LogInformation($"Post Created: {postResult.Uri} {postResult.Cid}");
            var url = $"https://bsky.app/profile/{postResult.Uri!.Did}/post/{postResult.Uri!.Pathname.Split("/").Last()}";
            Console.WriteLine($"Post: {postResult.Uri}");
            Console.WriteLine($"Post URL: {url}");
        }
    }

    /// <summary>
    /// Base Credential Command.
    /// </summary>
    public abstract class CredentialCommandBase(ILoggerFactory loggerFactory)
    {
        /// <summary>
        /// Gets or sets the Bluesky Username.
        /// This property is used to store the username of the user in the Bluesky platform.
        /// </summary>
        [CliOption(Description = "Bluesky Identifier")]
        public required string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the Bluesky App-Password.
        /// This property is used to store the password of the user in the Bluesky platform.
        /// </summary>
        [CliOption(Description = "Bluesky App-Password")]
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the Bluesky Instance URL.
        /// This property is used to store the URL of the Bluesky instance the user wants to connect to.
        /// By default, it is set to "bsky.social".
        /// </summary>
        [CliOption(Description = "Bluesky Instance URL.", ValidationRules = CliValidationRules.LegalUri)]
        public string Instance { get; set; } = "https://bsky.social";

        /// <summary>
        /// Gets or sets the ATProtocol.
        /// </summary>
        public ATProtocol? ATProtocol { get; set; }

        /// <summary>
        /// Gets or sets the users session information.
        /// </summary>
        public Session? Session { get; set; }

        /// <summary>
        /// Run the command.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual async Task RunAsync()
        {
            var logger = loggerFactory.CreateLogger<CredentialCommandBase>();
            ATProtocolBuilder builder = new ATProtocolBuilder();
            Uri.TryCreate(this.Instance, UriKind.Absolute, out var instance);
            if (instance is null)
            {
                throw new Exception("Invalid URL");
            }

            this.ATProtocol = builder
                .WithInstanceUrl(instance)
                .Build();
            this.Session = (await this.ATProtocol.Server.CreateSessionAsync(this.Identifier, this.Password)).HandleResult();
            logger.LogInformation($"Authenticated as {this.Session.Handle}.");
            logger.LogDebug($"Session Did: {this.Session.Did}");
        }
    }

    private class MarkdownLinkParser
    {
        /// <summary>
        /// Generates an array of Facet objects from a ParsedMarkdown object.
        /// </summary>
        /// <param name="markdown">The ParsedMarkdown object to generate facets from.</param>
        /// <returns>An array of Facet objects representing the links in the markdown text, or null if the ParsedMarkdown object is null or contains no links.</returns>
        public static Facet[]? GenerateFacets(ParsedMarkdown? markdown)
        {
            if (markdown?.Links == null)
            {
                return null;
            }

            var facets = new List<Facet>();
            foreach (var link in markdown.Links)
            {
                var index = new FacetIndex(link.NewTextStartIndex, link.NewTextEndIndex + 1);
                var facetLink = FacetFeature.CreateLink(link.Url!);
                facets.Add(new Facet(index, facetLink));
            }

            return facets.ToArray();
        }

        /// <summary>
        /// Parses a markdown string and extracts all the links from it.
        /// </summary>
        /// <param name="input">The markdown string to parse.</param>
        /// <returns>A ParsedMarkdown object containing the original string, the modified string with links replaced by their text, and a list of LinkInfo objects representing each link.</returns>
        public static ParsedMarkdown? ParseMarkdown(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            string pattern = @"\[(?<text>.+?)\]\((?<url>.+?)\)";
            List<LinkInfo> links = new List<LinkInfo>();
            string modifiedString = input;
            int offset = 0;

            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    string text = match.Groups["text"].Value;
                    LinkInfo link = new LinkInfo
                    {
                        Text = text,
                        Url = match.Groups["url"].Value,
                        StartIndex = match.Index,
                        EndIndex = match.Index + match.Length - 1,
                        NewTextStartIndex = match.Index - offset,
                        NewTextEndIndex = match.Index - offset + text.Length - 1,
                    };

                    links.Add(link);

                    // Update the modifiedString by replacing the markdown link with its text
                    modifiedString = modifiedString.Remove(match.Index - offset, match.Length);
                    modifiedString = modifiedString.Insert(match.Index - offset, text);

                    // Update offset for future calculations
                    offset += match.Length - text.Length;
                }
            }

            string httpPattern = @"\b(?:https?://|www\.)\S+\b";
            matches = Regex.Matches(modifiedString, httpPattern);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    string url = match.Value;
                    LinkInfo link = new LinkInfo
                    {
                        Text = url,
                        Url = url,
                        StartIndex = match.Index,
                        EndIndex = match.Index + match.Length - 1,
                        NewTextStartIndex = match.Index,
                        NewTextEndIndex = match.Index + match.Length - 1,
                    };

                    links.Add(link);
                }
            }

            return new ParsedMarkdown
            {
                Links = links,
                OriginalString = input,
                ModifiedString = modifiedString,
            };
        }

        /// <summary>
        /// Represents a link extracted from a markdown text.
        /// </summary>
        public class LinkInfo
        {
            /// <summary>
            /// Gets or sets the display text of the link.
            /// </summary>
            public string? Text { get; set; }

            /// <summary>
            /// Gets or sets the URL of the link.
            /// </summary>
            public string? Url { get; set; }

            /// <summary>
            /// Gets or sets the start index of the link in the original markdown text.
            /// </summary>
            public int StartIndex { get; set; }

            /// <summary>
            /// Gets or sets the end index of the link in the original markdown text.
            /// </summary>
            public int EndIndex { get; set; }

            /// <summary>
            /// Gets or sets the start index of the link in the modified markdown text.
            /// </summary>
            public int NewTextStartIndex { get; set; }

            /// <summary>
            /// Gets or sets the end index of the link in the modified markdown text.
            /// </summary>
            public int NewTextEndIndex { get; set; }
        }

        /// <summary>
        /// Represents a markdown text that has been parsed for links.
        /// </summary>
        public class ParsedMarkdown
        {
            /// <summary>
            /// Gets or sets the list of links extracted from the markdown text.
            /// </summary>
            public List<LinkInfo>? Links { get; set; }

            /// <summary>
            /// Gets or sets the original markdown text.
            /// </summary>
            public string? OriginalString { get; set; }

            /// <summary>
            /// Gets or sets the modified markdown text after link extraction.
            /// </summary>
            public string? ModifiedString { get; set; }
        }
    }

    private class OpenGraphParser
    {
        private readonly HttpClient httpClient;
        private readonly ILogger logger;
        private readonly Regex regex;
        private readonly FileContentTypeDetector fileContentTypeDetector;
        private readonly ATProtocol ATProtocol;

        public OpenGraphParser(ATProtocol protocol, ILogger logger)
        {
            this.ATProtocol = protocol;
            this.logger = logger;
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("FishyFlip");
            this.regex = new Regex("<meta property=\"og:([^\"]+)\" content=\"([^\"]+)\"");
            this.fileContentTypeDetector = new FileContentTypeDetector(logger);
        }

        public async Task<ExternalEmbed?> GenerateExternalEmbed(string url)
        {
            var ogTags = await this.ParseAsync(url);
            if (ogTags.Count == 0)
            {
                return null;
            }

            Image? image = null;
            string? title = null;
            string? description = null;
            string? urlEmbed = null;

            if (ogTags.ContainsKey("image"))
            {
                var imageUrl = ogTags["image"];
                var imageResult = await this.httpClient.GetByteArrayAsync(imageUrl);
                var contentType = this.fileContentTypeDetector.GetContentType(imageResult);
                if (contentType == "unsupported")
                {
                    this.logger.LogWarning($"Unsupported file type for {imageUrl}");
                }
                else
                {
                    var content = new StreamContent(new MemoryStream(imageResult));
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var blobResult = (await this.ATProtocol!.Repo.UploadBlobAsync(content)).HandleResult();
                    this.logger.LogDebug($"Uploaded {imageUrl} to {blobResult.Blob.Ref}");
                    image = blobResult.Blob.ToImage() ?? throw new Exception($"Failed to convert blob {imageUrl} to image.");
                }
            }

            if (ogTags.ContainsKey("title"))
            {
                title = ogTags["title"];
            }

            if (ogTags.ContainsKey("description"))
            {
                description = ogTags["description"];
            }

            if (ogTags.ContainsKey("url"))
            {
                urlEmbed = ogTags["url"];
            }

            var external = new External(image, title, description, urlEmbed);
            return new ExternalEmbed(external);
        }

        private async Task<Dictionary<string, string>> ParseAsync(string url)
        {
            try
            {
                var htmlContent = await this.httpClient.GetStringAsync(url);
                return this.ParseOpenGraphTags(htmlContent);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to parse OpenGraph tags.");
                return new Dictionary<string, string>();
            }
        }

        private Dictionary<string, string> ParseOpenGraphTags(string html)
        {
            var ogTags = new Dictionary<string, string>();

            foreach (Match match in this.regex.Matches(html))
            {
                if (match.Groups.Count == 3)
                {
                    var property = match.Groups[1].Value.Trim();
                    var content = match.Groups[2].Value.Trim();
                    ogTags[property] = content;
                }
            }

            return ogTags;
        }
    }

    private class FileContentTypeDetector
    {
        private (byte[], string)[] fileSignatures;
        private ILogger logger;

        public FileContentTypeDetector(ILogger logger)
        {
            this.logger = logger;
            this.fileSignatures = new[]
            {
                (new byte[] { 0xFF, 0xD8, 0xFF }, "image/jpeg"), // JPEG
                (new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, "image/png"), // PNG
                ("GIF8"u8.ToArray(), "image/gif"), // GIF
            };
        }

        public string GetContentType(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return "unsupported";
            }

            return this.GetContentType(new MemoryStream(bytes));
        }

        public string GetContentType(Stream? fileStream)
        {
            if (fileStream == null || fileStream.Length == 0)
            {
                return "unsupported";
            }

            long originalPosition = fileStream.Position;
            fileStream.Position = 0;

            foreach (var (signature, mimeType) in this.fileSignatures)
            {
                var buffer = new byte[signature.Length];
                if (fileStream.Read(buffer, 0, signature.Length) == signature.Length)
                {
                    if (signature.SequenceEqual(buffer))
                    {
                        fileStream.Position = originalPosition;
                        this.logger.LogDebug($"Detected file type: {mimeType}");
                        return mimeType;
                    }
                }

                fileStream.Position = 0;
            }

            fileStream.Position = originalPosition;
            this.logger.LogDebug("Unsupported file type.");
            return "unsupported";
        }
    }
}

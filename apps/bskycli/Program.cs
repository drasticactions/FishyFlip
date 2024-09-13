// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Bskycli;
using ConsoleAppFramework;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

var app = ConsoleApp.Create();
app.Add<AppCommands>();
app.Run(args);

/// <summary>
/// App Commands.
/// </summary>
#pragma warning disable SA1649 // File name should match first type name
public class AppCommands
#pragma warning restore SA1649 // File name should match first type name
{
    /// <summary>
    /// Creates a text post to Bluesky.
    /// </summary>
    /// <param name="text">The message to post. Can be formatted with Markdown for links.</param>
    /// <param name="linkCardUrl">Path to link card. Embed into the post as external.</param>
    /// <param name="languages">Languages used in the post. Can use two or four-letter ISO (Ex. 'th' 'en-US').</param>
    /// <param name="sessionJson">Path to the login users session.json.</param>
    /// <param name="verbose">Verbose output.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    [Command("post")]
    public async Task PostTextAsync(string text, string? linkCardUrl = default, string[]? languages = default, string sessionJson = "session.json", bool verbose = false, CancellationToken cancellationToken = default)
    {
        var consoleLog = new ConsoleLog(verbose);
        var session = await this.GetSessionAsync(consoleLog, sessionJson, cancellationToken);
        if (session is null)
        {
            return;
        }

        var protocol = await this.GenerateProtocolAsync(consoleLog, session);
        if (protocol is null)
        {
            consoleLog.LogError("Failed to generate protocol.");
            return;
        }

        ExternalEmbed? externalEmbed = null;
        if (!string.IsNullOrEmpty(linkCardUrl))
        {
            var parser = new OpenGraphParser(protocol);
            externalEmbed = await parser.GenerateExternalEmbed(linkCardUrl);
        }

        await this.PostAsync(protocol: protocol, text: text, externalEmbed: externalEmbed, languages: languages, sessionJson: sessionJson, verbose: verbose, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Login to Bluesky.
    /// </summary>
    /// <param name="verbose">-v, Verbose logging.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    [Command("login")]
    public async Task StartSessionAsync(bool verbose = false, CancellationToken cancellationToken = default)
    {
        var consoleLog = new ConsoleLog(verbose);
        var instanceUrl = new Uri("https://bsky.social");
        var outputName = "session.json";
        var clientId = "http://localhost";
        var scopeList = new List<string> { "atproto", "transition:generic", "transition:chat.bsky" };
        var browser = new SystemBrowser();
        var redirectUrl = $"http://127.0.0.1:{browser.Port}";

        var protocol = await this.GenerateProtocolAsync(consoleLog);
        if (protocol is null)
        {
            consoleLog.LogError("Failed to generate protocol.");
            return;
        }

        consoleLog.Log($"Starting OAuth2 Authentication for {instanceUrl}");
        var url = await protocol.GenerateOAuth2AuthenticationUrlAsync(clientId, redirectUrl, scopeList, instanceUrl.ToString(), cancellationToken);
        var result = await browser.InvokeAsync(url, cancellationToken);
        if (result.IsError)
        {
            consoleLog.LogError(result.Error);
            return;
        }

        consoleLog.Log($"Got session, finishing OAuth2 Authentication on {instanceUrl}");

        var session = await protocol.AuthenticateWithOAuth2CallbackAsync(result.Response, cancellationToken);
        if (session is null)
        {
            consoleLog.LogError("Failed to authenticate, session is null");
            return;
        }

        consoleLog.Log($"Authenticated as {session.Did}");
        consoleLog.LogDebug($"Did: {session.Did}");
        consoleLog.LogDebug($"Access Token: {session.AccessJwt}");
        consoleLog.LogDebug($"Refresh Token: {session.RefreshJwt}");

        var savedSession = await protocol.SaveOAuthSessionAsync();
        if (savedSession is null)
        {
            consoleLog.LogError("OAuth Session is null");
            return;
        }

        consoleLog.LogDebug($"OAuth Session: {savedSession}");
        await File.WriteAllTextAsync(outputName, savedSession.ToString(), cancellationToken);
        consoleLog.Log($"Session saved to {outputName}");
    }

    private async Task<OAuthSession?> GetSessionAsync(ConsoleLog consoleLog, string sessionJson, CancellationToken cancellationToken)
    {
        var sessionJsonText = await File.ReadAllTextAsync(sessionJson, cancellationToken);
        var session = OAuthSession.FromString(sessionJsonText);
        if (session is null)
        {
            consoleLog.LogError("Session is null. Please login first.");
            return null;
        }

        return session;
    }

    private async Task<ATProtocol?> GenerateProtocolAsync(ConsoleLog consoleLog, OAuthSession? session = default)
    {
        var debugLogger = new DebugLoggerProvider();
        var builder = new ATProtocolBuilder();
        builder.WithLogger(debugLogger.CreateLogger("ATProtocol"));
        builder.WithInstanceUrl(new Uri("https://bsky.social"));
        var protocol = builder.Build();
        if (session is null)
        {
            return protocol;
        }

        var oauthSession = await protocol.AuthenticateWithOAuth2SessionAsync(session, "http://localhost", "https://bsky.social");
        if (oauthSession is not null)
        {
            return protocol;
        }

        consoleLog.LogError("Failed to authenticate with OAuth2 session.");
        return null;
    }

    private async Task PostAsync(ATProtocol protocol, string text, ExternalEmbed? externalEmbed = default, string[]? languages = default, string sessionJson = "session.json", bool verbose = false, CancellationToken cancellationToken = default)
    {
        var consoleLog = new ConsoleLog(verbose);
        if (!File.Exists(sessionJson))
        {
            consoleLog.LogError("Session file does not exist. Please login first.");
            return;
        }

        var parsedText = MarkdownLinkParser.ParseMarkdown(text ?? string.Empty);
        var facets = MarkdownLinkParser.GenerateFacets(parsedText);

        languages ??= Array.Empty<string>();

        try
        {
            var postResult = (await protocol.Repo.CreatePostAsync(
                parsedText?.ModifiedString ?? string.Empty,
                facets,
                externalEmbed,
                langs: languages.ToArray(),
                cancellationToken: cancellationToken)).HandleResult();

            consoleLog.Log($"Post Created: {postResult.Uri} {postResult.Cid}");
            var url =
                $"https://bsky.app/profile/{postResult.Uri!.Did}/post/{postResult.Uri!.Pathname.Split("/").Last()}";
            consoleLog.Log($"Url: {url}");
        }
        catch (Exception e)
        {
            consoleLog.LogError($"Failed to create post: {e.Message}");
        }
        finally
        {
            await File.WriteAllTextAsync(sessionJson, protocol.OAuthSession!.ToString(), cancellationToken);
            consoleLog.Log($"Session saved to {sessionJson}");
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
        private readonly ATProtocol atProtocol;

        public OpenGraphParser(ATProtocol protocol)
        {
            this.atProtocol = protocol;
            var debugLogger = new DebugLoggerProvider();
            this.logger = debugLogger.CreateLogger("OpenGraphParser");
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("FishyFlip");
            this.regex = new Regex("<meta property=\"og:([^\"]+)\" content=\"([^\"]+)\"");
            this.fileContentTypeDetector = new FileContentTypeDetector(this.logger);
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

            if (ogTags.TryGetValue("image", out var imageUrl))
            {
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
                    var blobResult = (await this.atProtocol!.Repo.UploadBlobAsync(content)).HandleResult();
                    this.logger.LogDebug($"Uploaded {imageUrl} to {blobResult.Blob.Ref}");
                    image = blobResult.Blob.ToImage() ?? throw new Exception($"Failed to convert blob {imageUrl} to image.");
                }
            }

            if (ogTags.TryGetValue("title", out var tag))
            {
                title = tag;
            }

            if (ogTags.TryGetValue("description", out var ogTag))
            {
                description = ogTag;
            }

            if (ogTags.TryGetValue("url", out var tag1))
            {
                urlEmbed = tag1;
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
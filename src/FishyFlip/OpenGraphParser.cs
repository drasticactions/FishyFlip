// <copyright file="OpenGraphParser.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.App.Bsky.Embed;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip;

/// <summary>
/// Open Graph Parser.
/// Creates an Embedded Record from an Open Graph URL.
/// </summary>
public class OpenGraphParser : IDisposable
{
    private readonly HttpClient httpClient;
    private readonly ILogger? logger;
    private readonly Regex regex;
    private readonly FileContentTypeDetector fileContentTypeDetector;
    private readonly ATProtocol atProtocol;
    private bool disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphParser"/> class.
    /// </summary>
    /// <param name="protocol"><see cref="ATProtocol"/>.</param>
    /// <param name="httpClient">External HttpClient, will be disposed when class is disposed.</param>
    /// <param name="logger">Logger.</param>
    public OpenGraphParser(ATProtocol protocol, HttpClient httpClient)
    {
        this.atProtocol = protocol;
        this.logger = this.atProtocol.Options.Logger;
        this.httpClient = httpClient;
        this.regex = new Regex("<meta property=\"og:([^\"]+)\" content=\"([^\"]+)\"");
        this.fileContentTypeDetector = new FileContentTypeDetector(this.logger);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphParser"/> class.
    /// </summary>
    /// <param name="protocol"><see cref="ATProtocol"/>.</param>
    /// <param name="logger">Logger.</param>
    internal OpenGraphParser(ATProtocol protocol)
    {
        this.atProtocol = protocol;
        this.logger = protocol.Options.Logger;
        this.httpClient = new HttpClient();
        this.httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("FishyFlip");
        this.regex = new Regex("<meta property=\"og:([^\"]+)\" content=\"([^\"]+)\"");
        this.fileContentTypeDetector = new FileContentTypeDetector(this.logger);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Generate an External Embed from a URL.
    /// </summary>
    /// <param name="url">URL to embed.</param>
    /// <returns><see cref="EmbedExternal"/>.</returns>
    public async Task<EmbedExternal?> GenerateEmbedExternal(string url)
    {
        var ogTags = await this.ParseAsync(url);
        if (ogTags.Count == 0)
        {
            return null;
        }

        Blob? image = null;
        string? title = null;
        string? description = null;
        string? urlEmbed = null;

        if (ogTags.TryGetValue("image", out var imageUrl))
        {
            var imageResult = await this.httpClient.GetByteArrayAsync(imageUrl);
            var contentType = this.fileContentTypeDetector.GetContentType(imageResult);
            if (contentType == "unsupported")
            {
                this.logger?.LogWarning($"Unsupported file type for {imageUrl}");
            }
            else
            {
                var content = new StreamContent(new MemoryStream(imageResult));
                content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                var blobResult = (await this.atProtocol!.Repo.UploadBlobAsync(content)).HandleResult();
                if (blobResult?.Blob == null)
                {
                    this.logger?.LogError($"Failed to upload {imageUrl}");
                }
                else
                {
                    this.logger?.LogDebug($"Uploaded {imageUrl} to {blobResult.Blob.Ref}");
                    image = blobResult.Blob;
                }
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

        var external = new External(urlEmbed, title, description, image);
        return new EmbedExternal(external);
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <param name="disposing">Is Disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.httpClient.Dispose();
            }

            this.disposedValue = true;
        }
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
            this.logger?.LogError(ex, "Failed to parse OpenGraph tags.");
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

    private class FileContentTypeDetector
    {
        private (byte[], string)[] fileSignatures;
        private ILogger? logger;

        public FileContentTypeDetector(ILogger? logger)
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
                        this.logger?.LogDebug($"Detected file type: {mimeType}");
                        return mimeType;
                    }
                }

                fileStream.Position = 0;
            }

            fileStream.Position = originalPosition;
            this.logger?.LogDebug("Unsupported file type.");
            return "unsupported";
        }
    }
}

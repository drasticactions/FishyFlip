// <copyright file="WhiteWindBlog.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models.WhiteWind;

namespace FishyFlip;

/// <summary>
/// WhiteWind Blog.
/// </summary>
public sealed class WhiteWindBlog
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="WhiteWindBlog"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal WhiteWindBlog(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    /// <summary>
    /// Creates a blog post asynchronously.
    /// </summary>
    /// <param name="content">The content of the blog post.</param>
    /// <param name="title">The title of the blog post.</param>
    /// <param name="theme">The theme of the blog post.</param>
    /// <param name="createdAt">The creation date of the blog post. If null, the current UTC date will be used.</param>
    /// <param name="blobs">Blobs for a given post.</param>
    /// <param name="rkey">The rkey associated with the post.</param>
    /// <param name="validate">Validate the record, defaults to false.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the reference to the created repost.</returns>
    public Task<Result<RecordRef>> CreateBlogPostAsync(
        string content,
        string title,
        string? theme = default,
        DateTime? createdAt = null,
        BlobMetadata[]? blobs = null,
        string? rkey = null,
        bool validate = false,
        CancellationToken cancellationToken = default)
    {
        Models.WhiteWind.Entry record = new(content, title, theme, createdAt ?? DateTime.UtcNow, blobs, Constants.WhiteWindTypes.Entry);
        FishyFlip.Models.Internal.WhiteWind.CreateEntryRecord createRecord = new(Constants.WhiteWindTypes.Entry, this.proto.SessionManager!.Session!.Did.ToString()!,  record, rkey, validate);
        return !string.IsNullOrEmpty(rkey) ? this.proto.Repo.PutRecord<Models.Internal.WhiteWind.CreateEntryRecord, RecordRef>(createRecord, this.Options.SourceGenerationContext.CreateEntryRecord, this.Options.SourceGenerationContext.RecordRef, cancellationToken) : this.proto.Repo.CreateRecord<Models.Internal.WhiteWind.CreateEntryRecord, RecordRef>(createRecord, this.Options.SourceGenerationContext.CreateEntryRecord, this.Options.SourceGenerationContext.RecordRef, cancellationToken);
    }
}
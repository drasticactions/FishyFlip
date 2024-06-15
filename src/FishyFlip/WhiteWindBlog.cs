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
    /// <param name="rkey">The rkey associated with the post. Used to update existing post.</param>
    /// <param name="validate">Validate the record, defaults to false.</param>
    /// <param name="visibility">The visibility of the blog post.</param>
    /// <param name="ogp">The Open Graph Protocol item of the blog post.</param>
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
        string visibility = Constants.WhiteWindVisibility.Public,
        Ogp? ogp = null,
        CancellationToken cancellationToken = default)
    {
        Models.WhiteWind.Entry record = new(content, title, theme, createdAt ?? DateTime.UtcNow, blobs, visibility, ogp, Constants.WhiteWindTypes.Entry);
        FishyFlip.Models.Internal.WhiteWind.CreateEntryRecord createRecord = new(Constants.WhiteWindTypes.Entry, this.proto.SessionManager!.Session!.Did.ToString()!,  record, rkey, validate);
        return !string.IsNullOrEmpty(rkey) ? this.proto.Repo.PutRecord<Models.Internal.WhiteWind.CreateEntryRecord, RecordRef>(createRecord, this.Options.SourceGenerationContext.CreateEntryRecord, this.Options.SourceGenerationContext.RecordRef, cancellationToken) : this.proto.Repo.CreateRecord<Models.Internal.WhiteWind.CreateEntryRecord, RecordRef>(createRecord, this.Options.SourceGenerationContext.CreateEntryRecord, this.Options.SourceGenerationContext.RecordRef, cancellationToken);
    }

    /// <summary>
    /// Retrieves a Entry asynchronously.
    /// </summary>
    /// <param name="repo">The AT identifier.</param>
    /// <param name="rkey">The record key.</param>
    /// <param name="cid">The CID (Content Identifier) of the post.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved post record, or null if not found.</returns>
    public async Task<Result<EntryRecord?>> GetEntryAsync(ATIdentifier repo, string rkey, ATCid? cid = null, CancellationToken cancellationToken = default)
        => await this.proto.Repo.GetRecordAsync<EntryRecord>(Constants.WhiteWindTypes.Entry, this.Options.SourceGenerationContext.EntryRecord, repo, rkey, cid, cancellationToken);

    /// <summary>
    /// Asynchronously list the author posts.
    /// </summary>
    /// <param name="authorDid">Author repo.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of records, or null if no records were found.</returns>
    /// <returns>A list of author posts.</returns>
    public async Task<Result<ListRecords<Entry>?>> GetAuthorPostsAsync(
        ATIdentifier authorDid,
        int limit = 50,
        string? cursor = default,
        bool? reverse = default,
        CancellationToken cancellationToken = default)
    {
        var (protocol, did, usingCurrentProto) = await this.proto.GenerateClientFromATIdentifierAsync(authorDid, cancellationToken, this.Options.Logger);

        string url = $"{Constants.Urls.ATProtoRepo.ListRecords}?collection={Constants.WhiteWindTypes.Entry}&repo={authorDid}&limit={limit}";
        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        if (reverse is not null)
        {
            url += $"&reverse={reverse}";
        }

        try
        {
            return await protocol.Client.Get<ListRecords<Entry>>(url, this.Options.SourceGenerationContext.ListRecordsEntry, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        }
        finally
        {
            if (!usingCurrentProto)
            {
                protocol.Dispose();
            }
        }
    }
}
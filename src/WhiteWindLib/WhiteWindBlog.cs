// <copyright file="WhiteWindBlog.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization.Metadata;
using FishyFlip;
using FishyFlip.Tools;
using WhiteWindLib.Models.WhiteWind;

namespace WhiteWindLib;

/// <summary>
/// WhiteWind Blog.
/// </summary>
public sealed class WhiteWindBlog
{
    private ATProtocol proto;
    private SourceGenerationContext sourceGenerationContext;

    private JsonSerializerOptions jsonSerializerOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="WhiteWindBlog"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    public WhiteWindBlog(ATProtocol proto)
    {
        this.proto = proto;
        this.jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
            Converters =
            {
                new AtUriJsonConverter(),
                new AtHandlerJsonConverter(),
                new AtDidJsonConverter(),
                new EmbedConverter(),
                new ATRecordJsonConverter(),
                new ATCidConverter(),
            },
        };
        this.sourceGenerationContext = new SourceGenerationContext(this.jsonSerializerOptions);
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
        WhiteWindLib.Models.Internal.WhiteWind.CreateEntryRecord createRecord = new(Constants.WhiteWindTypes.Entry, this.proto.Session!.Did.ToString()!,  record, rkey, validate);
        return !string.IsNullOrEmpty(rkey) ? this.PutRecord<Models.Internal.WhiteWind.CreateEntryRecord, RecordRef>(createRecord, this.sourceGenerationContext.CreateEntryRecord, this.sourceGenerationContext.RecordRef, cancellationToken) : this.CreateRecord<Models.Internal.WhiteWind.CreateEntryRecord, RecordRef>(createRecord, this.sourceGenerationContext.CreateEntryRecord, this.sourceGenerationContext.RecordRef, cancellationToken);
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
    {
        var (protocol, did, usingCurrentProto) = await this.proto.GenerateClientFromATIdentifierAsync(repo, cancellationToken, this.Options.Logger);
        try
        {
            return await this.GetRecordAsync<EntryRecord>(protocol, Constants.WhiteWindTypes.Entry, this.sourceGenerationContext.EntryRecord, repo, rkey, cid, cancellationToken);
        }
        finally
        {
            if (!usingCurrentProto)
            {
                protocol.Dispose();
            }
        }
    }

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

        string url = $"{FishyFlip.Constants.Urls.ATProtoRepo.ListRecords}?collection={Constants.WhiteWindTypes.Entry}&repo={authorDid}&limit={limit}";
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
            return await protocol.Client.Get<ListRecords<Entry>>(url, this.sourceGenerationContext.ListRecordsEntry, this.jsonSerializerOptions, cancellationToken, this.Options.Logger);
        }
        finally
        {
            if (!usingCurrentProto)
            {
                protocol.Dispose();
            }
        }
    }

    /// <summary>
    /// Asynchronously creates a record of type T and returns a result of type T2.
    /// </summary>
    /// <typeparam name="T">The type of the record to create.</typeparam>
    /// <typeparam name="T2">The type of the result to return.</typeparam>
    /// <param name="record">The record of type T to create.</param>
    /// <param name="c1">The JsonTypeInfo of the record type T. Used for JSON serialization and deserialization.</param>
    /// <param name="c2">The JsonTypeInfo of the result type T2. Used for JSON serialization and deserialization.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the result of type T2.</returns>
    private Task<Result<T2>> CreateRecord<T, T2>(T record, JsonTypeInfo<T> c1, JsonTypeInfo<T2> c2, CancellationToken cancellationToken = default)
    {
        return
            this.proto.Client
                .Post<T, T2>(
                    FishyFlip.Constants.Urls.ATProtoRepo.CreateRecord,
                    c1,
                    c2,
                    this.jsonSerializerOptions,
                    record,
                    cancellationToken,
                    this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously creates or updates a record of type T and returns a result of type T2.
    /// </summary>
    /// <typeparam name="T">The type of the record to create or update.</typeparam>
    /// <typeparam name="T2">The type of the result to return.</typeparam>
    /// <param name="record">The record of type T to create or update.</param>
    /// <param name="c1">The JsonTypeInfo of the record type T. Used for JSON serialization and deserialization.</param>
    /// <param name="c2">The JsonTypeInfo of the result type T2. Used for JSON serialization and deserialization.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the result of type T2.</returns>
    private Task<Result<T2>> PutRecord<T, T2>(T record, JsonTypeInfo<T> c1, JsonTypeInfo<T2> c2, CancellationToken cancellationToken = default)
    {
        return
            this.proto.Client
                .Post<T, T2>(
                    FishyFlip.Constants.Urls.ATProtoRepo.PutRecord,
                    c1,
                    c2,
                    this.jsonSerializerOptions,
                    record,
                    cancellationToken,
                    this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves a record of type T from a specified repository.
    /// </summary>
    /// <typeparam name="T">The type of the record to retrieve. Must implement ATFeedTypeAPI.</typeparam>
    /// <param name="proto">The protocol to use to handle the request.</param>
    /// <param name="collection">The name of the collection where the record is stored.</param>
    /// <param name="type">The JsonTypeInfo of the record type T. Used for JSON serialization and deserialization.</param>
    /// <param name="repo">The ATIdentifier of the repository where the record is stored.</param>
    /// <param name="rkey">The key of the record to retrieve.</param>
    /// <param name="cid">Optional. The CID (Content Identifier) of the record. If specified, the method retrieves the record with this CID.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the retrieved record of type T, or null if the record was not found.</returns>
    private async Task<Result<T?>> GetRecordAsync<T>(ATProtocol proto, string collection, JsonTypeInfo<T> type, ATIdentifier repo, string rkey, ATCid? cid = null, CancellationToken cancellationToken = default)
        where T : ATFeedTypeAPI
    {
        string url = $"{FishyFlip.Constants.Urls.ATProtoRepo.GetRecord}?collection={collection}&repo={repo}&rkey={rkey}";
        if (cid is not null)
        {
            url += $"&cid={cid}";
        }

        return await proto.Client.Get<T>(url, type, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}
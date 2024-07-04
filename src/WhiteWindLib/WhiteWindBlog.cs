// <copyright file="WhiteWindBlog.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization.Metadata;
using FishyFlip;
using FishyFlip.Tools;
using WhiteWindLib.Models.WhiteWind;
using WhiteWindLib.Tools;

namespace WhiteWindLib;

/// <summary>
/// WhiteWind Blog.
/// </summary>
public sealed class WhiteWindBlog
{
    private static SourceGenerationContext? sourceGenerationContext;
    private static JsonSerializerOptions? jsonSerializerOptions;
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="WhiteWindBlog"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    public WhiteWindBlog(ATProtocol proto)
    {
        this.proto = proto;
        GenerateSourceGenerationContext();
    }

    private ATProtocolOptions Options => this.proto.Options;

    /// <summary>
    /// Generates custom record converter.
    /// </summary>
    /// <remarks>
    /// This method creates a list containing instances of custom embed converters used for JSON serialization
    /// and deserialization of embedded media content. It utilizes the <see cref="SourceGenerationContext.Default"/>
    /// for configuring the converters, ensuring they are compatible with the default serialization settings.
    /// </remarks>
    /// <returns>A <see cref="ICustomATRecordConverter"/> instance.</returns>
    public static ICustomATRecordConverter GenerateRecordConverter()
    {
        return new EntryConverter(WhiteWindBlog.sourceGenerationContext ?? GenerateSourceGenerationContext());
    }

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
    public Task<Result<RecordRef>> CreateEntryAsync(
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
        return !string.IsNullOrEmpty(rkey) ? PutRecord<Models.Internal.WhiteWind.CreateEntryRecord, RecordRef>(this.proto, jsonSerializerOptions!, createRecord, sourceGenerationContext!.CreateEntryRecord, sourceGenerationContext!.RecordRef, cancellationToken) : CreateRecord<Models.Internal.WhiteWind.CreateEntryRecord, RecordRef>(this.proto, jsonSerializerOptions!, createRecord, sourceGenerationContext!.CreateEntryRecord, sourceGenerationContext!.RecordRef, cancellationToken);
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
            return await GetRecordAsync<EntryRecord>(protocol, jsonSerializerOptions!, Constants.WhiteWindTypes.Entry, sourceGenerationContext!.EntryRecord, repo, rkey, cid, cancellationToken);
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
    /// Asynchronously deletes an entry from the blog.
    /// </summary>
    /// <param name="repo">The AT identifier of the repository where the entry is located.</param>
    /// <param name="rkey">The record key of the entry to be deleted.</param>
    /// <param name="swapRecord">Optional. The CID (Content Identifier) of the record to replace the deleted one. Defaults to null.</param>
    /// <param name="swapCommit">Optional. The CID (Content Identifier) of the commit to replace the deleted one. Defaults to null.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation. Defaults to None.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result of type Success indicating the success of the operation.</returns>
    public Task<Result<Success>> DeleteEntryAsync(ATIdentifier repo, string rkey, ATCid? swapRecord = null, ATCid? swapCommit = null, CancellationToken cancellationToken = default)
        => DeleteRecordAsync(this.proto, sourceGenerationContext!, jsonSerializerOptions!, Constants.WhiteWindTypes.Entry, rkey, swapRecord, swapCommit, cancellationToken);

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
    public async Task<Result<ListRecords<Entry>?>> GetAuthorEntriesAsync(
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
            return await protocol.Client.Get<ListRecords<Entry>>(url, sourceGenerationContext!.ListRecordsEntry, jsonSerializerOptions!, cancellationToken, this.Options.Logger);
        }
        finally
        {
            if (!usingCurrentProto)
            {
                protocol.Dispose();
            }
        }
    }

    private static async Task<Result<T?>> GetRecordAsync<T>(ATProtocol proto, JsonSerializerOptions options, string collection, JsonTypeInfo<T> type, ATIdentifier repo, string rkey, ATCid? cid = null, CancellationToken cancellationToken = default)
        where T : ATFeedTypeAPI
    {
        string url = $"{FishyFlip.Constants.Urls.ATProtoRepo.GetRecord}?collection={collection}&repo={repo}&rkey={rkey}";
        if (cid is not null)
        {
            url += $"&cid={cid}";
        }

        return await proto.Client.Get<T>(url, type, options, cancellationToken, proto.Options.Logger);
    }

    private static Task<Result<T2>> CreateRecord<T, T2>(ATProtocol proto, JsonSerializerOptions options, T record, JsonTypeInfo<T> c1, JsonTypeInfo<T2> c2, CancellationToken cancellationToken = default)
    {
        return
            proto.Client
                .Post<T, T2>(
                    FishyFlip.Constants.Urls.ATProtoRepo.CreateRecord,
                    c1,
                    c2,
                    options,
                    record,
                    cancellationToken,
                    proto.Options.Logger);
    }

    private static async Task<Result<Success>> DeleteRecordAsync(ATProtocol proto, SourceGenerationContext context, JsonSerializerOptions options, string collection, string rkey, ATCid? swapRecord = null, ATCid? swapCommit = null, CancellationToken cancellationToken = default)
    {
        DeleteRecord record = new(
            collection,
            proto.Session!.Did.ToString()!,
            rkey,
            swapRecord,
            swapCommit);
        return await proto.Client.Post<DeleteRecord, Success>(FishyFlip.Constants.Urls.ATProtoRepo.DeleteRecord, context.DeleteRecord, context.Success, options, record, cancellationToken, proto.Options.Logger);
    }

    private static Task<Result<T2>> PutRecord<T, T2>(ATProtocol proto, JsonSerializerOptions options, T record, JsonTypeInfo<T> c1, JsonTypeInfo<T2> c2, CancellationToken cancellationToken = default)
    {
        return
            proto.Client
                .Post<T, T2>(
                    FishyFlip.Constants.Urls.ATProtoRepo.PutRecord,
                    c1,
                    c2,
                    options,
                    record,
                    cancellationToken,
                    proto.Options.Logger);
    }

    private static SourceGenerationContext GenerateSourceGenerationContext()
    {
        jsonSerializerOptions = new JsonSerializerOptions()
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
        return sourceGenerationContext = new SourceGenerationContext(jsonSerializerOptions);
    }
}
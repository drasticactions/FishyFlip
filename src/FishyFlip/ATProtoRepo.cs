// <copyright file="ATProtoRepo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization.Metadata;
using System.Threading;
using static FishyFlip.Constants;

namespace FishyFlip;

/// <summary>
/// AT Proto Repo.
/// </summary>
public sealed class ATProtoRepo
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtoRepo"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal ATProtoRepo(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Creates a like asynchronously.
    /// </summary>
    /// <param name="cid">The CID of the like.</param>
    /// <param name="uri">The URI of the like.</param>
    /// <param name="createdAt">The creation date of the like. If not specified, the current UTC date and time will be used.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the reference to the created like record.</returns>
    public Task<Result<RecordRef>> CreateLikeAsync(
      ATCid cid,
      ATUri uri,
      DateTime? createdAt = null,
      CancellationToken cancellationToken = default)
    {
        CreateLikeRecord record = new(
            Constants.FeedType.Like,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new LikeRecord(new Subject(cid, uri), createdAt ?? DateTime.UtcNow));

        return this.CreateRecord<CreateLikeRecord, RecordRef>(record, this.Options.SourceGenerationContext.CreateLikeRecord, this.Options.SourceGenerationContext.RecordRef, cancellationToken);
    }

    /// <summary>
    /// Creates a repost asynchronously.
    /// </summary>
    /// <param name="cid">The CID of the repost.</param>
    /// <param name="uri">The URI of the repost.</param>
    /// <param name="createdAt">The creation date of the repost. If null, the current UTC date will be used.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the reference to the created repost.</returns>
    public Task<Result<RecordRef>> CreateRepostAsync(
       ATCid cid,
       ATUri uri,
       DateTime? createdAt = null,
       CancellationToken cancellationToken = default)
    {
        CreateRepostRecord record = new(
            Constants.FeedType.Repost,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new RepostRecord(new Subject(cid, uri), createdAt ?? DateTime.UtcNow));

        return this.CreateRecord<CreateRepostRecord, RecordRef>(record, this.Options.SourceGenerationContext.CreateRepostRecord, this.Options.SourceGenerationContext.RecordRef, cancellationToken);
    }

    /// <summary>
    /// Creates a follow record asynchronously.
    /// </summary>
    /// <param name="did">The ATDid to create a follow record for.</param>
    /// <param name="createdAt">The creation date of the follow record. If null, the current UTC date will be used.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created record reference.</returns>
    public Task<Result<RecordRef>> CreateFollowAsync(
        ATDid did,
        DateTime? createdAt = null,
        CancellationToken cancellationToken = default)
    {
        CreateFollowRecord record = new(
            Constants.GraphTypes.Follow,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new FollowRecord(did, createdAt ?? DateTime.UtcNow));

        return this.CreateRecord<CreateFollowRecord, RecordRef>(record, this.Options.SourceGenerationContext.CreateFollowRecord, this.Options.SourceGenerationContext.RecordRef, cancellationToken);
    }

    /// <summary>
    /// Creates a new list item asynchronously.
    /// </summary>
    /// <param name="subject">The ATDid of the subject.</param>
    /// <param name="list">The ATUri of the list.</param>
    /// <param name="createdAt">The optional creation date of the list item. If not provided, the current UTC date and time will be used.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created list item's RecordRef.</returns>
    public Task<Result<RecordRef>> CreateListItemAsync(
    ATDid subject,
    ATUri list,
    DateTime? createdAt = null,
    CancellationToken cancellationToken = default)
    {
        CreateListItemRecord record = new(
            Constants.GraphTypes.ListItem,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new ListItemRecord(subject, list, createdAt ?? DateTime.UtcNow));

        return this.CreateRecord<CreateListItemRecord, RecordRef>(record, this.Options.SourceGenerationContext.CreateListItemRecord, this.Options.SourceGenerationContext.RecordRef, cancellationToken);
    }

    /// <summary>
    /// Creates a post asynchronously.
    /// </summary>
    /// <param name="text">The text of the post.</param>
    /// <param name="facets">The facets associated with the post.</param>
    /// <param name="embed">The embed associated with the post.</param>
    /// <param name="langs">The languages associated with the post.</param>
    /// <param name="createdAt">The creation date of the post.</param>
    /// <param name="rkey">The rkey associated with the post.</param>
    /// <param name="swapCommit">The swap commit associated with the post.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of creating the post.</returns>
    public Task<Result<CreatePostResponse>> CreatePostAsync(
        string text,
        Facet[]? facets = null,
        Embed? embed = default,
        string[]? langs = null,
        DateTime? createdAt = null,
        string? rkey = null,
        string? swapCommit = null,
        CancellationToken cancellationToken = default)
        => this.CreatePostAsync(text, null, facets, embed, langs, createdAt, rkey, swapCommit, cancellationToken);

    /// <summary>
    /// Creates a post asynchronously.
    /// </summary>
    /// <param name="text">The text of the post.</param>
    /// <param name="reply">The post to reply to.</param>
    /// <param name="facets">The facets associated with the post.</param>
    /// <param name="embed">The embed associated with the post.</param>
    /// <param name="langs">The languages associated with the post.</param>
    /// <param name="createdAt">The creation date of the post.</param>
    /// <param name="rkey">The rkey associated with the post.</param>
    /// <param name="swapCommit">The swap commit associated with the post.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of creating the post.</returns>
    public Task<Result<CreatePostResponse>> CreatePostAsync(
        string text,
        Reply? reply,
        Facet[]? facets = null,
        Embed? embed = default,
        string[]? langs = null,
        DateTime? createdAt = null,
        string? rkey = null,
        string? swapCommit = null,
        CancellationToken cancellationToken = default)
    {
        CreatePostRecord record = new(
            Constants.FeedType.Post,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new Post(embed, facets, createdAt ?? DateTime.UtcNow, reply, text, langs, Constants.FeedType.Post),
            rkey,
            swapCommit);
        return this.CreateRecord<CreatePostRecord, CreatePostResponse>(record, this.Options.SourceGenerationContext.CreatePostRecord, this.Options.SourceGenerationContext.CreatePostResponse, cancellationToken);
    }

    /// <summary>
    /// Creates a block asynchronously.
    /// </summary>
    /// <param name="did">The ATDid.</param>
    /// <param name="createdAt">The creation date of the block. If null, the current UTC date and time will be used.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created block's record reference.</returns>
    public Task<Result<RecordRef>> CreateBlockAsync(
        ATDid did,
        DateTime? createdAt = null,
        CancellationToken cancellationToken = default)
    {
        CreateBlockRecord record = new(
            Constants.GraphTypes.Block,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new BlockRecord(did, createdAt ?? DateTime.UtcNow));

        return this.Client.Post<CreateBlockRecord, RecordRef>(
            Constants.Urls.ATProtoRepo.CreateRecord,
            this.Options.SourceGenerationContext.CreateBlockRecord,
            this.Options.SourceGenerationContext.RecordRef,
            this.Options.JsonSerializerOptions,
            record,
            cancellationToken,
            this.Options.Logger);
    }

    /// <summary>
    /// Creates a curate list asynchronously.
    /// </summary>
    /// <param name="name">The name of the list.</param>
    /// <param name="description">The description of the list.</param>
    /// <param name="createdAt">The creation date of the list. (optional).</param>
    /// <param name="rkey">The rkey of the list. (optional).</param>
    /// <param name="swapCommit">The swap commit of the list. (optional).</param>
    /// <param name="cancellationToken">The cancellation token. (optional).</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created record reference.</returns>
    public Task<Result<RecordRef>> CreateCurateListAsync(
        string name,
        string description,
        DateTime? createdAt = null,
        string? rkey = null,
        string? swapCommit = null,
        CancellationToken cancellationToken = default)
    {
        var listRecord = new ListRecordInternal(name, description, ListReasons.CurateList);

        CreateListRecord record = new(
            Constants.GraphTypes.List,
            this.proto.SessionManager!.Session!.Did.ToString(),
            listRecord);

        return this.Client.Post<CreateListRecord, RecordRef>(
            Constants.Urls.ATProtoRepo.CreateRecord,
            this.Options.SourceGenerationContext.CreateListRecord,
            this.Options.SourceGenerationContext.RecordRef,
            this.Options.JsonSerializerOptions,
            record,
            cancellationToken,
            this.Options.Logger);
    }

    /// <summary>
    /// Retrieves a post asynchronously.
    /// </summary>
    /// <param name="repo">The AT identifier.</param>
    /// <param name="rkey">The record key.</param>
    /// <param name="cid">The CID (Content Identifier) of the post.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved post record, or null if not found.</returns>
    public async Task<Result<PostRecord?>> GetPostAsync(ATIdentifier repo, string rkey, ATCid? cid = null, CancellationToken cancellationToken = default)
      => await this.GetRecordAsync<PostRecord>(Constants.FeedType.Post, this.Options.SourceGenerationContext.PostRecord, repo, rkey, cid, cancellationToken);

    /// <summary>
    /// Retrieves an actor asynchronously.
    /// </summary>
    /// <param name="repo">The AT identifier of the repository.</param>
    /// <param name="cid">The CID of the actor record. If null, the latest record will be retrieved.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the actor record, or null if not found.</returns>
    public async Task<Result<ActorRecord?>> GetActorAsync(ATIdentifier repo, ATCid? cid = null, CancellationToken cancellationToken = default)
        => await this.GetRecordAsync<ActorRecord>(Constants.ActorTypes.Profile, this.Options.SourceGenerationContext.ActorRecord, repo, "self", cid, cancellationToken);

    /// <summary>
    /// Uploads a blob asynchronously.
    /// </summary>
    /// <param name="content">The content of the blob as a stream.</param>
    /// <param name="cancellationToken">A token that may be used to cancel the operation. This is optional.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response from the blob upload operation.</returns>
    public Task<Result<UploadBlobResponse>> UploadBlobAsync(StreamContent content, CancellationToken cancellationToken = default)
    {
        return this.Client.Post<UploadBlobResponse>(
            Constants.Urls.ATProtoRepo.UploadBlob,
            this.Options.SourceGenerationContext.UploadBlobResponse,
            this.Options.JsonSerializerOptions,
            content,
            cancellationToken,
            this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves a record of type T from a specified repository.
    /// </summary>
    /// <typeparam name="T">The type of the record to retrieve. Must implement ATFeedTypeAPI.</typeparam>
    /// <param name="collection">The name of the collection where the record is stored.</param>
    /// <param name="type">The JsonTypeInfo of the record type T. Used for JSON serialization and deserialization.</param>
    /// <param name="repo">The ATIdentifier of the repository where the record is stored.</param>
    /// <param name="rkey">The key of the record to retrieve.</param>
    /// <param name="cid">Optional. The CID (Content Identifier) of the record. If specified, the method retrieves the record with this CID.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the retrieved record of type T, or null if the record was not found.</returns>
    public async Task<Result<T?>> GetRecordAsync<T>(string collection, JsonTypeInfo<T> type, ATIdentifier repo, string rkey, ATCid? cid = null, CancellationToken cancellationToken = default)
        where T : ATFeedTypeAPI
    {
        string url = $"{Constants.Urls.ATProtoRepo.GetRecord}?collection={collection}&repo={repo}&rkey={rkey}";
        if (cid is not null)
        {
            url += $"&cid={cid}";
        }

        return await this.Client.Get<T>(url, type, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the description of a repository.
    /// </summary>
    /// <param name="identifier">The ATIdentifier of the repository to describe.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the description of the repository, or null if the repository was not found.</returns>
    public async Task<Result<DescribeRepo?>> DescribeRepoAsync(ATIdentifier identifier, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.ATProtoRepo.DescribeRepo}?repo={identifier}";
        return await this.Client.Get<DescribeRepo?>(url, this.Options.SourceGenerationContext.DescribeRepo!, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously deletes a follow record.
    /// </summary>
    /// <param name="rkey">The key of the record to delete.</param>
    /// <param name="swapRecord">Optional. The CID (Content Identifier) of the record to swap with. If specified, the method swaps the record with this CID before deleting it.</param>
    /// <param name="swapCommit">Optional. The CID (Content Identifier) of the commit to swap with. If specified, the method swaps the commit with this CID before deleting the record.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Success object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> DeleteFollowAsync(
        string rkey,
        ATCid? swapRecord = null,
        ATCid? swapCommit = null,
        CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.GraphTypes.Follow, rkey, swapRecord, swapCommit, cancellationToken);

    /// <summary>
    /// Asynchronously deletes a block record.
    /// </summary>
    /// <param name="rkey">The key of the record to delete.</param>
    /// <param name="swapRecord">Optional. The CID (Content Identifier) of the record to swap with. If specified, the method swaps the record with this CID before deleting it.</param>
    /// <param name="swapCommit">Optional. The CID (Content Identifier) of the commit to swap with. If specified, the method swaps the commit with this CID before deleting the record.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Success object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> DeleteBlockAsync(
        string rkey,
        ATCid? swapRecord = null,
        ATCid? swapCommit = null,
        CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.GraphTypes.Block, rkey, swapRecord, swapCommit, cancellationToken);

    /// <summary>
    /// Asynchronously deletes a like record.
    /// </summary>
    /// <param name="rkey">The key of the record to delete.</param>
    /// <param name="swapRecord">Optional. The CID (Content Identifier) of the record to swap with. If specified, the method swaps the record with this CID before deleting it.</param>
    /// <param name="swapCommit">Optional. The CID (Content Identifier) of the commit to swap with. If specified, the method swaps the commit with this CID before deleting the record.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Success object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> DeleteLikeAsync(
        string rkey,
        ATCid? swapRecord = null,
        ATCid? swapCommit = null,
        CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.FeedType.Like, rkey, swapRecord, swapCommit, cancellationToken);

    /// <summary>
    /// Asynchronously deletes a post record.
    /// </summary>
    /// <param name="rkey">The key of the record to delete.</param>
    /// <param name="swapRecord">Optional. The CID (Content Identifier) of the record to swap with. If specified, the method swaps the record with this CID before deleting it.</param>
    /// <param name="swapCommit">Optional. The CID (Content Identifier) of the commit to swap with. If specified, the method swaps the commit with this CID before deleting the record.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Success object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> DeletePostAsync(
        string rkey,
        ATCid? swapRecord = null,
        ATCid? swapCommit = null,
        CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.FeedType.Post, rkey, swapRecord, swapCommit, cancellationToken);

    /// <summary>
    /// Asynchronously deletes a repost record.
    /// </summary>
    /// <param name="rkey">The key of the record to delete.</param>
    /// <param name="swapRecord">Optional. The CID (Content Identifier) of the record to swap with. If specified, the method swaps the record with this CID before deleting it.</param>
    /// <param name="swapCommit">Optional. The CID (Content Identifier) of the commit to swap with. If specified, the method swaps the commit with this CID before deleting the record.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Success object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> DeleteRepostAsync(
        string rkey,
        ATCid? swapRecord = null,
        ATCid? swapCommit = null,
        CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.FeedType.Repost, rkey, swapRecord, swapCommit, cancellationToken);

    /// <summary>
    /// Asynchronously deletes a list.
    /// </summary>
    /// <param name="rkey">The key of the record to delete.</param>
    /// <param name="swapRecord">Optional. The CID (Content Identifier) of the record to swap with. If specified, the method swaps the record with this CID before deleting it.</param>
    /// <param name="swapCommit">Optional. The CID (Content Identifier) of the commit to swap with. If specified, the method swaps the commit with this CID before deleting the record.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Success object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> DeleteListAsync(
        string rkey,
        ATCid? swapRecord = null,
        ATCid? swapCommit = null,
        CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.GraphTypes.List, rkey, swapRecord, swapCommit, cancellationToken);

    /// <summary>
    /// Asynchronously deletes a list item.
    /// </summary>
    /// <param name="rkey">The key of the record to delete.</param>
    /// <param name="swapRecord">Optional. The CID (Content Identifier) of the record to swap with. If specified, the method swaps the record with this CID before deleting it.</param>
    /// <param name="swapCommit">Optional. The CID (Content Identifier) of the commit to swap with. If specified, the method swaps the commit with this CID before deleting the record.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Success object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> DeleteListItemAsync(
        string rkey,
        ATCid? swapRecord = null,
        ATCid? swapCommit = null,
        CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.GraphTypes.ListItem, rkey, swapRecord, swapCommit, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of follow records from a specified repository.
    /// </summary>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of follow records, or null if no records were found.</returns>
    [Obsolete("Use ListFollowsAsync instead")]
    public Task<Result<ListRecords?>> ListFollowAsync(ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        => this.ListRecordsAsync(Constants.GraphTypes.Follow, repo, limit, cursor, reverse, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of follow records from a specified repository.
    /// </summary>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of follow records, or null if no records were found.</returns>
    public Task<Result<ListRecords?>> ListFollowsAsync(ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        => this.ListRecordsAsync(Constants.GraphTypes.Follow, repo, limit, cursor, reverse, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of block records from a specified repository.
    /// </summary>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of block records, or null if no records were found.</returns>
    [Obsolete("Use ListBlocksAsync instead")]
    public Task<Result<ListRecords?>> ListBlockAsync(ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        => this.ListRecordsAsync(Constants.GraphTypes.Block, repo, limit, cursor, reverse, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of block records from a specified repository.
    /// </summary>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of block records, or null if no records were found.</returns>
    public Task<Result<ListRecords?>> ListBlocksAsync(ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        => this.ListRecordsAsync(Constants.GraphTypes.Block, repo, limit, cursor, reverse, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of like records from a specified repository.
    /// </summary>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of records, or null if no records were found.</returns>
    [Obsolete("Use ListLikesAsync instead")]
    public Task<Result<ListRecords?>> ListLikeAsync(ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        => this.ListRecordsAsync(Constants.FeedType.Like, repo, limit, cursor, reverse, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of like records from a specified repository.
    /// </summary>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of records, or null if no records were found.</returns>
    public Task<Result<ListRecords?>> ListLikesAsync(ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        => this.ListRecordsAsync(Constants.FeedType.Like, repo, limit, cursor, reverse, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of post records from a specified repository.
    /// </summary>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of records, or null if no records were found.</returns>
    [Obsolete("Use ListPostsAsync instead")]
    public Task<Result<ListRecords?>> ListPostAsync(ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        => this.ListRecordsAsync(Constants.FeedType.Post, repo, limit, cursor, reverse, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of post records from a specified repository.
    /// </summary>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of records, or null if no records were found.</returns>
    public Task<Result<ListRecords?>> ListPostsAsync(ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
       => this.ListRecordsAsync(Constants.FeedType.Post, repo, limit, cursor, reverse, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of records from a specified repository.
    /// </summary>
    /// <param name="collection">The name of the collection where the records are stored.</param>
    /// <param name="repo">The ATIdentifier of the repository where the records are stored.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of records, or null if no records were found.</returns>
    public async Task<Result<ListRecords?>> ListRecordsAsync(string collection, ATIdentifier repo, int limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.ATProtoRepo.ListRecords}?collection={collection}&repo={repo}&limit={limit}";
        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        if (reverse is not null)
        {
            url += $"&reverse={reverse}";
        }

        return await this.Client.Get<ListRecords>(url, this.Options.SourceGenerationContext.ListRecords, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    private Task<Result<T2>> PutRecord<T, T2>(T record, JsonTypeInfo<T> c1, JsonTypeInfo<T2> c2, CancellationToken cancellationToken = default)
    {
        return
            this.Client
                .Post<T, T2>(
                    Constants.Urls.ATProtoRepo.PutRecord,
                    c1,
                    c2,
                    this.Options.JsonSerializerOptions,
                    record,
                    cancellationToken,
                    this.Options.Logger);
    }

    private Task<Result<T2>> CreateRecord<T, T2>(T record, JsonTypeInfo<T> c1, JsonTypeInfo<T2> c2, CancellationToken cancellationToken = default)
    {
        return
            this.Client
                .Post<T, T2>(
                    Constants.Urls.ATProtoRepo.CreateRecord,
                    c1,
                    c2,
                    this.Options.JsonSerializerOptions,
                    record,
                    cancellationToken,
                    this.Options.Logger);
    }

    private async Task<Result<Success>> DeleteRecordAsync(string collection, string rkey, ATCid? swapRecord = null, ATCid? swapCommit = null, CancellationToken cancellationToken = default)
    {
        DeleteRecord record = new(
            collection,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            rkey,
            swapRecord,
            swapCommit);
        return await this.Client.Post<DeleteRecord, Success>(Constants.Urls.ATProtoRepo.DeleteRecord, this.Options.SourceGenerationContext.DeleteRecord, this.Options.SourceGenerationContext.Success, this.Options.JsonSerializerOptions, record, cancellationToken, this.Options.Logger);
    }
}

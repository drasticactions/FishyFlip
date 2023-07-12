// <copyright file="ATProtoRepo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoRepo
{
    private ATProtocol proto;

    internal ATProtoRepo(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    public Task<Result<RecordRef>> CreateLikeAsync(
      Cid cid,
      ATUri uri,
      DateTime? createdAt = null,
      CancellationToken cancellationToken = default)
    {
        CreateLikeRecord record = new(
            Constants.FeedType.Like,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new LikeRecord(new Subject(cid, uri), createdAt ?? DateTime.UtcNow));

        return this.CreateRecord<CreateLikeRecord, RecordRef>(record, cancellationToken);
    }

    public Task<Result<RecordRef>> CreateRepostAsync(
       Cid cid,
       ATUri uri,
       DateTime? createdAt = null,
       CancellationToken cancellationToken = default)
    {
        CreateRepostRecord record = new(
            Constants.FeedType.Repost,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new RepostRecord(new Subject(cid, uri), createdAt ?? DateTime.UtcNow));

        return this.CreateRecord<CreateRepostRecord, RecordRef>(record, cancellationToken);
    }
    
    public Task<Result<RecordRef>> CreateFollowAsync(
        ATDid did,
        DateTime? createdAt = null,
        CancellationToken cancellationToken = default)
    {
        CreateFollowRecord record = new(
            Constants.GraphTypes.Follow,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new FollowRecord(did, createdAt ?? DateTime.UtcNow));

        return this.CreateRecord<CreateFollowRecord, RecordRef>(record, cancellationToken);
    }
    
    public Task<Result<CreatePostResponse>> CreatePostAsync(
        string text,
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
            new Post(embed, facets, createdAt, null, text, langs, Constants.FeedType.Post),
            rkey,
            swapCommit);
        return this.CreateRecord<CreatePostRecord, CreatePostResponse>(record, cancellationToken);
    }

    private Task<Result<Y>> CreateRecord<X, Y>(X record, CancellationToken cancellationToken = default)
    {
        return
            this.Client
                .Post<X, Y>(
                    Constants.Urls.ATProtoRepo.CreateRecord, this.Options.JsonSerializerOptions, record,
                    cancellationToken, this.Options.Logger);
    }
    
    private Task<Result<Y>> PutRecord<X, Y>(X record, CancellationToken cancellationToken = default)
    {
        return
            this.Client
                .Post<X, Y>(
                    Constants.Urls.ATProtoRepo.PutRecord, this.Options.JsonSerializerOptions, record,
                    cancellationToken, this.Options.Logger);
    }

    public Task<Result<RecordRef>> CreateBlockAsync(
        ATDid did,
        DateTime? createdAt = null,
        CancellationToken cancellationToken = default)
    {
        CreateBlockRecord record = new(
            Constants.GraphTypes.Block,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            new BlockRecord(did, createdAt ?? DateTime.UtcNow));

        return
            this.Client
                .Post<CreateBlockRecord, RecordRef>(
                    Constants.Urls.ATProtoRepo.CreateRecord, this.Options.JsonSerializerOptions, record,
                    cancellationToken, this.Options.Logger);
    }

    public async Task<Result<PostRecord?>> GetPostAsync(ATIdentifier repo, string rkey, Cid? cid = null, CancellationToken cancellationToken = default)
      => await this.GetRecordAsync<PostRecord>(Constants.FeedType.Post, repo, rkey, cid, cancellationToken);

    public async Task<Result<T?>> GetRecordAsync<T>(string collection, ATIdentifier repo, string rkey, Cid? cid = null, CancellationToken cancellationToken = default)
        where T : ATFeedTypeAPI
    {
        string url = $"{Constants.Urls.ATProtoRepo.GetRecord}?collection={collection}&repo={repo}&rkey={rkey}";
        if (cid is not null)
        {
            url += $"&cid={cid}";
        }

        return await this.Client.Get<T>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
    
    public Task<Result<Success>> DeleteFollowAsync(ATIdentifier repo, string rkey,
        Cid? swapRecord = null, Cid? swapCommit = null, CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.GraphTypes.Follow, repo, rkey, swapRecord, swapCommit, cancellationToken);
    
    public Task<Result<Success>> DeleteBlockAsync(ATIdentifier repo, string rkey,
        Cid? swapRecord = null, Cid? swapCommit = null, CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.GraphTypes.Block, repo, rkey, swapRecord, swapCommit, cancellationToken);
    
    public Task<Result<Success>> DeleteLikeAsync(ATIdentifier repo, string rkey,
        Cid? swapRecord = null, Cid? swapCommit = null, CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.FeedType.Like, repo, rkey, swapRecord, swapCommit, cancellationToken);

    public Task<Result<Success>> DeletePostAsync(ATIdentifier repo, string rkey,
        Cid? swapRecord = null, Cid? swapCommit = null, CancellationToken cancellationToken = default)
        => this.DeleteRecordAsync(Constants.FeedType.Post, repo, rkey, swapRecord, swapCommit, cancellationToken);
    
    public async Task<Result<Success>> DeleteRecordAsync(string collection, ATIdentifier repo, string rkey, Cid? swapRecord = null, Cid? swapCommit = null, CancellationToken cancellationToken = default)
    {
        DeleteRecord record = new(
            collection,
            this.proto.SessionManager!.Session!.Did.ToString()!,
            rkey,
            swapRecord,
            swapCommit);
        return await this.Client.Post<Success>(Constants.Urls.ATProtoRepo.DeleteRecord, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}

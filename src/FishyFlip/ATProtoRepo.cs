// <copyright file="ATProtoRepo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoRepo
{
    private ATProtocol proto;

    public ATProtoRepo(ATProtocol proto)
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

        return
            this.Client
                .Post<CreateLikeRecord, RecordRef>(
                    Constants.Urls.ATProtoRepo.CreateRecord, this.Options.JsonSerializerOptions, record,
                    cancellationToken, this.Options.Logger);
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

        return
            this.Client
                .Post<CreateRepostRecord, RecordRef>(
                    Constants.Urls.ATProtoRepo.CreateRecord, this.Options.JsonSerializerOptions, record,
                    cancellationToken, this.Options.Logger);
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

        return
            this.Client
                .Post<CreatePostRecord, CreatePostResponse>(
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
}

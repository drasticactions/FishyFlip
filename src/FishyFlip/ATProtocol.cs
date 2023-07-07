// <copyright file="ATProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading;
using FishyFlip.Commands;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging;

namespace FishyFlip;

public sealed class ATProtocol : IAsyncDisposable, IDisposable
{
    private ATProtocolOptions options;
    private bool isDisposed;
    private HttpClient client;
    private SessionManager? sessionManager;

    public ATProtocol(ATProtocolOptions options)
    {
        this.options = options;
        this.client = options.HttpClient ?? throw new NullReferenceException(nameof(options.HttpClient));
    }

    public ATProtocolOptions Options => this.options;

    internal HttpClient Client => this.client;

    public async Task<Result<Session>> LoginAsync(Login command, CancellationToken cancellationToken = default)
    {
        Result<Session> result =
            await this.client.Post<Login, Session>(Constants.Urls.AtProtoServer.Login, this.options.JsonSerializerOptions, command, cancellationToken);

        return
            result
                .Match(
                s =>
                {
                    this.OnUserLoggedIn(s);
                    return result;
                },
                error => error!);
    }

    public async Task<Result<PostCollection>> GetPostsAsync(IEnumerable<AtUri> query, CancellationToken cancellationToken = default)
    {
        var answer = string.Join(",", query.Select(n => n.ToString()));
        string url = $"{Constants.Urls.Bluesky.GetPosts}?uris={answer}";
        Multiple<PostCollection?, Error> result = await this.client.Get<PostCollection>(url, this.options.JsonSerializerOptions, cancellationToken);
        return result
            .Match<Result<PostCollection>>(
                timeline => (timeline ?? new PostCollection(new PostView[0]))!,
                error => error!);
    }
    
    public Task<Result<InviteCodes?>> GetAccountInviteCodesAsync(GetAccountInviteCodes? query = null, CancellationToken cancellationToken = default)
    {
        query ??= new GetAccountInviteCodes();
        var url = $"{Constants.Urls.AtProtoServer.GetAccountInviteCodes}?includeUsed={query.IncludeUsed.ToString().ToLowerInvariant()}&createAvailable={query.CreateAvailable.ToString().ToLowerInvariant()}";
        return this.client.Get<InviteCodes>(url, this.options.JsonSerializerOptions, cancellationToken);
    }
    
    public Task<Result<SessionInfo?>> GetSessionAsync(CancellationToken cancellationToken = default)
    {
        return this.client.Get<SessionInfo>(Constants.Urls.AtProtoServer.GetSession, this.options.JsonSerializerOptions, cancellationToken);
    }
    
    public Task<Result<AppPasswords?>> ListAppPasswordsAsync(CancellationToken cancellationToken = default)
    {
        return this.client.Get<AppPasswords>(Constants.Urls.AtProtoServer.ListAppPasswords, this.options.JsonSerializerOptions, cancellationToken);
    }
    
    public Task<Result<DescribeServer?>> DescribeServerAsync(CancellationToken cancellationToken = default)
    {
        return this.client.Get<DescribeServer>(Constants.Urls.AtProtoServer.DescribeServer, this.options.JsonSerializerOptions, cancellationToken);
    }
    
    public Task<Result<UploadBlobResponse>> UploadBlobAsync(StreamContent content, CancellationToken cancellationToken = default)
    {
        return
            this.client
                .Post<UploadBlobResponse>(
                    Constants.Urls.AtProtoRepo.UploadBlob, this.options.JsonSerializerOptions, content,
                    cancellationToken, this.options.Logger);
    }

    public Task<Result<CreatePostResponse>> CreatePostAsync(
        string Text,
        Facet[]? Facets = null,
        EmbedRecord? embed = default,
        DateTime? CreatedAt = null,
        CancellationToken cancellationToken = default)
    {
        CreatePostRecord record = new(
            Constants.FeedType.Post,
            this.sessionManager!.Session!.Did.ToString()!,
            new PostRecord()
            {
                Text = Text,
                Type = Constants.FeedType.Post,
                CreatedAt = CreatedAt ?? DateTime.UtcNow,
                Facets = Facets,
                Embed = embed,
            });

        return
            this.client
                .Post<CreatePostRecord, CreatePostResponse>(
                    Constants.Urls.AtProtoRepo.CreateRecord, this.options.JsonSerializerOptions, record,
                    cancellationToken, this.options.Logger);
    }

    public Task<Result<CreatePostResponse>> CreateRepostAsync(
        string cid,
        AtUri uri,
        DateTime? createdAt = null,
        CancellationToken cancellationToken = default)
    {
        CreateRepostRecord record = new(
            Constants.FeedType.Repost,
            this.sessionManager!.Session!.Did.ToString()!,
            new RepostRecord()
            {
                Subject = new Subject()
                {
                    Cid = cid,
                    Uri = uri.ToString(),
                },
                Type = Constants.FeedType.Repost,
                CreatedAt = createdAt ?? DateTime.UtcNow,
            });

        return
            this.client
                .Post<CreateRepostRecord, CreatePostResponse>(
                    Constants.Urls.AtProtoRepo.CreateRecord, this.options.JsonSerializerOptions, record,
                    cancellationToken, this.options.Logger);
    }

    public Task<Result<CreatePostResponse>> CreateLikeAsync(
        string cid,
        AtUri uri,
        DateTime? createdAt = null,
        CancellationToken cancellationToken = default)
    {
        CreateLikeRecord record = new(
            Constants.FeedType.Like,
            this.sessionManager!.Session!.Did.ToString()!,
            new LikeRecord()
            {
                Subject = new Subject()
                {
                    Cid = cid,
                    Uri = uri.ToString(),
                },
                Type = Constants.FeedType.Like,
                CreatedAt = createdAt ?? DateTime.UtcNow,
            });

        return
            this.client
                .Post<CreateLikeRecord, CreatePostResponse>(
                    Constants.Urls.AtProtoRepo.CreateRecord, this.options.JsonSerializerOptions, record,
                    cancellationToken, this.options.Logger);
    }

    public async Task<Result<Timeline>> GetTimelineAsync(string algorithm = "reverse-chronological", int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.GetTimeline}?algorithm={algorithm}&limit={limit}";
        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        Multiple<Timeline?, Error> result = await this.client.Get<Timeline>(url, this.options.JsonSerializerOptions, cancellationToken, this.options.Logger);
        return result
            .Match<Result<Timeline>>(
                timeline => (timeline ?? new Timeline(Array.Empty<FeedViewPost>(), null))!,
                error => error!);
    }

    public async Task<Result<ThreadPostViewFeed>> GetPostThreadAsync(AtUri uri, int depth = 0,
        CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.GetPostThread}?uri={uri}";
        if (depth > 0)
        {
            url += $"&depth={depth}";
        }
        
        Multiple<ThreadPostViewFeed?, Error> result = await this.client.Get<ThreadPostViewFeed>(url, this.options.JsonSerializerOptions, cancellationToken, this.options.Logger);
        return result
            .Match<Result<ThreadPostViewFeed>>(
                timeline => timeline!,
                error => error!);
    }

    public async Task<Result<RepostedFeed>> GetRepostedByAsync(AtUri uri, int limit = 50, string? cid = default,
        string? cursor = default, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.GetRepostedBy}?uri={uri.ToString()}&limit={limit}";

        if (cid is not null)
        {
            url += $"&cid={cid}";
        }

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        Multiple<RepostedFeed?, Error> result = await this.client.Get<RepostedFeed>(url, this.options.JsonSerializerOptions, cancellationToken, this.options.Logger);
        return result
            .Match<Result<RepostedFeed>>(
                timeline => (timeline ?? new RepostedFeed(Array.Empty<Profile>(), null))!,
                error => error!);
    }

    public Task<Result<Timeline>> GetAuthorFeedAsync(AtDid did, int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        => this.GetAuthorFeedAsync(did.ToString(), limit, cursor, cancellationToken);

    public Task<Result<Timeline>> GetAuthorFeedAsync(AtHandler handle, int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        => this.GetAuthorFeedAsync(handle.ToString(), limit, cursor, cancellationToken);

    public async Task<Result<Session>> RefreshSessionAsync(
        Session session,
        CancellationToken cancellationToken = default)
    {
        this.client
            .DefaultRequestHeaders
            .Authorization = new AuthenticationHeaderValue("Bearer", session.RefreshJwt);

        var result = await this.client.Post<Session>(Constants.Urls.AtProtoServer.RefreshSession, this.options.JsonSerializerOptions, cancellationToken, this.options.Logger);
        return
            result
                .Match(
                s =>
                {
                    this.sessionManager?.SetSession(s);
                    return result;
                },
                error => error!);
    }

    private async Task<Result<Timeline>> GetAuthorFeedAsync(string handle, int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.GetAuthorFeed}?actor={handle}&limit={limit}";
        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        Multiple<Timeline?, Error> result = await this.client.Get<Timeline>(url, this.options.JsonSerializerOptions, cancellationToken, this.options.Logger);
        return result
            .Match<Result<Timeline>>(
                authorFeed => (authorFeed ?? new Timeline(Array.Empty<FeedViewPost>(), null))!,
                error => error!);
    }

    public async Task<Result<HandleResolution?>> ResolveHandleAsync(AtHandler handler, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.AtProtoIdentity.ResolveHandle}?handle={handler}";
        return await this.client.Get<HandleResolution>(url, this.options.JsonSerializerOptions, cancellationToken);
    }

    public Task<Result<Profile?>> GetProfileAsync(AtDid identifier, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.GetActorProfile}?actor={identifier}";
        return this.client.Get<Profile>(url, this.options.JsonSerializerOptions, cancellationToken, this.options.Logger);
    }

    public Task<Result<Profile?>> GetProfileAsync(AtHandler identifier, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.GetActorProfile}?actor={identifier}";
        return this.client.Get<Profile>(url, this.options.JsonSerializerOptions, cancellationToken, this.options.Logger);
    }

    public async Task<Result<LikesFeed>> GetLikesAsync(AtUri uri, int limit = 50, string? cid = default, string? cursor = default, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.GetLikes}?uri={uri.ToString()}&limit={limit}";

        if (cid is not null)
        {
            url += $"&cid={cid}";
        }

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        Multiple<LikesFeed?, Error> result = await this.Client.Get<LikesFeed>(url, this.options.JsonSerializerOptions, cancellationToken, this.options.Logger);
        return result
            .Match<Result<LikesFeed>>(
                timeline => (timeline ?? new LikesFeed(Array.Empty<Like>(), null))!,
                error => error!);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.isDisposed = true;
        this.client.Dispose();
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        this.Dispose();
    }

    private void OnUserLoggedIn(Session session)
    {
        if (this.sessionManager is null)
        {
            this.sessionManager = new SessionManager(this, session);
        }
        else
        {
            this.sessionManager.SetSession(session);
        }
    }
}
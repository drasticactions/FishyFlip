// <copyright file="BlueskyGraph.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Bluesky Graph.
/// </summary>
public sealed class BlueskyGraph
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlueskyGraph"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal BlueskyGraph(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    public async Task<Result<ActorBlocks?>> GetBlocksAsync(
        int limit = 50,
        string? cursor = default,
        CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetBlocks + $"?limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ActorBlocks>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public async Task<Result<ActorMutes?>> GetMutesAsync(
        int limit = 50,
        string? cursor = default,
        CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetMutes + $"?limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ActorMutes>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public async Task<Result<ActorFollows?>> GetFollowsAsync(
        ATIdentifier identifier,
        int limit = 50,
        string? cursor = default,
        CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetFollows + $"?actor={identifier}&limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ActorFollows>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
    
    public async Task<Result<ActorFollowers?>> GetFollowersAsync(
        ATIdentifier identifier,
        int limit = 50,
        string? cursor = default,
        CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetFollowers + $"?actor={identifier}&limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ActorFollowers>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public Task<Result<Success>> MuteActorAsync(ATDid did, CancellationToken cancellationToken = default)
    {
        var muteRecord = new CreateMuteRecord(did);
        return this.Client.Post<CreateMuteRecord, Success>(Constants.Urls.Bluesky.Graph.MuteActor, this.Options.JsonSerializerOptions, muteRecord, cancellationToken, this.Options.Logger);
    }

    public Task<Result<Success>> UnmuteActorAsync(ATDid did, CancellationToken cancellationToken = default)
    {
        var muteRecord = new CreateMuteRecord(did);
        return this.Client.Post<CreateMuteRecord, Success>(Constants.Urls.Bluesky.Graph.UnmuteActor, this.Options.JsonSerializerOptions, muteRecord, cancellationToken, this.Options.Logger);
    }

    public async Task<Result<ListViewRecord?>> GetListsAsync(ATIdentifier identifier, int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetLists + $"?actor={identifier}&limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ListViewRecord>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public async Task<Result<ListViewRecord?>> GetListMutesAsync(int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetListMutes + $"?limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ListViewRecord>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
    
    public async Task<Result<ListViewRecord?>> GetListBlocksAsync(int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetListBlocks + $"?limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ListViewRecord>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public async Task<Result<ListItemViewRecord?>> GetListAsync(ATUri list, int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetList + $"?list={list}&limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ListItemViewRecord>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public async Task<Result<SuggestionsRecord?>> GetSuggestedFollowsByActorAsync(ATIdentifier identifier, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetSuggestedFollowsByActor + $"?actor={identifier}";
        return await this.Client.Get<SuggestionsRecord>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}

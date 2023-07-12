// <copyright file="BlueskyGraph.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class BlueskyGraph
{
    private ATProtocol proto;

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
        var url = Constants.Urls.Bluesky.Graph.GetFollowers + $"?actor={identifier}&limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ActorFollows>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
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
}

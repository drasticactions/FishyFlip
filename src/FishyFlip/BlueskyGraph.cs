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
}

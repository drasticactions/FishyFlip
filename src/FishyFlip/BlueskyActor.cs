// <copyright file="BlueskyActor.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class BlueskyActor
{
    private ATProtocol proto;

    internal BlueskyActor(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    public Task<Result<FeedProfile?>> GetProfileAsync(ATIdentifier identifier, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Actor.GetActorProfile}?actor={identifier}";
        return this.Client.Get<FeedProfile>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}

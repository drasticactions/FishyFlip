// <copyright file="BlueskyActor.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Bluesky Actor.
/// </summary>
public sealed class BlueskyActor
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlueskyActor"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
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

    public Task<Result<FeedProfiles?>> GetProfilesAsync(ATIdentifier[] identifiers, CancellationToken cancellationToken = default)
    {
        var identList = string.Join("&", identifiers.Select(n => $"actors={n}"));
        string url = $"{Constants.Urls.Bluesky.Actor.GetActorProfiles}?{identList}";
        return this.Client.Get<FeedProfiles>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public Task<Result<ActorResponse?>> GetSuggestionsAsync(int limit = 50, string? cursor = null, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Actor.GetActorSuggestions}?limit={limit}";
        if (!string.IsNullOrEmpty(cursor))
        {
            url += $"&cursor={cursor}";
        }

        return this.Client.Get<ActorResponse>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public Task<Result<ActorResponse?>> SearchActorsAsync(string query, int limit = 50, string? cursor = null, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Actor.SearchActors}?term={query}&limit={limit}";
        if (!string.IsNullOrEmpty(cursor))
        {
            url += $"&cursor={cursor}";
        }

        return this.Client.Get<ActorResponse>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public Task<Result<ActorResponse?>> SearchActorsTypeaheadAsync(string query, int limit = 50, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Actor.SearchActorsTypeahead}?term={query}&limit={limit}";

        return this.Client.Get<ActorResponse>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}

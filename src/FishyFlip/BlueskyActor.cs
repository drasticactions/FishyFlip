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

    /// <summary>
    /// Asynchronously retrieves the profile of a specified actor.
    /// </summary>
    /// <param name="identifier">The identifier of the actor whose profile is to be retrieved.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the actor's profile, or null if the actor's profile could not be retrieved.</returns>
    public Task<Result<FeedProfile?>> GetProfileAsync(ATIdentifier identifier, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Actor.GetActorProfile}?actor={identifier}";
        return this.Client.Get<FeedProfile>(url, this.Options.SourceGenerationContext.FeedProfile, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the profiles of specified actors.
    /// </summary>
    /// <param name="identifiers">An array of identifiers of the actors whose profiles are to be retrieved.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the actors' profiles, or null if the actors' profiles could not be retrieved.</returns>
    public Task<Result<FeedProfiles?>> GetProfilesAsync(ATIdentifier[] identifiers, CancellationToken cancellationToken = default)
    {
        var identList = string.Join("&", identifiers.Select(n => $"actors={n}"));
        string url = $"{Constants.Urls.Bluesky.Actor.GetActorProfiles}?{identList}";
        return this.Client.Get<FeedProfiles>(url, this.Options.SourceGenerationContext.FeedProfiles, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves actor suggestions.
    /// </summary>
    /// <param name="limit">Optional. The maximum number of actor suggestions to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through actor suggestions.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the actor suggestions, or null if no actor suggestions were found.</returns>
    public Task<Result<ActorResponse?>> GetSuggestionsAsync(int limit = 50, string? cursor = null, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Actor.GetActorSuggestions}?limit={limit}";
        if (!string.IsNullOrEmpty(cursor))
        {
            url += $"&cursor={cursor}";
        }

        return this.Client.Get<ActorResponse>(url, this.Options.SourceGenerationContext.ActorResponse, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously searches for actors based on a query.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="limit">Optional. The maximum number of actors to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through actors.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the actors that match the query, or null if no matching actors were found.</returns>
    public Task<Result<ActorResponse?>> SearchActorsAsync(string query, int limit = 50, string? cursor = null, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Actor.SearchActors}?term={query}&limit={limit}";
        if (!string.IsNullOrEmpty(cursor))
        {
            url += $"&cursor={cursor}";
        }

        return this.Client.Get<ActorResponse>(url, this.Options.SourceGenerationContext.ActorResponse, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously searches for actors based on a query, with typeahead functionality.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="limit">Optional. The maximum number of actors to retrieve. Default is 50.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the actors that match the query, or null if no matching actors were found.</returns>
    public Task<Result<ActorResponse?>> SearchActorsTypeaheadAsync(string query, int limit = 50, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Actor.SearchActorsTypeahead}?term={query}&limit={limit}";

        return this.Client.Get<ActorResponse>(url, this.Options.SourceGenerationContext.ActorResponse, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the preferences of the current actor.
    /// </summary>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the actors preferences, or null if no matching actors were found.</returns>
    public Task<Result<ActorPreferences?>> GetPreferencesAsync(CancellationToken cancellationToken = default)
        => this.Client.Get<ActorPreferences>(Constants.Urls.Bluesky.Actor.GetPreferences, this.Options.SourceGenerationContext.ActorPreferences, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
}

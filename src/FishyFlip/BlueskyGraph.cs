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

    /// <summary>
    /// Asynchronously retrieves the blocks of an actor.
    /// </summary>
    /// <param name="limit">Optional. The maximum number of blocks to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through blocks.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the blocks of the actor, or null if no blocks were found.</returns>
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

        return await this.Client.Get<ActorBlocks>(url, this.Options.SourceGenerationContext.ActorBlocks, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the mutes of an actor.
    /// </summary>
    /// <param name="limit">Optional. The maximum number of mutes to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through mutes.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the mutes of the actor, or null if no mutes were found.</returns>
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

        return await this.Client.Get<ActorMutes>(url, this.Options.SourceGenerationContext.ActorMutes, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the follows of an actor.
    /// </summary>
    /// <param name="identifier">The identifier of the actor whose follows are to be retrieved.</param>
    /// <param name="limit">Optional. The maximum number of follows to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through follows.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the follows of the actor, or null if no follows were found.</returns>
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

        return await this.Client.Get<ActorFollows>(url, this.Options.SourceGenerationContext.ActorFollows, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the followers of an actor.
    /// </summary>
    /// <param name="identifier">The identifier of the actor whose followers are to be retrieved.</param>
    /// <param name="limit">Optional. The maximum number of followers to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through followers.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the followers of the actor, or null if no followers were found.</returns>
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

        return await this.Client.Get<ActorFollowers>(url, this.Options.SourceGenerationContext.ActorFollowers, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously mutes an actor.
    /// </summary>
    /// <param name="did">The DID of the actor to be muted.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> MuteActorAsync(ATDid did, CancellationToken cancellationToken = default)
    {
        var muteRecord = new CreateMuteRecord(did);
        return this.Client.Post<CreateMuteRecord, Success>(Constants.Urls.Bluesky.Graph.MuteActor, this.Options.SourceGenerationContext.CreateMuteRecord, this.Options.SourceGenerationContext.Success, this.Options.JsonSerializerOptions, muteRecord, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously unmutes an actor.
    /// </summary>
    /// <param name="did">The DID of the actor to be unmuted.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> UnmuteActorAsync(ATDid did, CancellationToken cancellationToken = default)
    {
        var muteRecord = new CreateMuteRecord(did);
        return this.Client.Post<CreateMuteRecord, Success>(Constants.Urls.Bluesky.Graph.UnmuteActor, this.Options.SourceGenerationContext.CreateMuteRecord, this.Options.SourceGenerationContext.Success, this.Options.JsonSerializerOptions, muteRecord, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously mutes an actor list.
    /// </summary>
    /// <param name="list">The URI of the list to be muted.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> MuteActorListAsync(ATUri list, CancellationToken cancellationToken = default)
    {
        var muteRecord = new CreateMuteListRecord(list);
        return this.Client.Post<CreateMuteListRecord, Success>(Constants.Urls.Bluesky.Graph.MuteActorList, this.Options.SourceGenerationContext.CreateMuteListRecord, this.Options.SourceGenerationContext.Success, this.Options.JsonSerializerOptions, muteRecord, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously unmutes an actor list.
    /// </summary>
    /// <param name="list">The URI of the list to be unmuted.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object indicating whether the operation was successful.</returns>
    public Task<Result<Success>> UnmuteActorListAsync(ATUri list, CancellationToken cancellationToken = default)
    {
        var muteRecord = new CreateMuteListRecord(list);
        return this.Client.Post<CreateMuteListRecord, Success>(Constants.Urls.Bluesky.Graph.UnmuteActorList, this.Options.SourceGenerationContext.CreateMuteListRecord, this.Options.SourceGenerationContext.Success, this.Options.JsonSerializerOptions, muteRecord, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the lists of an actor.
    /// </summary>
    /// <param name="identifier">The identifier of the actor whose lists are to be retrieved.</param>
    /// <param name="limit">Optional. The maximum number of lists to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through lists.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the lists of the actor, or null if no lists were found.</returns>
    public async Task<Result<ListViewRecord?>> GetListsAsync(ATIdentifier identifier, int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetLists + $"?actor={identifier}&limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ListViewRecord>(url, this.Options.SourceGenerationContext.ListViewRecord, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the list mutes of an actor.
    /// </summary>
    /// <param name="limit">Optional. The maximum number of list mutes to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through list mutes.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the list mutes of the actor, or null if no list mutes were found.</returns>
    public async Task<Result<ListViewRecord?>> GetListMutesAsync(int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetListMutes + $"?limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ListViewRecord>(url, this.Options.SourceGenerationContext.ListViewRecord, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves the list blocks of an actor.
    /// </summary>
    /// <param name="limit">Optional. The maximum number of list blocks to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through list blocks.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the list blocks of the actor, or null if no list blocks were found.</returns>
    public async Task<Result<ListViewRecord?>> GetListBlocksAsync(int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetListBlocks + $"?limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ListViewRecord>(url, this.Options.SourceGenerationContext.ListViewRecord, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves a list.
    /// </summary>
    /// <param name="list">The URI of the list to be retrieved.</param>
    /// <param name="limit">Optional. The maximum number of items to retrieve from the list. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through list items.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the list, or null if the list was not found.</returns>
    public async Task<Result<ListItemViewRecord?>> GetListAsync(ATUri list, int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetList + $"?list={list}&limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<ListItemViewRecord>(url, this.Options.SourceGenerationContext.ListItemViewRecord, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves suggested follows for a specific actor.
    /// </summary>
    /// <param name="identifier">The identifier of the actor for whom to retrieve suggested follows.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the suggested follows for the actor, or null if no suggestions were found.</returns>
    public async Task<Result<SuggestionsRecord?>> GetSuggestedFollowsByActorAsync(ATIdentifier identifier, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Graph.GetSuggestedFollowsByActor + $"?actor={identifier}";
        return await this.Client.Get<SuggestionsRecord>(url, this.Options.SourceGenerationContext.SuggestionsRecord, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}

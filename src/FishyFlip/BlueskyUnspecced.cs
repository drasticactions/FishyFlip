// <copyright file="BlueskyUnspecced.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Bluesky Unspecced.
/// </summary>
public sealed class BlueskyUnspecced
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlueskyUnspecced"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal BlueskyUnspecced(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Asynchronously retrieves popular feed generators.
    /// </summary>
    /// <param name="limit">Optional. The maximum number of feed generators to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through feed generators.</param>
    /// <param name="query">Optional. A query that can be used to filter the feed generators.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the popular feed generators, or null if no feed generators were found.</returns>
    public async Task<Result<FeedResultList>> GetPopularFeedGeneratorsAsync(int limit = 50, string? cursor = default, string? query = default, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Unspecced.GetPopularFeedGenerators}?limit={limit}";
        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        if (query is not null)
        {
            url += $"&query={query}";
        }

        Multiple<FeedResultList?, ATError> result = await this.Client.Get<FeedResultList>(url, this.Options.SourceGenerationContext.FeedResultList, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        return result
            .Match<Result<FeedResultList>>(
                timeline => (timeline ?? new FeedResultList(Array.Empty<FeedRecord>(), null))!,
                error => error!);
    }

    /// <summary>
    /// Gets the tagged suggestions asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns><see cref="TagSuggestions"/>.</returns>
    public Task<Result<TagSuggestions?>> GetTaggedSuggestionsAsync(CancellationToken cancellationToken = default)
        => this.Client.Get<TagSuggestions>(Constants.Urls.Bluesky.Unspecced.GetTaggedSuggestions, this.Options.SourceGenerationContext.TagSuggestions, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
}

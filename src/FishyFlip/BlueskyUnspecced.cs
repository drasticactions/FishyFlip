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

    public async Task<Result<FeedResultList>> GetPopularFeedGeneratorsAsync(int limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.Bluesky.Unspecced.GetPopularFeedGenerators}?limit={limit}";
        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        Multiple<FeedResultList?, Error> result = await this.Client.Get<FeedResultList>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        return result
            .Match<Result<FeedResultList>>(
                timeline => (timeline ?? new FeedResultList(Array.Empty<FeedRecord>(), null))!,
                error => error!);
    }
}

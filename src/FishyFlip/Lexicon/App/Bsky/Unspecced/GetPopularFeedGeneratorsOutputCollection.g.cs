// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    /// <summary>
    /// GetPopularFeedGeneratorsOutput Collection.
    /// </summary>
    public class GetPopularFeedGeneratorsOutputCollection : ATObjectCollectionBase<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView>, IAsyncEnumerable<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView>
    {

        public GetPopularFeedGeneratorsOutputCollection(FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, string? query = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.Limit = limit;
            this.Cursor = cursor;
            this.Query = query;
            this.CancellationToken = cancellationToken;
        }

        public string? Query { get; }

        /// <inheritdoc/>
        public override async Task<(IList<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.GetPopularFeedGeneratorsAsync(query: this.Query, limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Feeds == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Feeds, result.Cursor ?? string.Empty);
        }

        public static GetPopularFeedGeneratorsOutputCollection Create(FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, string? query = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, query: query, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}


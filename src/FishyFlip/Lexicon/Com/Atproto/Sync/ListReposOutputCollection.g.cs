// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    /// <summary>
    /// ListReposOutput Collection.
    /// </summary>
    public class ListReposOutputCollection : ATObjectCollectionBase<FishyFlip.Lexicon.Com.Atproto.Sync.Repo>, IAsyncEnumerable<FishyFlip.Lexicon.Com.Atproto.Sync.Repo>
    {

        public ListReposOutputCollection(FishyFlip.ATProtocol atp, int? limit = 500, string? cursor = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.Limit = limit;
            this.Cursor = cursor;
            this.CancellationToken = cancellationToken;
        }

        /// <inheritdoc/>
        public override async Task<(IList<FishyFlip.Lexicon.Com.Atproto.Sync.Repo> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.ListReposAsync(limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Repos == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Repos, result.Cursor ?? string.Empty);
        }

        public static ListReposOutputCollection Create(FishyFlip.ATProtocol atp, int? limit = 500, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}


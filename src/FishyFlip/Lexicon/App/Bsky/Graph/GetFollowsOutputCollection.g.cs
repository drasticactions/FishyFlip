// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    /// <summary>
    /// GetFollowsOutput Collection.
    /// </summary>
    public class GetFollowsOutputCollection : ATObjectCollectionBase<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView>, IAsyncEnumerable<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView>
    {

        public GetFollowsOutputCollection(FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.Actor = actor;
            this.Limit = limit;
            this.Cursor = cursor;
            this.CancellationToken = cancellationToken;
        }

        public FishyFlip.Models.ATIdentifier Actor { get; }

        /// <inheritdoc/>
        public override async Task<(IList<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.GetFollowsAsync(actor: this.Actor, limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Follows == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Follows, result.Cursor ?? string.Empty);
        }

        public static GetFollowsOutputCollection Create(FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, actor: actor, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}


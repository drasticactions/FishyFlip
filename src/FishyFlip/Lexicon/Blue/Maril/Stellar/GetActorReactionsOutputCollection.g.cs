// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Maril.Stellar
{
    /// <summary>
    /// GetActorReactionsOutput Collection.
    /// </summary>
    public class GetActorReactionsOutputCollection : ATObjectCollectionBase<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction>, IAsyncEnumerable<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction>
    {

        public GetActorReactionsOutputCollection(FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.Actor = actor;
            this.Limit = limit;
            this.Cursor = cursor;
            this.CancellationToken = cancellationToken;
        }

        public FishyFlip.Models.ATIdentifier Actor { get; }

        /// <inheritdoc/>
        public override async Task<(IList<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.GetActorReactionsAsync(actor: this.Actor, limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Feed == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Feed, result.Cursor ?? string.Empty);
        }

        public static GetActorReactionsOutputCollection Create(FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, actor: actor, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}


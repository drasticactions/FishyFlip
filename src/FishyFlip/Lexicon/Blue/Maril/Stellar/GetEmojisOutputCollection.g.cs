// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Maril.Stellar
{
    /// <summary>
    /// GetEmojisOutput Collection.
    /// </summary>
    public class GetEmojisOutputCollection : ATObjectCollectionBase<FishyFlip.Lexicon.Blue.Maril.Stellar.ItemView>, IAsyncEnumerable<FishyFlip.Lexicon.Blue.Maril.Stellar.ItemView>
    {

        public GetEmojisOutputCollection(FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, FishyFlip.Models.ATIdentifier? did = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.Limit = limit;
            this.Cursor = cursor;
            this.Did = did;
            this.CancellationToken = cancellationToken;
        }

        public FishyFlip.Models.ATIdentifier? Did { get; }

        /// <inheritdoc/>
        public override async Task<(IList<FishyFlip.Lexicon.Blue.Maril.Stellar.ItemView> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.GetEmojisAsync(did: this.Did, limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Items == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Items, result.Cursor ?? string.Empty);
        }

        public static GetEmojisOutputCollection Create(FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, FishyFlip.Models.ATIdentifier? did = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, did: did, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}


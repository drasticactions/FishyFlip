// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Packs
{
    /// <summary>
    /// GetPackOutput Collection.
    /// </summary>
    public class GetPackOutputCollection : ATObjectCollectionBase<FishyFlip.Lexicon.Blue.Moji.Packs.PackItemView>, IAsyncEnumerable<FishyFlip.Lexicon.Blue.Moji.Packs.PackItemView>
    {

        public GetPackOutputCollection(FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri pack, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.Pack = pack;
            this.Limit = limit;
            this.Cursor = cursor;
            this.CancellationToken = cancellationToken;
        }

        public FishyFlip.Models.ATUri Pack { get; }

        /// <inheritdoc/>
        public override async Task<(IList<FishyFlip.Lexicon.Blue.Moji.Packs.PackItemView> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.GetPackAsync(pack: this.Pack, limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Items == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Items, result.Cursor ?? string.Empty);
        }

        public static GetPackOutputCollection Create(FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri pack, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, pack: pack, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}


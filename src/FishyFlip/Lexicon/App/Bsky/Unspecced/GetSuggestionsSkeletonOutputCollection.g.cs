// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    /// <summary>
    /// GetSuggestionsSkeletonOutput Collection.
    /// </summary>
    public class GetSuggestionsSkeletonOutputCollection : ATObjectCollectionBase<FishyFlip.Lexicon.App.Bsky.Unspecced.SkeletonSearchActor>, IAsyncEnumerable<FishyFlip.Lexicon.App.Bsky.Unspecced.SkeletonSearchActor>
    {

        public GetSuggestionsSkeletonOutputCollection(FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid? viewer = default, int? limit = 50, string? cursor = default, FishyFlip.Models.ATDid? relativeToDid = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.Viewer = viewer;
            this.Limit = limit;
            this.Cursor = cursor;
            this.RelativeToDid = relativeToDid;
            this.CancellationToken = cancellationToken;
        }

        public FishyFlip.Models.ATDid? Viewer { get; }

        public FishyFlip.Models.ATDid? RelativeToDid { get; }

        /// <inheritdoc/>
        public override async Task<(IList<FishyFlip.Lexicon.App.Bsky.Unspecced.SkeletonSearchActor> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.GetSuggestionsSkeletonAsync(viewer: this.Viewer, relativeToDid: this.RelativeToDid, limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Actors == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Actors, result.Cursor ?? string.Empty);
        }

        public static GetSuggestionsSkeletonOutputCollection Create(FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid? viewer = default, int? limit = 50, string? cursor = default, FishyFlip.Models.ATDid? relativeToDid = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, viewer: viewer, relativeToDid: relativeToDid, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}


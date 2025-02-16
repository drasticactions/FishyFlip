// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Label
{

    /// <summary>
    /// com.atproto.label Endpoint Class.
    /// </summary>
    public sealed class ATProtoLabel
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ATProtoLabel"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal ATProtoLabel(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Find labels relevant to the provided AT-URI patterns. Public endpoint for moderation services, though may return different or additional results with auth.
        /// </summary>
        /// <param name="uriPatterns"></param>
        /// <param name="sources"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Label.QueryLabelsOutput?>> QueryLabelsAsync (List<string> uriPatterns, List<FishyFlip.Models.ATDid>? sources = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.QueryLabelsAsync(uriPatterns, sources, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Find labels relevant to the provided AT-URI patterns. Public endpoint for moderation services, though may return different or additional results with auth.
        /// </summary>
        /// <param name="uriPatterns"></param>
        /// <param name="sources"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public QueryLabelsOutputCollection QueryLabelsCollectionAsync (List<string> uriPatterns, List<FishyFlip.Models.ATDid>? sources = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new QueryLabelsOutputCollection(atp, uriPatterns, sources, limit, cursor, cancellationToken);
        }

    }
}


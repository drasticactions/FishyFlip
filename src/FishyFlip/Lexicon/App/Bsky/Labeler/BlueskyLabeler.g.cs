// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Labeler
{

    /// <summary>
    /// app.bsky.labeler Endpoint Class.
    /// </summary>
    public sealed class BlueskyLabeler
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueskyLabeler"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal BlueskyLabeler(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Get information about a list of labeler services.
        /// </summary>
        /// <param name="dids"></param>
        /// <param name="detailed"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Labeler.GetServicesOutput?>> GetServicesAsync (List<FishyFlip.Models.ATDid> dids, bool? detailed = default, CancellationToken cancellationToken = default)
        {
            return atp.GetServicesAsync(dids, detailed, cancellationToken);
        }

    }
}


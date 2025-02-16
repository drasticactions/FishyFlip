// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Packs
{

    /// <summary>
    /// blue.moji.packs Endpoint Class.
    /// </summary>
    public sealed class BlueMojiPacks
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueMojiPacks"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal BlueMojiPacks(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Get a list of Bluemoji packs created by the actor.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Moji.Packs.GetActorPacksOutput?>> GetActorPacksAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetActorPacksAsync(actor, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Get a list of Bluemoji packs created by the actor.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetActorPacksOutputCollection GetActorPacksCollectionAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetActorPacksOutputCollection(atp, actor, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Gets a 'view' (with additional context) of a specified pack.
        /// </summary>
        /// <param name="pack"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput?>> GetPackAsync (FishyFlip.Models.ATUri pack, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetPackAsync(pack, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Gets a 'view' (with additional context) of a specified pack.
        /// </summary>
        /// <param name="pack"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetPackOutputCollection GetPackCollectionAsync (FishyFlip.Models.ATUri pack, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetPackOutputCollection(atp, pack, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Get views for a list of Bluemoji packs.
        /// </summary>
        /// <param name="uris"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Moji.Packs.GetPacksOutput?>> GetPacksAsync (List<FishyFlip.Models.ATUri> uris, CancellationToken cancellationToken = default)
        {
            return atp.GetPacksAsync(uris, cancellationToken);
        }

    }
}


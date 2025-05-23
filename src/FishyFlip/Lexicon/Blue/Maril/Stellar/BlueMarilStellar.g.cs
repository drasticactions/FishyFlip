// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Maril.Stellar
{

    /// <summary>
    /// blue.maril.stellar Endpoint Class.
    /// </summary>
    public sealed class BlueMarilStellar
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueMarilStellar"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal BlueMarilStellar(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Get a list of posts reaction by an actor. Requires auth, actor must be the requesting account.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Maril.Stellar.GetActorReactionsOutput?>> GetActorReactionsAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetActorReactionsAsync(actor, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Get a list of posts reaction by an actor. Requires auth, actor must be the requesting account.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetActorReactionsOutputCollection GetActorReactionsCollectionAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetActorReactionsOutputCollection(atp, actor, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Return all Bluemoji in the AppView.
        /// </summary>
        /// <param name="limit">The number of records to return.</param>
        /// <param name="cursor"></param>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Maril.Stellar.GetEmojisOutput?>> GetEmojisAsync (int? limit = 50, string? cursor = default, FishyFlip.Models.ATIdentifier? did = default, CancellationToken cancellationToken = default)
        {
            return atp.GetEmojisAsync(limit, cursor, did, cancellationToken);
        }

        /// <summary>
        /// Return all Bluemoji in the AppView.
        /// </summary>
        /// <param name="limit">The number of records to return.</param>
        /// <param name="cursor"></param>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        public GetEmojisOutputCollection GetEmojisCollectionAsync (int? limit = 50, string? cursor = default, FishyFlip.Models.ATIdentifier? did = default, CancellationToken cancellationToken = default)
        {
            return new GetEmojisOutputCollection(atp, limit, cursor, did, cancellationToken);
        }


        /// <summary>
        /// Get reaction records which reference a subject (by AT-URI and CID).
        /// </summary>
        /// <param name="uri">AT-URI of the subject (eg, a post record).</param>
        /// <param name="cid">CID of the subject record (aka, specific version of record), to filter reactions.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Maril.Stellar.GetReactionsOutput?>> GetReactionsAsync (FishyFlip.Models.ATUri uri, string? cid = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetReactionsAsync(uri, cid, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Get reaction records which reference a subject (by AT-URI and CID).
        /// </summary>
        /// <param name="uri">AT-URI of the subject (eg, a post record).</param>
        /// <param name="cid">CID of the subject record (aka, specific version of record), to filter reactions.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetReactionsOutputCollection GetReactionsCollectionAsync (FishyFlip.Models.ATUri uri, string? cid = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetReactionsOutputCollection(atp, uri, cid, limit, cursor, cancellationToken);
        }

    }
}


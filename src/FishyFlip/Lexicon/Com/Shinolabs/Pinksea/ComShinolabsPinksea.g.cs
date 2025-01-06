// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{

    /// <summary>
    /// com.shinolabs.pinksea Endpoint Class.
    /// </summary>
    public sealed class ComShinolabsPinksea
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComShinolabsPinksea"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal ComShinolabsPinksea(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Gets the feed for a given author.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="since"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorFeedOutput?>> GetAuthorFeedAsync (FishyFlip.Models.ATIdentifier did, DateTime? since = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return atp.GetAuthorFeedAsync(did, since, limit, cancellationToken);
        }


        /// <summary>
        /// Gets the replies for an author.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="since"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorRepliesOutput?>> GetAuthorRepliesAsync (FishyFlip.Models.ATIdentifier did, DateTime? since = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return atp.GetAuthorRepliesAsync(did, since, limit, cancellationToken);
        }


        /// <summary>
        /// Gets the handle for a DID.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetHandleFromDidOutput?>> GetHandleFromDidAsync (FishyFlip.Models.ATIdentifier did, CancellationToken cancellationToken = default)
        {
            return atp.GetHandleFromDidAsync(did, cancellationToken);
        }


        /// <summary>
        /// Returns the identity of the authenticated user.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetIdentityOutput?>> GetIdentityAsync (CancellationToken cancellationToken = default)
        {
            return atp.GetIdentityAsync(cancellationToken);
        }


        /// <summary>
        /// Gets the data about an oekaki post, with its children
        /// </summary>
        /// <param name="did"></param>
        /// <param name="rkey"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetOekakiOutput?>> GetOekakiAsync (FishyFlip.Models.ATIdentifier did, string rkey, CancellationToken cancellationToken = default)
        {
            return atp.GetOekakiAsync(did, rkey, cancellationToken);
        }


        /// <summary>
        /// Gets the parent for a reply.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="rkey"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetParentForReplyOutput?>> GetParentForReplyAsync (FishyFlip.Models.ATIdentifier did, string rkey, CancellationToken cancellationToken = default)
        {
            return atp.GetParentForReplyAsync(did, rkey, cancellationToken);
        }


        /// <summary>
        /// Gets the most recent posts on the timeline, in reverse chronological order.
        /// </summary>
        /// <param name="since"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetRecentOutput?>> GetRecentAsync (DateTime? since = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return atp.GetRecentAsync(since, limit, cancellationToken);
        }


        /// <summary>
        /// Gets the feed for a given tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="since"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetTagFeedOutput?>> GetTagFeedAsync (string tag, DateTime? since = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return atp.GetTagFeedAsync(tag, since, limit, cancellationToken);
        }

    }
}

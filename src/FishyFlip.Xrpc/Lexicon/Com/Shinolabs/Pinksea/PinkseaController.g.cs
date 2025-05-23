// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Xrpc.Lexicon.Com.Shinolabs.Pinksea
{

    /// <summary>
    /// com.shinolabs.pinksea XRPC Group.
    /// </summary>
    [ApiController]
    public abstract class PinkseaController : ControllerBase
    {

        /// <summary>
        /// Gets the feed for a given author.
        /// </summary>
        /// <param name="did">The DID of the author.</param>
        /// <param name="since">Since when should the query begin?</param>
        /// <param name="limit">The limit on posts to fetch.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorFeedOutput"/></returns>
        [HttpGet("/xrpc/com.shinolabs.pinksea.getAuthorFeed")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorFeedOutput>, ATErrorResult>> GetAuthorFeedAsync ([FromQuery] FishyFlip.Models.ATIdentifier did, [FromQuery] DateTime? since = default, [FromQuery] int? limit = 50, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the replies for an author.
        /// </summary>
        /// <param name="did">The DID of the author.</param>
        /// <param name="since">Since when should the query begin?</param>
        /// <param name="limit">The limit on posts to fetch.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorRepliesOutput"/></returns>
        [HttpGet("/xrpc/com.shinolabs.pinksea.getAuthorReplies")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorRepliesOutput>, ATErrorResult>> GetAuthorRepliesAsync ([FromQuery] FishyFlip.Models.ATIdentifier did, [FromQuery] DateTime? since = default, [FromQuery] int? limit = 50, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the handle for a DID.
        /// </summary>
        /// <param name="did">The DID.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetHandleFromDidOutput"/></returns>
        [HttpGet("/xrpc/com.shinolabs.pinksea.getHandleFromDid")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetHandleFromDidOutput>, ATErrorResult>> GetHandleFromDidAsync ([FromQuery] FishyFlip.Models.ATIdentifier did, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the identity of the authenticated user.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetIdentityOutput"/></returns>
        [HttpGet("/xrpc/com.shinolabs.pinksea.getIdentity")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetIdentityOutput>, ATErrorResult>> GetIdentityAsync (CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the data about an oekaki post, with its children
        /// </summary>
        /// <param name="did">The DID of the author.</param>
        /// <param name="rkey">The record key.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetOekakiOutput"/></returns>
        [HttpGet("/xrpc/com.shinolabs.pinksea.getOekaki")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetOekakiOutput>, ATErrorResult>> GetOekakiAsync ([FromQuery] FishyFlip.Models.ATIdentifier did, [FromQuery] string rkey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the parent for a reply.
        /// </summary>
        /// <param name="did">The DID of the author.</param>
        /// <param name="rkey">The record key.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetParentForReplyOutput"/></returns>
        [HttpGet("/xrpc/com.shinolabs.pinksea.getParentForReply")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetParentForReplyOutput>, ATErrorResult>> GetParentForReplyAsync ([FromQuery] FishyFlip.Models.ATIdentifier did, [FromQuery] string rkey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the most recent posts on the timeline, in reverse chronological order.
        /// </summary>
        /// <param name="since">Since when should the query begin?</param>
        /// <param name="limit">The limit on posts to fetch.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetRecentOutput"/></returns>
        [HttpGet("/xrpc/com.shinolabs.pinksea.getRecent")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetRecentOutput>, ATErrorResult>> GetRecentAsync ([FromQuery] DateTime? since = default, [FromQuery] int? limit = 50, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the feed for a given tag.
        /// </summary>
        /// <param name="tag">The tag to fetch posts for.</param>
        /// <param name="since">Since when should the query begin?</param>
        /// <param name="limit">The limit on posts to fetch.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetTagFeedOutput"/></returns>
        [HttpGet("/xrpc/com.shinolabs.pinksea.getTagFeed")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetTagFeedOutput>, ATErrorResult>> GetTagFeedAsync ([FromQuery] string tag, [FromQuery] DateTime? since = default, [FromQuery] int? limit = 50, CancellationToken cancellationToken = default);
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Xrpc.Lexicon.Fm.Teal.Alpha.Actor
{

    /// <summary>
    /// fm.teal.alpha.actor XRPC Group.
    /// </summary>
    [ApiController]
    public abstract class ActorController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actor">The author's DID</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.GetProfileOutput"/></returns>
        [HttpGet("/xrpc/fm.teal.alpha.actor.getProfile")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.GetProfileOutput>, ATErrorResult>> GetProfileAsync ([FromQuery] FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actors">Array of actor DIDs</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.GetProfilesOutput"/></returns>
        [HttpGet("/xrpc/fm.teal.alpha.actor.getProfiles")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.GetProfilesOutput>, ATErrorResult>> GetProfilesAsync ([FromQuery] List<FishyFlip.Models.ATIdentifier> actors, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="q">The search query</param>
        /// <param name="limit">The maximum number of actors to return</param>
        /// <param name="cursor">Cursor for pagination</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.SearchActorsOutput"/></returns>
        [HttpGet("/xrpc/fm.teal.alpha.actor.searchActors")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.SearchActorsOutput>, ATErrorResult>> SearchActorsAsync ([FromQuery] string q, [FromQuery] int? limit = 0, [FromQuery] string? cursor = default, CancellationToken cancellationToken = default);
    }
}


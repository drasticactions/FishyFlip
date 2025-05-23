// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Xrpc.Lexicon.Chat.Bsky.Convo
{

    /// <summary>
    /// chat.bsky.convo XRPC Group.
    /// </summary>
    [ApiController]
    public abstract class ConvoController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.acceptConvo")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoOutput>, ATErrorResult>> AcceptConvoAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoInput input, CancellationToken cancellationToken);

        /// <summary>
        /// Adds an emoji reaction to a message. Requires authentication. It is idempotent, so multiple calls from the same user with the same emoji result in a single reaction.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.ReactionMessageDeletedError"/> Indicates that the message has been deleted and reactions can no longer be added/removed. <br/>
        /// <see cref="FishyFlip.Lexicon.ReactionLimitReachedError"/> Indicates that the message has the maximum number of reactions allowed for a single user, and the requested reaction wasn't yet present. If it was already present, the request will not fail since it is idempotent. <br/>
        /// <see cref="FishyFlip.Lexicon.ReactionInvalidValueError"/> Indicates the value for the reaction is not acceptable. In general, this means it is not an emoji. <br/>
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="messageId"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.AddReactionOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.addReaction")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.AddReactionOutput>, ATErrorResult>> AddReactionAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.AddReactionInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.deleteMessageForSelf")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView>, ATErrorResult>> DeleteMessageForSelfAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.DeleteMessageForSelfInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoOutput"/></returns>
        [HttpGet("/xrpc/chat.bsky.convo.getConvo")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoOutput>, ATErrorResult>> GetConvoAsync ([FromQuery] string convoId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get whether the requester and the other members can chat. If an existing convo is found for these members, it is returned.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoAvailabilityOutput"/></returns>
        [HttpGet("/xrpc/chat.bsky.convo.getConvoAvailability")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoAvailabilityOutput>, ATErrorResult>> GetConvoAvailabilityAsync ([FromQuery] List<FishyFlip.Models.ATDid> members, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="members"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoForMembersOutput"/></returns>
        [HttpGet("/xrpc/chat.bsky.convo.getConvoForMembers")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoForMembersOutput>, ATErrorResult>> GetConvoForMembersAsync ([FromQuery] List<FishyFlip.Models.ATDid> members, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput"/></returns>
        [HttpGet("/xrpc/chat.bsky.convo.getLog")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput>, ATErrorResult>> GetLogAsync ([FromQuery] string? cursor = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetMessagesOutput"/></returns>
        [HttpGet("/xrpc/chat.bsky.convo.getMessages")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.GetMessagesOutput>, ATErrorResult>> GetMessagesAsync ([FromQuery] string convoId, [FromQuery] int? limit = 50, [FromQuery] string? cursor = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.leaveConvo")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput>, ATErrorResult>> LeaveConvoAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="readState"></param>
        /// <param name="status"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.ListConvosOutput"/></returns>
        [HttpGet("/xrpc/chat.bsky.convo.listConvos")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.ListConvosOutput>, ATErrorResult>> ListConvosAsync ([FromQuery] int? limit = 50, [FromQuery] string? cursor = default, [FromQuery] string? readState = default, [FromQuery] string? status = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.muteConvo")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput>, ATErrorResult>> MuteConvoAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoInput input, CancellationToken cancellationToken);

        /// <summary>
        /// Removes an emoji reaction from a message. Requires authentication. It is idempotent, so multiple calls from the same user with the same emoji result in that reaction not being present, even if it already wasn't.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.ReactionMessageDeletedError"/> Indicates that the message has been deleted and reactions can no longer be added/removed. <br/>
        /// <see cref="FishyFlip.Lexicon.ReactionInvalidValueError"/> Indicates the value for the reaction is not acceptable. In general, this means it is not an emoji. <br/>
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="messageId"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.RemoveReactionOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.removeReaction")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.RemoveReactionOutput>, ATErrorResult>> RemoveReactionAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.RemoveReactionInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.sendMessage")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView>, ATErrorResult>> SendMessageAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.sendMessageBatch")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput>, ATErrorResult>> SendMessageBatchAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.unmuteConvo")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoOutput>, ATErrorResult>> UnmuteConvoAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateAllReadOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.updateAllRead")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateAllReadOutput>, ATErrorResult>> UpdateAllReadAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateAllReadInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput"/></returns>
        [HttpPost("/xrpc/chat.bsky.convo.updateRead")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput>, ATErrorResult>> UpdateReadAsync ([FromBody] FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadInput input, CancellationToken cancellationToken);
    }
}


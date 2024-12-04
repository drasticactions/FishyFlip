// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Moderation
{

    /// <summary>
    /// chat.bsky.moderation Endpoint Group.
    /// </summary>
    public static class ModerationEndpoints
    {

       public const string GetActorMetadata = "/xrpc/chat.bsky.moderation.getActorMetadata";

       public const string GetMessageContext = "/xrpc/chat.bsky.moderation.getMessageContext";

       public const string UpdateActorAccess = "/xrpc/chat.bsky.moderation.updateActorAccess";


        /// <summary>
        /// Generated endpoint for chat.bsky.moderation.getActorMetadata
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Moderation.GetActorMetadataOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetActorMetadataOutput?>> GetActorMetadataAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid actor, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetActorMetadata.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("actor=" + actor);

            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetActorMetadataOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyModerationGetActorMetadataOutput!, cancellationToken);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.moderation.getMessageContext
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="messageId"></param>
        /// <param name="convoId"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Moderation.GetMessageContextOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetMessageContextOutput?>> GetMessageContextAsync (this FishyFlip.ATProtocol atp, string messageId, string? convoId = default, int? before = 5, int? after = 5, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetMessageContext.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("messageId=" + messageId);

            if (convoId != null)
            {
                queryStrings.Add("convoId=" + convoId);
            }

            if (before != null)
            {
                queryStrings.Add("before=" + before);
            }

            if (after != null)
            {
                queryStrings.Add("after=" + after);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetMessageContextOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyModerationGetMessageContextOutput!, cancellationToken);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.moderation.updateActorAccess
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="allowAccess"></param>
        /// <param name="@ref"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> UpdateActorAccessAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid actor, bool allowAccess, string? @ref = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UpdateActorAccess.ToString();
            var inputItem = new UpdateActorAccessInput();
            inputItem.Actor = actor;
            inputItem.AllowAccess = allowAccess;
            inputItem.Ref = @ref;
            return atp.Post<UpdateActorAccessInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyModerationUpdateActorAccessInput!, atp.Options.SourceGenerationContext.Success!, inputItem, cancellationToken);
        }

    }
}


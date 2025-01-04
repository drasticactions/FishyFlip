// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{

    /// <summary>
    /// chat.bsky.convo Endpoint Group.
    /// </summary>
    public static class ConvoEndpoints
    {

       public const string DeleteMessageForSelf = "/xrpc/chat.bsky.convo.deleteMessageForSelf";

       public const string GetConvo = "/xrpc/chat.bsky.convo.getConvo";

       public const string GetConvoForMembers = "/xrpc/chat.bsky.convo.getConvoForMembers";

       public const string GetLog = "/xrpc/chat.bsky.convo.getLog";

       public const string GetMessages = "/xrpc/chat.bsky.convo.getMessages";

       public const string LeaveConvo = "/xrpc/chat.bsky.convo.leaveConvo";

       public const string ListConvos = "/xrpc/chat.bsky.convo.listConvos";

       public const string MuteConvo = "/xrpc/chat.bsky.convo.muteConvo";

       public const string SendMessage = "/xrpc/chat.bsky.convo.sendMessage";

       public const string SendMessageBatch = "/xrpc/chat.bsky.convo.sendMessageBatch";

       public const string UnmuteConvo = "/xrpc/chat.bsky.convo.unmuteConvo";

       public const string UpdateRead = "/xrpc/chat.bsky.convo.updateRead";


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.deleteMessageForSelf
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="convoId"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView?>> DeleteMessageForSelfAsync (this FishyFlip.ATProtocol atp, string convoId, string messageId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = DeleteMessageForSelf.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            var inputItem = new DeleteMessageForSelfInput();
            inputItem.ConvoId = convoId;
            inputItem.MessageId = messageId;
            return atp.Post<DeleteMessageForSelfInput, FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoDeleteMessageForSelfInput!, atp.Options.SourceGenerationContext.ChatBskyConvoDeletedMessageView!, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.getConvo
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoOutput?>> GetConvoAsync (this FishyFlip.ATProtocol atp, string convoId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetConvo.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("convoId=" + convoId);

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoGetConvoOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.getConvoForMembers
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="members"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoForMembersOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoForMembersOutput?>> GetConvoForMembersAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATDid> members, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetConvoForMembers.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", members.Select(n => "members=" + n)));

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoForMembersOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoGetConvoForMembersOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.getLog
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput?>> GetLogAsync (this FishyFlip.ATProtocol atp, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetLog.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoGetLogOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.getMessages
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="convoId"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.GetMessagesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.GetMessagesOutput?>> GetMessagesAsync (this FishyFlip.ATProtocol atp, string convoId, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetMessages.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("convoId=" + convoId);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.GetMessagesOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoGetMessagesOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.leaveConvo
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput?>> LeaveConvoAsync (this FishyFlip.ATProtocol atp, string convoId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = LeaveConvo.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            var inputItem = new LeaveConvoInput();
            inputItem.ConvoId = convoId;
            return atp.Post<LeaveConvoInput, FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoLeaveConvoInput!, atp.Options.SourceGenerationContext.ChatBskyConvoLeaveConvoOutput!, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.listConvos
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.ListConvosOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.ListConvosOutput?>> ListConvosAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ListConvos.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.ListConvosOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoListConvosOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.muteConvo
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput?>> MuteConvoAsync (this FishyFlip.ATProtocol atp, string convoId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = MuteConvo.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            var inputItem = new MuteConvoInput();
            inputItem.ConvoId = convoId;
            return atp.Post<MuteConvoInput, FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoMuteConvoInput!, atp.Options.SourceGenerationContext.ChatBskyConvoMuteConvoOutput!, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.sendMessage
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="convoId"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView?>> SendMessageAsync (this FishyFlip.ATProtocol atp, string convoId, FishyFlip.Lexicon.Chat.Bsky.Convo.MessageInput message, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SendMessage.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            var inputItem = new SendMessageInput();
            inputItem.ConvoId = convoId;
            inputItem.Message = message;
            return atp.Post<SendMessageInput, FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoSendMessageInput!, atp.Options.SourceGenerationContext.ChatBskyConvoMessageView!, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.sendMessageBatch
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="items"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput?>> SendMessageBatchAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Lexicon.Chat.Bsky.Convo.BatchItem> items, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SendMessageBatch.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            var inputItem = new SendMessageBatchInput();
            inputItem.Items = items;
            return atp.Post<SendMessageBatchInput, FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoSendMessageBatchInput!, atp.Options.SourceGenerationContext.ChatBskyConvoSendMessageBatchOutput!, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.unmuteConvo
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="convoId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoOutput?>> UnmuteConvoAsync (this FishyFlip.ATProtocol atp, string convoId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UnmuteConvo.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            var inputItem = new UnmuteConvoInput();
            inputItem.ConvoId = convoId;
            return atp.Post<UnmuteConvoInput, FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoUnmuteConvoInput!, atp.Options.SourceGenerationContext.ChatBskyConvoUnmuteConvoOutput!, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.updateRead
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="convoId"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput?>> UpdateReadAsync (this FishyFlip.ATProtocol atp, string convoId, string? messageId = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UpdateRead.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            var inputItem = new UpdateReadInput();
            inputItem.ConvoId = convoId;
            inputItem.MessageId = messageId;
            return atp.Post<UpdateReadInput, FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoUpdateReadInput!, atp.Options.SourceGenerationContext.ChatBskyConvoUpdateReadOutput!, inputItem, cancellationToken, headers);
        }

    }
}


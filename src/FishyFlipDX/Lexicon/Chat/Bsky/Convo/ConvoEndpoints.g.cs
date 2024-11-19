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
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView?>> DeleteMessageForSelfAsync (this FishyFlip.ATProtocol atp, string convoId, string messageId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = DeleteMessageForSelf.ToString();
            var inputItem = new DeleteMessageForSelfInput();
            inputItem.ConvoId = convoId;
            inputItem.MessageId = messageId;
            return atp.Client.Post<DeleteMessageForSelfInput, FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoDeleteMessageForSelfInput!, atp.Options.SourceGenerationContext.ChatBskyConvoDeletedMessageView!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.getConvo
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoOutput?>> GetConvoAsync (this FishyFlip.ATProtocol atp, string convoId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetConvo.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("convoId=" + convoId);

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoGetConvoOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.getConvoForMembers
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoForMembersOutput?>> GetConvoForMembersAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATDid?> members, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetConvoForMembers.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (members != null)
            {
                queryStrings.Add("members=" + string.Join(",", members));
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.GetConvoForMembersOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoGetConvoForMembersOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.getLog
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput?>> GetLogAsync (this FishyFlip.ATProtocol atp, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetLog.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoGetLogOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.getMessages
        /// </summary>
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

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.GetMessagesOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoGetMessagesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.leaveConvo
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput?>> LeaveConvoAsync (this FishyFlip.ATProtocol atp, string convoId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = LeaveConvo.ToString();
            var inputItem = new LeaveConvoInput();
            inputItem.ConvoId = convoId;
            return atp.Client.Post<LeaveConvoInput, FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoLeaveConvoInput!, atp.Options.SourceGenerationContext.ChatBskyConvoLeaveConvoOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.listConvos
        /// </summary>
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

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.Chat.Bsky.Convo.ListConvosOutput>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoListConvosOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.muteConvo
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput?>> MuteConvoAsync (this FishyFlip.ATProtocol atp, string convoId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = MuteConvo.ToString();
            var inputItem = new MuteConvoInput();
            inputItem.ConvoId = convoId;
            return atp.Client.Post<MuteConvoInput, FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoMuteConvoInput!, atp.Options.SourceGenerationContext.ChatBskyConvoMuteConvoOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.sendMessage
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView?>> SendMessageAsync (this FishyFlip.ATProtocol atp, string convoId, FishyFlip.Lexicon.Chat.Bsky.Convo.MessageInput message, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SendMessage.ToString();
            var inputItem = new SendMessageInput();
            inputItem.ConvoId = convoId;
            inputItem.Message = message;
            return atp.Client.Post<SendMessageInput, FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoSendMessageInput!, atp.Options.SourceGenerationContext.ChatBskyConvoMessageView!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.sendMessageBatch
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput?>> SendMessageBatchAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Lexicon.Chat.Bsky.Convo.BatchItem?> items, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SendMessageBatch.ToString();
            var inputItem = new SendMessageBatchInput();
            inputItem.Items = items;
            return atp.Client.Post<SendMessageBatchInput, FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoSendMessageBatchInput!, atp.Options.SourceGenerationContext.ChatBskyConvoSendMessageBatchOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.unmuteConvo
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoOutput?>> UnmuteConvoAsync (this FishyFlip.ATProtocol atp, string convoId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UnmuteConvo.ToString();
            var inputItem = new UnmuteConvoInput();
            inputItem.ConvoId = convoId;
            return atp.Client.Post<UnmuteConvoInput, FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoUnmuteConvoInput!, atp.Options.SourceGenerationContext.ChatBskyConvoUnmuteConvoOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.convo.updateRead
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput?>> UpdateReadAsync (this FishyFlip.ATProtocol atp, string convoId, string? messageId = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UpdateRead.ToString();
            var inputItem = new UpdateReadInput();
            inputItem.ConvoId = convoId;
            inputItem.MessageId = messageId;
            return atp.Client.Post<UpdateReadInput, FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyConvoUpdateReadInput!, atp.Options.SourceGenerationContext.ChatBskyConvoUpdateReadOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }

    }
}


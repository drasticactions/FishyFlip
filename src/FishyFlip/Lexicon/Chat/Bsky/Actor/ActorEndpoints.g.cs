// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Actor
{

    /// <summary>
    /// chat.bsky.actor Endpoint Group.
    /// </summary>
    public static class ActorEndpoints
    {

       public const string GroupNamespace = "chat.bsky.actor";

       public const string DeleteAccount = "/xrpc/chat.bsky.actor.deleteAccount";

       public const string ExportAccountData = "/xrpc/chat.bsky.actor.exportAccountData";


        /// <summary>
        /// Generated endpoint for chat.bsky.actor.deleteAccount
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Chat.Bsky.Actor.DeleteAccountOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Chat.Bsky.Actor.DeleteAccountOutput?>> DeleteAccountAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = DeleteAccount.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            return atp.Post<FishyFlip.Lexicon.Chat.Bsky.Actor.DeleteAccountOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ChatBskyActorDeleteAccountOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Generated endpoint for chat.bsky.actor.exportAccountData
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> ExportAccountDataAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ExportAccountData.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            return atp.Get<Success>(endpointUrl, atp.Options.SourceGenerationContext.Success!, cancellationToken, headers);
        }

    }
}


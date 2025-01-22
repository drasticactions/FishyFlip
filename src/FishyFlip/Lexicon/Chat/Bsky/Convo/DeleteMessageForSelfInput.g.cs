// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class DeleteMessageForSelfInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteMessageForSelfInput"/> class.
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="messageId"></param>
        public DeleteMessageForSelfInput(string convoId = default, string messageId = default)
        {
            this.ConvoId = convoId;
            this.MessageId = messageId;
            this.Type = "chat.bsky.convo.deleteMessageForSelf#DeleteMessageForSelfInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteMessageForSelfInput"/> class.
        /// </summary>
        public DeleteMessageForSelfInput()
        {
            this.Type = "chat.bsky.convo.deleteMessageForSelf#DeleteMessageForSelfInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteMessageForSelfInput"/> class.
        /// </summary>
        public DeleteMessageForSelfInput(CBORObject obj)
        {
            if (obj["convoId"] is not null) this.ConvoId = obj["convoId"].AsString();
            if (obj["messageId"] is not null) this.MessageId = obj["messageId"].AsString();
        }

        /// <summary>
        /// Gets or sets the convoId.
        /// </summary>
        [JsonPropertyName("convoId")]
        [JsonRequired]
        public string ConvoId { get; set; }

        /// <summary>
        /// Gets or sets the messageId.
        /// </summary>
        [JsonPropertyName("messageId")]
        [JsonRequired]
        public string MessageId { get; set; }

        public const string RecordType = "chat.bsky.convo.deleteMessageForSelf#DeleteMessageForSelfInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Chat.Bsky.Convo.DeleteMessageForSelfInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.DeleteMessageForSelfInput>)SourceGenerationContext.Default.ChatBskyConvoDeleteMessageForSelfInput)!;
        }

        public static DeleteMessageForSelfInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.DeleteMessageForSelfInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.DeleteMessageForSelfInput>)SourceGenerationContext.Default.ChatBskyConvoDeleteMessageForSelfInput)!;
        }
    }
}


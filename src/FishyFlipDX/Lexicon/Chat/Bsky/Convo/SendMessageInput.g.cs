// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class SendMessageInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageInput"/> class.
        /// </summary>
        public SendMessageInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageInput"/> class.
        /// </summary>
        public SendMessageInput(CBORObject obj)
        {
            if (obj["convoId"] is not null) this.ConvoId = obj["convoId"].AsString();
            if (obj["message"] is not null) this.Message = new Chat.Bsky.Convo.MessageInput(obj["message"]);
        }

        [JsonPropertyName("convoId")]
        [JsonRequired]
        public string? ConvoId { get; set; }

        [JsonPropertyName("message")]
        [JsonRequired]
        public Chat.Bsky.Convo.MessageInput? Message { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "chat.bsky.convo.sendMessage#SendMessageInput";

        public const string RecordType = "chat.bsky.convo.sendMessage#SendMessageInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Chat.Bsky.Convo.SendMessageInput>(this, (JsonTypeInfo<Chat.Bsky.Convo.SendMessageInput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageInput)!;
        }

        public static SendMessageInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Chat.Bsky.Convo.SendMessageInput>(json, (JsonTypeInfo<Chat.Bsky.Convo.SendMessageInput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageInput)!;
        }
    }
}

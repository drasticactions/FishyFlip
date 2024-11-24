// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class SendMessageBatchOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchOutput"/> class.
        /// </summary>
        /// <param name="items"></param>
        public SendMessageBatchOutput(List<Chat.Bsky.Convo.MessageView>? items = default)
        {
            this.Items = items;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchOutput"/> class.
        /// </summary>
        public SendMessageBatchOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchOutput"/> class.
        /// </summary>
        public SendMessageBatchOutput(CBORObject obj)
        {
            if (obj["items"] is not null) this.Items = obj["items"].Values.Select(n =>new Chat.Bsky.Convo.MessageView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<Chat.Bsky.Convo.MessageView>? Items { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "chat.bsky.convo.sendMessageBatch#SendMessageBatchOutput";

        public const string RecordType = "chat.bsky.convo.sendMessageBatch#SendMessageBatchOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Chat.Bsky.Convo.SendMessageBatchOutput>(this, (JsonTypeInfo<Chat.Bsky.Convo.SendMessageBatchOutput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageBatchOutput)!;
        }

        public static SendMessageBatchOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Chat.Bsky.Convo.SendMessageBatchOutput>(json, (JsonTypeInfo<Chat.Bsky.Convo.SendMessageBatchOutput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageBatchOutput)!;
        }
    }
}

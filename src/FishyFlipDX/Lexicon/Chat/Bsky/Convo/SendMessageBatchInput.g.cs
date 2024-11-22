// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class SendMessageBatchInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchInput"/> class.
        /// </summary>
        /// <param name="items"></param>
        public SendMessageBatchInput(List<Chat.Bsky.Convo.BatchItem>? items = default)
        {
            this.Items = items;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchInput"/> class.
        /// </summary>
        public SendMessageBatchInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchInput"/> class.
        /// </summary>
        public SendMessageBatchInput(CBORObject obj)
        {
            if (obj["items"] is not null) this.Items = obj["items"].Values.Select(n =>new Chat.Bsky.Convo.BatchItem(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<Chat.Bsky.Convo.BatchItem>? Items { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "chat.bsky.convo.sendMessageBatch#SendMessageBatchInput";

        public const string RecordType = "chat.bsky.convo.sendMessageBatch#SendMessageBatchInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Chat.Bsky.Convo.SendMessageBatchInput>(this, (JsonTypeInfo<Chat.Bsky.Convo.SendMessageBatchInput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageBatchInput)!;
        }

        public static SendMessageBatchInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Chat.Bsky.Convo.SendMessageBatchInput>(json, (JsonTypeInfo<Chat.Bsky.Convo.SendMessageBatchInput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageBatchInput)!;
        }
    }
}


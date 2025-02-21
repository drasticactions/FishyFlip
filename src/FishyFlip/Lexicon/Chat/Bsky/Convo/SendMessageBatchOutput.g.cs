// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class SendMessageBatchOutput : ATObject, ICBOREncodable<SendMessageBatchOutput>, IJsonEncodable<SendMessageBatchOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchOutput"/> class.
        /// </summary>
        /// <param name="items"></param>
        public SendMessageBatchOutput(List<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView> items = default)
        {
            this.Items = items;
            this.Type = "chat.bsky.convo.sendMessageBatch#SendMessageBatchOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchOutput"/> class.
        /// </summary>
        public SendMessageBatchOutput()
        {
            this.Type = "chat.bsky.convo.sendMessageBatch#SendMessageBatchOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageBatchOutput"/> class.
        /// </summary>
        public SendMessageBatchOutput(CBORObject obj)
        {
            if (obj["items"] is not null) this.Items = obj["items"].Values.Select(n =>new FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView> Items { get; set; }

        public const string RecordType = "chat.bsky.convo.sendMessageBatch#SendMessageBatchOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageBatchOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageBatchOutput);
        }

        public static new SendMessageBatchOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.SendMessageBatchOutput>)SourceGenerationContext.Default.ChatBskyConvoSendMessageBatchOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new SendMessageBatchOutput FromCBORObject(CBORObject obj)
        {
            return new SendMessageBatchOutput(obj);
        }

    }
}


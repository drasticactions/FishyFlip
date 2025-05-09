// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Moderation
{
    public partial class GetMessageContextOutput : ATObject, ICBOREncodable<GetMessageContextOutput>, IJsonEncodable<GetMessageContextOutput>, IParsable<GetMessageContextOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMessageContextOutput"/> class.
        /// </summary>
        /// <param name="messages">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView"/> (chat.bsky.convo.defs#messageView) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView"/> (chat.bsky.convo.defs#deletedMessageView) <br/>
        /// </param>
        public GetMessageContextOutput(List<ATObject> messages = default)
        {
            this.Messages = messages;
            this.Type = "chat.bsky.moderation.getMessageContext#GetMessageContextOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetMessageContextOutput"/> class.
        /// </summary>
        public GetMessageContextOutput()
        {
            this.Type = "chat.bsky.moderation.getMessageContext#GetMessageContextOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetMessageContextOutput"/> class.
        /// </summary>
        public GetMessageContextOutput(CBORObject obj)
        {
            if (obj["messages"] is not null) this.Messages = obj["messages"].Values.Select(n =>n.ToATObject()).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the messages.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView"/> (chat.bsky.convo.defs#messageView) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView"/> (chat.bsky.convo.defs#deletedMessageView) <br/>
        /// </summary>
        [JsonPropertyName("messages")]
        [JsonRequired]
        public List<ATObject> Messages { get; set; }

        public const string RecordType = "chat.bsky.moderation.getMessageContext#GetMessageContextOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetMessageContextOutput>)SourceGenerationContext.Default.ChatBskyModerationGetMessageContextOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetMessageContextOutput>)SourceGenerationContext.Default.ChatBskyModerationGetMessageContextOutput);
        }

        public static new GetMessageContextOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetMessageContextOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetMessageContextOutput>)SourceGenerationContext.Default.ChatBskyModerationGetMessageContextOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetMessageContextOutput FromCBORObject(CBORObject obj)
        {
            return new GetMessageContextOutput(obj);
        }

        /// <inheritdoc/>
        public static GetMessageContextOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetMessageContextOutput>(s, (JsonTypeInfo<GetMessageContextOutput>)SourceGenerationContext.Default.ChatBskyModerationGetMessageContextOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetMessageContextOutput result)
        {
            result = JsonSerializer.Deserialize<GetMessageContextOutput>(s, (JsonTypeInfo<GetMessageContextOutput>)SourceGenerationContext.Default.ChatBskyModerationGetMessageContextOutput);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Moderation
{
    public partial class Metadata : ATObject, ICBOREncodable<Metadata>, IJsonEncodable<Metadata>, IParsable<Metadata>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        /// <param name="messagesSent"></param>
        /// <param name="messagesReceived"></param>
        /// <param name="convos"></param>
        /// <param name="convosStarted"></param>
        public Metadata(long messagesSent = default, long messagesReceived = default, long convos = default, long convosStarted = default)
        {
            this.MessagesSent = messagesSent;
            this.MessagesReceived = messagesReceived;
            this.Convos = convos;
            this.ConvosStarted = convosStarted;
            this.Type = "chat.bsky.moderation.getActorMetadata#metadata";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        public Metadata()
        {
            this.Type = "chat.bsky.moderation.getActorMetadata#metadata";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        public Metadata(CBORObject obj)
        {
            if (obj["messagesSent"] is not null) this.MessagesSent = obj["messagesSent"].AsInt64Value();
            if (obj["messagesReceived"] is not null) this.MessagesReceived = obj["messagesReceived"].AsInt64Value();
            if (obj["convos"] is not null) this.Convos = obj["convos"].AsInt64Value();
            if (obj["convosStarted"] is not null) this.ConvosStarted = obj["convosStarted"].AsInt64Value();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the messagesSent.
        /// </summary>
        [JsonPropertyName("messagesSent")]
        [JsonRequired]
        public long MessagesSent { get; set; }

        /// <summary>
        /// Gets or sets the messagesReceived.
        /// </summary>
        [JsonPropertyName("messagesReceived")]
        [JsonRequired]
        public long MessagesReceived { get; set; }

        /// <summary>
        /// Gets or sets the convos.
        /// </summary>
        [JsonPropertyName("convos")]
        [JsonRequired]
        public long Convos { get; set; }

        /// <summary>
        /// Gets or sets the convosStarted.
        /// </summary>
        [JsonPropertyName("convosStarted")]
        [JsonRequired]
        public long ConvosStarted { get; set; }

        public const string RecordType = "chat.bsky.moderation.getActorMetadata#metadata";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata>)SourceGenerationContext.Default.ChatBskyModerationMetadata);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata>)SourceGenerationContext.Default.ChatBskyModerationMetadata);
        }

        public static new Metadata FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata>)SourceGenerationContext.Default.ChatBskyModerationMetadata)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Metadata FromCBORObject(CBORObject obj)
        {
            return new Metadata(obj);
        }

        /// <inheritdoc/>
        public static Metadata Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Metadata>(s, (JsonTypeInfo<Metadata>)SourceGenerationContext.Default.ChatBskyModerationMetadata)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Metadata result)
        {
            result = JsonSerializer.Deserialize<Metadata>(s, (JsonTypeInfo<Metadata>)SourceGenerationContext.Default.ChatBskyModerationMetadata);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Place.Stream.Chat
{
    /// <summary>
    /// Record containing a Streamplace chat message.
    /// </summary>
    public partial class Message : ATObject, ICBOREncodable<Message>, IJsonEncodable<Message>, IParsable<Message>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="text">The primary message content. May be an empty string, if there are embeds.</param>
        /// <param name="streamer">The DID of the streamer whose chat this is.</param>
        /// <param name="createdAt">Client-declared timestamp when this message was originally created.</param>
        /// <param name="facets">Annotations of text (mentions, URLs, etc)</param>
        /// <param name="reply">
        /// place.stream.chat.defs#replyRef <br/>
        /// </param>
        public Message(string? text, FishyFlip.Models.ATDid? streamer, DateTime? createdAt = default, List<FishyFlip.Lexicon.Place.Stream.Richtext.Facet>? facets = default, FishyFlip.Lexicon.Place.Stream.Chat.ReplyRef? reply = default)
        {
            this.Text = text;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Facets = facets;
            this.Streamer = streamer;
            this.Reply = reply;
            this.Type = "place.stream.chat.message";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message()
        {
            this.Type = "place.stream.chat.message";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message(CBORObject obj)
        {
            if (obj["text"] is not null) this.Text = obj["text"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["facets"] is not null) this.Facets = obj["facets"].Values.Select(n =>new FishyFlip.Lexicon.Place.Stream.Richtext.Facet(n)).ToList();
            if (obj["streamer"] is not null) this.Streamer = obj["streamer"].ToATDid();
            if (obj["reply"] is not null) this.Reply = new FishyFlip.Lexicon.Place.Stream.Chat.ReplyRef(obj["reply"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the text.
        /// <br/> The primary message content. May be an empty string, if there are embeds.
        /// </summary>
        [JsonPropertyName("text")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// <br/> Client-declared timestamp when this message was originally created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the facets.
        /// <br/> Annotations of text (mentions, URLs, etc)
        /// </summary>
        [JsonPropertyName("facets")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FishyFlip.Lexicon.Place.Stream.Richtext.Facet>? Facets { get; set; }

        /// <summary>
        /// Gets or sets the streamer.
        /// <br/> The DID of the streamer whose chat this is.
        /// </summary>
        [JsonPropertyName("streamer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Streamer { get; set; }

        /// <summary>
        /// Gets or sets the reply.
        /// place.stream.chat.defs#replyRef <br/>
        /// </summary>
        [JsonPropertyName("reply")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FishyFlip.Lexicon.Place.Stream.Chat.ReplyRef? Reply { get; set; }

        public const string RecordType = "place.stream.chat.message";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Chat.Message>)SourceGenerationContext.Default.PlaceStreamChatMessage);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Chat.Message>)SourceGenerationContext.Default.PlaceStreamChatMessage);
        }

        public static new Message FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Place.Stream.Chat.Message>(json, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Chat.Message>)SourceGenerationContext.Default.PlaceStreamChatMessage)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Message FromCBORObject(CBORObject obj)
        {
            return new Message(obj);
        }

        /// <inheritdoc/>
        public static Message Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Message>(s, (JsonTypeInfo<Message>)SourceGenerationContext.Default.PlaceStreamChatMessage)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Message result)
        {
            result = JsonSerializer.Deserialize<Message>(s, (JsonTypeInfo<Message>)SourceGenerationContext.Default.PlaceStreamChatMessage);
            return result != null;
        }
    }
}


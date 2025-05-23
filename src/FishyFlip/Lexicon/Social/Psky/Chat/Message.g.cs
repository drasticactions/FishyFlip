// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Social.Psky.Chat
{
    /// <summary>
    /// A Picosky message containing at most 2048 graphemes.
    /// </summary>
    public partial class Message : ATObject, ICBOREncodable<Message>, IJsonEncodable<Message>, IParsable<Message>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="content">Text content.</param>
        /// <param name="room"></param>
        /// <param name="facets">Annotations of text (mentions, URLs, hashtags, etc)</param>
        /// <param name="reply">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        public Message(string? content, FishyFlip.Models.ATUri? room, List<FishyFlip.Lexicon.Social.Psky.Richtext.Facet>? facets = default, Com.Atproto.Repo.StrongRef? reply = default)
        {
            this.Content = content;
            this.Room = room;
            this.Facets = facets;
            this.Reply = reply;
            this.Type = "social.psky.chat.message";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message()
        {
            this.Type = "social.psky.chat.message";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message(CBORObject obj)
        {
            if (obj["content"] is not null) this.Content = obj["content"].AsString();
            if (obj["room"] is not null) this.Room = obj["room"].ToATUri();
            if (obj["facets"] is not null) this.Facets = obj["facets"].Values.Select(n =>new FishyFlip.Lexicon.Social.Psky.Richtext.Facet(n)).ToList();
            if (obj["reply"] is not null) this.Reply = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["reply"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the content.
        /// <br/> Text content.
        /// </summary>
        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        [JsonPropertyName("room")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Room { get; set; }

        /// <summary>
        /// Gets or sets the facets.
        /// <br/> Annotations of text (mentions, URLs, hashtags, etc)
        /// </summary>
        [JsonPropertyName("facets")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FishyFlip.Lexicon.Social.Psky.Richtext.Facet>? Facets { get; set; }

        /// <summary>
        /// Gets or sets the reply.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("reply")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Com.Atproto.Repo.StrongRef? Reply { get; set; }

        public const string RecordType = "social.psky.chat.message";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Chat.Message>)SourceGenerationContext.Default.SocialPskyChatMessage);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Chat.Message>)SourceGenerationContext.Default.SocialPskyChatMessage);
        }

        public static new Message FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Social.Psky.Chat.Message>(json, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Chat.Message>)SourceGenerationContext.Default.SocialPskyChatMessage)!;
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
            return JsonSerializer.Deserialize<Message>(s, (JsonTypeInfo<Message>)SourceGenerationContext.Default.SocialPskyChatMessage)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Message result)
        {
            result = JsonSerializer.Deserialize<Message>(s, (JsonTypeInfo<Message>)SourceGenerationContext.Default.SocialPskyChatMessage);
            return result != null;
        }
    }
}


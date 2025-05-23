// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    /// <summary>
    /// Record representing a block relationship against an entire an entire list of accounts (actors).
    /// </summary>
    public partial class Listblock : ATObject, ICBOREncodable<Listblock>, IJsonEncodable<Listblock>, IParsable<Listblock>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Listblock"/> class.
        /// </summary>
        /// <param name="subject">Reference (AT-URI) to the mod list record.</param>
        /// <param name="createdAt"></param>
        public Listblock(FishyFlip.Models.ATUri? subject, DateTime? createdAt = default)
        {
            this.Subject = subject;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "app.bsky.graph.listblock";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Listblock"/> class.
        /// </summary>
        public Listblock()
        {
            this.Type = "app.bsky.graph.listblock";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Listblock"/> class.
        /// </summary>
        public Listblock(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATUri();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> Reference (AT-URI) to the mod list record.
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Subject { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "app.bsky.graph.listblock";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.Listblock>)SourceGenerationContext.Default.AppBskyGraphListblock);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.Listblock>)SourceGenerationContext.Default.AppBskyGraphListblock);
        }

        public static new Listblock FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.Listblock>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.Listblock>)SourceGenerationContext.Default.AppBskyGraphListblock)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Listblock FromCBORObject(CBORObject obj)
        {
            return new Listblock(obj);
        }

        /// <inheritdoc/>
        public static Listblock Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Listblock>(s, (JsonTypeInfo<Listblock>)SourceGenerationContext.Default.AppBskyGraphListblock)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Listblock result)
        {
            result = JsonSerializer.Deserialize<Listblock>(s, (JsonTypeInfo<Listblock>)SourceGenerationContext.Default.AppBskyGraphListblock);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class TrendingTopic : ATObject, ICBOREncodable<TrendingTopic>, IJsonEncodable<TrendingTopic>, IParsable<TrendingTopic>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TrendingTopic"/> class.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="link"></param>
        public TrendingTopic(string topic = default, string? displayName = default, string? description = default, string link = default)
        {
            this.Topic = topic;
            this.DisplayName = displayName;
            this.Description = description;
            this.Link = link;
            this.Type = "app.bsky.unspecced.defs#trendingTopic";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TrendingTopic"/> class.
        /// </summary>
        public TrendingTopic()
        {
            this.Type = "app.bsky.unspecced.defs#trendingTopic";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TrendingTopic"/> class.
        /// </summary>
        public TrendingTopic(CBORObject obj)
        {
            if (obj["topic"] is not null) this.Topic = obj["topic"].AsString();
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["link"] is not null) this.Link = obj["link"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        [JsonPropertyName("topic")]
        [JsonRequired]
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the displayName.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        [JsonPropertyName("link")]
        [JsonRequired]
        public string Link { get; set; }

        public const string RecordType = "app.bsky.unspecced.defs#trendingTopic";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic>)SourceGenerationContext.Default.AppBskyUnspeccedTrendingTopic);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic>)SourceGenerationContext.Default.AppBskyUnspeccedTrendingTopic);
        }

        public static new TrendingTopic FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic>)SourceGenerationContext.Default.AppBskyUnspeccedTrendingTopic)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new TrendingTopic FromCBORObject(CBORObject obj)
        {
            return new TrendingTopic(obj);
        }

        /// <inheritdoc/>
        public static TrendingTopic Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<TrendingTopic>(s, (JsonTypeInfo<TrendingTopic>)SourceGenerationContext.Default.AppBskyUnspeccedTrendingTopic)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out TrendingTopic result)
        {
            result = JsonSerializer.Deserialize<TrendingTopic>(s, (JsonTypeInfo<TrendingTopic>)SourceGenerationContext.Default.AppBskyUnspeccedTrendingTopic);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class SkeletonTrend : ATObject, ICBOREncodable<SkeletonTrend>, IJsonEncodable<SkeletonTrend>, IParsable<SkeletonTrend>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonTrend"/> class.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="displayName"></param>
        /// <param name="link"></param>
        /// <param name="startedAt"></param>
        /// <param name="postCount"></param>
        /// <param name="status">
        /// <br/> Known Values: <br/>
        /// hot <br/>
        /// </param>
        /// <param name="category"></param>
        /// <param name="dids"></param>
        public SkeletonTrend(string topic = default, string displayName = default, string link = default, DateTime? startedAt = default, long postCount = default, string? status = default, string? category = default, List<FishyFlip.Models.ATDid> dids = default)
        {
            this.Topic = topic;
            this.DisplayName = displayName;
            this.Link = link;
            this.StartedAt = startedAt;
            this.PostCount = postCount;
            this.Status = status;
            this.Category = category;
            this.Dids = dids;
            this.Type = "app.bsky.unspecced.defs#skeletonTrend";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonTrend"/> class.
        /// </summary>
        public SkeletonTrend()
        {
            this.Type = "app.bsky.unspecced.defs#skeletonTrend";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonTrend"/> class.
        /// </summary>
        public SkeletonTrend(CBORObject obj)
        {
            if (obj["topic"] is not null) this.Topic = obj["topic"].AsString();
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["link"] is not null) this.Link = obj["link"].AsString();
            if (obj["startedAt"] is not null) this.StartedAt = obj["startedAt"].ToDateTime();
            if (obj["postCount"] is not null) this.PostCount = obj["postCount"].AsInt64Value();
            if (obj["status"] is not null) this.Status = obj["status"].AsString();
            if (obj["category"] is not null) this.Category = obj["category"].AsString();
            if (obj["dids"] is not null) this.Dids = obj["dids"].Values.Select(n =>n.ToATDid()!).ToList();
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
        [JsonRequired]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        [JsonPropertyName("link")]
        [JsonRequired]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the startedAt.
        /// </summary>
        [JsonPropertyName("startedAt")]
        [JsonRequired]
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Gets or sets the postCount.
        /// </summary>
        [JsonPropertyName("postCount")]
        [JsonRequired]
        public long PostCount { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// <br/> Known Values: <br/>
        /// hot <br/>
        /// </summary>
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [JsonPropertyName("category")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the dids.
        /// </summary>
        [JsonPropertyName("dids")]
        [JsonRequired]
        public List<FishyFlip.Models.ATDid> Dids { get; set; }

        public const string RecordType = "app.bsky.unspecced.defs#skeletonTrend";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.SkeletonTrend>)SourceGenerationContext.Default.AppBskyUnspeccedSkeletonTrend);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.SkeletonTrend>)SourceGenerationContext.Default.AppBskyUnspeccedSkeletonTrend);
        }

        public static new SkeletonTrend FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.SkeletonTrend>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.SkeletonTrend>)SourceGenerationContext.Default.AppBskyUnspeccedSkeletonTrend)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new SkeletonTrend FromCBORObject(CBORObject obj)
        {
            return new SkeletonTrend(obj);
        }

        /// <inheritdoc/>
        public static SkeletonTrend Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<SkeletonTrend>(s, (JsonTypeInfo<SkeletonTrend>)SourceGenerationContext.Default.AppBskyUnspeccedSkeletonTrend)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out SkeletonTrend result)
        {
            result = JsonSerializer.Deserialize<SkeletonTrend>(s, (JsonTypeInfo<SkeletonTrend>)SourceGenerationContext.Default.AppBskyUnspeccedSkeletonTrend);
            return result != null;
        }
    }
}


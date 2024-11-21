// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Record defining interaction gating rules for a thread (aka, reply controls). The record key (rkey) of the threadgate record must match the record key of the thread's root post, and that record must be in the same repository.
    /// </summary>
    public partial class Threadgate : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Threadgate"/> class.
        /// </summary>
        public Threadgate(FishyFlip.Models.ATUri? post, DateTime? createdAt, List<ATObject>? allow = default, List<FishyFlip.Models.ATUri>? hiddenReplies = default)
        {
            this.Post = post;
            this.Allow = allow;
            this.CreatedAt = createdAt;
            this.HiddenReplies = hiddenReplies;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Threadgate"/> class.
        /// </summary>
        public Threadgate()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Threadgate"/> class.
        /// </summary>
        public Threadgate(CBORObject obj)
        {
            if (obj["post"] is not null) this.Post = obj["post"].ToATUri();
            if (obj["allow"] is not null) this.Allow = obj["allow"].Values.Select(n =>n.ToATObject()).ToList();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["hiddenReplies"] is not null) this.HiddenReplies = obj["hiddenReplies"].Values.Select(n =>n.ToATUri()!).ToList();
        }

        /// <summary>
        /// Reference (AT-URI) to the post record.
        /// </summary>
        [JsonPropertyName("post")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Post { get; set; }

        [JsonPropertyName("allow")]
        public List<ATObject>? Allow { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// List of hidden reply URIs.
        /// </summary>
        [JsonPropertyName("hiddenReplies")]
        public List<FishyFlip.Models.ATUri>? HiddenReplies { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.threadgate";

        public const string RecordType = "app.bsky.feed.threadgate";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.Threadgate>(this, (JsonTypeInfo<App.Bsky.Feed.Threadgate>)SourceGenerationContext.Default.AppBskyFeedThreadgate)!;
        }

        public static Threadgate FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.Threadgate>(json, (JsonTypeInfo<App.Bsky.Feed.Threadgate>)SourceGenerationContext.Default.AppBskyFeedThreadgate)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Metadata about the requesting account's relationship with the subject content. Only has meaningful content for authed requests.
    /// </summary>
    public partial class ViewerState : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerState"/> class.
        /// </summary>
        /// <param name="repost"></param>
        /// <param name="like"></param>
        /// <param name="threadMuted"></param>
        /// <param name="replyDisabled"></param>
        /// <param name="embeddingDisabled"></param>
        /// <param name="pinned"></param>
        public ViewerState(FishyFlip.Models.ATUri? repost = default, FishyFlip.Models.ATUri? like = default, bool? threadMuted = default, bool? replyDisabled = default, bool? embeddingDisabled = default, bool? pinned = default)
        {
            this.Repost = repost;
            this.Like = like;
            this.ThreadMuted = threadMuted;
            this.ReplyDisabled = replyDisabled;
            this.EmbeddingDisabled = embeddingDisabled;
            this.Pinned = pinned;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerState"/> class.
        /// </summary>
        public ViewerState()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerState"/> class.
        /// </summary>
        public ViewerState(CBORObject obj)
        {
            if (obj["repost"] is not null) this.Repost = obj["repost"].ToATUri();
            if (obj["like"] is not null) this.Like = obj["like"].ToATUri();
            if (obj["threadMuted"] is not null) this.ThreadMuted = obj["threadMuted"].AsBoolean();
            if (obj["replyDisabled"] is not null) this.ReplyDisabled = obj["replyDisabled"].AsBoolean();
            if (obj["embeddingDisabled"] is not null) this.EmbeddingDisabled = obj["embeddingDisabled"].AsBoolean();
            if (obj["pinned"] is not null) this.Pinned = obj["pinned"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the repost.
        /// </summary>
        [JsonPropertyName("repost")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Repost { get; set; }

        /// <summary>
        /// Gets or sets the like.
        /// </summary>
        [JsonPropertyName("like")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Like { get; set; }

        /// <summary>
        /// Gets or sets the threadMuted.
        /// </summary>
        [JsonPropertyName("threadMuted")]
        public bool? ThreadMuted { get; set; }

        /// <summary>
        /// Gets or sets the replyDisabled.
        /// </summary>
        [JsonPropertyName("replyDisabled")]
        public bool? ReplyDisabled { get; set; }

        /// <summary>
        /// Gets or sets the embeddingDisabled.
        /// </summary>
        [JsonPropertyName("embeddingDisabled")]
        public bool? EmbeddingDisabled { get; set; }

        /// <summary>
        /// Gets or sets the pinned.
        /// </summary>
        [JsonPropertyName("pinned")]
        public bool? Pinned { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.defs#viewerState";

        public const string RecordType = "app.bsky.feed.defs#viewerState";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Feed.ViewerState>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.ViewerState>)SourceGenerationContext.Default.AppBskyFeedViewerState)!;
        }

        public static ViewerState FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.ViewerState>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.ViewerState>)SourceGenerationContext.Default.AppBskyFeedViewerState)!;
        }
    }
}


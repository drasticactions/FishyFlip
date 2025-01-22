// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class FeedViewPref : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedViewPref"/> class.
        /// </summary>
        /// <param name="feed">The URI of the feed, or an identifier which describes the feed.</param>
        /// <param name="hideReplies">Hide replies in the feed.</param>
        /// <param name="hideRepliesByUnfollowed">Hide replies in the feed if they are not by followed users.</param>
        /// <param name="hideRepliesByLikeCount">Hide replies in the feed if they do not have this number of likes.</param>
        /// <param name="hideReposts">Hide reposts in the feed.</param>
        /// <param name="hideQuotePosts">Hide quote posts in the feed.</param>
        public FeedViewPref(string feed = default, bool? hideReplies = default, bool? hideRepliesByUnfollowed = default, long? hideRepliesByLikeCount = default, bool? hideReposts = default, bool? hideQuotePosts = default)
        {
            this.Feed = feed;
            this.HideReplies = hideReplies;
            this.HideRepliesByUnfollowed = hideRepliesByUnfollowed;
            this.HideRepliesByLikeCount = hideRepliesByLikeCount;
            this.HideReposts = hideReposts;
            this.HideQuotePosts = hideQuotePosts;
            this.Type = "app.bsky.actor.defs#feedViewPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeedViewPref"/> class.
        /// </summary>
        public FeedViewPref()
        {
            this.Type = "app.bsky.actor.defs#feedViewPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeedViewPref"/> class.
        /// </summary>
        public FeedViewPref(CBORObject obj)
        {
            if (obj["feed"] is not null) this.Feed = obj["feed"].AsString();
            if (obj["hideReplies"] is not null) this.HideReplies = obj["hideReplies"].AsBoolean();
            if (obj["hideRepliesByUnfollowed"] is not null) this.HideRepliesByUnfollowed = obj["hideRepliesByUnfollowed"].AsBoolean();
            if (obj["hideRepliesByLikeCount"] is not null) this.HideRepliesByLikeCount = obj["hideRepliesByLikeCount"].AsInt64Value();
            if (obj["hideReposts"] is not null) this.HideReposts = obj["hideReposts"].AsBoolean();
            if (obj["hideQuotePosts"] is not null) this.HideQuotePosts = obj["hideQuotePosts"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the feed.
        /// <br/> The URI of the feed, or an identifier which describes the feed.
        /// </summary>
        [JsonPropertyName("feed")]
        [JsonRequired]
        public string Feed { get; set; }

        /// <summary>
        /// Gets or sets the hideReplies.
        /// <br/> Hide replies in the feed.
        /// </summary>
        [JsonPropertyName("hideReplies")]
        public bool? HideReplies { get; set; }

        /// <summary>
        /// Gets or sets the hideRepliesByUnfollowed.
        /// <br/> Hide replies in the feed if they are not by followed users.
        /// </summary>
        [JsonPropertyName("hideRepliesByUnfollowed")]
        public bool? HideRepliesByUnfollowed { get; set; } = true;

        /// <summary>
        /// Gets or sets the hideRepliesByLikeCount.
        /// <br/> Hide replies in the feed if they do not have this number of likes.
        /// </summary>
        [JsonPropertyName("hideRepliesByLikeCount")]
        public long? HideRepliesByLikeCount { get; set; }

        /// <summary>
        /// Gets or sets the hideReposts.
        /// <br/> Hide reposts in the feed.
        /// </summary>
        [JsonPropertyName("hideReposts")]
        public bool? HideReposts { get; set; }

        /// <summary>
        /// Gets or sets the hideQuotePosts.
        /// <br/> Hide quote posts in the feed.
        /// </summary>
        [JsonPropertyName("hideQuotePosts")]
        public bool? HideQuotePosts { get; set; }

        public const string RecordType = "app.bsky.actor.defs#feedViewPref";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Actor.FeedViewPref>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.FeedViewPref>)SourceGenerationContext.Default.AppBskyActorFeedViewPref)!;
        }

        public static FeedViewPref FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.FeedViewPref>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.FeedViewPref>)SourceGenerationContext.Default.AppBskyActorFeedViewPref)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class ReasonRepost : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonRepost"/> class.
        /// </summary>
        /// <param name="by">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic"/> (app.bsky.actor.defs#profileViewBasic)
        /// </param>
        /// <param name="indexedAt"></param>
        public ReasonRepost(App.Bsky.Actor.ProfileViewBasic? by = default, DateTime? indexedAt = default)
        {
            this.By = by;
            this.IndexedAt = indexedAt;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonRepost"/> class.
        /// </summary>
        public ReasonRepost()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonRepost"/> class.
        /// </summary>
        public ReasonRepost(CBORObject obj)
        {
            if (obj["by"] is not null) this.By = new App.Bsky.Actor.ProfileViewBasic(obj["by"]);
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the by.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic"/> (app.bsky.actor.defs#profileViewBasic)
        /// </summary>
        [JsonPropertyName("by")]
        [JsonRequired]
        public App.Bsky.Actor.ProfileViewBasic? By { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.defs#reasonRepost";

        public const string RecordType = "app.bsky.feed.defs#reasonRepost";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.ReasonRepost>(this, (JsonTypeInfo<App.Bsky.Feed.ReasonRepost>)SourceGenerationContext.Default.AppBskyFeedReasonRepost)!;
        }

        public static ReasonRepost FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.ReasonRepost>(json, (JsonTypeInfo<App.Bsky.Feed.ReasonRepost>)SourceGenerationContext.Default.AppBskyFeedReasonRepost)!;
        }
    }
}


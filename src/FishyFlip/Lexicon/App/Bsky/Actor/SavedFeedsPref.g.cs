// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class SavedFeedsPref : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeedsPref"/> class.
        /// </summary>
        /// <param name="pinned"></param>
        /// <param name="saved"></param>
        /// <param name="timelineIndex"></param>
        public SavedFeedsPref(List<FishyFlip.Models.ATUri> pinned = default, List<FishyFlip.Models.ATUri> saved = default, long? timelineIndex = default)
        {
            this.Pinned = pinned;
            this.Saved = saved;
            this.TimelineIndex = timelineIndex;
            this.Type = "app.bsky.actor.defs#savedFeedsPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeedsPref"/> class.
        /// </summary>
        public SavedFeedsPref()
        {
            this.Type = "app.bsky.actor.defs#savedFeedsPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeedsPref"/> class.
        /// </summary>
        public SavedFeedsPref(CBORObject obj)
        {
            if (obj["pinned"] is not null) this.Pinned = obj["pinned"].Values.Select(n =>n.ToATUri()!).ToList();
            if (obj["saved"] is not null) this.Saved = obj["saved"].Values.Select(n =>n.ToATUri()!).ToList();
            if (obj["timelineIndex"] is not null) this.TimelineIndex = obj["timelineIndex"].AsInt64Value();
        }

        /// <summary>
        /// Gets or sets the pinned.
        /// </summary>
        [JsonPropertyName("pinned")]
        [JsonRequired]
        public List<FishyFlip.Models.ATUri> Pinned { get; set; }

        /// <summary>
        /// Gets or sets the saved.
        /// </summary>
        [JsonPropertyName("saved")]
        [JsonRequired]
        public List<FishyFlip.Models.ATUri> Saved { get; set; }

        /// <summary>
        /// Gets or sets the timelineIndex.
        /// </summary>
        [JsonPropertyName("timelineIndex")]
        public long? TimelineIndex { get; set; }

        public const string RecordType = "app.bsky.actor.defs#savedFeedsPref";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPref>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPref>)SourceGenerationContext.Default.AppBskyActorSavedFeedsPref)!;
        }

        public static SavedFeedsPref FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPref>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPref>)SourceGenerationContext.Default.AppBskyActorSavedFeedsPref)!;
        }
    }
}


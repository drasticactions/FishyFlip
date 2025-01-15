// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class SavedFeedsPrefV2 : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeedsPrefV2"/> class.
        /// </summary>
        /// <param name="items"></param>
        public SavedFeedsPrefV2(List<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeed>? items = default)
        {
            this.Items = items;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeedsPrefV2"/> class.
        /// </summary>
        public SavedFeedsPrefV2()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeedsPrefV2"/> class.
        /// </summary>
        public SavedFeedsPrefV2(CBORObject obj)
        {
            if (obj["items"] is not null) this.Items = obj["items"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Actor.SavedFeed(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeed>? Items { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.actor.defs#savedFeedsPrefV2";

        public const string RecordType = "app.bsky.actor.defs#savedFeedsPrefV2";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPrefV2>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPrefV2>)SourceGenerationContext.Default.AppBskyActorSavedFeedsPrefV2)!;
        }

        public static SavedFeedsPrefV2 FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPrefV2>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPrefV2>)SourceGenerationContext.Default.AppBskyActorSavedFeedsPrefV2)!;
        }
    }
}


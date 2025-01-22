// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class SavedFeed : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeed"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">
        /// <br/> Known Values: <br/>
        /// feed <br/>
        /// list <br/>
        /// timeline <br/>
        /// </param>
        /// <param name="value"></param>
        /// <param name="pinned"></param>
        public SavedFeed(string id = default, string type = default, string value = default, bool pinned = default)
        {
            this.Id = id;
            this.TypeValue = type;
            this.Value = value;
            this.Pinned = pinned;
            this.Type = "app.bsky.actor.defs#savedFeed";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeed"/> class.
        /// </summary>
        public SavedFeed()
        {
            this.Type = "app.bsky.actor.defs#savedFeed";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SavedFeed"/> class.
        /// </summary>
        public SavedFeed(CBORObject obj)
        {
            if (obj["id"] is not null) this.Id = obj["id"].AsString();
            if (obj["type"] is not null) this.TypeValue = obj["type"].AsString();
            if (obj["value"] is not null) this.Value = obj["value"].AsString();
            if (obj["pinned"] is not null) this.Pinned = obj["pinned"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonRequired]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// <br/> Known Values: <br/>
        /// feed <br/>
        /// list <br/>
        /// timeline <br/>
        /// </summary>
        [JsonPropertyName("type")]
        [JsonRequired]
        public string TypeValue { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the pinned.
        /// </summary>
        [JsonPropertyName("pinned")]
        [JsonRequired]
        public bool Pinned { get; set; }

        public const string RecordType = "app.bsky.actor.defs#savedFeed";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeed>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeed>)SourceGenerationContext.Default.AppBskyActorSavedFeed)!;
        }

        public static SavedFeed FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeed>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SavedFeed>)SourceGenerationContext.Default.AppBskyActorSavedFeed)!;
        }
    }
}


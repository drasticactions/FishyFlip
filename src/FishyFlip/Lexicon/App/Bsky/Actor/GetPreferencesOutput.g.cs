// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class GetPreferencesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPreferencesOutput"/> class.
        /// </summary>
        /// <param name="preferences">
        /// app.bsky.actor.defs#preferences <br/>
        /// </param>
        public GetPreferencesOutput(List<ATObject>? preferences = default)
        {
            this.Preferences = preferences;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPreferencesOutput"/> class.
        /// </summary>
        public GetPreferencesOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPreferencesOutput"/> class.
        /// </summary>
        public GetPreferencesOutput(CBORObject obj)
        {
            if (obj["preferences"] is not null) this.Preferences = obj["preferences"].Values.Select(n =>n.ToATObject()).ToList();
        }

        /// <summary>
        /// Gets or sets the preferences.
        /// app.bsky.actor.defs#preferences <br/>
        /// </summary>
        [JsonPropertyName("preferences")]
        [JsonRequired]
        public List<ATObject>? Preferences { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.actor.getPreferences#GetPreferencesOutput";

        public const string RecordType = "app.bsky.actor.getPreferences#GetPreferencesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>)SourceGenerationContext.Default.AppBskyActorGetPreferencesOutput)!;
        }

        public static GetPreferencesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>)SourceGenerationContext.Default.AppBskyActorGetPreferencesOutput)!;
        }
    }
}


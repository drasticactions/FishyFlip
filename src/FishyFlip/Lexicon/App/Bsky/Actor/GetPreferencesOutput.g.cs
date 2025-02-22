// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class GetPreferencesOutput : ATObject, ICBOREncodable<GetPreferencesOutput>, IJsonEncodable<GetPreferencesOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPreferencesOutput"/> class.
        /// </summary>
        /// <param name="preferences">
        /// app.bsky.actor.defs#preferences <br/>
        /// </param>
        public GetPreferencesOutput(List<ATObject> preferences = default)
        {
            this.Preferences = preferences;
            this.Type = "app.bsky.actor.getPreferences#GetPreferencesOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPreferencesOutput"/> class.
        /// </summary>
        public GetPreferencesOutput()
        {
            this.Type = "app.bsky.actor.getPreferences#GetPreferencesOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPreferencesOutput"/> class.
        /// </summary>
        public GetPreferencesOutput(CBORObject obj)
        {
            if (obj["preferences"] is not null) this.Preferences = obj["preferences"].Values.Select(n =>n.ToATObject()).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the preferences.
        /// app.bsky.actor.defs#preferences <br/>
        /// </summary>
        [JsonPropertyName("preferences")]
        [JsonRequired]
        public List<ATObject> Preferences { get; set; }

        public const string RecordType = "app.bsky.actor.getPreferences#GetPreferencesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>)SourceGenerationContext.Default.AppBskyActorGetPreferencesOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>)SourceGenerationContext.Default.AppBskyActorGetPreferencesOutput);
        }

        public static new GetPreferencesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>)SourceGenerationContext.Default.AppBskyActorGetPreferencesOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetPreferencesOutput FromCBORObject(CBORObject obj)
        {
            return new GetPreferencesOutput(obj);
        }

    }
}


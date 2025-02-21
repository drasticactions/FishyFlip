// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class PutPreferencesInput : ATObject, ICBOREncodable<PutPreferencesInput>, IJsonEncodable<PutPreferencesInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PutPreferencesInput"/> class.
        /// </summary>
        /// <param name="preferences">
        /// app.bsky.actor.defs#preferences <br/>
        /// </param>
        public PutPreferencesInput(List<ATObject> preferences = default)
        {
            this.Preferences = preferences;
            this.Type = "app.bsky.actor.putPreferences#PutPreferencesInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PutPreferencesInput"/> class.
        /// </summary>
        public PutPreferencesInput()
        {
            this.Type = "app.bsky.actor.putPreferences#PutPreferencesInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PutPreferencesInput"/> class.
        /// </summary>
        public PutPreferencesInput(CBORObject obj)
        {
            if (obj["preferences"] is not null) this.Preferences = obj["preferences"].Values.Select(n =>n.ToATObject()).ToList();
        }

        /// <summary>
        /// Gets or sets the preferences.
        /// app.bsky.actor.defs#preferences <br/>
        /// </summary>
        [JsonPropertyName("preferences")]
        [JsonRequired]
        public List<ATObject> Preferences { get; set; }

        public const string RecordType = "app.bsky.actor.putPreferences#PutPreferencesInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.PutPreferencesInput>)SourceGenerationContext.Default.AppBskyActorPutPreferencesInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.PutPreferencesInput>)SourceGenerationContext.Default.AppBskyActorPutPreferencesInput);
        }

        public static new PutPreferencesInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.PutPreferencesInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.PutPreferencesInput>)SourceGenerationContext.Default.AppBskyActorPutPreferencesInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new PutPreferencesInput FromCBORObject(CBORObject obj)
        {
            return new PutPreferencesInput(obj);
        }

    }
}


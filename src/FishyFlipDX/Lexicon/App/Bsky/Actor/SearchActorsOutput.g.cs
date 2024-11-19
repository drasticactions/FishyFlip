// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class SearchActorsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchActorsOutput"/> class.
        /// </summary>
        public SearchActorsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchActorsOutput"/> class.
        /// </summary>
        public SearchActorsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["actors"] is not null) this.Actors = obj["actors"].Values.Select(n => n is not null ? new App.Bsky.Actor.ProfileView(n) : null).ToList();
        }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        [JsonPropertyName("actors")]
        [JsonRequired]
        public List<App.Bsky.Actor.ProfileView?>? Actors { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.actor.searchActors#SearchActorsOutput";

        public const string RecordType = "app.bsky.actor.searchActors#SearchActorsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Actor.SearchActorsOutput>(this, (JsonTypeInfo<App.Bsky.Actor.SearchActorsOutput>)SourceGenerationContext.Default.AppBskyActorSearchActorsOutput)!;
        }

        public static SearchActorsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Actor.SearchActorsOutput>(json, (JsonTypeInfo<App.Bsky.Actor.SearchActorsOutput>)SourceGenerationContext.Default.AppBskyActorSearchActorsOutput)!;
        }
    }
}


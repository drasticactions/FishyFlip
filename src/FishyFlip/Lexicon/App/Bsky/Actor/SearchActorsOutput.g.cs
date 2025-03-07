// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class SearchActorsOutput : ATObject, ICBOREncodable<SearchActorsOutput>, IJsonEncodable<SearchActorsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchActorsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="actors"></param>
        public SearchActorsOutput(string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> actors = default)
        {
            this.Cursor = cursor;
            this.Actors = actors;
            this.Type = "app.bsky.actor.searchActors#SearchActorsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchActorsOutput"/> class.
        /// </summary>
        public SearchActorsOutput()
        {
            this.Type = "app.bsky.actor.searchActors#SearchActorsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchActorsOutput"/> class.
        /// </summary>
        public SearchActorsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["actors"] is not null) this.Actors = obj["actors"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the actors.
        /// </summary>
        [JsonPropertyName("actors")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> Actors { get; set; }

        public const string RecordType = "app.bsky.actor.searchActors#SearchActorsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsOutput>)SourceGenerationContext.Default.AppBskyActorSearchActorsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsOutput>)SourceGenerationContext.Default.AppBskyActorSearchActorsOutput);
        }

        public static new SearchActorsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsOutput>)SourceGenerationContext.Default.AppBskyActorSearchActorsOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new SearchActorsOutput FromCBORObject(CBORObject obj)
        {
            return new SearchActorsOutput(obj);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Fm.Teal.Alpha.Actor
{
    public partial class FeaturedItem : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedItem"/> class.
        /// </summary>
        /// <param name="mbid">The Musicbrainz ID of the item</param>
        /// <param name="type">The type of the item. Must be a valid Musicbrainz type, e.g. album, track, recording, etc.</param>
        public FeaturedItem(string mbid = default, string type = default)
        {
            this.Mbid = mbid;
            this.TypeValue = type;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedItem"/> class.
        /// </summary>
        public FeaturedItem()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedItem"/> class.
        /// </summary>
        public FeaturedItem(CBORObject obj)
        {
            if (obj["mbid"] is not null) this.Mbid = obj["mbid"].AsString();
            if (obj["type"] is not null) this.TypeValue = obj["type"].AsString();
        }

        /// <summary>
        /// Gets or sets the mbid.
        /// <br/> The Musicbrainz ID of the item
        /// </summary>
        [JsonPropertyName("mbid")]
        [JsonRequired]
        public string Mbid { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// <br/> The type of the item. Must be a valid Musicbrainz type, e.g. album, track, recording, etc.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonRequired]
        public string TypeValue { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "fm.teal.alpha.actor.profile#featuredItem";

        public const string RecordType = "fm.teal.alpha.actor.profile#featuredItem";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem>(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem>)SourceGenerationContext.Default.FmTealAlphaActorFeaturedItem)!;
        }

        public static FeaturedItem FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem>(json, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem>)SourceGenerationContext.Default.FmTealAlphaActorFeaturedItem)!;
        }
    }
}


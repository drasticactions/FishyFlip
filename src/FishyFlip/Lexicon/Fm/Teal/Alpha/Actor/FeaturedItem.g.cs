// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Fm.Teal.Alpha.Actor
{
    public partial class FeaturedItem : ATObject, ICBOREncodable<FeaturedItem>, IJsonEncodable<FeaturedItem>
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
            this.Type = "fm.teal.alpha.actor.profile#featuredItem";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedItem"/> class.
        /// </summary>
        public FeaturedItem()
        {
            this.Type = "fm.teal.alpha.actor.profile#featuredItem";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedItem"/> class.
        /// </summary>
        public FeaturedItem(CBORObject obj)
        {
            if (obj["mbid"] is not null) this.Mbid = obj["mbid"].AsString();
            if (obj["type"] is not null) this.TypeValue = obj["type"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
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

        public const string RecordType = "fm.teal.alpha.actor.profile#featuredItem";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem>)SourceGenerationContext.Default.FmTealAlphaActorFeaturedItem);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem>)SourceGenerationContext.Default.FmTealAlphaActorFeaturedItem);
        }

        public static new FeaturedItem FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem>(json, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem>)SourceGenerationContext.Default.FmTealAlphaActorFeaturedItem)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new FeaturedItem FromCBORObject(CBORObject obj)
        {
            return new FeaturedItem(obj);
        }

    }
}


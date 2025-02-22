// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Social.Psky.Richtext
{
    /// <summary>
    /// Annotation of a sub-string within rich text.
    /// </summary>
    public partial class Facet : ATObject, ICBOREncodable<Facet>, IJsonEncodable<Facet>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        /// <param name="index">
        /// social.psky.richtext.defs#byteSlice <br/>
        /// </param>
        /// <param name="features">
        /// <br/> Union Types: <br/>
        /// #mention <br/>
        /// #link <br/>
        /// #room <br/>
        /// </param>
        public Facet(FishyFlip.Lexicon.Social.Psky.Richtext.ByteSlice index = default, List<ATObject> features = default)
        {
            this.Index = index;
            this.Features = features;
            this.Type = "social.psky.richtext.facet";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet()
        {
            this.Type = "social.psky.richtext.facet";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet(CBORObject obj)
        {
            if (obj["index"] is not null) this.Index = new FishyFlip.Lexicon.Social.Psky.Richtext.ByteSlice(obj["index"]);
            if (obj["features"] is not null) this.Features = obj["features"].Values.Select(n =>n.ToATObject()).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the index.
        /// social.psky.richtext.defs#byteSlice <br/>
        /// </summary>
        [JsonPropertyName("index")]
        [JsonRequired]
        public FishyFlip.Lexicon.Social.Psky.Richtext.ByteSlice Index { get; set; }

        /// <summary>
        /// Gets or sets the features.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Social.Psky.Richtext.Mention"/> (social.psky.richtext.facet#mention) <br/>
        /// <see cref="FishyFlip.Lexicon.Social.Psky.Richtext.Link"/> (social.psky.richtext.facet#link) <br/>
        /// <see cref="FishyFlip.Lexicon.Social.Psky.Richtext.Room"/> (social.psky.richtext.facet#room) <br/>
        /// </summary>
        [JsonPropertyName("features")]
        [JsonRequired]
        public List<ATObject> Features { get; set; }

        public const string RecordType = "social.psky.richtext.facet";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Richtext.Facet>)SourceGenerationContext.Default.SocialPskyRichtextFacet);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Richtext.Facet>)SourceGenerationContext.Default.SocialPskyRichtextFacet);
        }

        public static new Facet FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Social.Psky.Richtext.Facet>(json, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Richtext.Facet>)SourceGenerationContext.Default.SocialPskyRichtextFacet)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Facet FromCBORObject(CBORObject obj)
        {
            return new Facet(obj);
        }

    }
}


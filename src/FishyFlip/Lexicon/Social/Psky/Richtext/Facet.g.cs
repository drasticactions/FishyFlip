// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Social.Psky.Richtext
{
    /// <summary>
    /// Annotation of a sub-string within rich text.
    /// </summary>
    public partial class Facet : ATObject
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
        public Facet(Social.Psky.Richtext.ByteSlice? index = default, List<ATObject>? features = default)
        {
            this.Index = index;
            this.Features = features;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet(CBORObject obj)
        {
            if (obj["index"] is not null) this.Index = new Social.Psky.Richtext.ByteSlice(obj["index"]);
            if (obj["features"] is not null) this.Features = obj["features"].Values.Select(n =>n.ToATObject()).ToList();
        }

        /// <summary>
        /// Gets or sets the index.
        /// social.psky.richtext.defs#byteSlice <br/>
        /// </summary>
        [JsonPropertyName("index")]
        [JsonRequired]
        public Social.Psky.Richtext.ByteSlice? Index { get; set; }

        /// <summary>
        /// Gets or sets the features.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Social.Psky.Richtext.Mention"/> (social.psky.richtext.facet#mention) <br/>
        /// <see cref="FishyFlip.Lexicon.Social.Psky.Richtext.Link"/> (social.psky.richtext.facet#link) <br/>
        /// <see cref="FishyFlip.Lexicon.Social.Psky.Richtext.Room"/> (social.psky.richtext.facet#room) <br/>
        /// </summary>
        [JsonPropertyName("features")]
        [JsonRequired]
        public List<ATObject>? Features { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "social.psky.richtext.facet";

        public const string RecordType = "social.psky.richtext.facet";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Social.Psky.Richtext.Facet>(this, (JsonTypeInfo<Social.Psky.Richtext.Facet>)SourceGenerationContext.Default.SocialPskyRichtextFacet)!;
        }

        public static Facet FromJson(string json)
        {
            return JsonSerializer.Deserialize<Social.Psky.Richtext.Facet>(json, (JsonTypeInfo<Social.Psky.Richtext.Facet>)SourceGenerationContext.Default.SocialPskyRichtextFacet)!;
        }
    }
}


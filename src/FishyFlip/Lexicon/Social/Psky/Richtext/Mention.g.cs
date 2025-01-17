// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Social.Psky.Richtext
{
    /// <summary>
    /// Facet feature for mention of another account. The text is usually a handle, including a '@' prefix, but the facet reference is a DID.
    /// </summary>
    public partial class Mention : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Mention"/> class.
        /// </summary>
        /// <param name="did"></param>
        public Mention(FishyFlip.Models.ATDid did = default)
        {
            this.Did = did;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Mention"/> class.
        /// </summary>
        public Mention()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Mention"/> class.
        /// </summary>
        public Mention(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "social.psky.richtext.facet#mention";

        public const string RecordType = "social.psky.richtext.facet#mention";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Social.Psky.Richtext.Mention>(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Richtext.Mention>)SourceGenerationContext.Default.SocialPskyRichtextMention)!;
        }

        public static Mention FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Social.Psky.Richtext.Mention>(json, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Richtext.Mention>)SourceGenerationContext.Default.SocialPskyRichtextMention)!;
        }
    }
}


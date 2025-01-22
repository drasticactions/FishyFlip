// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Richtext
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
            this.Type = "app.bsky.richtext.facet#mention";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Mention"/> class.
        /// </summary>
        public Mention()
        {
            this.Type = "app.bsky.richtext.facet#mention";
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

        public const string RecordType = "app.bsky.richtext.facet#mention";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Richtext.Mention>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Richtext.Mention>)SourceGenerationContext.Default.AppBskyRichtextMention)!;
        }

        public static Mention FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Richtext.Mention>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Richtext.Mention>)SourceGenerationContext.Default.AppBskyRichtextMention)!;
        }
    }
}


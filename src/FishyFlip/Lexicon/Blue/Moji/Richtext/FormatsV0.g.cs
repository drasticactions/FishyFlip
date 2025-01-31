// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Richtext
{
    /// <summary>
    /// On the facet, only the CID is provided as this can be combined with the DID to create CDN URLs for non-animated blobs. For APNG and dotLottie, raw Bytes are served and require a com.atproto.repo.getRecord roundtrip on render so are marked with a boolean
    /// </summary>
    public partial class FormatsV0 : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatsV0"/> class.
        /// </summary>
        /// <param name="png_128"></param>
        /// <param name="webp_128"></param>
        /// <param name="gif_128"></param>
        /// <param name="apng_128"></param>
        /// <param name="lottie"></param>
        public FormatsV0(string? png_128 = default, string? webp_128 = default, string? gif_128 = default, bool? apng_128 = default, bool? lottie = default)
        {
            this.Png128 = png_128;
            this.Webp128 = webp_128;
            this.Gif128 = gif_128;
            this.Apng128 = apng_128;
            this.Lottie = lottie;
            this.Type = "blue.moji.richtext.facet#formats_v0";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FormatsV0"/> class.
        /// </summary>
        public FormatsV0()
        {
            this.Type = "blue.moji.richtext.facet#formats_v0";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FormatsV0"/> class.
        /// </summary>
        public FormatsV0(CBORObject obj)
        {
            if (obj["png_128"] is not null) this.Png128 = obj["png_128"].AsString();
            if (obj["webp_128"] is not null) this.Webp128 = obj["webp_128"].AsString();
            if (obj["gif_128"] is not null) this.Gif128 = obj["gif_128"].AsString();
            if (obj["apng_128"] is not null) this.Apng128 = obj["apng_128"].AsBoolean();
            if (obj["lottie"] is not null) this.Lottie = obj["lottie"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the png_128.
        /// </summary>
        [JsonPropertyName("png_128")]
        public string? Png128 { get; set; }

        /// <summary>
        /// Gets or sets the webp_128.
        /// </summary>
        [JsonPropertyName("webp_128")]
        public string? Webp128 { get; set; }

        /// <summary>
        /// Gets or sets the gif_128.
        /// </summary>
        [JsonPropertyName("gif_128")]
        public string? Gif128 { get; set; }

        /// <summary>
        /// Gets or sets the apng_128.
        /// </summary>
        [JsonPropertyName("apng_128")]
        public bool? Apng128 { get; set; } = false;

        /// <summary>
        /// Gets or sets the lottie.
        /// </summary>
        [JsonPropertyName("lottie")]
        public bool? Lottie { get; set; } = false;

        public const string RecordType = "blue.moji.richtext.facet#formats_v0";

        public static FormatsV0 FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Richtext.FormatsV0>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Richtext.FormatsV0>)SourceGenerationContext.Default.BlueMojiRichtextFormatsV0)!;
        }
    }
}


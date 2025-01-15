// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class EmbedImages : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedImages"/> class.
        /// </summary>
        /// <param name="images"></param>
        public EmbedImages(List<FishyFlip.Lexicon.App.Bsky.Embed.Image>? images = default)
        {
            this.Images = images;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedImages"/> class.
        /// </summary>
        public EmbedImages()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedImages"/> class.
        /// </summary>
        public EmbedImages(CBORObject obj)
        {
            if (obj["images"] is not null) this.Images = obj["images"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Embed.Image(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        [JsonPropertyName("images")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Embed.Image>? Images { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.embed.images";

        public const string RecordType = "app.bsky.embed.images";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Embed.EmbedImages>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.EmbedImages>)SourceGenerationContext.Default.AppBskyEmbedEmbedImages)!;
        }

        public static EmbedImages FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Embed.EmbedImages>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.EmbedImages>)SourceGenerationContext.Default.AppBskyEmbedEmbedImages)!;
        }
    }
}


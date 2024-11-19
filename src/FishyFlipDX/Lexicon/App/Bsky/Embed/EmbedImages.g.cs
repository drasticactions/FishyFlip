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
        public EmbedImages()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedImages"/> class.
        /// </summary>
        public EmbedImages(CBORObject obj)
        {
            if (obj["images"] is not null) this.Images = obj["images"].Values.Select(n => n is not null ? new App.Bsky.Embed.Image(n) : null).ToList();
        }

        [JsonPropertyName("images")]
        [JsonRequired]
        public List<App.Bsky.Embed.Image?>? Images { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.embed.images";

        public const string RecordType = "app.bsky.embed.images";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Embed.EmbedImages>(this, (JsonTypeInfo<App.Bsky.Embed.EmbedImages>)SourceGenerationContext.Default.AppBskyEmbedEmbedImages)!;
        }

        public static EmbedImages FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Embed.EmbedImages>(json, (JsonTypeInfo<App.Bsky.Embed.EmbedImages>)SourceGenerationContext.Default.AppBskyEmbedEmbedImages)!;
        }
    }
}


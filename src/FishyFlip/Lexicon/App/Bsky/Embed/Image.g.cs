// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class Image : ATObject, ICBOREncodable<Image>, IJsonEncodable<Image>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alt">Alt text description of the image, for accessibility.</param>
        /// <param name="aspectRatio">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio"/> (app.bsky.embed.defs#aspectRatio)
        /// </param>
        public Image(Blob image = default, string alt = default, FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio? aspectRatio = default)
        {
            this.ImageValue = image;
            this.Alt = alt;
            this.AspectRatio = aspectRatio;
            this.Type = "app.bsky.embed.images#image";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
            this.Type = "app.bsky.embed.images#image";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image(CBORObject obj)
        {
            if (obj["image"] is not null) this.ImageValue = new FishyFlip.Models.Blob(obj["image"]);
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
            if (obj["aspectRatio"] is not null) this.AspectRatio = new FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio(obj["aspectRatio"]);
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [JsonPropertyName("image")]
        [JsonRequired]
        public Blob ImageValue { get; set; }

        /// <summary>
        /// Gets or sets the alt.
        /// <br/> Alt text description of the image, for accessibility.
        /// </summary>
        [JsonPropertyName("alt")]
        [JsonRequired]
        public string Alt { get; set; }

        /// <summary>
        /// Gets or sets the aspectRatio.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio"/> (app.bsky.embed.defs#aspectRatio)
        /// </summary>
        [JsonPropertyName("aspectRatio")]
        public FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio? AspectRatio { get; set; }

        public const string RecordType = "app.bsky.embed.images#image";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.Image>)SourceGenerationContext.Default.AppBskyEmbedImage);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.Image>)SourceGenerationContext.Default.AppBskyEmbedImage);
        }

        public static new Image FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Embed.Image>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.Image>)SourceGenerationContext.Default.AppBskyEmbedImage)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Image FromCBORObject(CBORObject obj)
        {
            return new Image(obj);
        }

    }
}


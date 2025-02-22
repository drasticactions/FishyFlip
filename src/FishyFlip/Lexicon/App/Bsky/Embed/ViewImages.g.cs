// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class ViewImages : ATObject, ICBOREncodable<ViewImages>, IJsonEncodable<ViewImages>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewImages"/> class.
        /// </summary>
        /// <param name="images"></param>
        public ViewImages(List<FishyFlip.Lexicon.App.Bsky.Embed.ViewImage> images = default)
        {
            this.Images = images;
            this.Type = "app.bsky.embed.images#view";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewImages"/> class.
        /// </summary>
        public ViewImages()
        {
            this.Type = "app.bsky.embed.images#view";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewImages"/> class.
        /// </summary>
        public ViewImages(CBORObject obj)
        {
            if (obj["images"] is not null) this.Images = obj["images"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Embed.ViewImage(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        [JsonPropertyName("images")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Embed.ViewImage> Images { get; set; }

        public const string RecordType = "app.bsky.embed.images#view";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.ViewImages>)SourceGenerationContext.Default.AppBskyEmbedViewImages);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.ViewImages>)SourceGenerationContext.Default.AppBskyEmbedViewImages);
        }

        public static new ViewImages FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Embed.ViewImages>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.ViewImages>)SourceGenerationContext.Default.AppBskyEmbedViewImages)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ViewImages FromCBORObject(CBORObject obj)
        {
            return new ViewImages(obj);
        }

    }
}


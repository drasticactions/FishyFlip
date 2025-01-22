// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class ViewImage : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewImage"/> class.
        /// </summary>
        /// <param name="thumb">Fully-qualified URL where a thumbnail of the image can be fetched. For example, CDN location provided by the App View.</param>
        /// <param name="fullsize">Fully-qualified URL where a large version of the image can be fetched. May or may not be the exact original blob. For example, CDN location provided by the App View.</param>
        /// <param name="alt">Alt text description of the image, for accessibility.</param>
        /// <param name="aspectRatio">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio"/> (app.bsky.embed.defs#aspectRatio)
        /// </param>
        public ViewImage(string thumb = default, string fullsize = default, string alt = default, FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio? aspectRatio = default)
        {
            this.Thumb = thumb;
            this.Fullsize = fullsize;
            this.Alt = alt;
            this.AspectRatio = aspectRatio;
            this.Type = "app.bsky.embed.images#viewImage";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewImage"/> class.
        /// </summary>
        public ViewImage()
        {
            this.Type = "app.bsky.embed.images#viewImage";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewImage"/> class.
        /// </summary>
        public ViewImage(CBORObject obj)
        {
            if (obj["thumb"] is not null) this.Thumb = obj["thumb"].AsString();
            if (obj["fullsize"] is not null) this.Fullsize = obj["fullsize"].AsString();
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
            if (obj["aspectRatio"] is not null) this.AspectRatio = new FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio(obj["aspectRatio"]);
        }

        /// <summary>
        /// Gets or sets the thumb.
        /// <br/> Fully-qualified URL where a thumbnail of the image can be fetched. For example, CDN location provided by the App View.
        /// </summary>
        [JsonPropertyName("thumb")]
        [JsonRequired]
        public string Thumb { get; set; }

        /// <summary>
        /// Gets or sets the fullsize.
        /// <br/> Fully-qualified URL where a large version of the image can be fetched. May or may not be the exact original blob. For example, CDN location provided by the App View.
        /// </summary>
        [JsonPropertyName("fullsize")]
        [JsonRequired]
        public string Fullsize { get; set; }

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

        public const string RecordType = "app.bsky.embed.images#viewImage";

        public static ViewImage FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Embed.ViewImage>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.ViewImage>)SourceGenerationContext.Default.AppBskyEmbedViewImage)!;
        }
    }
}


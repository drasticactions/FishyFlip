// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    /// <summary>
    /// A link to the image, it can be either directly to the PDS or to a CDN.
    /// </summary>
    public partial class ImageLink : ATObject, ICBOREncodable<ImageLink>, IJsonEncodable<ImageLink>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageLink"/> class.
        /// </summary>
        /// <param name="fullsize">Fully-qualified URL where a large version of the image can be fetched.</param>
        /// <param name="alt">Alt text description of the image, for accessibility.</param>
        public ImageLink(string fullsize = default, string? alt = default)
        {
            this.Fullsize = fullsize;
            this.Alt = alt;
            this.Type = "com.shinolabs.pinksea.oekaki#imageLink";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ImageLink"/> class.
        /// </summary>
        public ImageLink()
        {
            this.Type = "com.shinolabs.pinksea.oekaki#imageLink";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ImageLink"/> class.
        /// </summary>
        public ImageLink(CBORObject obj)
        {
            if (obj["fullsize"] is not null) this.Fullsize = obj["fullsize"].AsString();
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
        }

        /// <summary>
        /// Gets or sets the fullsize.
        /// <br/> Fully-qualified URL where a large version of the image can be fetched.
        /// </summary>
        [JsonPropertyName("fullsize")]
        [JsonRequired]
        public string Fullsize { get; set; }

        /// <summary>
        /// Gets or sets the alt.
        /// <br/> Alt text description of the image, for accessibility.
        /// </summary>
        [JsonPropertyName("alt")]
        public string? Alt { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.oekaki#imageLink";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.ImageLink>)SourceGenerationContext.Default.ComShinolabsPinkseaImageLink);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.ImageLink>)SourceGenerationContext.Default.ComShinolabsPinkseaImageLink);
        }

        public static new ImageLink FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.ImageLink>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.ImageLink>)SourceGenerationContext.Default.ComShinolabsPinkseaImageLink)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ImageLink FromCBORObject(CBORObject obj)
        {
            return new ImageLink(obj);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    public partial class Image : ATObject, ICBOREncodable<Image>, IJsonEncodable<Image>, IParsable<Image>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="blob">The actual atproto image blob.</param>
        /// <param name="imageLink">
        /// com.shinolabs.pinksea.defs#imageLink <br/>
        /// </param>
        public Image(Blob blob = default, FishyFlip.Lexicon.Com.Shinolabs.Pinksea.ImageLink imageLink = default)
        {
            this.Blob = blob;
            this.ImageLink = imageLink;
            this.Type = "com.shinolabs.pinksea.oekaki#image";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
            this.Type = "com.shinolabs.pinksea.oekaki#image";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image(CBORObject obj)
        {
            if (obj["blob"] is not null) this.Blob = new FishyFlip.Models.Blob(obj["blob"]);
            if (obj["imageLink"] is not null) this.ImageLink = new FishyFlip.Lexicon.Com.Shinolabs.Pinksea.ImageLink(obj["imageLink"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the blob.
        /// <br/> The actual atproto image blob.
        /// </summary>
        [JsonPropertyName("blob")]
        [JsonRequired]
        public Blob Blob { get; set; }

        /// <summary>
        /// Gets or sets the imageLink.
        /// com.shinolabs.pinksea.defs#imageLink <br/>
        /// </summary>
        [JsonPropertyName("imageLink")]
        [JsonRequired]
        public FishyFlip.Lexicon.Com.Shinolabs.Pinksea.ImageLink ImageLink { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.oekaki#image";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Image>)SourceGenerationContext.Default.ComShinolabsPinkseaImage);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Image>)SourceGenerationContext.Default.ComShinolabsPinkseaImage);
        }

        public static new Image FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Image>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Image>)SourceGenerationContext.Default.ComShinolabsPinkseaImage)!;
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

        /// <inheritdoc/>
        public static Image Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Image>(s, (JsonTypeInfo<Image>)SourceGenerationContext.Default.ComShinolabsPinkseaImage)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Image result)
        {
            result = JsonSerializer.Deserialize<Image>(s, (JsonTypeInfo<Image>)SourceGenerationContext.Default.ComShinolabsPinkseaImage);
            return result != null;
        }
    }
}


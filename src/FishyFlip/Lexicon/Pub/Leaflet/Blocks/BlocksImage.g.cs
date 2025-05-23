// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Pub.Leaflet.Blocks
{
    public partial class BlocksImage : ATObject, ICBOREncodable<BlocksImage>, IJsonEncodable<BlocksImage>, IParsable<BlocksImage>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BlocksImage"/> class.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alt">Alt text description of the image, for accessibility.</param>
        /// <param name="aspectRatio">
        /// pub.leaflet.blocks.defs#aspectRatio <br/>
        /// </param>
        public BlocksImage(Blob image = default, string? alt = default, FishyFlip.Lexicon.Pub.Leaflet.Blocks.AspectRatio aspectRatio = default)
        {
            this.Image = image;
            this.Alt = alt;
            this.AspectRatio = aspectRatio;
            this.Type = "pub.leaflet.blocks.image";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlocksImage"/> class.
        /// </summary>
        public BlocksImage()
        {
            this.Type = "pub.leaflet.blocks.image";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlocksImage"/> class.
        /// </summary>
        public BlocksImage(CBORObject obj)
        {
            if (obj["image"] is not null) this.Image = new FishyFlip.Models.Blob(obj["image"]);
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
            if (obj["aspectRatio"] is not null) this.AspectRatio = new FishyFlip.Lexicon.Pub.Leaflet.Blocks.AspectRatio(obj["aspectRatio"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [JsonPropertyName("image")]
        [JsonRequired]
        public Blob Image { get; set; }

        /// <summary>
        /// Gets or sets the alt.
        /// <br/> Alt text description of the image, for accessibility.
        /// </summary>
        [JsonPropertyName("alt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Alt { get; set; }

        /// <summary>
        /// Gets or sets the aspectRatio.
        /// pub.leaflet.blocks.defs#aspectRatio <br/>
        /// </summary>
        [JsonPropertyName("aspectRatio")]
        [JsonRequired]
        public FishyFlip.Lexicon.Pub.Leaflet.Blocks.AspectRatio AspectRatio { get; set; }

        public const string RecordType = "pub.leaflet.blocks.image";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Pub.Leaflet.Blocks.BlocksImage>)SourceGenerationContext.Default.PubLeafletBlocksBlocksImage);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Pub.Leaflet.Blocks.BlocksImage>)SourceGenerationContext.Default.PubLeafletBlocksBlocksImage);
        }

        public static new BlocksImage FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Pub.Leaflet.Blocks.BlocksImage>(json, (JsonTypeInfo<FishyFlip.Lexicon.Pub.Leaflet.Blocks.BlocksImage>)SourceGenerationContext.Default.PubLeafletBlocksBlocksImage)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new BlocksImage FromCBORObject(CBORObject obj)
        {
            return new BlocksImage(obj);
        }

        /// <inheritdoc/>
        public static BlocksImage Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<BlocksImage>(s, (JsonTypeInfo<BlocksImage>)SourceGenerationContext.Default.PubLeafletBlocksBlocksImage)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out BlocksImage result)
        {
            result = JsonSerializer.Deserialize<BlocksImage>(s, (JsonTypeInfo<BlocksImage>)SourceGenerationContext.Default.PubLeafletBlocksBlocksImage);
            return result != null;
        }
    }
}


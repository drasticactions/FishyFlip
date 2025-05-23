// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Pub.Leaflet.Blocks
{
    public partial class AspectRatio : ATObject, ICBOREncodable<AspectRatio>, IJsonEncodable<AspectRatio>, IParsable<AspectRatio>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AspectRatio"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public AspectRatio(long width = default, long height = default)
        {
            this.Width = width;
            this.Height = height;
            this.Type = "pub.leaflet.blocks.image#aspectRatio";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AspectRatio"/> class.
        /// </summary>
        public AspectRatio()
        {
            this.Type = "pub.leaflet.blocks.image#aspectRatio";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AspectRatio"/> class.
        /// </summary>
        public AspectRatio(CBORObject obj)
        {
            if (obj["width"] is not null) this.Width = obj["width"].AsInt64Value();
            if (obj["height"] is not null) this.Height = obj["height"].AsInt64Value();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [JsonPropertyName("width")]
        [JsonRequired]
        public long Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [JsonPropertyName("height")]
        [JsonRequired]
        public long Height { get; set; }

        public const string RecordType = "pub.leaflet.blocks.image#aspectRatio";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Pub.Leaflet.Blocks.AspectRatio>)SourceGenerationContext.Default.PubLeafletBlocksAspectRatio);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Pub.Leaflet.Blocks.AspectRatio>)SourceGenerationContext.Default.PubLeafletBlocksAspectRatio);
        }

        public static new AspectRatio FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Pub.Leaflet.Blocks.AspectRatio>(json, (JsonTypeInfo<FishyFlip.Lexicon.Pub.Leaflet.Blocks.AspectRatio>)SourceGenerationContext.Default.PubLeafletBlocksAspectRatio)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new AspectRatio FromCBORObject(CBORObject obj)
        {
            return new AspectRatio(obj);
        }

        /// <inheritdoc/>
        public static AspectRatio Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<AspectRatio>(s, (JsonTypeInfo<AspectRatio>)SourceGenerationContext.Default.PubLeafletBlocksAspectRatio)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out AspectRatio result)
        {
            result = JsonSerializer.Deserialize<AspectRatio>(s, (JsonTypeInfo<AspectRatio>)SourceGenerationContext.Default.PubLeafletBlocksAspectRatio);
            return result != null;
        }
    }
}


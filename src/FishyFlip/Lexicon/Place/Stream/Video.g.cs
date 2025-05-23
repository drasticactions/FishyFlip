// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Place.Stream
{
    public partial class Video : ATObject, ICBOREncodable<Video>, IJsonEncodable<Video>, IParsable<Video>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        /// <param name="codec"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="framerate">
        /// place.stream.defs#framerate <br/>
        /// </param>
        public Video(string codec = default, long width = default, long height = default, FishyFlip.Lexicon.Place.Stream.Framerate? framerate = default)
        {
            this.Codec = codec;
            this.Width = width;
            this.Height = height;
            this.Framerate = framerate;
            this.Type = "place.stream.segment#video";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        public Video()
        {
            this.Type = "place.stream.segment#video";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        public Video(CBORObject obj)
        {
            if (obj["codec"] is not null) this.Codec = obj["codec"].AsString();
            if (obj["width"] is not null) this.Width = obj["width"].AsInt64Value();
            if (obj["height"] is not null) this.Height = obj["height"].AsInt64Value();
            if (obj["framerate"] is not null) this.Framerate = new FishyFlip.Lexicon.Place.Stream.Framerate(obj["framerate"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the codec.
        /// </summary>
        [JsonPropertyName("codec")]
        [JsonRequired]
        public string Codec { get; set; }

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

        /// <summary>
        /// Gets or sets the framerate.
        /// place.stream.defs#framerate <br/>
        /// </summary>
        [JsonPropertyName("framerate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FishyFlip.Lexicon.Place.Stream.Framerate? Framerate { get; set; }

        public const string RecordType = "place.stream.segment#video";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Video>)SourceGenerationContext.Default.PlaceStreamVideo);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Video>)SourceGenerationContext.Default.PlaceStreamVideo);
        }

        public static new Video FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Place.Stream.Video>(json, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Video>)SourceGenerationContext.Default.PlaceStreamVideo)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Video FromCBORObject(CBORObject obj)
        {
            return new Video(obj);
        }

        /// <inheritdoc/>
        public static Video Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Video>(s, (JsonTypeInfo<Video>)SourceGenerationContext.Default.PlaceStreamVideo)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Video result)
        {
            result = JsonSerializer.Deserialize<Video>(s, (JsonTypeInfo<Video>)SourceGenerationContext.Default.PlaceStreamVideo);
            return result != null;
        }
    }
}


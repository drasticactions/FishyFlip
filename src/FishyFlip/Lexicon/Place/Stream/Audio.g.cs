// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Place.Stream
{
    public partial class Audio : ATObject, ICBOREncodable<Audio>, IJsonEncodable<Audio>, IParsable<Audio>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Audio"/> class.
        /// </summary>
        /// <param name="codec"></param>
        /// <param name="rate"></param>
        /// <param name="channels"></param>
        public Audio(string codec = default, long rate = default, long channels = default)
        {
            this.Codec = codec;
            this.Rate = rate;
            this.Channels = channels;
            this.Type = "place.stream.segment#audio";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Audio"/> class.
        /// </summary>
        public Audio()
        {
            this.Type = "place.stream.segment#audio";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Audio"/> class.
        /// </summary>
        public Audio(CBORObject obj)
        {
            if (obj["codec"] is not null) this.Codec = obj["codec"].AsString();
            if (obj["rate"] is not null) this.Rate = obj["rate"].AsInt64Value();
            if (obj["channels"] is not null) this.Channels = obj["channels"].AsInt64Value();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the codec.
        /// </summary>
        [JsonPropertyName("codec")]
        [JsonRequired]
        public string Codec { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        [JsonPropertyName("rate")]
        [JsonRequired]
        public long Rate { get; set; }

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
        [JsonPropertyName("channels")]
        [JsonRequired]
        public long Channels { get; set; }

        public const string RecordType = "place.stream.segment#audio";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Audio>)SourceGenerationContext.Default.PlaceStreamAudio);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Audio>)SourceGenerationContext.Default.PlaceStreamAudio);
        }

        public static new Audio FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Place.Stream.Audio>(json, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Audio>)SourceGenerationContext.Default.PlaceStreamAudio)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Audio FromCBORObject(CBORObject obj)
        {
            return new Audio(obj);
        }

        /// <inheritdoc/>
        public static Audio Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Audio>(s, (JsonTypeInfo<Audio>)SourceGenerationContext.Default.PlaceStreamAudio)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Audio result)
        {
            result = JsonSerializer.Deserialize<Audio>(s, (JsonTypeInfo<Audio>)SourceGenerationContext.Default.PlaceStreamAudio);
            return result != null;
        }
    }
}


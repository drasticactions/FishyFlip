// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.My.Skylights
{
    public partial class Rating : ATObject, ICBOREncodable<Rating>, IJsonEncodable<Rating>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="createdAt"></param>
        public Rating(long value = default, DateTime? createdAt = default)
        {
            this.Value = value;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "my.skylights.rel#rating";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        public Rating()
        {
            this.Type = "my.skylights.rel#rating";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        public Rating(CBORObject obj)
        {
            if (obj["value"] is not null) this.Value = obj["value"].AsInt64Value();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public long Value { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonRequired]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "my.skylights.rel#rating";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.My.Skylights.Rating>)SourceGenerationContext.Default.MySkylightsRating);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.My.Skylights.Rating>)SourceGenerationContext.Default.MySkylightsRating);
        }

        public static new Rating FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.My.Skylights.Rating>(json, (JsonTypeInfo<FishyFlip.Lexicon.My.Skylights.Rating>)SourceGenerationContext.Default.MySkylightsRating)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Rating FromCBORObject(CBORObject obj)
        {
            return new Rating(obj);
        }

    }
}


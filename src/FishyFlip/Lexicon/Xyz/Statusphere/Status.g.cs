// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Xyz.Statusphere
{
    public partial class Status : ATObject, ICBOREncodable<Status>, IJsonEncodable<Status>, IParsable<Status>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="createdAt"></param>
        public Status(string? status, DateTime? createdAt = default)
        {
            this.StatusValue = status;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "xyz.statusphere.status";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status()
        {
            this.Type = "xyz.statusphere.status";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status(CBORObject obj)
        {
            if (obj["status"] is not null) this.StatusValue = obj["status"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StatusValue { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "xyz.statusphere.status";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Xyz.Statusphere.Status>)SourceGenerationContext.Default.XyzStatusphereStatus);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Xyz.Statusphere.Status>)SourceGenerationContext.Default.XyzStatusphereStatus);
        }

        public static new Status FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Xyz.Statusphere.Status>(json, (JsonTypeInfo<FishyFlip.Lexicon.Xyz.Statusphere.Status>)SourceGenerationContext.Default.XyzStatusphereStatus)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Status FromCBORObject(CBORObject obj)
        {
            return new Status(obj);
        }

        /// <inheritdoc/>
        public static Status Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Status>(s, (JsonTypeInfo<Status>)SourceGenerationContext.Default.XyzStatusphereStatus)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Status result)
        {
            result = JsonSerializer.Deserialize<Status>(s, (JsonTypeInfo<Status>)SourceGenerationContext.Default.XyzStatusphereStatus);
            return result != null;
        }
    }
}


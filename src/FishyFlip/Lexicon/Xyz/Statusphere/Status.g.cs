// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Xyz.Statusphere
{
    public partial class Status : ATObject
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
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status(CBORObject obj)
        {
            if (obj["status"] is not null) this.StatusValue = obj["status"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonPropertyName("status")]
        public string? StatusValue { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "xyz.statusphere.status";

        public const string RecordType = "xyz.statusphere.status";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Xyz.Statusphere.Status>(this, (JsonTypeInfo<Xyz.Statusphere.Status>)SourceGenerationContext.Default.XyzStatusphereStatus)!;
        }

        public static Status FromJson(string json)
        {
            return JsonSerializer.Deserialize<Xyz.Statusphere.Status>(json, (JsonTypeInfo<Xyz.Statusphere.Status>)SourceGenerationContext.Default.XyzStatusphereStatus)!;
        }
    }
}

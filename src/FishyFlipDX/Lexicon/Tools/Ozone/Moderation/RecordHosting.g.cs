// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class RecordHosting : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordHosting"/> class.
        /// </summary>
        public RecordHosting()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordHosting"/> class.
        /// </summary>
        public RecordHosting(CBORObject obj)
        {
            // enum
            if (obj["updatedAt"] is not null) this.UpdatedAt = obj["updatedAt"].ToDateTime();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["deletedAt"] is not null) this.DeletedAt = obj["deletedAt"].ToDateTime();
        }

        [JsonPropertyName("status")]
        [JsonRequired]
        public string? Status { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("deletedAt")]
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#recordHosting";

        public const string RecordType = "tools.ozone.moderation.defs#recordHosting";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.RecordHosting>(this, (JsonTypeInfo<Tools.Ozone.Moderation.RecordHosting>)SourceGenerationContext.Default.ToolsOzoneModerationRecordHosting)!;
        }

        public static RecordHosting FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.RecordHosting>(json, (JsonTypeInfo<Tools.Ozone.Moderation.RecordHosting>)SourceGenerationContext.Default.ToolsOzoneModerationRecordHosting)!;
        }
    }
}


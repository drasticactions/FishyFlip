// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class RecordHosting : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordHosting"/> class.
        /// </summary>
        /// <param name="status">
        /// <br/> Known Values: <br/>
        /// deleted <br/>
        /// unknown <br/>
        /// </param>
        /// <param name="updatedAt"></param>
        /// <param name="createdAt"></param>
        /// <param name="deletedAt"></param>
        public RecordHosting(string status = default, DateTime? updatedAt = default, DateTime? createdAt = default, DateTime? deletedAt = default)
        {
            this.Status = status;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.DeletedAt = deletedAt;
            this.Type = "tools.ozone.moderation.defs#recordHosting";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordHosting"/> class.
        /// </summary>
        public RecordHosting()
        {
            this.Type = "tools.ozone.moderation.defs#recordHosting";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordHosting"/> class.
        /// </summary>
        public RecordHosting(CBORObject obj)
        {
            if (obj["status"] is not null) this.Status = obj["status"].AsString();
            if (obj["updatedAt"] is not null) this.UpdatedAt = obj["updatedAt"].ToDateTime();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["deletedAt"] is not null) this.DeletedAt = obj["deletedAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the status.
        /// <br/> Known Values: <br/>
        /// deleted <br/>
        /// unknown <br/>
        /// </summary>
        [JsonPropertyName("status")]
        [JsonRequired]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the updatedAt.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the deletedAt.
        /// </summary>
        [JsonPropertyName("deletedAt")]
        public DateTime? DeletedAt { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#recordHosting";

        public static RecordHosting FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordHosting>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordHosting>)SourceGenerationContext.Default.ToolsOzoneModerationRecordHosting)!;
        }
    }
}


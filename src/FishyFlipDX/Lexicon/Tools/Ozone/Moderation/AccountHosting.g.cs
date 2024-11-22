// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class AccountHosting : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountHosting"/> class.
        /// </summary>
        /// <param name="status">
        /// Known Values:
        /// takendown
        /// suspended
        /// deleted
        /// deactivated
        /// unknown
        /// </param>
        /// <param name="updatedAt"></param>
        /// <param name="createdAt"></param>
        /// <param name="deletedAt"></param>
        /// <param name="deactivatedAt"></param>
        /// <param name="reactivatedAt"></param>
        public AccountHosting(string? status = default, DateTime? updatedAt = default, DateTime? createdAt = default, DateTime? deletedAt = default, DateTime? deactivatedAt = default, DateTime? reactivatedAt = default)
        {
            this.Status = status;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.DeletedAt = deletedAt;
            this.DeactivatedAt = deactivatedAt;
            this.ReactivatedAt = reactivatedAt;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountHosting"/> class.
        /// </summary>
        public AccountHosting()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountHosting"/> class.
        /// </summary>
        public AccountHosting(CBORObject obj)
        {
            if (obj["status"] is not null) this.Status = obj["status"].AsString();
            if (obj["updatedAt"] is not null) this.UpdatedAt = obj["updatedAt"].ToDateTime();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["deletedAt"] is not null) this.DeletedAt = obj["deletedAt"].ToDateTime();
            if (obj["deactivatedAt"] is not null) this.DeactivatedAt = obj["deactivatedAt"].ToDateTime();
            if (obj["reactivatedAt"] is not null) this.ReactivatedAt = obj["reactivatedAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the status.
        /// Known Values:
        /// takendown
        /// suspended
        /// deleted
        /// deactivated
        /// unknown
        /// </summary>
        [JsonPropertyName("status")]
        [JsonRequired]
        public string? Status { get; set; }

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

        /// <summary>
        /// Gets or sets the deactivatedAt.
        /// </summary>
        [JsonPropertyName("deactivatedAt")]
        public DateTime? DeactivatedAt { get; set; }

        /// <summary>
        /// Gets or sets the reactivatedAt.
        /// </summary>
        [JsonPropertyName("reactivatedAt")]
        public DateTime? ReactivatedAt { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#accountHosting";

        public const string RecordType = "tools.ozone.moderation.defs#accountHosting";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.AccountHosting>(this, (JsonTypeInfo<Tools.Ozone.Moderation.AccountHosting>)SourceGenerationContext.Default.ToolsOzoneModerationAccountHosting)!;
        }

        public static AccountHosting FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.AccountHosting>(json, (JsonTypeInfo<Tools.Ozone.Moderation.AccountHosting>)SourceGenerationContext.Default.ToolsOzoneModerationAccountHosting)!;
        }
    }
}


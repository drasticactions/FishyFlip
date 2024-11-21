// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Moderation
{
    public partial class CreateReportInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportInput"/> class.
        /// </summary>
        public CreateReportInput(string? reasonType = default, string? reason = default, ATObject? subject = default)
        {
            this.ReasonType = reasonType;
            this.Reason = reason;
            this.Subject = subject;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportInput"/> class.
        /// </summary>
        public CreateReportInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportInput"/> class.
        /// </summary>
        public CreateReportInput(CBORObject obj)
        {
            if (obj["reasonType"] is not null) this.ReasonType = obj["reasonType"].AsString();
            if (obj["reason"] is not null) this.Reason = obj["reason"].AsString();
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATObject();
        }

        /// <summary>
        /// Indicates the broad category of violation the report is for.
        /// </summary>
        [JsonPropertyName("reasonType")]
        [JsonRequired]
        public string? ReasonType { get; set; }

        /// <summary>
        /// Additional context about the content and violation.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [JsonPropertyName("subject")]
        [JsonRequired]
        public ATObject? Subject { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.moderation.createReport#CreateReportInput";

        public const string RecordType = "com.atproto.moderation.createReport#CreateReportInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Moderation.CreateReportInput>(this, (JsonTypeInfo<Com.Atproto.Moderation.CreateReportInput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportInput)!;
        }

        public static CreateReportInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Moderation.CreateReportInput>(json, (JsonTypeInfo<Com.Atproto.Moderation.CreateReportInput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportInput)!;
        }
    }
}


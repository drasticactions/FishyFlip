// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Report a subject
    /// </summary>
    public partial class ModEventReport : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventReport"/> class.
        /// </summary>
        public ModEventReport()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventReport"/> class.
        /// </summary>
        public ModEventReport(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
            if (obj["isReporterMuted"] is not null) this.IsReporterMuted = obj["isReporterMuted"].AsBoolean();
            if (obj["reportType"] is not null) this.ReportType = obj["reportType"].AsString();
        }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Set to true if the reporter was muted from reporting at the time of the event. These reports won't impact the reviewState of the subject.
        /// </summary>
        [JsonPropertyName("isReporterMuted")]
        public bool? IsReporterMuted { get; set; }

        [JsonPropertyName("reportType")]
        [JsonRequired]
        public string? ReportType { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#modEventReport";

        public const string RecordType = "tools.ozone.moderation.defs#modEventReport";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.ModEventReport>(this, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventReport>)SourceGenerationContext.Default.ToolsOzoneModerationModEventReport)!;
        }

        public static ModEventReport FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.ModEventReport>(json, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventReport>)SourceGenerationContext.Default.ToolsOzoneModerationModEventReport)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class QueryStatusesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStatusesOutput"/> class.
        /// </summary>
        public QueryStatusesOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStatusesOutput"/> class.
        /// </summary>
        public QueryStatusesOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["subjectStatuses"] is not null) this.SubjectStatuses = obj["subjectStatuses"].Values.Select(n => n is not null ? new Tools.Ozone.Moderation.SubjectStatusView(n) : null).ToList();
        }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        [JsonPropertyName("subjectStatuses")]
        [JsonRequired]
        public List<Tools.Ozone.Moderation.SubjectStatusView?>? SubjectStatuses { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.queryStatuses#QueryStatusesOutput";

        public const string RecordType = "tools.ozone.moderation.queryStatuses#QueryStatusesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.QueryStatusesOutput>(this, (JsonTypeInfo<Tools.Ozone.Moderation.QueryStatusesOutput>)SourceGenerationContext.Default.ToolsOzoneModerationQueryStatusesOutput)!;
        }

        public static QueryStatusesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.QueryStatusesOutput>(json, (JsonTypeInfo<Tools.Ozone.Moderation.QueryStatusesOutput>)SourceGenerationContext.Default.ToolsOzoneModerationQueryStatusesOutput)!;
        }
    }
}


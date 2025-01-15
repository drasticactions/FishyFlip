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
        /// <param name="cursor"></param>
        /// <param name="subjectStatuses"></param>
        public QueryStatusesOutput(string? cursor = default, List<FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView>? subjectStatuses = default)
        {
            this.Cursor = cursor;
            this.SubjectStatuses = subjectStatuses;
        }


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
            if (obj["subjectStatuses"] is not null) this.SubjectStatuses = obj["subjectStatuses"].Values.Select(n =>new FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the subjectStatuses.
        /// </summary>
        [JsonPropertyName("subjectStatuses")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView>? SubjectStatuses { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.queryStatuses#QueryStatusesOutput";

        public const string RecordType = "tools.ozone.moderation.queryStatuses#QueryStatusesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>)SourceGenerationContext.Default.ToolsOzoneModerationQueryStatusesOutput)!;
        }

        public static QueryStatusesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>)SourceGenerationContext.Default.ToolsOzoneModerationQueryStatusesOutput)!;
        }
    }
}


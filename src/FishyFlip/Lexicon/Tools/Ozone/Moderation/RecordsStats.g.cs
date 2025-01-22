// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Statistics about a set of record subject items
    /// </summary>
    public partial class RecordsStats : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordsStats"/> class.
        /// </summary>
        /// <param name="totalReports">Cumulative sum of the number of reports on the items in the set</param>
        /// <param name="reportedCount">Number of items that were reported at least once</param>
        /// <param name="escalatedCount">Number of items that were escalated at least once</param>
        /// <param name="appealedCount">Number of items that were appealed at least once</param>
        /// <param name="subjectCount">Total number of item in the set</param>
        /// <param name="pendingCount">Number of item currently in "reviewOpen" or "reviewEscalated" state</param>
        /// <param name="processedCount">Number of item currently in "reviewNone" or "reviewClosed" state</param>
        /// <param name="takendownCount">Number of item currently taken down</param>
        public RecordsStats(long? totalReports = default, long? reportedCount = default, long? escalatedCount = default, long? appealedCount = default, long? subjectCount = default, long? pendingCount = default, long? processedCount = default, long? takendownCount = default)
        {
            this.TotalReports = totalReports;
            this.ReportedCount = reportedCount;
            this.EscalatedCount = escalatedCount;
            this.AppealedCount = appealedCount;
            this.SubjectCount = subjectCount;
            this.PendingCount = pendingCount;
            this.ProcessedCount = processedCount;
            this.TakendownCount = takendownCount;
            this.Type = "tools.ozone.moderation.defs#recordsStats";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordsStats"/> class.
        /// </summary>
        public RecordsStats()
        {
            this.Type = "tools.ozone.moderation.defs#recordsStats";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordsStats"/> class.
        /// </summary>
        public RecordsStats(CBORObject obj)
        {
            if (obj["totalReports"] is not null) this.TotalReports = obj["totalReports"].AsInt64Value();
            if (obj["reportedCount"] is not null) this.ReportedCount = obj["reportedCount"].AsInt64Value();
            if (obj["escalatedCount"] is not null) this.EscalatedCount = obj["escalatedCount"].AsInt64Value();
            if (obj["appealedCount"] is not null) this.AppealedCount = obj["appealedCount"].AsInt64Value();
            if (obj["subjectCount"] is not null) this.SubjectCount = obj["subjectCount"].AsInt64Value();
            if (obj["pendingCount"] is not null) this.PendingCount = obj["pendingCount"].AsInt64Value();
            if (obj["processedCount"] is not null) this.ProcessedCount = obj["processedCount"].AsInt64Value();
            if (obj["takendownCount"] is not null) this.TakendownCount = obj["takendownCount"].AsInt64Value();
        }

        /// <summary>
        /// Gets or sets the totalReports.
        /// <br/> Cumulative sum of the number of reports on the items in the set
        /// </summary>
        [JsonPropertyName("totalReports")]
        public long? TotalReports { get; set; }

        /// <summary>
        /// Gets or sets the reportedCount.
        /// <br/> Number of items that were reported at least once
        /// </summary>
        [JsonPropertyName("reportedCount")]
        public long? ReportedCount { get; set; }

        /// <summary>
        /// Gets or sets the escalatedCount.
        /// <br/> Number of items that were escalated at least once
        /// </summary>
        [JsonPropertyName("escalatedCount")]
        public long? EscalatedCount { get; set; }

        /// <summary>
        /// Gets or sets the appealedCount.
        /// <br/> Number of items that were appealed at least once
        /// </summary>
        [JsonPropertyName("appealedCount")]
        public long? AppealedCount { get; set; }

        /// <summary>
        /// Gets or sets the subjectCount.
        /// <br/> Total number of item in the set
        /// </summary>
        [JsonPropertyName("subjectCount")]
        public long? SubjectCount { get; set; }

        /// <summary>
        /// Gets or sets the pendingCount.
        /// <br/> Number of item currently in "reviewOpen" or "reviewEscalated" state
        /// </summary>
        [JsonPropertyName("pendingCount")]
        public long? PendingCount { get; set; }

        /// <summary>
        /// Gets or sets the processedCount.
        /// <br/> Number of item currently in "reviewNone" or "reviewClosed" state
        /// </summary>
        [JsonPropertyName("processedCount")]
        public long? ProcessedCount { get; set; }

        /// <summary>
        /// Gets or sets the takendownCount.
        /// <br/> Number of item currently taken down
        /// </summary>
        [JsonPropertyName("takendownCount")]
        public long? TakendownCount { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#recordsStats";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordsStats>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordsStats>)SourceGenerationContext.Default.ToolsOzoneModerationRecordsStats)!;
        }

        public static RecordsStats FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordsStats>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordsStats>)SourceGenerationContext.Default.ToolsOzoneModerationRecordsStats)!;
        }
    }
}


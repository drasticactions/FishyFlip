// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class ReporterStats : ATObject, ICBOREncodable<ReporterStats>, IJsonEncodable<ReporterStats>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReporterStats"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="accountReportCount">The total number of reports made by the user on accounts.</param>
        /// <param name="recordReportCount">The total number of reports made by the user on records.</param>
        /// <param name="reportedAccountCount">The total number of accounts reported by the user.</param>
        /// <param name="reportedRecordCount">The total number of records reported by the user.</param>
        /// <param name="takendownAccountCount">The total number of accounts taken down as a result of the user's reports.</param>
        /// <param name="takendownRecordCount">The total number of records taken down as a result of the user's reports.</param>
        /// <param name="labeledAccountCount">The total number of accounts labeled as a result of the user's reports.</param>
        /// <param name="labeledRecordCount">The total number of records labeled as a result of the user's reports.</param>
        public ReporterStats(FishyFlip.Models.ATDid did = default, long accountReportCount = default, long recordReportCount = default, long reportedAccountCount = default, long reportedRecordCount = default, long takendownAccountCount = default, long takendownRecordCount = default, long labeledAccountCount = default, long labeledRecordCount = default)
        {
            this.Did = did;
            this.AccountReportCount = accountReportCount;
            this.RecordReportCount = recordReportCount;
            this.ReportedAccountCount = reportedAccountCount;
            this.ReportedRecordCount = reportedRecordCount;
            this.TakendownAccountCount = takendownAccountCount;
            this.TakendownRecordCount = takendownRecordCount;
            this.LabeledAccountCount = labeledAccountCount;
            this.LabeledRecordCount = labeledRecordCount;
            this.Type = "tools.ozone.moderation.defs#reporterStats";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReporterStats"/> class.
        /// </summary>
        public ReporterStats()
        {
            this.Type = "tools.ozone.moderation.defs#reporterStats";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReporterStats"/> class.
        /// </summary>
        public ReporterStats(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["accountReportCount"] is not null) this.AccountReportCount = obj["accountReportCount"].AsInt64Value();
            if (obj["recordReportCount"] is not null) this.RecordReportCount = obj["recordReportCount"].AsInt64Value();
            if (obj["reportedAccountCount"] is not null) this.ReportedAccountCount = obj["reportedAccountCount"].AsInt64Value();
            if (obj["reportedRecordCount"] is not null) this.ReportedRecordCount = obj["reportedRecordCount"].AsInt64Value();
            if (obj["takendownAccountCount"] is not null) this.TakendownAccountCount = obj["takendownAccountCount"].AsInt64Value();
            if (obj["takendownRecordCount"] is not null) this.TakendownRecordCount = obj["takendownRecordCount"].AsInt64Value();
            if (obj["labeledAccountCount"] is not null) this.LabeledAccountCount = obj["labeledAccountCount"].AsInt64Value();
            if (obj["labeledRecordCount"] is not null) this.LabeledRecordCount = obj["labeledRecordCount"].AsInt64Value();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets or sets the accountReportCount.
        /// <br/> The total number of reports made by the user on accounts.
        /// </summary>
        [JsonPropertyName("accountReportCount")]
        [JsonRequired]
        public long AccountReportCount { get; set; }

        /// <summary>
        /// Gets or sets the recordReportCount.
        /// <br/> The total number of reports made by the user on records.
        /// </summary>
        [JsonPropertyName("recordReportCount")]
        [JsonRequired]
        public long RecordReportCount { get; set; }

        /// <summary>
        /// Gets or sets the reportedAccountCount.
        /// <br/> The total number of accounts reported by the user.
        /// </summary>
        [JsonPropertyName("reportedAccountCount")]
        [JsonRequired]
        public long ReportedAccountCount { get; set; }

        /// <summary>
        /// Gets or sets the reportedRecordCount.
        /// <br/> The total number of records reported by the user.
        /// </summary>
        [JsonPropertyName("reportedRecordCount")]
        [JsonRequired]
        public long ReportedRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the takendownAccountCount.
        /// <br/> The total number of accounts taken down as a result of the user's reports.
        /// </summary>
        [JsonPropertyName("takendownAccountCount")]
        [JsonRequired]
        public long TakendownAccountCount { get; set; }

        /// <summary>
        /// Gets or sets the takendownRecordCount.
        /// <br/> The total number of records taken down as a result of the user's reports.
        /// </summary>
        [JsonPropertyName("takendownRecordCount")]
        [JsonRequired]
        public long TakendownRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the labeledAccountCount.
        /// <br/> The total number of accounts labeled as a result of the user's reports.
        /// </summary>
        [JsonPropertyName("labeledAccountCount")]
        [JsonRequired]
        public long LabeledAccountCount { get; set; }

        /// <summary>
        /// Gets or sets the labeledRecordCount.
        /// <br/> The total number of records labeled as a result of the user's reports.
        /// </summary>
        [JsonPropertyName("labeledRecordCount")]
        [JsonRequired]
        public long LabeledRecordCount { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#reporterStats";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ReporterStats>)SourceGenerationContext.Default.ToolsOzoneModerationReporterStats);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ReporterStats>)SourceGenerationContext.Default.ToolsOzoneModerationReporterStats);
        }

        public static new ReporterStats FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.ReporterStats>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ReporterStats>)SourceGenerationContext.Default.ToolsOzoneModerationReporterStats)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ReporterStats FromCBORObject(CBORObject obj)
        {
            return new ReporterStats(obj);
        }

    }
}


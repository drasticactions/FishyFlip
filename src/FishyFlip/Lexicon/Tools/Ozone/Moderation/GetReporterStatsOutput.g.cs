// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class GetReporterStatsOutput : ATObject, ICBOREncodable<GetReporterStatsOutput>, IJsonEncodable<GetReporterStatsOutput>, IParsable<GetReporterStatsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetReporterStatsOutput"/> class.
        /// </summary>
        /// <param name="stats"></param>
        public GetReporterStatsOutput(List<FishyFlip.Lexicon.Tools.Ozone.Moderation.ReporterStats> stats = default)
        {
            this.Stats = stats;
            this.Type = "tools.ozone.moderation.getReporterStats#GetReporterStatsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetReporterStatsOutput"/> class.
        /// </summary>
        public GetReporterStatsOutput()
        {
            this.Type = "tools.ozone.moderation.getReporterStats#GetReporterStatsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetReporterStatsOutput"/> class.
        /// </summary>
        public GetReporterStatsOutput(CBORObject obj)
        {
            if (obj["stats"] is not null) this.Stats = obj["stats"].Values.Select(n =>new FishyFlip.Lexicon.Tools.Ozone.Moderation.ReporterStats(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the stats.
        /// </summary>
        [JsonPropertyName("stats")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Tools.Ozone.Moderation.ReporterStats> Stats { get; set; }

        public const string RecordType = "tools.ozone.moderation.getReporterStats#GetReporterStatsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReporterStatsOutput>)SourceGenerationContext.Default.ToolsOzoneModerationGetReporterStatsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReporterStatsOutput>)SourceGenerationContext.Default.ToolsOzoneModerationGetReporterStatsOutput);
        }

        public static new GetReporterStatsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReporterStatsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReporterStatsOutput>)SourceGenerationContext.Default.ToolsOzoneModerationGetReporterStatsOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetReporterStatsOutput FromCBORObject(CBORObject obj)
        {
            return new GetReporterStatsOutput(obj);
        }

        /// <inheritdoc/>
        public static GetReporterStatsOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetReporterStatsOutput>(s, (JsonTypeInfo<GetReporterStatsOutput>)SourceGenerationContext.Default.ToolsOzoneModerationGetReporterStatsOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetReporterStatsOutput result)
        {
            result = JsonSerializer.Deserialize<GetReporterStatsOutput>(s, (JsonTypeInfo<GetReporterStatsOutput>)SourceGenerationContext.Default.ToolsOzoneModerationGetReporterStatsOutput);
            return result != null;
        }
    }
}


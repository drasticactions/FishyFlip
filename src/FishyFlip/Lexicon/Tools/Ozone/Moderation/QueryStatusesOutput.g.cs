// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class QueryStatusesOutput : ATObject, ICBOREncodable<QueryStatusesOutput>, IJsonEncodable<QueryStatusesOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStatusesOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="subjectStatuses"></param>
        public QueryStatusesOutput(string? cursor = default, List<FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView> subjectStatuses = default)
        {
            this.Cursor = cursor;
            this.SubjectStatuses = subjectStatuses;
            this.Type = "tools.ozone.moderation.queryStatuses#QueryStatusesOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStatusesOutput"/> class.
        /// </summary>
        public QueryStatusesOutput()
        {
            this.Type = "tools.ozone.moderation.queryStatuses#QueryStatusesOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStatusesOutput"/> class.
        /// </summary>
        public QueryStatusesOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["subjectStatuses"] is not null) this.SubjectStatuses = obj["subjectStatuses"].Values.Select(n =>new FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
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
        public List<FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView> SubjectStatuses { get; set; }

        public const string RecordType = "tools.ozone.moderation.queryStatuses#QueryStatusesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>)SourceGenerationContext.Default.ToolsOzoneModerationQueryStatusesOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>)SourceGenerationContext.Default.ToolsOzoneModerationQueryStatusesOutput);
        }

        public static new QueryStatusesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>)SourceGenerationContext.Default.ToolsOzoneModerationQueryStatusesOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new QueryStatusesOutput FromCBORObject(CBORObject obj)
        {
            return new QueryStatusesOutput(obj);
        }

    }
}


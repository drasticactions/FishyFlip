// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Logs lifecycle event on a record subject. Normally captured by automod from the firehose and emitted to ozone for historical tracking.
    /// </summary>
    public partial class RecordEvent : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordEvent"/> class.
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="op">
        /// <br/> Known Values: <br/>
        /// create <br/>
        /// update <br/>
        /// delete <br/>
        /// </param>
        /// <param name="cid"></param>
        /// <param name="timestamp"></param>
        public RecordEvent(string? comment = default, string op = default, string? cid = default, DateTime? timestamp = default)
        {
            this.Comment = comment;
            this.Op = op;
            this.Cid = cid;
            this.Timestamp = timestamp;
            this.Type = "tools.ozone.moderation.defs#recordEvent";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordEvent"/> class.
        /// </summary>
        public RecordEvent()
        {
            this.Type = "tools.ozone.moderation.defs#recordEvent";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordEvent"/> class.
        /// </summary>
        public RecordEvent(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
            if (obj["op"] is not null) this.Op = obj["op"].AsString();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["timestamp"] is not null) this.Timestamp = obj["timestamp"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets the op.
        /// <br/> Known Values: <br/>
        /// create <br/>
        /// update <br/>
        /// delete <br/>
        /// </summary>
        [JsonPropertyName("op")]
        [JsonRequired]
        public string Op { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        public string? Cid { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonRequired]
        public DateTime? Timestamp { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#recordEvent";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordEvent>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordEvent>)SourceGenerationContext.Default.ToolsOzoneModerationRecordEvent)!;
        }

        public static RecordEvent FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordEvent>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordEvent>)SourceGenerationContext.Default.ToolsOzoneModerationRecordEvent)!;
        }
    }
}


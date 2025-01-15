// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Label
{
    public partial class QueryLabelsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryLabelsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="labels"></param>
        public QueryLabelsOutput(string? cursor = default, List<FishyFlip.Lexicon.Com.Atproto.Label.Label> labels = default)
        {
            this.Cursor = cursor;
            this.Labels = labels;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="QueryLabelsOutput"/> class.
        /// </summary>
        public QueryLabelsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="QueryLabelsOutput"/> class.
        /// </summary>
        public QueryLabelsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Label.Label(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        [JsonPropertyName("labels")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Atproto.Label.Label> Labels { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.label.queryLabels#QueryLabelsOutput";

        public const string RecordType = "com.atproto.label.queryLabels#QueryLabelsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Label.QueryLabelsOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Label.QueryLabelsOutput>)SourceGenerationContext.Default.ComAtprotoLabelQueryLabelsOutput)!;
        }

        public static QueryLabelsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Label.QueryLabelsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Label.QueryLabelsOutput>)SourceGenerationContext.Default.ComAtprotoLabelQueryLabelsOutput)!;
        }
    }
}


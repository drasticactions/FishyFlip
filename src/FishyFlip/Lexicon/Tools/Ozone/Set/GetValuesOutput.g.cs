// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Set
{
    public partial class GetValuesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetValuesOutput"/> class.
        /// </summary>
        /// <param name="set">
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Set.SetView"/> (tools.ozone.set.defs#setView)
        /// </param>
        /// <param name="values"></param>
        /// <param name="cursor"></param>
        public GetValuesOutput(FishyFlip.Lexicon.Tools.Ozone.Set.SetView set = default, List<string> values = default, string? cursor = default)
        {
            this.Set = set;
            this.Values = values;
            this.Cursor = cursor;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetValuesOutput"/> class.
        /// </summary>
        public GetValuesOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetValuesOutput"/> class.
        /// </summary>
        public GetValuesOutput(CBORObject obj)
        {
            if (obj["set"] is not null) this.Set = new FishyFlip.Lexicon.Tools.Ozone.Set.SetView(obj["set"]);
            if (obj["values"] is not null) this.Values = obj["values"].Values.Select(n =>n.AsString()).ToList();
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
        }

        /// <summary>
        /// Gets or sets the set.
        /// <br/> <see cref="FishyFlip.Lexicon.Tools.Ozone.Set.SetView"/> (tools.ozone.set.defs#setView)
        /// </summary>
        [JsonPropertyName("set")]
        [JsonRequired]
        public FishyFlip.Lexicon.Tools.Ozone.Set.SetView Set { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        [JsonPropertyName("values")]
        [JsonRequired]
        public List<string> Values { get; set; }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.set.getValues#GetValuesOutput";

        public const string RecordType = "tools.ozone.set.getValues#GetValuesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Set.GetValuesOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Set.GetValuesOutput>)SourceGenerationContext.Default.ToolsOzoneSetGetValuesOutput)!;
        }

        public static GetValuesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Set.GetValuesOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Set.GetValuesOutput>)SourceGenerationContext.Default.ToolsOzoneSetGetValuesOutput)!;
        }
    }
}


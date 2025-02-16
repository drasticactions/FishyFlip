// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Set
{
    public partial class QuerySetsOutput : ATObject, IBatchItem
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySetsOutput"/> class.
        /// </summary>
        /// <param name="sets"></param>
        /// <param name="cursor"></param>
        public QuerySetsOutput(List<FishyFlip.Lexicon.Tools.Ozone.Set.SetView> sets = default, string? cursor = default)
        {
            this.Sets = sets;
            this.Cursor = cursor;
            this.Type = "tools.ozone.set.querySets#QuerySetsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySetsOutput"/> class.
        /// </summary>
        public QuerySetsOutput()
        {
            this.Type = "tools.ozone.set.querySets#QuerySetsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySetsOutput"/> class.
        /// </summary>
        public QuerySetsOutput(CBORObject obj)
        {
            if (obj["sets"] is not null) this.Sets = obj["sets"].Values.Select(n =>new FishyFlip.Lexicon.Tools.Ozone.Set.SetView(n)).ToList();
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
        }

        /// <summary>
        /// Gets or sets the sets.
        /// </summary>
        [JsonPropertyName("sets")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Tools.Ozone.Set.SetView> Sets { get; set; }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        public const string RecordType = "tools.ozone.set.querySets#QuerySetsOutput";

        public static QuerySetsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Set.QuerySetsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Set.QuerySetsOutput>)SourceGenerationContext.Default.ToolsOzoneSetQuerySetsOutput)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetListsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="lists"></param>
        public GetListsOutput(string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Graph.ListView>? lists = default)
        {
            this.Cursor = cursor;
            this.Lists = lists;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetListsOutput"/> class.
        /// </summary>
        public GetListsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetListsOutput"/> class.
        /// </summary>
        public GetListsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["lists"] is not null) this.Lists = obj["lists"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Graph.ListView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the lists.
        /// </summary>
        [JsonPropertyName("lists")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Graph.ListView>? Lists { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.getLists#GetListsOutput";

        public const string RecordType = "app.bsky.graph.getLists#GetListsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>)SourceGenerationContext.Default.AppBskyGraphGetListsOutput)!;
        }

        public static GetListsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>)SourceGenerationContext.Default.AppBskyGraphGetListsOutput)!;
        }
    }
}


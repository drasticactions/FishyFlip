// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetListBlocksOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListBlocksOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="lists"></param>
        public GetListBlocksOutput(string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Graph.ListView> lists = default)
        {
            this.Cursor = cursor;
            this.Lists = lists;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetListBlocksOutput"/> class.
        /// </summary>
        public GetListBlocksOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetListBlocksOutput"/> class.
        /// </summary>
        public GetListBlocksOutput(CBORObject obj)
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
        public List<FishyFlip.Lexicon.App.Bsky.Graph.ListView> Lists { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.getListBlocks#GetListBlocksOutput";

        public const string RecordType = "app.bsky.graph.getListBlocks#GetListBlocksOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Graph.GetListBlocksOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetListBlocksOutput>)SourceGenerationContext.Default.AppBskyGraphGetListBlocksOutput)!;
        }

        public static GetListBlocksOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetListBlocksOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetListBlocksOutput>)SourceGenerationContext.Default.AppBskyGraphGetListBlocksOutput)!;
        }
    }
}


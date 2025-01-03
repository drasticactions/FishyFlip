// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class SearchStarterPacksOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchStarterPacksOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="starterPacks"></param>
        public SearchStarterPacksOutput(string? cursor = default, List<App.Bsky.Graph.StarterPackViewBasic>? starterPacks = default)
        {
            this.Cursor = cursor;
            this.StarterPacks = starterPacks;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchStarterPacksOutput"/> class.
        /// </summary>
        public SearchStarterPacksOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchStarterPacksOutput"/> class.
        /// </summary>
        public SearchStarterPacksOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["starterPacks"] is not null) this.StarterPacks = obj["starterPacks"].Values.Select(n =>new App.Bsky.Graph.StarterPackViewBasic(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the starterPacks.
        /// </summary>
        [JsonPropertyName("starterPacks")]
        [JsonRequired]
        public List<App.Bsky.Graph.StarterPackViewBasic>? StarterPacks { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.searchStarterPacks#SearchStarterPacksOutput";

        public const string RecordType = "app.bsky.graph.searchStarterPacks#SearchStarterPacksOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.SearchStarterPacksOutput>(this, (JsonTypeInfo<App.Bsky.Graph.SearchStarterPacksOutput>)SourceGenerationContext.Default.AppBskyGraphSearchStarterPacksOutput)!;
        }

        public static SearchStarterPacksOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.SearchStarterPacksOutput>(json, (JsonTypeInfo<App.Bsky.Graph.SearchStarterPacksOutput>)SourceGenerationContext.Default.AppBskyGraphSearchStarterPacksOutput)!;
        }
    }
}


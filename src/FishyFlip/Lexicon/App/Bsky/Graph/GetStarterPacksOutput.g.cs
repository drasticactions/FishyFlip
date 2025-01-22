// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetStarterPacksOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStarterPacksOutput"/> class.
        /// </summary>
        /// <param name="starterPacks"></param>
        public GetStarterPacksOutput(List<FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic> starterPacks = default)
        {
            this.StarterPacks = starterPacks;
            this.Type = "app.bsky.graph.getStarterPacks#GetStarterPacksOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetStarterPacksOutput"/> class.
        /// </summary>
        public GetStarterPacksOutput()
        {
            this.Type = "app.bsky.graph.getStarterPacks#GetStarterPacksOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetStarterPacksOutput"/> class.
        /// </summary>
        public GetStarterPacksOutput(CBORObject obj)
        {
            if (obj["starterPacks"] is not null) this.StarterPacks = obj["starterPacks"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the starterPacks.
        /// </summary>
        [JsonPropertyName("starterPacks")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic> StarterPacks { get; set; }

        public const string RecordType = "app.bsky.graph.getStarterPacks#GetStarterPacksOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPacksOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPacksOutput>)SourceGenerationContext.Default.AppBskyGraphGetStarterPacksOutput)!;
        }

        public static GetStarterPacksOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPacksOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPacksOutput>)SourceGenerationContext.Default.AppBskyGraphGetStarterPacksOutput)!;
        }
    }
}


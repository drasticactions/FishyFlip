// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetMutesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMutesOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="mutes"></param>
        public GetMutesOutput(string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> mutes = default)
        {
            this.Cursor = cursor;
            this.Mutes = mutes;
            this.Type = "app.bsky.graph.getMutes#GetMutesOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetMutesOutput"/> class.
        /// </summary>
        public GetMutesOutput()
        {
            this.Type = "app.bsky.graph.getMutes#GetMutesOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetMutesOutput"/> class.
        /// </summary>
        public GetMutesOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["mutes"] is not null) this.Mutes = obj["mutes"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the mutes.
        /// </summary>
        [JsonPropertyName("mutes")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> Mutes { get; set; }

        public const string RecordType = "app.bsky.graph.getMutes#GetMutesOutput";

        public static GetMutesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetMutesOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetMutesOutput>)SourceGenerationContext.Default.AppBskyGraphGetMutesOutput)!;
        }
    }
}


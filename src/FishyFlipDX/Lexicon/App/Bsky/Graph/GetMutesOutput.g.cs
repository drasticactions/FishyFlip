// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetMutesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMutesOutput"/> class.
        /// </summary>
        public GetMutesOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetMutesOutput"/> class.
        /// </summary>
        public GetMutesOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["mutes"] is not null) this.Mutes = obj["mutes"].Values.Select(n => n is not null ? new App.Bsky.Actor.ProfileView(n) : null).ToList();
        }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        [JsonPropertyName("mutes")]
        [JsonRequired]
        public List<App.Bsky.Actor.ProfileView?>? Mutes { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.getMutes#GetMutesOutput";

        public const string RecordType = "app.bsky.graph.getMutes#GetMutesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.GetMutesOutput>(this, (JsonTypeInfo<App.Bsky.Graph.GetMutesOutput>)SourceGenerationContext.Default.AppBskyGraphGetMutesOutput)!;
        }

        public static GetMutesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.GetMutesOutput>(json, (JsonTypeInfo<App.Bsky.Graph.GetMutesOutput>)SourceGenerationContext.Default.AppBskyGraphGetMutesOutput)!;
        }
    }
}

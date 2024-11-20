// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class UnmuteThreadInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UnmuteThreadInput"/> class.
        /// </summary>
        public UnmuteThreadInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnmuteThreadInput"/> class.
        /// </summary>
        public UnmuteThreadInput(CBORObject obj)
        {
            if (obj["root"] is not null) this.Root = obj["root"].ToATUri();
        }

        [JsonPropertyName("root")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Root { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.unmuteThread#UnmuteThreadInput";

        public const string RecordType = "app.bsky.graph.unmuteThread#UnmuteThreadInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.UnmuteThreadInput>(this, (JsonTypeInfo<App.Bsky.Graph.UnmuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphUnmuteThreadInput)!;
        }

        public static UnmuteThreadInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.UnmuteThreadInput>(json, (JsonTypeInfo<App.Bsky.Graph.UnmuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphUnmuteThreadInput)!;
        }
    }
}

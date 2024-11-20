// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class MuteThreadInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MuteThreadInput"/> class.
        /// </summary>
        public MuteThreadInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteThreadInput"/> class.
        /// </summary>
        public MuteThreadInput(CBORObject obj)
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
        public override string Type => "app.bsky.graph.muteThread#MuteThreadInput";

        public const string RecordType = "app.bsky.graph.muteThread#MuteThreadInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.MuteThreadInput>(this, (JsonTypeInfo<App.Bsky.Graph.MuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphMuteThreadInput)!;
        }

        public static MuteThreadInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.MuteThreadInput>(json, (JsonTypeInfo<App.Bsky.Graph.MuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphMuteThreadInput)!;
        }
    }
}

// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GeneratorViewerState : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorViewerState"/> class.
        /// </summary>
        public GeneratorViewerState(FishyFlip.Models.ATUri? like = default)
        {
            this.Like = like;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorViewerState"/> class.
        /// </summary>
        public GeneratorViewerState()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorViewerState"/> class.
        /// </summary>
        public GeneratorViewerState(CBORObject obj)
        {
            if (obj["like"] is not null) this.Like = obj["like"].ToATUri();
        }

        [JsonPropertyName("like")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Like { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.defs#generatorViewerState";

        public const string RecordType = "app.bsky.feed.defs#generatorViewerState";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.GeneratorViewerState>(this, (JsonTypeInfo<App.Bsky.Feed.GeneratorViewerState>)SourceGenerationContext.Default.AppBskyFeedGeneratorViewerState)!;
        }

        public static GeneratorViewerState FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.GeneratorViewerState>(json, (JsonTypeInfo<App.Bsky.Feed.GeneratorViewerState>)SourceGenerationContext.Default.AppBskyFeedGeneratorViewerState)!;
        }
    }
}


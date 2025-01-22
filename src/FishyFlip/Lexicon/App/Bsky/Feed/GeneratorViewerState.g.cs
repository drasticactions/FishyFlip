// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GeneratorViewerState : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorViewerState"/> class.
        /// </summary>
        /// <param name="like"></param>
        public GeneratorViewerState(FishyFlip.Models.ATUri? like = default)
        {
            this.Like = like;
            this.Type = "app.bsky.feed.defs#generatorViewerState";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorViewerState"/> class.
        /// </summary>
        public GeneratorViewerState()
        {
            this.Type = "app.bsky.feed.defs#generatorViewerState";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorViewerState"/> class.
        /// </summary>
        public GeneratorViewerState(CBORObject obj)
        {
            if (obj["like"] is not null) this.Like = obj["like"].ToATUri();
        }

        /// <summary>
        /// Gets or sets the like.
        /// </summary>
        [JsonPropertyName("like")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Like { get; set; }

        public const string RecordType = "app.bsky.feed.defs#generatorViewerState";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorViewerState>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorViewerState>)SourceGenerationContext.Default.AppBskyFeedGeneratorViewerState)!;
        }

        public static GeneratorViewerState FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorViewerState>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorViewerState>)SourceGenerationContext.Default.AppBskyFeedGeneratorViewerState)!;
        }
    }
}


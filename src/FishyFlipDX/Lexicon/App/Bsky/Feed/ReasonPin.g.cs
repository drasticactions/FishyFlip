// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class ReasonPin : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonPin"/> class.
        /// </summary>
        public ReasonPin()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonPin"/> class.
        /// </summary>
        public ReasonPin(CBORObject obj)
        {
        }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.defs#reasonPin";

        public const string RecordType = "app.bsky.feed.defs#reasonPin";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.ReasonPin>(this, (JsonTypeInfo<App.Bsky.Feed.ReasonPin>)SourceGenerationContext.Default.AppBskyFeedReasonPin)!;
        }

        public static ReasonPin FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.ReasonPin>(json, (JsonTypeInfo<App.Bsky.Feed.ReasonPin>)SourceGenerationContext.Default.AppBskyFeedReasonPin)!;
        }
    }
}


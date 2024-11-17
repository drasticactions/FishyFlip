// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Disables embedding of this post.
    /// </summary>
    public partial class DisableRule : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DisableRule"/> class.
        /// </summary>
        public DisableRule()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DisableRule"/> class.
        /// </summary>
        public DisableRule(CBORObject obj)
        {
        }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.postgate#disableRule";

        public const string RecordType = "app.bsky.feed.postgate#disableRule";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.DisableRule>(this, (JsonTypeInfo<App.Bsky.Feed.DisableRule>)SourceGenerationContext.Default.AppBskyFeedDisableRule)!;
        }

        public static DisableRule FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.DisableRule>(json, (JsonTypeInfo<App.Bsky.Feed.DisableRule>)SourceGenerationContext.Default.AppBskyFeedDisableRule)!;
        }
    }
}


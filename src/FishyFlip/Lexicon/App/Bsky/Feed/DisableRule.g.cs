// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

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
            this.Type = "app.bsky.feed.postgate#disableRule";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DisableRule"/> class.
        /// </summary>
        public DisableRule(CBORObject obj)
        {
        }

        public const string RecordType = "app.bsky.feed.postgate#disableRule";

        public static DisableRule FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.DisableRule>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.DisableRule>)SourceGenerationContext.Default.AppBskyFeedDisableRule)!;
        }
    }
}


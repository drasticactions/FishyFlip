// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class SkeletonReasonPin : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonReasonPin"/> class.
        /// </summary>
        public SkeletonReasonPin()
        {
            this.Type = "app.bsky.feed.defs#skeletonReasonPin";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonReasonPin"/> class.
        /// </summary>
        public SkeletonReasonPin(CBORObject obj)
        {
        }

        public const string RecordType = "app.bsky.feed.defs#skeletonReasonPin";

        public static SkeletonReasonPin FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.SkeletonReasonPin>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.SkeletonReasonPin>)SourceGenerationContext.Default.AppBskyFeedSkeletonReasonPin)!;
        }
    }
}


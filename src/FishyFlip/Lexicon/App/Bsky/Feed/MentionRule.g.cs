// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Allow replies from actors mentioned in your post.
    /// </summary>
    public partial class MentionRule : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MentionRule"/> class.
        /// </summary>
        public MentionRule()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MentionRule"/> class.
        /// </summary>
        public MentionRule(CBORObject obj)
        {
        }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.threadgate#mentionRule";

        public const string RecordType = "app.bsky.feed.threadgate#mentionRule";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Feed.MentionRule>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.MentionRule>)SourceGenerationContext.Default.AppBskyFeedMentionRule)!;
        }

        public static MentionRule FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.MentionRule>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.MentionRule>)SourceGenerationContext.Default.AppBskyFeedMentionRule)!;
        }
    }
}


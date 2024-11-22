// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GetFeedOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="feed"></param>
        public GetFeedOutput(string? cursor = default, List<App.Bsky.Feed.FeedViewPost>? feed = default)
        {
            this.Cursor = cursor;
            this.Feed = feed;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedOutput"/> class.
        /// </summary>
        public GetFeedOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedOutput"/> class.
        /// </summary>
        public GetFeedOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["feed"] is not null) this.Feed = obj["feed"].Values.Select(n =>new App.Bsky.Feed.FeedViewPost(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the feed.
        /// </summary>
        [JsonPropertyName("feed")]
        [JsonRequired]
        public List<App.Bsky.Feed.FeedViewPost>? Feed { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.getFeed#GetFeedOutput";

        public const string RecordType = "app.bsky.feed.getFeed#GetFeedOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.GetFeedOutput>(this, (JsonTypeInfo<App.Bsky.Feed.GetFeedOutput>)SourceGenerationContext.Default.AppBskyFeedGetFeedOutput)!;
        }

        public static GetFeedOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.GetFeedOutput>(json, (JsonTypeInfo<App.Bsky.Feed.GetFeedOutput>)SourceGenerationContext.Default.AppBskyFeedGetFeedOutput)!;
        }
    }
}


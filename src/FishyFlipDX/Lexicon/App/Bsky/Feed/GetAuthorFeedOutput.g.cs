// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GetAuthorFeedOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorFeedOutput"/> class.
        /// </summary>
        public GetAuthorFeedOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorFeedOutput"/> class.
        /// </summary>
        public GetAuthorFeedOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["feed"] is not null) this.Feed = obj["feed"].Values.Select(n => n is not null ? new App.Bsky.Feed.FeedViewPost(n) : null).ToList();
        }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        [JsonPropertyName("feed")]
        [JsonRequired]
        public List<App.Bsky.Feed.FeedViewPost?>? Feed { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.getAuthorFeed#GetAuthorFeedOutput";

        public const string RecordType = "app.bsky.feed.getAuthorFeed#GetAuthorFeedOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.GetAuthorFeedOutput>(this, (JsonTypeInfo<App.Bsky.Feed.GetAuthorFeedOutput>)SourceGenerationContext.Default.AppBskyFeedGetAuthorFeedOutput)!;
        }

        public static GetAuthorFeedOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.GetAuthorFeedOutput>(json, (JsonTypeInfo<App.Bsky.Feed.GetAuthorFeedOutput>)SourceGenerationContext.Default.AppBskyFeedGetAuthorFeedOutput)!;
        }
    }
}

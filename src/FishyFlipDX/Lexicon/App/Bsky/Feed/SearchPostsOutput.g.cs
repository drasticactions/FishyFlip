// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class SearchPostsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPostsOutput"/> class.
        /// </summary>
        public SearchPostsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPostsOutput"/> class.
        /// </summary>
        public SearchPostsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["hitsTotal"] is not null) this.HitsTotal = obj["hitsTotal"].AsInt64Value();
            if (obj["posts"] is not null) this.Posts = obj["posts"].Values.Select(n => n is not null ? new App.Bsky.Feed.PostView(n) : null).ToList();
        }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Count of search hits. Optional, may be rounded/truncated, and may not be possible to paginate through all hits.
        /// </summary>
        [JsonPropertyName("hitsTotal")]
        public long? HitsTotal { get; set; }

        [JsonPropertyName("posts")]
        [JsonRequired]
        public List<App.Bsky.Feed.PostView?>? Posts { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.searchPosts#SearchPostsOutput";

        public const string RecordType = "app.bsky.feed.searchPosts#SearchPostsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.SearchPostsOutput>(this, (JsonTypeInfo<App.Bsky.Feed.SearchPostsOutput>)SourceGenerationContext.Default.AppBskyFeedSearchPostsOutput)!;
        }

        public static SearchPostsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.SearchPostsOutput>(json, (JsonTypeInfo<App.Bsky.Feed.SearchPostsOutput>)SourceGenerationContext.Default.AppBskyFeedSearchPostsOutput)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GetFeedOutput : ATObject, ICBOREncodable<GetFeedOutput>, IJsonEncodable<GetFeedOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="feed"></param>
        public GetFeedOutput(string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Feed.FeedViewPost> feed = default)
        {
            this.Cursor = cursor;
            this.Feed = feed;
            this.Type = "app.bsky.feed.getFeed#GetFeedOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedOutput"/> class.
        /// </summary>
        public GetFeedOutput()
        {
            this.Type = "app.bsky.feed.getFeed#GetFeedOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedOutput"/> class.
        /// </summary>
        public GetFeedOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["feed"] is not null) this.Feed = obj["feed"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Feed.FeedViewPost(n)).ToList();
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
        public List<FishyFlip.Lexicon.App.Bsky.Feed.FeedViewPost> Feed { get; set; }

        public const string RecordType = "app.bsky.feed.getFeed#GetFeedOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedOutput>)SourceGenerationContext.Default.AppBskyFeedGetFeedOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedOutput>)SourceGenerationContext.Default.AppBskyFeedGetFeedOutput);
        }

        public static new GetFeedOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedOutput>)SourceGenerationContext.Default.AppBskyFeedGetFeedOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetFeedOutput FromCBORObject(CBORObject obj)
        {
            return new GetFeedOutput(obj);
        }

    }
}


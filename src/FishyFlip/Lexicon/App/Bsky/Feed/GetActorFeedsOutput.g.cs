// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GetActorFeedsOutput : ATObject, ICBOREncodable<GetActorFeedsOutput>, IJsonEncodable<GetActorFeedsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorFeedsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="feeds"></param>
        public GetActorFeedsOutput(string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView> feeds = default)
        {
            this.Cursor = cursor;
            this.Feeds = feeds;
            this.Type = "app.bsky.feed.getActorFeeds#GetActorFeedsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorFeedsOutput"/> class.
        /// </summary>
        public GetActorFeedsOutput()
        {
            this.Type = "app.bsky.feed.getActorFeeds#GetActorFeedsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorFeedsOutput"/> class.
        /// </summary>
        public GetActorFeedsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["feeds"] is not null) this.Feeds = obj["feeds"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the feeds.
        /// </summary>
        [JsonPropertyName("feeds")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView> Feeds { get; set; }

        public const string RecordType = "app.bsky.feed.getActorFeeds#GetActorFeedsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetActorFeedsOutput>)SourceGenerationContext.Default.AppBskyFeedGetActorFeedsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetActorFeedsOutput>)SourceGenerationContext.Default.AppBskyFeedGetActorFeedsOutput);
        }

        public static new GetActorFeedsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.GetActorFeedsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetActorFeedsOutput>)SourceGenerationContext.Default.AppBskyFeedGetActorFeedsOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetActorFeedsOutput FromCBORObject(CBORObject obj)
        {
            return new GetActorFeedsOutput(obj);
        }

    }
}


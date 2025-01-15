// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GetFeedGeneratorsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedGeneratorsOutput"/> class.
        /// </summary>
        /// <param name="feeds"></param>
        public GetFeedGeneratorsOutput(List<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView> feeds = default)
        {
            this.Feeds = feeds;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedGeneratorsOutput"/> class.
        /// </summary>
        public GetFeedGeneratorsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeedGeneratorsOutput"/> class.
        /// </summary>
        public GetFeedGeneratorsOutput(CBORObject obj)
        {
            if (obj["feeds"] is not null) this.Feeds = obj["feeds"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the feeds.
        /// </summary>
        [JsonPropertyName("feeds")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView> Feeds { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.getFeedGenerators#GetFeedGeneratorsOutput";

        public const string RecordType = "app.bsky.feed.getFeedGenerators#GetFeedGeneratorsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorsOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorsOutput>)SourceGenerationContext.Default.AppBskyFeedGetFeedGeneratorsOutput)!;
        }

        public static GetFeedGeneratorsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorsOutput>)SourceGenerationContext.Default.AppBskyFeedGetFeedGeneratorsOutput)!;
        }
    }
}


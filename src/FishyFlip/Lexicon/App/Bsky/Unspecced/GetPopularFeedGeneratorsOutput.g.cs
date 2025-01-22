// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class GetPopularFeedGeneratorsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPopularFeedGeneratorsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="feeds"></param>
        public GetPopularFeedGeneratorsOutput(string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView> feeds = default)
        {
            this.Cursor = cursor;
            this.Feeds = feeds;
            this.Type = "app.bsky.unspecced.getPopularFeedGenerators#GetPopularFeedGeneratorsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPopularFeedGeneratorsOutput"/> class.
        /// </summary>
        public GetPopularFeedGeneratorsOutput()
        {
            this.Type = "app.bsky.unspecced.getPopularFeedGenerators#GetPopularFeedGeneratorsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPopularFeedGeneratorsOutput"/> class.
        /// </summary>
        public GetPopularFeedGeneratorsOutput(CBORObject obj)
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

        public const string RecordType = "app.bsky.unspecced.getPopularFeedGenerators#GetPopularFeedGeneratorsOutput";

        public static GetPopularFeedGeneratorsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.GetPopularFeedGeneratorsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetPopularFeedGeneratorsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetPopularFeedGeneratorsOutput)!;
        }
    }
}


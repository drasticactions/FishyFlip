// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class GetTrendingTopicsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingTopicsOutput"/> class.
        /// </summary>
        /// <param name="topics"></param>
        /// <param name="suggested"></param>
        public GetTrendingTopicsOutput(List<App.Bsky.Unspecced.TrendingTopic>? topics = default, List<App.Bsky.Unspecced.TrendingTopic>? suggested = default)
        {
            this.Topics = topics;
            this.Suggested = suggested;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingTopicsOutput"/> class.
        /// </summary>
        public GetTrendingTopicsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingTopicsOutput"/> class.
        /// </summary>
        public GetTrendingTopicsOutput(CBORObject obj)
        {
            if (obj["topics"] is not null) this.Topics = obj["topics"].Values.Select(n =>new App.Bsky.Unspecced.TrendingTopic(n)).ToList();
            if (obj["suggested"] is not null) this.Suggested = obj["suggested"].Values.Select(n =>new App.Bsky.Unspecced.TrendingTopic(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        [JsonPropertyName("topics")]
        [JsonRequired]
        public List<App.Bsky.Unspecced.TrendingTopic>? Topics { get; set; }

        /// <summary>
        /// Gets or sets the suggested.
        /// </summary>
        [JsonPropertyName("suggested")]
        [JsonRequired]
        public List<App.Bsky.Unspecced.TrendingTopic>? Suggested { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.unspecced.getTrendingTopics#GetTrendingTopicsOutput";

        public const string RecordType = "app.bsky.unspecced.getTrendingTopics#GetTrendingTopicsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Unspecced.GetTrendingTopicsOutput>(this, (JsonTypeInfo<App.Bsky.Unspecced.GetTrendingTopicsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendingTopicsOutput)!;
        }

        public static GetTrendingTopicsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Unspecced.GetTrendingTopicsOutput>(json, (JsonTypeInfo<App.Bsky.Unspecced.GetTrendingTopicsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendingTopicsOutput)!;
        }
    }
}


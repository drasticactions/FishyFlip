// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class GetTrendingTopicsOutput : ATObject, ICBOREncodable<GetTrendingTopicsOutput>, IJsonEncodable<GetTrendingTopicsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingTopicsOutput"/> class.
        /// </summary>
        /// <param name="topics"></param>
        /// <param name="suggested"></param>
        public GetTrendingTopicsOutput(List<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic> topics = default, List<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic> suggested = default)
        {
            this.Topics = topics;
            this.Suggested = suggested;
            this.Type = "app.bsky.unspecced.getTrendingTopics#GetTrendingTopicsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingTopicsOutput"/> class.
        /// </summary>
        public GetTrendingTopicsOutput()
        {
            this.Type = "app.bsky.unspecced.getTrendingTopics#GetTrendingTopicsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingTopicsOutput"/> class.
        /// </summary>
        public GetTrendingTopicsOutput(CBORObject obj)
        {
            if (obj["topics"] is not null) this.Topics = obj["topics"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic(n)).ToList();
            if (obj["suggested"] is not null) this.Suggested = obj["suggested"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        [JsonPropertyName("topics")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic> Topics { get; set; }

        /// <summary>
        /// Gets or sets the suggested.
        /// </summary>
        [JsonPropertyName("suggested")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendingTopic> Suggested { get; set; }

        public const string RecordType = "app.bsky.unspecced.getTrendingTopics#GetTrendingTopicsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendingTopicsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendingTopicsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendingTopicsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendingTopicsOutput);
        }

        public static new GetTrendingTopicsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendingTopicsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendingTopicsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendingTopicsOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetTrendingTopicsOutput FromCBORObject(CBORObject obj)
        {
            return new GetTrendingTopicsOutput(obj);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class GetTrendsOutput : ATObject, ICBOREncodable<GetTrendsOutput>, IJsonEncodable<GetTrendsOutput>, IParsable<GetTrendsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendsOutput"/> class.
        /// </summary>
        /// <param name="trends"></param>
        public GetTrendsOutput(List<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendView> trends = default)
        {
            this.Trends = trends;
            this.Type = "app.bsky.unspecced.getTrends#GetTrendsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendsOutput"/> class.
        /// </summary>
        public GetTrendsOutput()
        {
            this.Type = "app.bsky.unspecced.getTrends#GetTrendsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendsOutput"/> class.
        /// </summary>
        public GetTrendsOutput(CBORObject obj)
        {
            if (obj["trends"] is not null) this.Trends = obj["trends"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Unspecced.TrendView(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the trends.
        /// </summary>
        [JsonPropertyName("trends")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Unspecced.TrendView> Trends { get; set; }

        public const string RecordType = "app.bsky.unspecced.getTrends#GetTrendsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendsOutput);
        }

        public static new GetTrendsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendsOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetTrendsOutput FromCBORObject(CBORObject obj)
        {
            return new GetTrendsOutput(obj);
        }

        /// <inheritdoc/>
        public static GetTrendsOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetTrendsOutput>(s, (JsonTypeInfo<GetTrendsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendsOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetTrendsOutput result)
        {
            result = JsonSerializer.Deserialize<GetTrendsOutput>(s, (JsonTypeInfo<GetTrendsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTrendsOutput);
            return result != null;
        }
    }
}


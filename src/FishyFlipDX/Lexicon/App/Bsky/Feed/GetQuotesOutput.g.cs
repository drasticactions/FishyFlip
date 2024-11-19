// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GetQuotesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuotesOutput"/> class.
        /// </summary>
        public GetQuotesOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuotesOutput"/> class.
        /// </summary>
        public GetQuotesOutput(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["posts"] is not null) this.Posts = obj["posts"].Values.Select(n => n is not null ? new App.Bsky.Feed.PostView(n) : null).ToList();
        }

        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Uri { get; set; }

        [JsonPropertyName("cid")]
        public string? Cid { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        [JsonPropertyName("posts")]
        [JsonRequired]
        public List<App.Bsky.Feed.PostView?>? Posts { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.getQuotes#GetQuotesOutput";

        public const string RecordType = "app.bsky.feed.getQuotes#GetQuotesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.GetQuotesOutput>(this, (JsonTypeInfo<App.Bsky.Feed.GetQuotesOutput>)SourceGenerationContext.Default.AppBskyFeedGetQuotesOutput)!;
        }

        public static GetQuotesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.GetQuotesOutput>(json, (JsonTypeInfo<App.Bsky.Feed.GetQuotesOutput>)SourceGenerationContext.Default.AppBskyFeedGetQuotesOutput)!;
        }
    }
}


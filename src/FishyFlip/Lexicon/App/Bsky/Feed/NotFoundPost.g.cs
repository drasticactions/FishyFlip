// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class NotFoundPost : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundPost"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="notFound"></param>
        public NotFoundPost(FishyFlip.Models.ATUri? uri = default, bool? notFound = default)
        {
            this.Uri = uri;
            this.NotFound = notFound;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundPost"/> class.
        /// </summary>
        public NotFoundPost()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundPost"/> class.
        /// </summary>
        public NotFoundPost(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["notFound"] is not null) this.NotFound = obj["notFound"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Uri { get; set; }

        /// <summary>
        /// Gets or sets the notFound.
        /// </summary>
        [JsonPropertyName("notFound")]
        [JsonRequired]
        public bool? NotFound { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.defs#notFoundPost";

        public const string RecordType = "app.bsky.feed.defs#notFoundPost";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.NotFoundPost>(this, (JsonTypeInfo<App.Bsky.Feed.NotFoundPost>)SourceGenerationContext.Default.AppBskyFeedNotFoundPost)!;
        }

        public static NotFoundPost FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.NotFoundPost>(json, (JsonTypeInfo<App.Bsky.Feed.NotFoundPost>)SourceGenerationContext.Default.AppBskyFeedNotFoundPost)!;
        }
    }
}

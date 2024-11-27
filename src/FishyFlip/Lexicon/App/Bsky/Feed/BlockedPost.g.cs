// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class BlockedPost : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockedPost"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="blocked"></param>
        /// <param name="author">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedAuthor"/> (app.bsky.feed.defs#blockedAuthor)
        /// </param>
        public BlockedPost(FishyFlip.Models.ATUri? uri = default, bool? blocked = default, App.Bsky.Feed.BlockedAuthor? author = default)
        {
            this.Uri = uri;
            this.Blocked = blocked;
            this.Author = author;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlockedPost"/> class.
        /// </summary>
        public BlockedPost()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlockedPost"/> class.
        /// </summary>
        public BlockedPost(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["blocked"] is not null) this.Blocked = obj["blocked"].AsBoolean();
            if (obj["author"] is not null) this.Author = new App.Bsky.Feed.BlockedAuthor(obj["author"]);
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Uri { get; set; }

        /// <summary>
        /// Gets or sets the blocked.
        /// </summary>
        [JsonPropertyName("blocked")]
        [JsonRequired]
        public bool? Blocked { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedAuthor"/> (app.bsky.feed.defs#blockedAuthor)
        /// </summary>
        [JsonPropertyName("author")]
        [JsonRequired]
        public App.Bsky.Feed.BlockedAuthor? Author { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.defs#blockedPost";

        public const string RecordType = "app.bsky.feed.defs#blockedPost";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.BlockedPost>(this, (JsonTypeInfo<App.Bsky.Feed.BlockedPost>)SourceGenerationContext.Default.AppBskyFeedBlockedPost)!;
        }

        public static BlockedPost FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.BlockedPost>(json, (JsonTypeInfo<App.Bsky.Feed.BlockedPost>)SourceGenerationContext.Default.AppBskyFeedBlockedPost)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class BlockedPost : ATObject, ICBOREncodable<BlockedPost>, IJsonEncodable<BlockedPost>, IParsable<BlockedPost>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockedPost"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="blocked"></param>
        /// <param name="author">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedAuthor"/> (app.bsky.feed.defs#blockedAuthor)
        /// </param>
        public BlockedPost(FishyFlip.Models.ATUri uri = default, bool blocked = default, FishyFlip.Lexicon.App.Bsky.Feed.BlockedAuthor author = default)
        {
            this.Uri = uri;
            this.Blocked = blocked;
            this.Author = author;
            this.Type = "app.bsky.feed.defs#blockedPost";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlockedPost"/> class.
        /// </summary>
        public BlockedPost()
        {
            this.Type = "app.bsky.feed.defs#blockedPost";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlockedPost"/> class.
        /// </summary>
        public BlockedPost(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["blocked"] is not null) this.Blocked = obj["blocked"].AsBoolean();
            if (obj["author"] is not null) this.Author = new FishyFlip.Lexicon.App.Bsky.Feed.BlockedAuthor(obj["author"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri Uri { get; set; }

        /// <summary>
        /// Gets or sets the blocked.
        /// </summary>
        [JsonPropertyName("blocked")]
        [JsonRequired]
        public bool Blocked { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedAuthor"/> (app.bsky.feed.defs#blockedAuthor)
        /// </summary>
        [JsonPropertyName("author")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Feed.BlockedAuthor Author { get; set; }

        public const string RecordType = "app.bsky.feed.defs#blockedPost";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost>)SourceGenerationContext.Default.AppBskyFeedBlockedPost);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost>)SourceGenerationContext.Default.AppBskyFeedBlockedPost);
        }

        public static new BlockedPost FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost>)SourceGenerationContext.Default.AppBskyFeedBlockedPost)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new BlockedPost FromCBORObject(CBORObject obj)
        {
            return new BlockedPost(obj);
        }

        /// <inheritdoc/>
        public static BlockedPost Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<BlockedPost>(s, (JsonTypeInfo<BlockedPost>)SourceGenerationContext.Default.AppBskyFeedBlockedPost)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out BlockedPost result)
        {
            result = JsonSerializer.Deserialize<BlockedPost>(s, (JsonTypeInfo<BlockedPost>)SourceGenerationContext.Default.AppBskyFeedBlockedPost);
            return result != null;
        }
    }
}


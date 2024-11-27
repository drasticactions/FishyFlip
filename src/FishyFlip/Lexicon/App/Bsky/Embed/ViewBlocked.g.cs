// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class ViewBlocked : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBlocked"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="blocked"></param>
        /// <param name="author">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedAuthor"/> (app.bsky.feed.defs#blockedAuthor)
        /// </param>
        public ViewBlocked(FishyFlip.Models.ATUri? uri = default, bool? blocked = default, App.Bsky.Feed.BlockedAuthor? author = default)
        {
            this.Uri = uri;
            this.Blocked = blocked;
            this.Author = author;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBlocked"/> class.
        /// </summary>
        public ViewBlocked()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBlocked"/> class.
        /// </summary>
        public ViewBlocked(CBORObject obj)
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
        public override string Type => "app.bsky.embed.record#viewBlocked";

        public const string RecordType = "app.bsky.embed.record#viewBlocked";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Embed.ViewBlocked>(this, (JsonTypeInfo<App.Bsky.Embed.ViewBlocked>)SourceGenerationContext.Default.AppBskyEmbedViewBlocked)!;
        }

        public static ViewBlocked FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Embed.ViewBlocked>(json, (JsonTypeInfo<App.Bsky.Embed.ViewBlocked>)SourceGenerationContext.Default.AppBskyEmbedViewBlocked)!;
        }
    }
}


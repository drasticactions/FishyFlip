// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Record containing a Bluesky post.
    /// </summary>
    public partial class Post : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        public Post()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        public Post(CBORObject obj)
        {
            if (obj["text"] is not null) this.Text = obj["text"].AsString();
            if (obj["entities"] is not null) this.Entities = obj["entities"].Values.Select(n => n is not null ? new App.Bsky.Feed.Entity(n) : null).ToList();
            if (obj["facets"] is not null) this.Facets = obj["facets"].Values.Select(n => n is not null ? new App.Bsky.Richtext.Facet(n) : null).ToList();
            if (obj["reply"] is not null) this.Reply = new App.Bsky.Feed.ReplyRefDef(obj["reply"]);
            if (obj["embed"] is not null) this.Embed = obj["embed"].ToATObject();
            if (obj["langs"] is not null) this.Langs = obj["langs"].Values.Select(n => n is not null ? n.AsString() : default).ToList();
            if (obj["labels"] is not null) this.Labels = obj["labels"].ToATObject();
            if (obj["tags"] is not null) this.Tags = obj["tags"].Values.Select(n => n is not null ? n.AsString() : default).ToList();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// The primary post content. May be an empty string, if there are embeds.
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// DEPRECATED: replaced by app.bsky.richtext.facet.
        /// </summary>
        [JsonPropertyName("entities")]
        public List<App.Bsky.Feed.Entity?>? Entities { get; set; }

        /// <summary>
        /// Annotations of text (mentions, URLs, hashtags, etc)
        /// </summary>
        [JsonPropertyName("facets")]
        public List<App.Bsky.Richtext.Facet?>? Facets { get; set; }

        [JsonPropertyName("reply")]
        public App.Bsky.Feed.ReplyRefDef? Reply { get; set; }

        [JsonPropertyName("embed")]
        public ATObject? Embed { get; set; }

        /// <summary>
        /// Indicates human language of post primary text content.
        /// </summary>
        [JsonPropertyName("langs")]
        public List<string?>? Langs { get; set; }

        /// <summary>
        /// Self-label values for this post. Effectively content warnings.
        /// </summary>
        [JsonPropertyName("labels")]
        public ATObject? Labels { get; set; }

        /// <summary>
        /// Additional hashtags, in addition to any included in post text and facets.
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string?>? Tags { get; set; }

        /// <summary>
        /// Client-declared timestamp when this post was originally created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.post";

        public const string RecordType = "app.bsky.feed.post";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.Post>(this, (JsonTypeInfo<App.Bsky.Feed.Post>)SourceGenerationContext.Default.AppBskyFeedPost)!;
        }

        public static Post FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.Post>(json, (JsonTypeInfo<App.Bsky.Feed.Post>)SourceGenerationContext.Default.AppBskyFeedPost)!;
        }
    }
}

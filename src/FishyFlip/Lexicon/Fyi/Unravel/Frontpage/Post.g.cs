// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Fyi.Unravel.Frontpage
{
    /// <summary>
    /// Record containing a Frontpage post.
    /// </summary>
    public partial class Post : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        /// <param name="title">The title of the post.</param>
        /// <param name="url">The URL of the post.</param>
        /// <param name="createdAt">Client-declared timestamp when this post was originally created.</param>
        public Post(string? title, string? url, DateTime? createdAt = default)
        {
            this.Title = title;
            this.Url = url;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "fyi.unravel.frontpage.post";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        public Post()
        {
            this.Type = "fyi.unravel.frontpage.post";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        public Post(CBORObject obj)
        {
            if (obj["title"] is not null) this.Title = obj["title"].AsString();
            if (obj["url"] is not null) this.Url = obj["url"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the title.
        /// <br/> The title of the post.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// <br/> The URL of the post.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// <br/> Client-declared timestamp when this post was originally created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "fyi.unravel.frontpage.post";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Fyi.Unravel.Frontpage.Post>(this, (JsonTypeInfo<FishyFlip.Lexicon.Fyi.Unravel.Frontpage.Post>)SourceGenerationContext.Default.FyiUnravelFrontpagePost)!;
        }

        public static Post FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Fyi.Unravel.Frontpage.Post>(json, (JsonTypeInfo<FishyFlip.Lexicon.Fyi.Unravel.Frontpage.Post>)SourceGenerationContext.Default.FyiUnravelFrontpagePost)!;
        }
    }
}


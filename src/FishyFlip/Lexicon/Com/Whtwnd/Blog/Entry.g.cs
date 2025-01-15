// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    /// <summary>
    /// A declaration of a post.
    /// </summary>
    public partial class Entry : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="createdAt"></param>
        /// <param name="title"></param>
        /// <param name="ogp">
        /// <see cref="FishyFlip.Lexicon.Com.Whtwnd.Blog.Ogp"/> (com.whtwnd.blog.defs#ogp)
        /// </param>
        /// <param name="theme"></param>
        /// <param name="blobs"></param>
        /// <param name="visibility">Tells the visibility of the article to AppView.</param>
        public Entry(string? content, DateTime? createdAt = default, string? title = default, FishyFlip.Lexicon.Com.Whtwnd.Blog.Ogp? ogp = default, string? theme = default, List<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlobMetadata>? blobs = default, string? visibility = default)
        {
            this.Content = content;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Title = title;
            this.Ogp = ogp;
            this.Theme = theme;
            this.Blobs = blobs;
            this.Visibility = visibility;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class.
        /// </summary>
        public Entry()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class.
        /// </summary>
        public Entry(CBORObject obj)
        {
            if (obj["content"] is not null) this.Content = obj["content"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["title"] is not null) this.Title = obj["title"].AsString();
            if (obj["ogp"] is not null) this.Ogp = new FishyFlip.Lexicon.Com.Whtwnd.Blog.Ogp(obj["ogp"]);
            if (obj["theme"] is not null) this.Theme = obj["theme"].AsString();
            if (obj["blobs"] is not null) this.Blobs = obj["blobs"].Values.Select(n =>new FishyFlip.Lexicon.Com.Whtwnd.Blog.BlobMetadata(n)).ToList();
            if (obj["visibility"] is not null) this.Visibility = obj["visibility"].AsString();
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the ogp.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Whtwnd.Blog.Ogp"/> (com.whtwnd.blog.defs#ogp)
        /// </summary>
        [JsonPropertyName("ogp")]
        public FishyFlip.Lexicon.Com.Whtwnd.Blog.Ogp? Ogp { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        [JsonPropertyName("theme")]
        public string? Theme { get; set; }

        /// <summary>
        /// Gets or sets the blobs.
        /// </summary>
        [JsonPropertyName("blobs")]
        public List<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlobMetadata>? Blobs { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// <br/> Tells the visibility of the article to AppView.
        /// </summary>
        [JsonPropertyName("visibility")]
        public string? Visibility { get; set; } = "public";

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.whtwnd.blog.entry";

        public const string RecordType = "com.whtwnd.blog.entry";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.Entry>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.Entry>)SourceGenerationContext.Default.ComWhtwndBlogEntry)!;
        }

        public static Entry FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.Entry>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.Entry>)SourceGenerationContext.Default.ComWhtwndBlogEntry)!;
        }
    }
}


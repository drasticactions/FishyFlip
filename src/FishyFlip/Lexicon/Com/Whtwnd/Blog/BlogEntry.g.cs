// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class BlogEntry : ATObject, ICBOREncodable<BlogEntry>, IJsonEncodable<BlogEntry>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogEntry"/> class.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="createdAt"></param>
        public BlogEntry(string content = default, DateTime? createdAt = default)
        {
            this.Content = content;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "com.whtwnd.blog.defs#blogEntry";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlogEntry"/> class.
        /// </summary>
        public BlogEntry()
        {
            this.Type = "com.whtwnd.blog.defs#blogEntry";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlogEntry"/> class.
        /// </summary>
        public BlogEntry(CBORObject obj)
        {
            if (obj["content"] is not null) this.Content = obj["content"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [JsonPropertyName("content")]
        [JsonRequired]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "com.whtwnd.blog.defs#blogEntry";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlogEntry>)SourceGenerationContext.Default.ComWhtwndBlogBlogEntry);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlogEntry>)SourceGenerationContext.Default.ComWhtwndBlogBlogEntry);
        }

        public static new BlogEntry FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlogEntry>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlogEntry>)SourceGenerationContext.Default.ComWhtwndBlogBlogEntry)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new BlogEntry FromCBORObject(CBORObject obj)
        {
            return new BlogEntry(obj);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class Comment : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="entryUri"></param>
        public Comment(string? content = default, FishyFlip.Models.ATUri? entryUri = default)
        {
            this.Content = content;
            this.EntryUri = entryUri;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment(CBORObject obj)
        {
            if (obj["content"] is not null) this.Content = obj["content"].AsString();
            if (obj["entryUri"] is not null) this.EntryUri = obj["entryUri"].ToATUri();
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [JsonPropertyName("content")]
        [JsonRequired]
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the entryUri.
        /// </summary>
        [JsonPropertyName("entryUri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? EntryUri { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.whtwnd.blog.defs#comment";

        public const string RecordType = "com.whtwnd.blog.defs#comment";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Whtwnd.Blog.Comment>(this, (JsonTypeInfo<Com.Whtwnd.Blog.Comment>)SourceGenerationContext.Default.ComWhtwndBlogComment)!;
        }

        public static Comment FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Whtwnd.Blog.Comment>(json, (JsonTypeInfo<Com.Whtwnd.Blog.Comment>)SourceGenerationContext.Default.ComWhtwndBlogComment)!;
        }
    }
}


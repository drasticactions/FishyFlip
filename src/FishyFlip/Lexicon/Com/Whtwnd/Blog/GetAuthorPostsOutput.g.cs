// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class GetAuthorPostsOutput : ATObject, ICBOREncodable<GetAuthorPostsOutput>, IJsonEncodable<GetAuthorPostsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorPostsOutput"/> class.
        /// </summary>
        /// <param name="post"></param>
        public GetAuthorPostsOutput(List<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlogEntry> post = default)
        {
            this.Post = post;
            this.Type = "com.whtwnd.blog.getAuthorPosts#GetAuthorPostsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorPostsOutput"/> class.
        /// </summary>
        public GetAuthorPostsOutput()
        {
            this.Type = "com.whtwnd.blog.getAuthorPosts#GetAuthorPostsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorPostsOutput"/> class.
        /// </summary>
        public GetAuthorPostsOutput(CBORObject obj)
        {
            if (obj["post"] is not null) this.Post = obj["post"].Values.Select(n =>new FishyFlip.Lexicon.Com.Whtwnd.Blog.BlogEntry(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the post.
        /// </summary>
        [JsonPropertyName("post")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlogEntry> Post { get; set; }

        public const string RecordType = "com.whtwnd.blog.getAuthorPosts#GetAuthorPostsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetAuthorPostsOutput>)SourceGenerationContext.Default.ComWhtwndBlogGetAuthorPostsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetAuthorPostsOutput>)SourceGenerationContext.Default.ComWhtwndBlogGetAuthorPostsOutput);
        }

        public static new GetAuthorPostsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetAuthorPostsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetAuthorPostsOutput>)SourceGenerationContext.Default.ComWhtwndBlogGetAuthorPostsOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetAuthorPostsOutput FromCBORObject(CBORObject obj)
        {
            return new GetAuthorPostsOutput(obj);
        }

    }
}


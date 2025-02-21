// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Fyi.Unravel.Frontpage
{
    /// <summary>
    /// Record containing a Frontpage comment.
    /// </summary>
    public partial class Comment : ATObject, ICBOREncodable<Comment>, IJsonEncodable<Comment>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="content">The content of the comment.</param>
        /// <param name="post">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="createdAt">Client-declared timestamp when this comment was originally created.</param>
        /// <param name="parent">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        public Comment(string? content, Com.Atproto.Repo.StrongRef? post, DateTime? createdAt = default, Com.Atproto.Repo.StrongRef? parent = default)
        {
            this.Content = content;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Parent = parent;
            this.Post = post;
            this.Type = "fyi.unravel.frontpage.comment";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
            this.Type = "fyi.unravel.frontpage.comment";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment(CBORObject obj)
        {
            if (obj["content"] is not null) this.Content = obj["content"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["parent"] is not null) this.Parent = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["parent"]);
            if (obj["post"] is not null) this.Post = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["post"]);
        }

        /// <summary>
        /// Gets or sets the content.
        /// <br/> The content of the comment.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// <br/> Client-declared timestamp when this comment was originally created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the parent.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("parent")]
        public Com.Atproto.Repo.StrongRef? Parent { get; set; }

        /// <summary>
        /// Gets or sets the post.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("post")]
        public Com.Atproto.Repo.StrongRef? Post { get; set; }

        public const string RecordType = "fyi.unravel.frontpage.comment";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Fyi.Unravel.Frontpage.Comment>)SourceGenerationContext.Default.FyiUnravelFrontpageComment);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Fyi.Unravel.Frontpage.Comment>)SourceGenerationContext.Default.FyiUnravelFrontpageComment);
        }

        public static new Comment FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Fyi.Unravel.Frontpage.Comment>(json, (JsonTypeInfo<FishyFlip.Lexicon.Fyi.Unravel.Frontpage.Comment>)SourceGenerationContext.Default.FyiUnravelFrontpageComment)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Comment FromCBORObject(CBORObject obj)
        {
            return new Comment(obj);
        }

    }
}


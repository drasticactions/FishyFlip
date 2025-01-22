// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Buzz.Bookhive
{
    /// <summary>
    /// Record containing a Bookhive comment.
    /// </summary>
    public partial class Buzz : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Buzz"/> class.
        /// </summary>
        /// <param name="comment">The content of the comment.</param>
        /// <param name="parent">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="book">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="createdAt">Client-declared timestamp when this comment was originally created.</param>
        public Buzz(string? comment, Com.Atproto.Repo.StrongRef? parent, Com.Atproto.Repo.StrongRef? book, DateTime? createdAt = default)
        {
            this.Comment = comment;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Parent = parent;
            this.Book = book;
            this.Type = "buzz.bookhive.buzz";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Buzz"/> class.
        /// </summary>
        public Buzz()
        {
            this.Type = "buzz.bookhive.buzz";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Buzz"/> class.
        /// </summary>
        public Buzz(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["parent"] is not null) this.Parent = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["parent"]);
            if (obj["book"] is not null) this.Book = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["book"]);
        }

        /// <summary>
        /// Gets or sets the comment.
        /// <br/> The content of the comment.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

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
        /// Gets or sets the book.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("book")]
        public Com.Atproto.Repo.StrongRef? Book { get; set; }

        public const string RecordType = "buzz.bookhive.buzz";

        public static Buzz FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Buzz.Bookhive.Buzz>(json, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.Buzz>)SourceGenerationContext.Default.BuzzBookhiveBuzz)!;
        }
    }
}


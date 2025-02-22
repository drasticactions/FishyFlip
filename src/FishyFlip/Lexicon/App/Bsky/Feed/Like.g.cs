// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Record declaring a 'like' of a piece of subject content.
    /// </summary>
    public partial class Like : ATObject, ICBOREncodable<Like>, IJsonEncodable<Like>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Like"/> class.
        /// </summary>
        /// <param name="subject">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="createdAt"></param>
        public Like(Com.Atproto.Repo.StrongRef? subject, DateTime? createdAt = default)
        {
            this.Subject = subject;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "app.bsky.feed.like";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Like"/> class.
        /// </summary>
        public Like()
        {
            this.Type = "app.bsky.feed.like";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Like"/> class.
        /// </summary>
        public Like(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["subject"]);
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("subject")]
        public Com.Atproto.Repo.StrongRef? Subject { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "app.bsky.feed.like";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Like>)SourceGenerationContext.Default.AppBskyFeedLike);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Like>)SourceGenerationContext.Default.AppBskyFeedLike);
        }

        public static new Like FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.Like>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Like>)SourceGenerationContext.Default.AppBskyFeedLike)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Like FromCBORObject(CBORObject obj)
        {
            return new Like(obj);
        }

    }
}


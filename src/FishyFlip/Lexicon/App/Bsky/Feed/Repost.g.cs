// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Record representing a 'repost' of an existing Bluesky post.
    /// </summary>
    public partial class Repost : ATObject, ICBOREncodable<Repost>, IJsonEncodable<Repost>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Repost"/> class.
        /// </summary>
        /// <param name="subject">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="createdAt"></param>
        public Repost(Com.Atproto.Repo.StrongRef? subject, DateTime? createdAt = default)
        {
            this.Subject = subject;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "app.bsky.feed.repost";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Repost"/> class.
        /// </summary>
        public Repost()
        {
            this.Type = "app.bsky.feed.repost";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Repost"/> class.
        /// </summary>
        public Repost(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["subject"]);
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
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

        public const string RecordType = "app.bsky.feed.repost";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Repost>)SourceGenerationContext.Default.AppBskyFeedRepost);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Repost>)SourceGenerationContext.Default.AppBskyFeedRepost);
        }

        public static new Repost FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.Repost>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Repost>)SourceGenerationContext.Default.AppBskyFeedRepost)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Repost FromCBORObject(CBORObject obj)
        {
            return new Repost(obj);
        }

    }
}


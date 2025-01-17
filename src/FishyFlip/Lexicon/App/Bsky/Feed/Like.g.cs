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
    public partial class Like : ATObject
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
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Like"/> class.
        /// </summary>
        public Like()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Like"/> class.
        /// </summary>
        public Like(CBORObject obj)
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

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.like";

        public const string RecordType = "app.bsky.feed.like";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Feed.Like>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Like>)SourceGenerationContext.Default.AppBskyFeedLike)!;
        }

        public static Like FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.Like>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Like>)SourceGenerationContext.Default.AppBskyFeedLike)!;
        }
    }
}


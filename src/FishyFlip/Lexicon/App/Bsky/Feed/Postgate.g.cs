// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Record defining interaction rules for a post. The record key (rkey) of the postgate record must match the record key of the post, and that record must be in the same repository.
    /// </summary>
    public partial class Postgate : ATObject, ICBOREncodable<Postgate>, IJsonEncodable<Postgate>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Postgate"/> class.
        /// </summary>
        /// <param name="post">Reference (AT-URI) to the post record.</param>
        /// <param name="createdAt"></param>
        /// <param name="detachedEmbeddingUris">List of AT-URIs embedding this post that the author has detached from.</param>
        /// <param name="embeddingRules">List of rules defining who can embed this post. If value is an empty array or is undefined, no particular rules apply and anyone can embed.
        /// <br/> Union Types: <br/>
        /// #disableRule <br/>
        /// </param>
        public Postgate(FishyFlip.Models.ATUri? post, DateTime? createdAt = default, List<FishyFlip.Models.ATUri>? detachedEmbeddingUris = default, List<FishyFlip.Lexicon.App.Bsky.Feed.DisableRule>? embeddingRules = default)
        {
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Post = post;
            this.DetachedEmbeddingUris = detachedEmbeddingUris;
            this.EmbeddingRules = embeddingRules;
            this.Type = "app.bsky.feed.postgate";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Postgate"/> class.
        /// </summary>
        public Postgate()
        {
            this.Type = "app.bsky.feed.postgate";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Postgate"/> class.
        /// </summary>
        public Postgate(CBORObject obj)
        {
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["post"] is not null) this.Post = obj["post"].ToATUri();
            if (obj["detachedEmbeddingUris"] is not null) this.DetachedEmbeddingUris = obj["detachedEmbeddingUris"].Values.Select(n =>n.ToATUri()!).ToList();
            if (obj["embeddingRules"] is not null) this.EmbeddingRules = obj["embeddingRules"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Feed.DisableRule(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the post.
        /// <br/> Reference (AT-URI) to the post record.
        /// </summary>
        [JsonPropertyName("post")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Post { get; set; }

        /// <summary>
        /// Gets or sets the detachedEmbeddingUris.
        /// <br/> List of AT-URIs embedding this post that the author has detached from.
        /// </summary>
        [JsonPropertyName("detachedEmbeddingUris")]
        public List<FishyFlip.Models.ATUri>? DetachedEmbeddingUris { get; set; }

        /// <summary>
        /// Gets or sets the embeddingRules.
        /// <br/> List of rules defining who can embed this post. If value is an empty array or is undefined, no particular rules apply and anyone can embed.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.DisableRule"/> (app.bsky.feed.postgate#disableRule) <br/>
        /// </summary>
        [JsonPropertyName("embeddingRules")]
        public List<FishyFlip.Lexicon.App.Bsky.Feed.DisableRule>? EmbeddingRules { get; set; }

        public const string RecordType = "app.bsky.feed.postgate";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Postgate>)SourceGenerationContext.Default.AppBskyFeedPostgate);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Postgate>)SourceGenerationContext.Default.AppBskyFeedPostgate);
        }

        public static new Postgate FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.Postgate>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Postgate>)SourceGenerationContext.Default.AppBskyFeedPostgate)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Postgate FromCBORObject(CBORObject obj)
        {
            return new Postgate(obj);
        }

    }
}


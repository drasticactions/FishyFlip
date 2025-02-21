// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    /// <summary>
    /// Record declaring a 'block' relationship against another account. NOTE: blocks are public in Bluesky; see blog posts for details.
    /// </summary>
    public partial class Block : ATObject, ICBOREncodable<Block>, IJsonEncodable<Block>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        /// <param name="subject">DID of the account to be blocked.</param>
        /// <param name="createdAt"></param>
        public Block(FishyFlip.Models.ATDid? subject, DateTime? createdAt = default)
        {
            this.Subject = subject;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "app.bsky.graph.block";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        public Block()
        {
            this.Type = "app.bsky.graph.block";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        public Block(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATDid();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> DID of the account to be blocked.
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Subject { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "app.bsky.graph.block";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.Block>)SourceGenerationContext.Default.AppBskyGraphBlock);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.Block>)SourceGenerationContext.Default.AppBskyGraphBlock);
        }

        public static new Block FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.Block>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.Block>)SourceGenerationContext.Default.AppBskyGraphBlock)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Block FromCBORObject(CBORObject obj)
        {
            return new Block(obj);
        }

    }
}


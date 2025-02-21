// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    /// <summary>
    /// An oekaki post.
    /// </summary>
    public partial class Oekaki : ATObject, ICBOREncodable<Oekaki>, IJsonEncodable<Oekaki>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Oekaki"/> class.
        /// </summary>
        /// <param name="image">
        /// com.shinolabs.pinksea.defs#image <br/>
        /// </param>
        /// <param name="createdAt">The timestamp of creation.</param>
        /// <param name="tags">An array of tags this image had.</param>
        /// <param name="inResponseTo">What this oekaki post is a response to.
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="nsfw">Is this oekaki NSFW?</param>
        public Oekaki(FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Image? image, DateTime? createdAt = default, List<string>? tags = default, Com.Atproto.Repo.StrongRef? inResponseTo = default, bool? nsfw = default)
        {
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Image = image;
            this.Tags = tags;
            this.InResponseTo = inResponseTo;
            this.Nsfw = nsfw;
            this.Type = "com.shinolabs.pinksea.oekaki";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Oekaki"/> class.
        /// </summary>
        public Oekaki()
        {
            this.Type = "com.shinolabs.pinksea.oekaki";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Oekaki"/> class.
        /// </summary>
        public Oekaki(CBORObject obj)
        {
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["image"] is not null) this.Image = new FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Image(obj["image"]);
            if (obj["tags"] is not null) this.Tags = obj["tags"].Values.Select(n =>n.AsString()).ToList();
            if (obj["inResponseTo"] is not null) this.InResponseTo = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["inResponseTo"]);
            if (obj["nsfw"] is not null) this.Nsfw = obj["nsfw"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the createdAt.
        /// <br/> The timestamp of creation.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the image.
        /// com.shinolabs.pinksea.defs#image <br/>
        /// </summary>
        [JsonPropertyName("image")]
        public FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Image? Image { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// <br/> An array of tags this image had.
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Gets or sets the inResponseTo.
        /// <br/> What this oekaki post is a response to.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("inResponseTo")]
        public Com.Atproto.Repo.StrongRef? InResponseTo { get; set; }

        /// <summary>
        /// Gets or sets the nsfw.
        /// <br/> Is this oekaki NSFW?
        /// </summary>
        [JsonPropertyName("nsfw")]
        public bool? Nsfw { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.oekaki";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Oekaki>)SourceGenerationContext.Default.ComShinolabsPinkseaOekaki);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Oekaki>)SourceGenerationContext.Default.ComShinolabsPinkseaOekaki);
        }

        public static new Oekaki FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Oekaki>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Oekaki>)SourceGenerationContext.Default.ComShinolabsPinkseaOekaki)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Oekaki FromCBORObject(CBORObject obj)
        {
            return new Oekaki(obj);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    /// <summary>
    /// A hydrated oekaki post returned from the PinkSea app view.
    /// </summary>
    public partial class HydratedOekaki : ATObject, ICBOREncodable<HydratedOekaki>, IJsonEncodable<HydratedOekaki>, IParsable<HydratedOekaki>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HydratedOekaki"/> class.
        /// </summary>
        /// <param name="author">
        /// com.shinolabs.pinksea.defs#author <br/>
        /// </param>
        /// <param name="image">The image link.</param>
        /// <param name="at">The AT protocol link.</param>
        /// <param name="cid">The oekaki CID.</param>
        /// <param name="creationTime">The creation time.</param>
        /// <param name="nsfw">Is this oekaki NSFW?</param>
        /// <param name="tags">An array of tags this image had.</param>
        /// <param name="alt">Alt text description of the image, for accessibility.</param>
        public HydratedOekaki(FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Author author = default, string image = default, string at = default, string cid = default, DateTime? creationTime = default, bool nsfw = default, List<string>? tags = default, string? alt = default)
        {
            this.Author = author;
            this.Image = image;
            this.At = at;
            this.Cid = cid;
            this.CreationTime = creationTime;
            this.Nsfw = nsfw;
            this.Tags = tags;
            this.Alt = alt;
            this.Type = "com.shinolabs.pinksea.appViewDefs#hydratedOekaki";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="HydratedOekaki"/> class.
        /// </summary>
        public HydratedOekaki()
        {
            this.Type = "com.shinolabs.pinksea.appViewDefs#hydratedOekaki";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="HydratedOekaki"/> class.
        /// </summary>
        public HydratedOekaki(CBORObject obj)
        {
            if (obj["author"] is not null) this.Author = new FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Author(obj["author"]);
            if (obj["image"] is not null) this.Image = obj["image"].AsString();
            if (obj["at"] is not null) this.At = obj["at"].AsString();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["creationTime"] is not null) this.CreationTime = obj["creationTime"].ToDateTime();
            if (obj["nsfw"] is not null) this.Nsfw = obj["nsfw"].AsBoolean();
            if (obj["tags"] is not null) this.Tags = obj["tags"].Values.Select(n =>n.AsString()).ToList();
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the author.
        /// com.shinolabs.pinksea.defs#author <br/>
        /// </summary>
        [JsonPropertyName("author")]
        [JsonRequired]
        public FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Author Author { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// <br/> The image link.
        /// </summary>
        [JsonPropertyName("image")]
        [JsonRequired]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the at.
        /// <br/> The AT protocol link.
        /// </summary>
        [JsonPropertyName("at")]
        [JsonRequired]
        public string At { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// <br/> The oekaki CID.
        /// </summary>
        [JsonPropertyName("cid")]
        [JsonRequired]
        public string Cid { get; set; }

        /// <summary>
        /// Gets or sets the creationTime.
        /// <br/> The creation time.
        /// </summary>
        [JsonPropertyName("creationTime")]
        [JsonRequired]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the nsfw.
        /// <br/> Is this oekaki NSFW?
        /// </summary>
        [JsonPropertyName("nsfw")]
        [JsonRequired]
        public bool Nsfw { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// <br/> An array of tags this image had.
        /// </summary>
        [JsonPropertyName("tags")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Gets or sets the alt.
        /// <br/> Alt text description of the image, for accessibility.
        /// </summary>
        [JsonPropertyName("alt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Alt { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.appViewDefs#hydratedOekaki";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki>)SourceGenerationContext.Default.ComShinolabsPinkseaHydratedOekaki);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki>)SourceGenerationContext.Default.ComShinolabsPinkseaHydratedOekaki);
        }

        public static new HydratedOekaki FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki>)SourceGenerationContext.Default.ComShinolabsPinkseaHydratedOekaki)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new HydratedOekaki FromCBORObject(CBORObject obj)
        {
            return new HydratedOekaki(obj);
        }

        /// <inheritdoc/>
        public static HydratedOekaki Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<HydratedOekaki>(s, (JsonTypeInfo<HydratedOekaki>)SourceGenerationContext.Default.ComShinolabsPinkseaHydratedOekaki)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out HydratedOekaki result)
        {
            result = JsonSerializer.Deserialize<HydratedOekaki>(s, (JsonTypeInfo<HydratedOekaki>)SourceGenerationContext.Default.ComShinolabsPinkseaHydratedOekaki);
            return result != null;
        }
    }
}


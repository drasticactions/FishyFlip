// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    /// <summary>
    /// Record declaring of the existence of a feed generator, and containing metadata about it. The record can exist in any repository.
    /// </summary>
    public partial class Generator : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="descriptionFacets"></param>
        /// <param name="avatar"></param>
        /// <param name="acceptsInteractions">Declaration that a feed accepts feedback interactions from a client through app.bsky.feed.sendInteractions</param>
        /// <param name="labels">Self-label values
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels"/> (com.atproto.label.defs#selfLabels) <br/>
        /// </param>
        /// <param name="createdAt"></param>
        public Generator(FishyFlip.Models.ATDid? did, string? displayName, string? description = default, List<App.Bsky.Richtext.Facet>? descriptionFacets = default, Blob? avatar = default, bool? acceptsInteractions = default, Com.Atproto.Label.SelfLabels? labels = default, DateTime? createdAt = default)
        {
            this.Did = did;
            this.DisplayName = displayName;
            this.Description = description;
            this.DescriptionFacets = descriptionFacets;
            this.Avatar = avatar;
            this.AcceptsInteractions = acceptsInteractions;
            this.Labels = labels;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        public Generator()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        public Generator(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["descriptionFacets"] is not null) this.DescriptionFacets = obj["descriptionFacets"].Values.Select(n =>new App.Bsky.Richtext.Facet(n)).ToList();
            if (obj["avatar"] is not null) this.Avatar = new FishyFlip.Models.Blob(obj["avatar"]);
            if (obj["acceptsInteractions"] is not null) this.AcceptsInteractions = obj["acceptsInteractions"].AsBoolean();
            if (obj["labels"] is not null) this.Labels = new Com.Atproto.Label.SelfLabels(obj["labels"]);
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Gets or sets the displayName.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the descriptionFacets.
        /// </summary>
        [JsonPropertyName("descriptionFacets")]
        public List<App.Bsky.Richtext.Facet>? DescriptionFacets { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        [JsonPropertyName("avatar")]
        public Blob? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the acceptsInteractions.
        /// <br/> Declaration that a feed accepts feedback interactions from a client through app.bsky.feed.sendInteractions
        /// </summary>
        [JsonPropertyName("acceptsInteractions")]
        public bool? AcceptsInteractions { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// <br/> Self-label values
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels"/> (com.atproto.label.defs#selfLabels) <br/>
        /// </summary>
        [JsonPropertyName("labels")]
        public Com.Atproto.Label.SelfLabels? Labels { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.generator";

        public const string RecordType = "app.bsky.feed.generator";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Feed.Generator>(this, (JsonTypeInfo<App.Bsky.Feed.Generator>)SourceGenerationContext.Default.AppBskyFeedGenerator)!;
        }

        public static Generator FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Feed.Generator>(json, (JsonTypeInfo<App.Bsky.Feed.Generator>)SourceGenerationContext.Default.AppBskyFeedGenerator)!;
        }
    }
}


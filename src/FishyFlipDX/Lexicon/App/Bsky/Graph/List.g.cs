// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    /// <summary>
    /// Record representing a list of accounts (actors). Scope includes both moderation-oriented lists and curration-oriented lists.
    /// </summary>
    public partial class List : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class.
        /// </summary>
        public List(string? purpose, string? name, string? description = default, List<App.Bsky.Richtext.Facet>? descriptionFacets = default, Blob? avatar = default, Com.Atproto.Label.SelfLabels? labels = default, DateTime? createdAt = default)
        {
            this.Purpose = purpose;
            this.Name = name;
            this.Description = description;
            this.DescriptionFacets = descriptionFacets;
            this.Avatar = avatar;
            this.Labels = labels;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class.
        /// </summary>
        public List()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class.
        /// </summary>
        public List(CBORObject obj)
        {
            if (obj["purpose"] is not null) this.Purpose = obj["purpose"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["descriptionFacets"] is not null) this.DescriptionFacets = obj["descriptionFacets"].Values.Select(n =>new App.Bsky.Richtext.Facet(n)).ToList();
            if (obj["avatar"] is not null) this.Avatar = new FishyFlip.Models.Blob(obj["avatar"]);
            if (obj["labels"] is not null) this.Labels = new Com.Atproto.Label.SelfLabels(obj["labels"]);
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Defines the purpose of the list (aka, moderation-oriented or curration-oriented)
        /// </summary>
        [JsonPropertyName("purpose")]
        public string? Purpose { get; set; }

        /// <summary>
        /// Display name for list; can not be empty.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("descriptionFacets")]
        public List<App.Bsky.Richtext.Facet>? DescriptionFacets { get; set; }

        [JsonPropertyName("avatar")]
        public Blob? Avatar { get; set; }

        [JsonPropertyName("labels")]
        public Com.Atproto.Label.SelfLabels? Labels { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.list";

        public const string RecordType = "app.bsky.graph.list";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.List>(this, (JsonTypeInfo<App.Bsky.Graph.List>)SourceGenerationContext.Default.AppBskyGraphList)!;
        }

        public static List FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.List>(json, (JsonTypeInfo<App.Bsky.Graph.List>)SourceGenerationContext.Default.AppBskyGraphList)!;
        }
    }
}


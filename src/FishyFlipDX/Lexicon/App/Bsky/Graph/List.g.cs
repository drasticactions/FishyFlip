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
            if (obj["descriptionFacets"] is not null) this.DescriptionFacets = obj["descriptionFacets"].Values.Select(n => n is not null ? new App.Bsky.Richtext.Facet(n) : null).ToList();
            if (obj["avatar"] is not null) this.Avatar = new FishyFlip.Models.Blob(obj["avatar"]);
            if (obj["labels"] is not null) this.Labels = obj["labels"].ToATObject();
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
        public List<App.Bsky.Richtext.Facet?>? DescriptionFacets { get; set; }

        [JsonPropertyName("avatar")]
        public Blob? Avatar { get; set; }

        [JsonPropertyName("labels")]
        public ATObject? Labels { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

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


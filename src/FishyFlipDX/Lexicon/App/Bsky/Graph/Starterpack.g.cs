// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    /// <summary>
    /// Record defining a starter pack of actors and feeds for new users.
    /// </summary>
    public partial class Starterpack : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Starterpack"/> class.
        /// </summary>
        public Starterpack(string? name, FishyFlip.Models.ATUri? list, string? description = default, List<App.Bsky.Richtext.Facet>? descriptionFacets = default, List<App.Bsky.Graph.FeedItem>? feeds = default, DateTime? createdAt = default)
        {
            this.Name = name;
            this.Description = description;
            this.DescriptionFacets = descriptionFacets;
            this.List = list;
            this.Feeds = feeds;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Starterpack"/> class.
        /// </summary>
        public Starterpack()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Starterpack"/> class.
        /// </summary>
        public Starterpack(CBORObject obj)
        {
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["descriptionFacets"] is not null) this.DescriptionFacets = obj["descriptionFacets"].Values.Select(n =>new App.Bsky.Richtext.Facet(n)).ToList();
            if (obj["list"] is not null) this.List = obj["list"].ToATUri();
            if (obj["feeds"] is not null) this.Feeds = obj["feeds"].Values.Select(n =>new App.Bsky.Graph.FeedItem(n)).ToList();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Display name for starter pack; can not be empty.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("descriptionFacets")]
        public List<App.Bsky.Richtext.Facet>? DescriptionFacets { get; set; }

        /// <summary>
        /// Reference (AT-URI) to the list record.
        /// </summary>
        [JsonPropertyName("list")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? List { get; set; }

        [JsonPropertyName("feeds")]
        public List<App.Bsky.Graph.FeedItem>? Feeds { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.starterpack";

        public const string RecordType = "app.bsky.graph.starterpack";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.Starterpack>(this, (JsonTypeInfo<App.Bsky.Graph.Starterpack>)SourceGenerationContext.Default.AppBskyGraphStarterpack)!;
        }

        public static Starterpack FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.Starterpack>(json, (JsonTypeInfo<App.Bsky.Graph.Starterpack>)SourceGenerationContext.Default.AppBskyGraphStarterpack)!;
        }
    }
}


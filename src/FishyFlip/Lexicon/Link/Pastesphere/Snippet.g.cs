// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Link.Pastesphere
{
    /// <summary>
    /// Record representing a text snippet.
    /// </summary>
    public partial class Snippet : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Snippet"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="body"></param>
        /// <param name="description"></param>
        /// <param name="createdAt"></param>
        public Snippet(string? title, string? type, string? body, string? description = default, DateTime? createdAt = default)
        {
            this.Title = title;
            this.Description = description;
            this.TypeValue = type;
            this.Body = body;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Snippet"/> class.
        /// </summary>
        public Snippet()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Snippet"/> class.
        /// </summary>
        public Snippet(CBORObject obj)
        {
            if (obj["title"] is not null) this.Title = obj["title"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["type"] is not null) this.TypeValue = obj["type"].AsString();
            if (obj["body"] is not null) this.Body = obj["body"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonPropertyName("type")]
        public string? TypeValue { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        [JsonPropertyName("body")]
        public string? Body { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "link.pastesphere.snippet";

        public const string RecordType = "link.pastesphere.snippet";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Link.Pastesphere.Snippet>(this, (JsonTypeInfo<FishyFlip.Lexicon.Link.Pastesphere.Snippet>)SourceGenerationContext.Default.LinkPastesphereSnippet)!;
        }

        public static Snippet FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Link.Pastesphere.Snippet>(json, (JsonTypeInfo<FishyFlip.Lexicon.Link.Pastesphere.Snippet>)SourceGenerationContext.Default.LinkPastesphereSnippet)!;
        }
    }
}


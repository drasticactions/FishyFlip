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
    public partial class Snippet : ATObject, ICBOREncodable<Snippet>, IJsonEncodable<Snippet>, IParsable<Snippet>
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
            this.Type = "link.pastesphere.snippet";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Snippet"/> class.
        /// </summary>
        public Snippet()
        {
            this.Type = "link.pastesphere.snippet";
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
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TypeValue { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        [JsonPropertyName("body")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Body { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "link.pastesphere.snippet";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Link.Pastesphere.Snippet>)SourceGenerationContext.Default.LinkPastesphereSnippet);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Link.Pastesphere.Snippet>)SourceGenerationContext.Default.LinkPastesphereSnippet);
        }

        public static new Snippet FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Link.Pastesphere.Snippet>(json, (JsonTypeInfo<FishyFlip.Lexicon.Link.Pastesphere.Snippet>)SourceGenerationContext.Default.LinkPastesphereSnippet)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Snippet FromCBORObject(CBORObject obj)
        {
            return new Snippet(obj);
        }

        /// <inheritdoc/>
        public static Snippet Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Snippet>(s, (JsonTypeInfo<Snippet>)SourceGenerationContext.Default.LinkPastesphereSnippet)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Snippet result)
        {
            result = JsonSerializer.Deserialize<Snippet>(s, (JsonTypeInfo<Snippet>)SourceGenerationContext.Default.LinkPastesphereSnippet);
            return result != null;
        }
    }
}


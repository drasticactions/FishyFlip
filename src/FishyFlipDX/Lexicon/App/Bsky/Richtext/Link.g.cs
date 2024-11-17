// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Richtext
{
    /// <summary>
    /// Facet feature for a URL. The text URL may have been simplified or truncated, but the facet reference should be a complete URL.
    /// </summary>
    public partial class Link : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].AsString();
        }

        [JsonPropertyName("uri")]
        [JsonRequired]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.richtext.facet#link";

        public const string RecordType = "app.bsky.richtext.facet#link";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Richtext.Link>(this, (JsonTypeInfo<App.Bsky.Richtext.Link>)SourceGenerationContext.Default.AppBskyRichtextLink)!;
        }

        public static Link FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Richtext.Link>(json, (JsonTypeInfo<App.Bsky.Richtext.Link>)SourceGenerationContext.Default.AppBskyRichtextLink)!;
        }
    }
}


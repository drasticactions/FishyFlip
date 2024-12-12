// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Social.Psky.Richtext
{
    /// <summary>
    /// Facet feature for a URL. The text URL may have been simplified or truncated, but the facet reference should be a complete URL.
    /// </summary>
    public partial class Link : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="uri"></param>
        public Link(string? uri = default)
        {
            this.Uri = uri;
        }


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

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "social.psky.richtext.facet#link";

        public const string RecordType = "social.psky.richtext.facet#link";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Social.Psky.Richtext.Link>(this, (JsonTypeInfo<Social.Psky.Richtext.Link>)SourceGenerationContext.Default.SocialPskyRichtextLink)!;
        }

        public static Link FromJson(string json)
        {
            return JsonSerializer.Deserialize<Social.Psky.Richtext.Link>(json, (JsonTypeInfo<Social.Psky.Richtext.Link>)SourceGenerationContext.Default.SocialPskyRichtextLink)!;
        }
    }
}

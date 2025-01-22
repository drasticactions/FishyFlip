// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

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
        public Link(string uri = default)
        {
            this.Uri = uri;
            this.Type = "social.psky.richtext.facet#link";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link()
        {
            this.Type = "social.psky.richtext.facet#link";
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
        public string Uri { get; set; }

        public const string RecordType = "social.psky.richtext.facet#link";

        public static Link FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Social.Psky.Richtext.Link>(json, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Richtext.Link>)SourceGenerationContext.Default.SocialPskyRichtextLink)!;
        }
    }
}


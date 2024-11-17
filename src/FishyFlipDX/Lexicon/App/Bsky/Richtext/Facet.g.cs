// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Richtext
{
    /// <summary>
    /// Annotation of a sub-string within rich text.
    /// </summary>
    public partial class Facet : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet(CBORObject obj)
        {
            if (obj["index"] is not null) this.Index = new App.Bsky.Richtext.ByteSlice(obj["index"]);
            if (obj["features"] is not null) this.Features = obj["features"].Values.Select(n => n is not null ? n.ToATObject() : null).ToList();
        }

        [JsonPropertyName("index")]
        [JsonRequired]
        public App.Bsky.Richtext.ByteSlice? Index { get; set; }

        [JsonPropertyName("features")]
        [JsonRequired]
        public List<ATObject?>? Features { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.richtext.facet";

        public const string RecordType = "app.bsky.richtext.facet";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Richtext.Facet>(this, (JsonTypeInfo<App.Bsky.Richtext.Facet>)SourceGenerationContext.Default.AppBskyRichtextFacet)!;
        }

        public static Facet FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Richtext.Facet>(json, (JsonTypeInfo<App.Bsky.Richtext.Facet>)SourceGenerationContext.Default.AppBskyRichtextFacet)!;
        }
    }
}


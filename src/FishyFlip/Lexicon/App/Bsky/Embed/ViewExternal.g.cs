// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class ViewExternal : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewExternal"/> class.
        /// </summary>
        /// <param name="external">
        /// app.bsky.embed.defs#viewExternal <br/>
        /// </param>
        public ViewExternal(App.Bsky.Embed.ViewExternalExternal? external = default)
        {
            this.External = external;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewExternal"/> class.
        /// </summary>
        public ViewExternal()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewExternal"/> class.
        /// </summary>
        public ViewExternal(CBORObject obj)
        {
            if (obj["external"] is not null) this.External = new App.Bsky.Embed.ViewExternalExternal(obj["external"]);
        }

        /// <summary>
        /// Gets or sets the external.
        /// app.bsky.embed.defs#viewExternal <br/>
        /// </summary>
        [JsonPropertyName("external")]
        [JsonRequired]
        public App.Bsky.Embed.ViewExternalExternal? External { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.embed.external#view";

        public const string RecordType = "app.bsky.embed.external#view";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Embed.ViewExternal>(this, (JsonTypeInfo<App.Bsky.Embed.ViewExternal>)SourceGenerationContext.Default.AppBskyEmbedViewExternal)!;
        }

        public static ViewExternal FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Embed.ViewExternal>(json, (JsonTypeInfo<App.Bsky.Embed.ViewExternal>)SourceGenerationContext.Default.AppBskyEmbedViewExternal)!;
        }
    }
}


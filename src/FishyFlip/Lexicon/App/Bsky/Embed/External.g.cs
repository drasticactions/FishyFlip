// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class External : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="External"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="thumb"></param>
        public External(string? uri = default, string? title = default, string? description = default, Blob? thumb = default)
        {
            this.Uri = uri;
            this.Title = title;
            this.Description = description;
            this.Thumb = thumb;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="External"/> class.
        /// </summary>
        public External()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="External"/> class.
        /// </summary>
        public External(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].AsString();
            if (obj["title"] is not null) this.Title = obj["title"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["thumb"] is not null) this.Thumb = new FishyFlip.Models.Blob(obj["thumb"]);
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonPropertyName("title")]
        [JsonRequired]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonRequired]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the thumb.
        /// </summary>
        [JsonPropertyName("thumb")]
        public Blob? Thumb { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.embed.external#external";

        public const string RecordType = "app.bsky.embed.external#external";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Embed.External>(this, (JsonTypeInfo<App.Bsky.Embed.External>)SourceGenerationContext.Default.AppBskyEmbedExternal)!;
        }

        public static External FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Embed.External>(json, (JsonTypeInfo<App.Bsky.Embed.External>)SourceGenerationContext.Default.AppBskyEmbedExternal)!;
        }
    }
}

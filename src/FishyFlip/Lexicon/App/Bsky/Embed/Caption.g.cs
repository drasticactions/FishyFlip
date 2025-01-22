// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class Caption : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Caption"/> class.
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="file"></param>
        public Caption(string lang = default, Blob file = default)
        {
            this.Lang = lang;
            this.File = file;
            this.Type = "app.bsky.embed.video#caption";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Caption"/> class.
        /// </summary>
        public Caption()
        {
            this.Type = "app.bsky.embed.video#caption";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Caption"/> class.
        /// </summary>
        public Caption(CBORObject obj)
        {
            if (obj["lang"] is not null) this.Lang = obj["lang"].AsString();
            if (obj["file"] is not null) this.File = new FishyFlip.Models.Blob(obj["file"]);
        }

        /// <summary>
        /// Gets or sets the lang.
        /// </summary>
        [JsonPropertyName("lang")]
        [JsonRequired]
        public string Lang { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        [JsonPropertyName("file")]
        [JsonRequired]
        public Blob File { get; set; }

        public const string RecordType = "app.bsky.embed.video#caption";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Embed.Caption>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.Caption>)SourceGenerationContext.Default.AppBskyEmbedCaption)!;
        }

        public static Caption FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Embed.Caption>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.Caption>)SourceGenerationContext.Default.AppBskyEmbedCaption)!;
        }
    }
}


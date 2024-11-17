// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class EmbedVideo : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedVideo"/> class.
        /// </summary>
        public EmbedVideo()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedVideo"/> class.
        /// </summary>
        public EmbedVideo(CBORObject obj)
        {
            if (obj["video"] is not null) this.Video = new FishyFlip.Models.Blob(obj["video"]);
            if (obj["captions"] is not null) this.Captions = obj["captions"].Values.Select(n => n is not null ? new Caption(n) : null).ToList();
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
            if (obj["aspectRatio"] is not null) this.AspectRatio = new App.Bsky.Embed.AspectRatio(obj["aspectRatio"]);
        }

        [JsonPropertyName("video")]
        [JsonRequired]
        public Blob? Video { get; set; }

        [JsonPropertyName("captions")]
        public List<FishyFlip.Lexicon.App.Bsky.Embed.Caption?>? Captions { get; set; }

        /// <summary>
        /// Alt text description of the video, for accessibility.
        /// </summary>
        [JsonPropertyName("alt")]
        public string? Alt { get; set; }

        [JsonPropertyName("aspectRatio")]
        public App.Bsky.Embed.AspectRatio? AspectRatio { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.embed.video";

        public const string RecordType = "app.bsky.embed.video";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Embed.EmbedVideo>(this, (JsonTypeInfo<App.Bsky.Embed.EmbedVideo>)SourceGenerationContext.Default.AppBskyEmbedEmbedVideo)!;
        }

        public static EmbedVideo FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Embed.EmbedVideo>(json, (JsonTypeInfo<App.Bsky.Embed.EmbedVideo>)SourceGenerationContext.Default.AppBskyEmbedEmbedVideo)!;
        }
    }
}


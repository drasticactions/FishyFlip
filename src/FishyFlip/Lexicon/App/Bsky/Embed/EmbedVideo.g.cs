// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class EmbedVideo : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedVideo"/> class.
        /// </summary>
        /// <param name="video"></param>
        /// <param name="captions"></param>
        /// <param name="alt">Alt text description of the video, for accessibility.</param>
        /// <param name="aspectRatio">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio"/> (app.bsky.embed.defs#aspectRatio)
        /// </param>
        public EmbedVideo(Blob video = default, List<FishyFlip.Lexicon.App.Bsky.Embed.Caption>? captions = default, string? alt = default, FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio? aspectRatio = default)
        {
            this.Video = video;
            this.Captions = captions;
            this.Alt = alt;
            this.AspectRatio = aspectRatio;
            this.Type = "app.bsky.embed.video";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedVideo"/> class.
        /// </summary>
        public EmbedVideo()
        {
            this.Type = "app.bsky.embed.video";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedVideo"/> class.
        /// </summary>
        public EmbedVideo(CBORObject obj)
        {
            if (obj["video"] is not null) this.Video = new FishyFlip.Models.Blob(obj["video"]);
            if (obj["captions"] is not null) this.Captions = obj["captions"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Embed.Caption(n)).ToList();
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
            if (obj["aspectRatio"] is not null) this.AspectRatio = new FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio(obj["aspectRatio"]);
        }

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        [JsonPropertyName("video")]
        [JsonRequired]
        public Blob Video { get; set; }

        /// <summary>
        /// Gets or sets the captions.
        /// </summary>
        [JsonPropertyName("captions")]
        public List<FishyFlip.Lexicon.App.Bsky.Embed.Caption>? Captions { get; set; }

        /// <summary>
        /// Gets or sets the alt.
        /// <br/> Alt text description of the video, for accessibility.
        /// </summary>
        [JsonPropertyName("alt")]
        public string? Alt { get; set; }

        /// <summary>
        /// Gets or sets the aspectRatio.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio"/> (app.bsky.embed.defs#aspectRatio)
        /// </summary>
        [JsonPropertyName("aspectRatio")]
        public FishyFlip.Lexicon.App.Bsky.Embed.AspectRatio? AspectRatio { get; set; }

        public const string RecordType = "app.bsky.embed.video";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Embed.EmbedVideo>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.EmbedVideo>)SourceGenerationContext.Default.AppBskyEmbedEmbedVideo)!;
        }

        public static EmbedVideo FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Embed.EmbedVideo>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.EmbedVideo>)SourceGenerationContext.Default.AppBskyEmbedEmbedVideo)!;
        }
    }
}


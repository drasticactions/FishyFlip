// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class RecordWithMedia : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordWithMedia"/> class.
        /// </summary>
        /// <param name="record">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord"/> (app.bsky.embed.record)
        /// </param>
        /// <param name="media">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedImages"/> (app.bsky.embed.images) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedVideo"/> (app.bsky.embed.video) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedExternal"/> (app.bsky.embed.external) <br/>
        /// </param>
        public RecordWithMedia(FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord? record = default, ATObject? media = default)
        {
            this.Record = record;
            this.Media = media;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordWithMedia"/> class.
        /// </summary>
        public RecordWithMedia()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordWithMedia"/> class.
        /// </summary>
        public RecordWithMedia(CBORObject obj)
        {
            if (obj["record"] is not null) this.Record = new FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord(obj["record"]);
            if (obj["media"] is not null) this.Media = obj["media"].ToATObject();
        }

        /// <summary>
        /// Gets or sets the record.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord"/> (app.bsky.embed.record)
        /// </summary>
        [JsonPropertyName("record")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord? Record { get; set; }

        /// <summary>
        /// Gets or sets the media.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedImages"/> (app.bsky.embed.images) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedVideo"/> (app.bsky.embed.video) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedExternal"/> (app.bsky.embed.external) <br/>
        /// </summary>
        [JsonPropertyName("media")]
        [JsonRequired]
        public ATObject? Media { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.embed.recordWithMedia";

        public const string RecordType = "app.bsky.embed.recordWithMedia";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Embed.RecordWithMedia>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.RecordWithMedia>)SourceGenerationContext.Default.AppBskyEmbedRecordWithMedia)!;
        }

        public static RecordWithMedia FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Embed.RecordWithMedia>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.RecordWithMedia>)SourceGenerationContext.Default.AppBskyEmbedRecordWithMedia)!;
        }
    }
}


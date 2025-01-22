// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Moderation
{
    public partial class GetActorMetadataOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorMetadataOutput"/> class.
        /// </summary>
        /// <param name="day">
        /// chat.bsky.moderation.defs#metadata <br/>
        /// </param>
        /// <param name="month">
        /// chat.bsky.moderation.defs#metadata <br/>
        /// </param>
        /// <param name="all">
        /// chat.bsky.moderation.defs#metadata <br/>
        /// </param>
        public GetActorMetadataOutput(FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata day = default, FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata month = default, FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata all = default)
        {
            this.Day = day;
            this.Month = month;
            this.All = all;
            this.Type = "chat.bsky.moderation.getActorMetadata#GetActorMetadataOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorMetadataOutput"/> class.
        /// </summary>
        public GetActorMetadataOutput()
        {
            this.Type = "chat.bsky.moderation.getActorMetadata#GetActorMetadataOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorMetadataOutput"/> class.
        /// </summary>
        public GetActorMetadataOutput(CBORObject obj)
        {
            if (obj["day"] is not null) this.Day = new FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata(obj["day"]);
            if (obj["month"] is not null) this.Month = new FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata(obj["month"]);
            if (obj["all"] is not null) this.All = new FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata(obj["all"]);
        }

        /// <summary>
        /// Gets or sets the day.
        /// chat.bsky.moderation.defs#metadata <br/>
        /// </summary>
        [JsonPropertyName("day")]
        [JsonRequired]
        public FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata Day { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// chat.bsky.moderation.defs#metadata <br/>
        /// </summary>
        [JsonPropertyName("month")]
        [JsonRequired]
        public FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata Month { get; set; }

        /// <summary>
        /// Gets or sets the all.
        /// chat.bsky.moderation.defs#metadata <br/>
        /// </summary>
        [JsonPropertyName("all")]
        [JsonRequired]
        public FishyFlip.Lexicon.Chat.Bsky.Moderation.Metadata All { get; set; }

        public const string RecordType = "chat.bsky.moderation.getActorMetadata#GetActorMetadataOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetActorMetadataOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetActorMetadataOutput>)SourceGenerationContext.Default.ChatBskyModerationGetActorMetadataOutput)!;
        }

        public static GetActorMetadataOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetActorMetadataOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.GetActorMetadataOutput>)SourceGenerationContext.Default.ChatBskyModerationGetActorMetadataOutput)!;
        }
    }
}


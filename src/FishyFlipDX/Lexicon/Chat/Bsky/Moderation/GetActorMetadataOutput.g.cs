// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Moderation
{
    public partial class GetActorMetadataOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorMetadataOutput"/> class.
        /// </summary>
        public GetActorMetadataOutput(Chat.Bsky.Moderation.Metadata? day = default, Chat.Bsky.Moderation.Metadata? month = default, Chat.Bsky.Moderation.Metadata? all = default)
        {
            this.Day = day;
            this.Month = month;
            this.All = all;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorMetadataOutput"/> class.
        /// </summary>
        public GetActorMetadataOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetActorMetadataOutput"/> class.
        /// </summary>
        public GetActorMetadataOutput(CBORObject obj)
        {
            if (obj["day"] is not null) this.Day = new Chat.Bsky.Moderation.Metadata(obj["day"]);
            if (obj["month"] is not null) this.Month = new Chat.Bsky.Moderation.Metadata(obj["month"]);
            if (obj["all"] is not null) this.All = new Chat.Bsky.Moderation.Metadata(obj["all"]);
        }

        [JsonPropertyName("day")]
        [JsonRequired]
        public Chat.Bsky.Moderation.Metadata? Day { get; set; }

        [JsonPropertyName("month")]
        [JsonRequired]
        public Chat.Bsky.Moderation.Metadata? Month { get; set; }

        [JsonPropertyName("all")]
        [JsonRequired]
        public Chat.Bsky.Moderation.Metadata? All { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "chat.bsky.moderation.getActorMetadata#GetActorMetadataOutput";

        public const string RecordType = "chat.bsky.moderation.getActorMetadata#GetActorMetadataOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Chat.Bsky.Moderation.GetActorMetadataOutput>(this, (JsonTypeInfo<Chat.Bsky.Moderation.GetActorMetadataOutput>)SourceGenerationContext.Default.ChatBskyModerationGetActorMetadataOutput)!;
        }

        public static GetActorMetadataOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Chat.Bsky.Moderation.GetActorMetadataOutput>(json, (JsonTypeInfo<Chat.Bsky.Moderation.GetActorMetadataOutput>)SourceGenerationContext.Default.ChatBskyModerationGetActorMetadataOutput)!;
        }
    }
}


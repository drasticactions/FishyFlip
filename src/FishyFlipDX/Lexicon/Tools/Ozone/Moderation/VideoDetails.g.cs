// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class VideoDetails : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDetails"/> class.
        /// </summary>
        public VideoDetails()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDetails"/> class.
        /// </summary>
        public VideoDetails(CBORObject obj)
        {
            if (obj["width"] is not null) this.Width = obj["width"].AsInt64Value();
            if (obj["height"] is not null) this.Height = obj["height"].AsInt64Value();
            if (obj["length"] is not null) this.Length = obj["length"].AsInt64Value();
        }

        [JsonPropertyName("width")]
        [JsonRequired]
        public long? Width { get; set; }

        [JsonPropertyName("height")]
        [JsonRequired]
        public long? Height { get; set; }

        [JsonPropertyName("length")]
        [JsonRequired]
        public long? Length { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#videoDetails";

        public const string RecordType = "tools.ozone.moderation.defs#videoDetails";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.VideoDetails>(this, (JsonTypeInfo<Tools.Ozone.Moderation.VideoDetails>)SourceGenerationContext.Default.ToolsOzoneModerationVideoDetails)!;
        }

        public static VideoDetails FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.VideoDetails>(json, (JsonTypeInfo<Tools.Ozone.Moderation.VideoDetails>)SourceGenerationContext.Default.ToolsOzoneModerationVideoDetails)!;
        }
    }
}


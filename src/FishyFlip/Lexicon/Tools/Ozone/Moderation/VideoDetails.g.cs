// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class VideoDetails : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDetails"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="length"></param>
        public VideoDetails(long width = default, long height = default, long length = default)
        {
            this.Width = width;
            this.Height = height;
            this.Length = length;
            this.Type = "tools.ozone.moderation.defs#videoDetails";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDetails"/> class.
        /// </summary>
        public VideoDetails()
        {
            this.Type = "tools.ozone.moderation.defs#videoDetails";
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

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [JsonPropertyName("width")]
        [JsonRequired]
        public long Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [JsonPropertyName("height")]
        [JsonRequired]
        public long Height { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        [JsonPropertyName("length")]
        [JsonRequired]
        public long Length { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#videoDetails";

        public static VideoDetails FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.VideoDetails>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.VideoDetails>)SourceGenerationContext.Default.ToolsOzoneModerationVideoDetails)!;
        }
    }
}


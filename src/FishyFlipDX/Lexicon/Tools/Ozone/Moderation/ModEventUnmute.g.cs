// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Unmute action on a subject
    /// </summary>
    public partial class ModEventUnmute : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventUnmute"/> class.
        /// </summary>
        public ModEventUnmute(string? comment = default)
        {
            this.Comment = comment;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventUnmute"/> class.
        /// </summary>
        public ModEventUnmute()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventUnmute"/> class.
        /// </summary>
        public ModEventUnmute(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
        }

        /// <summary>
        /// Describe reasoning behind the reversal.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#modEventUnmute";

        public const string RecordType = "tools.ozone.moderation.defs#modEventUnmute";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.ModEventUnmute>(this, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventUnmute>)SourceGenerationContext.Default.ToolsOzoneModerationModEventUnmute)!;
        }

        public static ModEventUnmute FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.ModEventUnmute>(json, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventUnmute>)SourceGenerationContext.Default.ToolsOzoneModerationModEventUnmute)!;
        }
    }
}


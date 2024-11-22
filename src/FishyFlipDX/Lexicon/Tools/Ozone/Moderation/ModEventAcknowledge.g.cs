// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class ModEventAcknowledge : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventAcknowledge"/> class.
        /// </summary>
        /// <param name="comment"></param>
        public ModEventAcknowledge(string? comment = default)
        {
            this.Comment = comment;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventAcknowledge"/> class.
        /// </summary>
        public ModEventAcknowledge()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventAcknowledge"/> class.
        /// </summary>
        public ModEventAcknowledge(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#modEventAcknowledge";

        public const string RecordType = "tools.ozone.moderation.defs#modEventAcknowledge";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.ModEventAcknowledge>(this, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventAcknowledge>)SourceGenerationContext.Default.ToolsOzoneModerationModEventAcknowledge)!;
        }

        public static ModEventAcknowledge FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.ModEventAcknowledge>(json, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventAcknowledge>)SourceGenerationContext.Default.ToolsOzoneModerationModEventAcknowledge)!;
        }
    }
}


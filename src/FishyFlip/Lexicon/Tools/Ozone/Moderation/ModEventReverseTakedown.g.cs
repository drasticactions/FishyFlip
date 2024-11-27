// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Revert take down action on a subject
    /// </summary>
    public partial class ModEventReverseTakedown : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventReverseTakedown"/> class.
        /// </summary>
        /// <param name="comment">Describe reasoning behind the reversal.</param>
        public ModEventReverseTakedown(string? comment = default)
        {
            this.Comment = comment;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventReverseTakedown"/> class.
        /// </summary>
        public ModEventReverseTakedown()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventReverseTakedown"/> class.
        /// </summary>
        public ModEventReverseTakedown(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
        }

        /// <summary>
        /// Gets or sets the comment.
        /// <br/> Describe reasoning behind the reversal.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#modEventReverseTakedown";

        public const string RecordType = "tools.ozone.moderation.defs#modEventReverseTakedown";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.ModEventReverseTakedown>(this, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventReverseTakedown>)SourceGenerationContext.Default.ToolsOzoneModerationModEventReverseTakedown)!;
        }

        public static ModEventReverseTakedown FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.ModEventReverseTakedown>(json, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventReverseTakedown>)SourceGenerationContext.Default.ToolsOzoneModerationModEventReverseTakedown)!;
        }
    }
}


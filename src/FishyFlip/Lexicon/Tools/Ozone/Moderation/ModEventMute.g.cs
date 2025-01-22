// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Mute incoming reports on a subject
    /// </summary>
    public partial class ModEventMute : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventMute"/> class.
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="durationInHours">Indicates how long the subject should remain muted.</param>
        public ModEventMute(string? comment = default, long durationInHours = default)
        {
            this.Comment = comment;
            this.DurationInHours = durationInHours;
            this.Type = "tools.ozone.moderation.defs#modEventMute";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventMute"/> class.
        /// </summary>
        public ModEventMute()
        {
            this.Type = "tools.ozone.moderation.defs#modEventMute";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventMute"/> class.
        /// </summary>
        public ModEventMute(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
            if (obj["durationInHours"] is not null) this.DurationInHours = obj["durationInHours"].AsInt64Value();
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets the durationInHours.
        /// <br/> Indicates how long the subject should remain muted.
        /// </summary>
        [JsonPropertyName("durationInHours")]
        [JsonRequired]
        public long DurationInHours { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#modEventMute";

        public static ModEventMute FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMute>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMute>)SourceGenerationContext.Default.ToolsOzoneModerationModEventMute)!;
        }
    }
}


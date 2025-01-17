// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Divert a record's blobs to a 3rd party service for further scanning/tagging
    /// </summary>
    public partial class ModEventDivert : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventDivert"/> class.
        /// </summary>
        /// <param name="comment"></param>
        public ModEventDivert(string? comment = default)
        {
            this.Comment = comment;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventDivert"/> class.
        /// </summary>
        public ModEventDivert()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventDivert"/> class.
        /// </summary>
        public ModEventDivert(CBORObject obj)
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
        public override string Type => "tools.ozone.moderation.defs#modEventDivert";

        public const string RecordType = "tools.ozone.moderation.defs#modEventDivert";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventDivert>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventDivert>)SourceGenerationContext.Default.ToolsOzoneModerationModEventDivert)!;
        }

        public static ModEventDivert FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventDivert>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventDivert>)SourceGenerationContext.Default.ToolsOzoneModerationModEventDivert)!;
        }
    }
}


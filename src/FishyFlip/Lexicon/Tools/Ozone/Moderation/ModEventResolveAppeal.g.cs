// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Resolve appeal on a subject
    /// </summary>
    public partial class ModEventResolveAppeal : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventResolveAppeal"/> class.
        /// </summary>
        /// <param name="comment">Describe resolution.</param>
        public ModEventResolveAppeal(string? comment = default)
        {
            this.Comment = comment;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventResolveAppeal"/> class.
        /// </summary>
        public ModEventResolveAppeal()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventResolveAppeal"/> class.
        /// </summary>
        public ModEventResolveAppeal(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
        }

        /// <summary>
        /// Gets or sets the comment.
        /// <br/> Describe resolution.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#modEventResolveAppeal";

        public const string RecordType = "tools.ozone.moderation.defs#modEventResolveAppeal";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.ModEventResolveAppeal>(this, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventResolveAppeal>)SourceGenerationContext.Default.ToolsOzoneModerationModEventResolveAppeal)!;
        }

        public static ModEventResolveAppeal FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.ModEventResolveAppeal>(json, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventResolveAppeal>)SourceGenerationContext.Default.ToolsOzoneModerationModEventResolveAppeal)!;
        }
    }
}


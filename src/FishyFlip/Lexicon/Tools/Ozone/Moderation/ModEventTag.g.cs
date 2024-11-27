// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Add/Remove a tag on a subject
    /// </summary>
    public partial class ModEventTag : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventTag"/> class.
        /// </summary>
        /// <param name="add">Tags to be added to the subject. If already exists, won't be duplicated.</param>
        /// <param name="remove">Tags to be removed to the subject. Ignores a tag If it doesn't exist, won't be duplicated.</param>
        /// <param name="comment">Additional comment about added/removed tags.</param>
        public ModEventTag(List<string>? add = default, List<string>? remove = default, string? comment = default)
        {
            this.Add = add;
            this.Remove = remove;
            this.Comment = comment;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventTag"/> class.
        /// </summary>
        public ModEventTag()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventTag"/> class.
        /// </summary>
        public ModEventTag(CBORObject obj)
        {
            if (obj["add"] is not null) this.Add = obj["add"].Values.Select(n =>n.AsString()).ToList();
            if (obj["remove"] is not null) this.Remove = obj["remove"].Values.Select(n =>n.AsString()).ToList();
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
        }

        /// <summary>
        /// Gets or sets the add.
        /// <br/> Tags to be added to the subject. If already exists, won't be duplicated.
        /// </summary>
        [JsonPropertyName("add")]
        [JsonRequired]
        public List<string>? Add { get; set; }

        /// <summary>
        /// Gets or sets the remove.
        /// <br/> Tags to be removed to the subject. Ignores a tag If it doesn't exist, won't be duplicated.
        /// </summary>
        [JsonPropertyName("remove")]
        [JsonRequired]
        public List<string>? Remove { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// <br/> Additional comment about added/removed tags.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#modEventTag";

        public const string RecordType = "tools.ozone.moderation.defs#modEventTag";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.ModEventTag>(this, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventTag>)SourceGenerationContext.Default.ToolsOzoneModerationModEventTag)!;
        }

        public static ModEventTag FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.ModEventTag>(json, (JsonTypeInfo<Tools.Ozone.Moderation.ModEventTag>)SourceGenerationContext.Default.ToolsOzoneModerationModEventTag)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Resolve appeal on a subject
    /// </summary>
    public partial class ModEventResolveAppeal : ATObject, ICBOREncodable<ModEventResolveAppeal>, IJsonEncodable<ModEventResolveAppeal>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventResolveAppeal"/> class.
        /// </summary>
        /// <param name="comment">Describe resolution.</param>
        public ModEventResolveAppeal(string? comment = default)
        {
            this.Comment = comment;
            this.Type = "tools.ozone.moderation.defs#modEventResolveAppeal";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventResolveAppeal"/> class.
        /// </summary>
        public ModEventResolveAppeal()
        {
            this.Type = "tools.ozone.moderation.defs#modEventResolveAppeal";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventResolveAppeal"/> class.
        /// </summary>
        public ModEventResolveAppeal(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the comment.
        /// <br/> Describe resolution.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#modEventResolveAppeal";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventResolveAppeal>)SourceGenerationContext.Default.ToolsOzoneModerationModEventResolveAppeal);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventResolveAppeal>)SourceGenerationContext.Default.ToolsOzoneModerationModEventResolveAppeal);
        }

        public static new ModEventResolveAppeal FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventResolveAppeal>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventResolveAppeal>)SourceGenerationContext.Default.ToolsOzoneModerationModEventResolveAppeal)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ModEventResolveAppeal FromCBORObject(CBORObject obj)
        {
            return new ModEventResolveAppeal(obj);
        }

    }
}


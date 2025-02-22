// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class ModEventAcknowledge : ATObject, ICBOREncodable<ModEventAcknowledge>, IJsonEncodable<ModEventAcknowledge>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventAcknowledge"/> class.
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="acknowledgeAccountSubjects">If true, all other reports on content authored by this account will be resolved (acknowledged).</param>
        public ModEventAcknowledge(string? comment = default, bool? acknowledgeAccountSubjects = default)
        {
            this.Comment = comment;
            this.AcknowledgeAccountSubjects = acknowledgeAccountSubjects;
            this.Type = "tools.ozone.moderation.defs#modEventAcknowledge";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventAcknowledge"/> class.
        /// </summary>
        public ModEventAcknowledge()
        {
            this.Type = "tools.ozone.moderation.defs#modEventAcknowledge";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventAcknowledge"/> class.
        /// </summary>
        public ModEventAcknowledge(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
            if (obj["acknowledgeAccountSubjects"] is not null) this.AcknowledgeAccountSubjects = obj["acknowledgeAccountSubjects"].AsBoolean();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets the acknowledgeAccountSubjects.
        /// <br/> If true, all other reports on content authored by this account will be resolved (acknowledged).
        /// </summary>
        [JsonPropertyName("acknowledgeAccountSubjects")]
        public bool? AcknowledgeAccountSubjects { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#modEventAcknowledge";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventAcknowledge>)SourceGenerationContext.Default.ToolsOzoneModerationModEventAcknowledge);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventAcknowledge>)SourceGenerationContext.Default.ToolsOzoneModerationModEventAcknowledge);
        }

        public static new ModEventAcknowledge FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventAcknowledge>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventAcknowledge>)SourceGenerationContext.Default.ToolsOzoneModerationModEventAcknowledge)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ModEventAcknowledge FromCBORObject(CBORObject obj)
        {
            return new ModEventAcknowledge(obj);
        }

    }
}


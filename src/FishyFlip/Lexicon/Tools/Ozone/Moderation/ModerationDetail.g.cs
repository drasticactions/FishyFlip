// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class ModerationDetail : ATObject, ICBOREncodable<ModerationDetail>, IJsonEncodable<ModerationDetail>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModerationDetail"/> class.
        /// </summary>
        /// <param name="subjectStatus">
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView"/> (tools.ozone.moderation.defs#subjectStatusView)
        /// </param>
        public ModerationDetail(FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView? subjectStatus = default)
        {
            this.SubjectStatus = subjectStatus;
            this.Type = "tools.ozone.moderation.defs#moderationDetail";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModerationDetail"/> class.
        /// </summary>
        public ModerationDetail()
        {
            this.Type = "tools.ozone.moderation.defs#moderationDetail";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModerationDetail"/> class.
        /// </summary>
        public ModerationDetail(CBORObject obj)
        {
            if (obj["subjectStatus"] is not null) this.SubjectStatus = new FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView(obj["subjectStatus"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the subjectStatus.
        /// <br/> <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView"/> (tools.ozone.moderation.defs#subjectStatusView)
        /// </summary>
        [JsonPropertyName("subjectStatus")]
        public FishyFlip.Lexicon.Tools.Ozone.Moderation.SubjectStatusView? SubjectStatus { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#moderationDetail";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModerationDetail>)SourceGenerationContext.Default.ToolsOzoneModerationModerationDetail);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModerationDetail>)SourceGenerationContext.Default.ToolsOzoneModerationModerationDetail);
        }

        public static new ModerationDetail FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModerationDetail>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModerationDetail>)SourceGenerationContext.Default.ToolsOzoneModerationModerationDetail)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ModerationDetail FromCBORObject(CBORObject obj)
        {
            return new ModerationDetail(obj);
        }

    }
}


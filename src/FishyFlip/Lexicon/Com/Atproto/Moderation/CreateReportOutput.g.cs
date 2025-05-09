// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Moderation
{
    public partial class CreateReportOutput : ATObject, ICBOREncodable<CreateReportOutput>, IJsonEncodable<CreateReportOutput>, IParsable<CreateReportOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportOutput"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reasonType">
        /// <br/> Known Values: <br/>
        /// com.atproto.moderation.defs#reasonSpam - Spam: frequent unwanted promotion, replies, mentions <br/>
        /// com.atproto.moderation.defs#reasonViolation - Direct violation of server rules, laws, terms of service <br/>
        /// com.atproto.moderation.defs#reasonMisleading - Misleading identity, affiliation, or content <br/>
        /// com.atproto.moderation.defs#reasonSexual - Unwanted or mislabeled sexual content <br/>
        /// com.atproto.moderation.defs#reasonRude - Rude, harassing, explicit, or otherwise unwelcoming behavior <br/>
        /// com.atproto.moderation.defs#reasonOther - Other: reports not falling under another report category <br/>
        /// com.atproto.moderation.defs#reasonAppeal - Appeal: appeal a previously taken moderation action <br/>
        /// </param>
        /// <param name="reason"></param>
        /// <param name="subject">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// </param>
        /// <param name="reportedBy"></param>
        /// <param name="createdAt"></param>
        public CreateReportOutput(long id = default, string reasonType = default, string? reason = default, ATObject subject = default, FishyFlip.Models.ATDid reportedBy = default, DateTime? createdAt = default)
        {
            this.Id = id;
            this.ReasonType = reasonType;
            this.Reason = reason;
            this.Subject = subject;
            this.ReportedBy = reportedBy;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "com.atproto.moderation.createReport#CreateReportOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportOutput"/> class.
        /// </summary>
        public CreateReportOutput()
        {
            this.Type = "com.atproto.moderation.createReport#CreateReportOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportOutput"/> class.
        /// </summary>
        public CreateReportOutput(CBORObject obj)
        {
            if (obj["id"] is not null) this.Id = obj["id"].AsInt64Value();
            if (obj["reasonType"] is not null) this.ReasonType = obj["reasonType"].AsString();
            if (obj["reason"] is not null) this.Reason = obj["reason"].AsString();
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATObject();
            if (obj["reportedBy"] is not null) this.ReportedBy = obj["reportedBy"].ToATDid();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonRequired]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the reasonType.
        /// <br/> Known Values: <br/>
        /// com.atproto.moderation.defs#reasonSpam - Spam: frequent unwanted promotion, replies, mentions <br/>
        /// com.atproto.moderation.defs#reasonViolation - Direct violation of server rules, laws, terms of service <br/>
        /// com.atproto.moderation.defs#reasonMisleading - Misleading identity, affiliation, or content <br/>
        /// com.atproto.moderation.defs#reasonSexual - Unwanted or mislabeled sexual content <br/>
        /// com.atproto.moderation.defs#reasonRude - Rude, harassing, explicit, or otherwise unwelcoming behavior <br/>
        /// com.atproto.moderation.defs#reasonOther - Other: reports not falling under another report category <br/>
        /// com.atproto.moderation.defs#reasonAppeal - Appeal: appeal a previously taken moderation action <br/>
        /// </summary>
        [JsonPropertyName("reasonType")]
        [JsonRequired]
        public string ReasonType { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        [JsonPropertyName("reason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public ATObject Subject { get; set; }

        /// <summary>
        /// Gets or sets the reportedBy.
        /// </summary>
        [JsonPropertyName("reportedBy")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid ReportedBy { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonRequired]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "com.atproto.moderation.createReport#CreateReportOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Moderation.CreateReportOutput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Moderation.CreateReportOutput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportOutput);
        }

        public static new CreateReportOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Moderation.CreateReportOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Moderation.CreateReportOutput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new CreateReportOutput FromCBORObject(CBORObject obj)
        {
            return new CreateReportOutput(obj);
        }

        /// <inheritdoc/>
        public static CreateReportOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<CreateReportOutput>(s, (JsonTypeInfo<CreateReportOutput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out CreateReportOutput result)
        {
            result = JsonSerializer.Deserialize<CreateReportOutput>(s, (JsonTypeInfo<CreateReportOutput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportOutput);
            return result != null;
        }
    }
}


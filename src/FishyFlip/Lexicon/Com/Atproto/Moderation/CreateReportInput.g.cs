// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Moderation
{
    public partial class CreateReportInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportInput"/> class.
        /// </summary>
        /// <param name="reasonType">Indicates the broad category of violation the report is for.
        /// <br/> Known Values: <br/>
        /// com.atproto.moderation.defs#reasonSpam - Spam: frequent unwanted promotion, replies, mentions <br/>
        /// com.atproto.moderation.defs#reasonViolation - Direct violation of server rules, laws, terms of service <br/>
        /// com.atproto.moderation.defs#reasonMisleading - Misleading identity, affiliation, or content <br/>
        /// com.atproto.moderation.defs#reasonSexual - Unwanted or mislabeled sexual content <br/>
        /// com.atproto.moderation.defs#reasonRude - Rude, harassing, explicit, or otherwise unwelcoming behavior <br/>
        /// com.atproto.moderation.defs#reasonOther - Other: reports not falling under another report category <br/>
        /// com.atproto.moderation.defs#reasonAppeal - Appeal: appeal a previously taken moderation action <br/>
        /// </param>
        /// <param name="reason">Additional context about the content and violation.</param>
        /// <param name="subject">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// </param>
        public CreateReportInput(string? reasonType = default, string? reason = default, ATObject? subject = default)
        {
            this.ReasonType = reasonType;
            this.Reason = reason;
            this.Subject = subject;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportInput"/> class.
        /// </summary>
        public CreateReportInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportInput"/> class.
        /// </summary>
        public CreateReportInput(CBORObject obj)
        {
            if (obj["reasonType"] is not null) this.ReasonType = obj["reasonType"].AsString();
            if (obj["reason"] is not null) this.Reason = obj["reason"].AsString();
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATObject();
        }

        /// <summary>
        /// Gets or sets the reasonType.
        /// <br/> Indicates the broad category of violation the report is for.
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
        public string? ReasonType { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// <br/> Additional context about the content and violation.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public ATObject? Subject { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.moderation.createReport#CreateReportInput";

        public const string RecordType = "com.atproto.moderation.createReport#CreateReportInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Moderation.CreateReportInput>(this, (JsonTypeInfo<Com.Atproto.Moderation.CreateReportInput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportInput)!;
        }

        public static CreateReportInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Moderation.CreateReportInput>(json, (JsonTypeInfo<Com.Atproto.Moderation.CreateReportInput>)SourceGenerationContext.Default.ComAtprotoModerationCreateReportInput)!;
        }
    }
}


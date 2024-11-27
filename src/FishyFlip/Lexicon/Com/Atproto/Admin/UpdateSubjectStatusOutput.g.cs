// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class UpdateSubjectStatusOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubjectStatusOutput"/> class.
        /// </summary>
        /// <param name="subject">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoBlobRef"/> (com.atproto.admin.defs#repoBlobRef) <br/>
        /// </param>
        /// <param name="takedown">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr"/> (com.atproto.admin.defs#statusAttr)
        /// </param>
        public UpdateSubjectStatusOutput(ATObject? subject = default, Com.Atproto.Admin.StatusAttr? takedown = default)
        {
            this.Subject = subject;
            this.Takedown = takedown;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubjectStatusOutput"/> class.
        /// </summary>
        public UpdateSubjectStatusOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubjectStatusOutput"/> class.
        /// </summary>
        public UpdateSubjectStatusOutput(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATObject();
            if (obj["takedown"] is not null) this.Takedown = new Com.Atproto.Admin.StatusAttr(obj["takedown"]);
        }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoBlobRef"/> (com.atproto.admin.defs#repoBlobRef) <br/>
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public ATObject? Subject { get; set; }

        /// <summary>
        /// Gets or sets the takedown.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr"/> (com.atproto.admin.defs#statusAttr)
        /// </summary>
        [JsonPropertyName("takedown")]
        public Com.Atproto.Admin.StatusAttr? Takedown { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.updateSubjectStatus#UpdateSubjectStatusOutput";

        public const string RecordType = "com.atproto.admin.updateSubjectStatus#UpdateSubjectStatusOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Admin.UpdateSubjectStatusOutput>(this, (JsonTypeInfo<Com.Atproto.Admin.UpdateSubjectStatusOutput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateSubjectStatusOutput)!;
        }

        public static UpdateSubjectStatusOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Admin.UpdateSubjectStatusOutput>(json, (JsonTypeInfo<Com.Atproto.Admin.UpdateSubjectStatusOutput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateSubjectStatusOutput)!;
        }
    }
}


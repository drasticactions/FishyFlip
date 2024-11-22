// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class UpdateSubjectStatusInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubjectStatusInput"/> class.
        /// </summary>
        /// <param name="subject">
        /// Union Types:
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef)
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoBlobRef"/> (com.atproto.admin.defs#repoBlobRef)
        /// </param>
        /// <param name="takedown"></param>
        /// <param name="deactivated"></param>
        public UpdateSubjectStatusInput(ATObject? subject = default, Com.Atproto.Admin.StatusAttr? takedown = default, Com.Atproto.Admin.StatusAttr? deactivated = default)
        {
            this.Subject = subject;
            this.Takedown = takedown;
            this.Deactivated = deactivated;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubjectStatusInput"/> class.
        /// </summary>
        public UpdateSubjectStatusInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubjectStatusInput"/> class.
        /// </summary>
        public UpdateSubjectStatusInput(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATObject();
            if (obj["takedown"] is not null) this.Takedown = new Com.Atproto.Admin.StatusAttr(obj["takedown"]);
            if (obj["deactivated"] is not null) this.Deactivated = new Com.Atproto.Admin.StatusAttr(obj["deactivated"]);
        }

        /// <summary>
        /// Gets or sets the subject.
        /// Union Types:
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef)
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoBlobRef"/> (com.atproto.admin.defs#repoBlobRef)
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public ATObject? Subject { get; set; }

        /// <summary>
        /// Gets or sets the takedown.
        /// </summary>
        [JsonPropertyName("takedown")]
        public Com.Atproto.Admin.StatusAttr? Takedown { get; set; }

        /// <summary>
        /// Gets or sets the deactivated.
        /// </summary>
        [JsonPropertyName("deactivated")]
        public Com.Atproto.Admin.StatusAttr? Deactivated { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.updateSubjectStatus#UpdateSubjectStatusInput";

        public const string RecordType = "com.atproto.admin.updateSubjectStatus#UpdateSubjectStatusInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Admin.UpdateSubjectStatusInput>(this, (JsonTypeInfo<Com.Atproto.Admin.UpdateSubjectStatusInput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateSubjectStatusInput)!;
        }

        public static UpdateSubjectStatusInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Admin.UpdateSubjectStatusInput>(json, (JsonTypeInfo<Com.Atproto.Admin.UpdateSubjectStatusInput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateSubjectStatusInput)!;
        }
    }
}


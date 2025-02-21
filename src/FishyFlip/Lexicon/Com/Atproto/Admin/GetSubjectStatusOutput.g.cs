// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class GetSubjectStatusOutput : ATObject, ICBOREncodable<GetSubjectStatusOutput>, IJsonEncodable<GetSubjectStatusOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSubjectStatusOutput"/> class.
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
        /// <param name="deactivated">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr"/> (com.atproto.admin.defs#statusAttr)
        /// </param>
        public GetSubjectStatusOutput(ATObject subject = default, FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr? takedown = default, FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr? deactivated = default)
        {
            this.Subject = subject;
            this.Takedown = takedown;
            this.Deactivated = deactivated;
            this.Type = "com.atproto.admin.getSubjectStatus#GetSubjectStatusOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSubjectStatusOutput"/> class.
        /// </summary>
        public GetSubjectStatusOutput()
        {
            this.Type = "com.atproto.admin.getSubjectStatus#GetSubjectStatusOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSubjectStatusOutput"/> class.
        /// </summary>
        public GetSubjectStatusOutput(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATObject();
            if (obj["takedown"] is not null) this.Takedown = new FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr(obj["takedown"]);
            if (obj["deactivated"] is not null) this.Deactivated = new FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr(obj["deactivated"]);
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
        public ATObject Subject { get; set; }

        /// <summary>
        /// Gets or sets the takedown.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr"/> (com.atproto.admin.defs#statusAttr)
        /// </summary>
        [JsonPropertyName("takedown")]
        public FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr? Takedown { get; set; }

        /// <summary>
        /// Gets or sets the deactivated.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr"/> (com.atproto.admin.defs#statusAttr)
        /// </summary>
        [JsonPropertyName("deactivated")]
        public FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr? Deactivated { get; set; }

        public const string RecordType = "com.atproto.admin.getSubjectStatus#GetSubjectStatusOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.GetSubjectStatusOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetSubjectStatusOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.GetSubjectStatusOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetSubjectStatusOutput);
        }

        public static new GetSubjectStatusOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.GetSubjectStatusOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.GetSubjectStatusOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetSubjectStatusOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetSubjectStatusOutput FromCBORObject(CBORObject obj)
        {
            return new GetSubjectStatusOutput(obj);
        }

    }
}


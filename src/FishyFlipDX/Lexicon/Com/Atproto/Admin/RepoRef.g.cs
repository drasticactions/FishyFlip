// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class RepoRef : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoRef"/> class.
        /// </summary>
        public RepoRef()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RepoRef"/> class.
        /// </summary>
        public RepoRef(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
        }

        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.defs#repoRef";

        public const string RecordType = "com.atproto.admin.defs#repoRef";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Admin.RepoRef>(this, (JsonTypeInfo<Com.Atproto.Admin.RepoRef>)SourceGenerationContext.Default.ComAtprotoAdminRepoRef)!;
        }

        public static RepoRef FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Admin.RepoRef>(json, (JsonTypeInfo<Com.Atproto.Admin.RepoRef>)SourceGenerationContext.Default.ComAtprotoAdminRepoRef)!;
        }
    }
}


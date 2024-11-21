// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class Links : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Links"/> class.
        /// </summary>
        public Links(string? privacyPolicy = default, string? termsOfService = default)
        {
            this.PrivacyPolicy = privacyPolicy;
            this.TermsOfService = termsOfService;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Links"/> class.
        /// </summary>
        public Links()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Links"/> class.
        /// </summary>
        public Links(CBORObject obj)
        {
            if (obj["privacyPolicy"] is not null) this.PrivacyPolicy = obj["privacyPolicy"].AsString();
            if (obj["termsOfService"] is not null) this.TermsOfService = obj["termsOfService"].AsString();
        }

        [JsonPropertyName("privacyPolicy")]
        public string? PrivacyPolicy { get; set; }

        [JsonPropertyName("termsOfService")]
        public string? TermsOfService { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.server.describeServer#links";

        public const string RecordType = "com.atproto.server.describeServer#links";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Server.Links>(this, (JsonTypeInfo<Com.Atproto.Server.Links>)SourceGenerationContext.Default.ComAtprotoServerLinks)!;
        }

        public static Links FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Server.Links>(json, (JsonTypeInfo<Com.Atproto.Server.Links>)SourceGenerationContext.Default.ComAtprotoServerLinks)!;
        }
    }
}


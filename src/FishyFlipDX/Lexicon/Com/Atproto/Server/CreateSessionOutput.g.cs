// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class CreateSessionOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSessionOutput"/> class.
        /// </summary>
        public CreateSessionOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSessionOutput"/> class.
        /// </summary>
        public CreateSessionOutput(CBORObject obj)
        {
            if (obj["accessJwt"] is not null) this.AccessJwt = obj["accessJwt"].AsString();
            if (obj["refreshJwt"] is not null) this.RefreshJwt = obj["refreshJwt"].AsString();
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            // Ignore DidDoc
            if (obj["email"] is not null) this.Email = obj["email"].AsString();
            if (obj["emailConfirmed"] is not null) this.EmailConfirmed = obj["emailConfirmed"].AsBoolean();
            if (obj["emailAuthFactor"] is not null) this.EmailAuthFactor = obj["emailAuthFactor"].AsBoolean();
            if (obj["active"] is not null) this.Active = obj["active"].AsBoolean();
            // enum
        }

        [JsonPropertyName("accessJwt")]
        [JsonRequired]
        public string? AccessJwt { get; set; }

        [JsonPropertyName("refreshJwt")]
        [JsonRequired]
        public string? RefreshJwt { get; set; }

        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle? Handle { get; set; }

        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        [JsonPropertyName("didDoc")]
        public FishyFlip.Models.DidDoc? DidDoc { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("emailConfirmed")]
        public bool? EmailConfirmed { get; set; }

        [JsonPropertyName("emailAuthFactor")]
        public bool? EmailAuthFactor { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        /// <summary>
        /// If active=false, this optional field indicates a possible reason for why the account is not active. If active=false and no status is supplied, then the host makes no claim for why the repository is no longer being hosted.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.server.createSession#CreateSessionOutput";

        public const string RecordType = "com.atproto.server.createSession#CreateSessionOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Server.CreateSessionOutput>(this, (JsonTypeInfo<Com.Atproto.Server.CreateSessionOutput>)SourceGenerationContext.Default.ComAtprotoServerCreateSessionOutput)!;
        }

        public static CreateSessionOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Server.CreateSessionOutput>(json, (JsonTypeInfo<Com.Atproto.Server.CreateSessionOutput>)SourceGenerationContext.Default.ComAtprotoServerCreateSessionOutput)!;
        }
    }
}

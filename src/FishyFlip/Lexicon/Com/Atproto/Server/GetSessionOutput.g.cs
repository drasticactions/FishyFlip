// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class GetSessionOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSessionOutput"/> class.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="did"></param>
        /// <param name="email"></param>
        /// <param name="emailConfirmed"></param>
        /// <param name="emailAuthFactor"></param>
        /// <param name="didDoc"></param>
        /// <param name="active"></param>
        /// <param name="status">If active=false, this optional field indicates a possible reason for why the account is not active. If active=false and no status is supplied, then the host makes no claim for why the repository is no longer being hosted.
        /// <br/> Known Values: <br/>
        /// takendown <br/>
        /// suspended <br/>
        /// deactivated <br/>
        /// </param>
        public GetSessionOutput(FishyFlip.Models.ATHandle handle = default, FishyFlip.Models.ATDid did = default, string? email = default, bool? emailConfirmed = default, bool? emailAuthFactor = default, FishyFlip.Models.DidDoc? didDoc = default, bool? active = default, string? status = default)
        {
            this.Handle = handle;
            this.Did = did;
            this.Email = email;
            this.EmailConfirmed = emailConfirmed;
            this.EmailAuthFactor = emailAuthFactor;
            this.DidDoc = didDoc;
            this.Active = active;
            this.Status = status;
            this.Type = "com.atproto.server.getSession#GetSessionOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSessionOutput"/> class.
        /// </summary>
        public GetSessionOutput()
        {
            this.Type = "com.atproto.server.getSession#GetSessionOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSessionOutput"/> class.
        /// </summary>
        public GetSessionOutput(CBORObject obj)
        {
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["email"] is not null) this.Email = obj["email"].AsString();
            if (obj["emailConfirmed"] is not null) this.EmailConfirmed = obj["emailConfirmed"].AsBoolean();
            if (obj["emailAuthFactor"] is not null) this.EmailAuthFactor = obj["emailAuthFactor"].AsBoolean();
            // Ignore DidDoc
            if (obj["active"] is not null) this.Active = obj["active"].AsBoolean();
            if (obj["status"] is not null) this.Status = obj["status"].AsString();
        }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle Handle { get; set; }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the emailConfirmed.
        /// </summary>
        [JsonPropertyName("emailConfirmed")]
        public bool? EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the emailAuthFactor.
        /// </summary>
        [JsonPropertyName("emailAuthFactor")]
        public bool? EmailAuthFactor { get; set; }

        /// <summary>
        /// Gets or sets the didDoc.
        /// </summary>
        [JsonPropertyName("didDoc")]
        public FishyFlip.Models.DidDoc? DidDoc { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// <br/> If active=false, this optional field indicates a possible reason for why the account is not active. If active=false and no status is supplied, then the host makes no claim for why the repository is no longer being hosted.
        /// <br/> Known Values: <br/>
        /// takendown <br/>
        /// suspended <br/>
        /// deactivated <br/>
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        public const string RecordType = "com.atproto.server.getSession#GetSessionOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Server.GetSessionOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.GetSessionOutput>)SourceGenerationContext.Default.ComAtprotoServerGetSessionOutput)!;
        }

        public static GetSessionOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.GetSessionOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.GetSessionOutput>)SourceGenerationContext.Default.ComAtprotoServerGetSessionOutput)!;
        }
    }
}


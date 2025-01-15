// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class RefreshSessionOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshSessionOutput"/> class.
        /// </summary>
        /// <param name="accessJwt"></param>
        /// <param name="refreshJwt"></param>
        /// <param name="handle"></param>
        /// <param name="did"></param>
        /// <param name="didDoc"></param>
        /// <param name="active"></param>
        /// <param name="status">Hosting status of the account. If not specified, then assume 'active'.
        /// <br/> Known Values: <br/>
        /// takendown <br/>
        /// suspended <br/>
        /// deactivated <br/>
        /// </param>
        public RefreshSessionOutput(string accessJwt = default, string refreshJwt = default, FishyFlip.Models.ATHandle handle = default, FishyFlip.Models.ATDid did = default, FishyFlip.Models.DidDoc? didDoc = default, bool? active = default, string? status = default)
        {
            this.AccessJwt = accessJwt;
            this.RefreshJwt = refreshJwt;
            this.Handle = handle;
            this.Did = did;
            this.DidDoc = didDoc;
            this.Active = active;
            this.Status = status;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshSessionOutput"/> class.
        /// </summary>
        public RefreshSessionOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshSessionOutput"/> class.
        /// </summary>
        public RefreshSessionOutput(CBORObject obj)
        {
            if (obj["accessJwt"] is not null) this.AccessJwt = obj["accessJwt"].AsString();
            if (obj["refreshJwt"] is not null) this.RefreshJwt = obj["refreshJwt"].AsString();
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            // Ignore DidDoc
            if (obj["active"] is not null) this.Active = obj["active"].AsBoolean();
            if (obj["status"] is not null) this.Status = obj["status"].AsString();
        }

        /// <summary>
        /// Gets or sets the accessJwt.
        /// </summary>
        [JsonPropertyName("accessJwt")]
        [JsonRequired]
        public string AccessJwt { get; set; }

        /// <summary>
        /// Gets or sets the refreshJwt.
        /// </summary>
        [JsonPropertyName("refreshJwt")]
        [JsonRequired]
        public string RefreshJwt { get; set; }

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
        /// <br/> Hosting status of the account. If not specified, then assume 'active'.
        /// <br/> Known Values: <br/>
        /// takendown <br/>
        /// suspended <br/>
        /// deactivated <br/>
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.server.refreshSession#RefreshSessionOutput";

        public const string RecordType = "com.atproto.server.refreshSession#RefreshSessionOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Server.RefreshSessionOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RefreshSessionOutput>)SourceGenerationContext.Default.ComAtprotoServerRefreshSessionOutput)!;
        }

        public static RefreshSessionOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.RefreshSessionOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RefreshSessionOutput>)SourceGenerationContext.Default.ComAtprotoServerRefreshSessionOutput)!;
        }
    }
}


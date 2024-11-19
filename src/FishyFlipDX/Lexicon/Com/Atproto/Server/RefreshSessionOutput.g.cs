// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class RefreshSessionOutput : ATObject
    {

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

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        /// <summary>
        /// Hosting status of the account. If not specified, then assume 'active'.
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
            return JsonSerializer.Serialize<Com.Atproto.Server.RefreshSessionOutput>(this, (JsonTypeInfo<Com.Atproto.Server.RefreshSessionOutput>)SourceGenerationContext.Default.ComAtprotoServerRefreshSessionOutput)!;
        }

        public static RefreshSessionOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Server.RefreshSessionOutput>(json, (JsonTypeInfo<Com.Atproto.Server.RefreshSessionOutput>)SourceGenerationContext.Default.ComAtprotoServerRefreshSessionOutput)!;
        }
    }
}


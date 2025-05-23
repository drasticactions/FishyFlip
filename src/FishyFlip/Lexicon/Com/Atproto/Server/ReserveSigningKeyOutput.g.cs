// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class ReserveSigningKeyOutput : ATObject, ICBOREncodable<ReserveSigningKeyOutput>, IJsonEncodable<ReserveSigningKeyOutput>, IParsable<ReserveSigningKeyOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReserveSigningKeyOutput"/> class.
        /// </summary>
        /// <param name="signingKey">The public key for the reserved signing key, in did:key serialization.</param>
        public ReserveSigningKeyOutput(string signingKey = default)
        {
            this.SigningKey = signingKey;
            this.Type = "com.atproto.server.reserveSigningKey#ReserveSigningKeyOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReserveSigningKeyOutput"/> class.
        /// </summary>
        public ReserveSigningKeyOutput()
        {
            this.Type = "com.atproto.server.reserveSigningKey#ReserveSigningKeyOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReserveSigningKeyOutput"/> class.
        /// </summary>
        public ReserveSigningKeyOutput(CBORObject obj)
        {
            if (obj["signingKey"] is not null) this.SigningKey = obj["signingKey"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the signingKey.
        /// <br/> The public key for the reserved signing key, in did:key serialization.
        /// </summary>
        [JsonPropertyName("signingKey")]
        [JsonRequired]
        public string SigningKey { get; set; }

        public const string RecordType = "com.atproto.server.reserveSigningKey#ReserveSigningKeyOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ReserveSigningKeyOutput>)SourceGenerationContext.Default.ComAtprotoServerReserveSigningKeyOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ReserveSigningKeyOutput>)SourceGenerationContext.Default.ComAtprotoServerReserveSigningKeyOutput);
        }

        public static new ReserveSigningKeyOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.ReserveSigningKeyOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ReserveSigningKeyOutput>)SourceGenerationContext.Default.ComAtprotoServerReserveSigningKeyOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new ReserveSigningKeyOutput FromCBORObject(CBORObject obj)
        {
            return new ReserveSigningKeyOutput(obj);
        }

        /// <inheritdoc/>
        public static ReserveSigningKeyOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<ReserveSigningKeyOutput>(s, (JsonTypeInfo<ReserveSigningKeyOutput>)SourceGenerationContext.Default.ComAtprotoServerReserveSigningKeyOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out ReserveSigningKeyOutput result)
        {
            result = JsonSerializer.Deserialize<ReserveSigningKeyOutput>(s, (JsonTypeInfo<ReserveSigningKeyOutput>)SourceGenerationContext.Default.ComAtprotoServerReserveSigningKeyOutput);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Identity
{
    public partial class SignPlcOperationInput : ATObject, ICBOREncodable<SignPlcOperationInput>, IJsonEncodable<SignPlcOperationInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SignPlcOperationInput"/> class.
        /// </summary>
        /// <param name="token">A token received through com.atproto.identity.requestPlcOperationSignature</param>
        /// <param name="rotationKeys"></param>
        /// <param name="alsoKnownAs"></param>
        /// <param name="verificationMethods"></param>
        /// <param name="services"></param>
        public SignPlcOperationInput(string? token = default, List<string>? rotationKeys = default, List<string>? alsoKnownAs = default, ATObject? verificationMethods = default, ATObject? services = default)
        {
            this.Token = token;
            this.RotationKeys = rotationKeys;
            this.AlsoKnownAs = alsoKnownAs;
            this.VerificationMethods = verificationMethods;
            this.Services = services;
            this.Type = "com.atproto.identity.signPlcOperation#SignPlcOperationInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SignPlcOperationInput"/> class.
        /// </summary>
        public SignPlcOperationInput()
        {
            this.Type = "com.atproto.identity.signPlcOperation#SignPlcOperationInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SignPlcOperationInput"/> class.
        /// </summary>
        public SignPlcOperationInput(CBORObject obj)
        {
            if (obj["token"] is not null) this.Token = obj["token"].AsString();
            if (obj["rotationKeys"] is not null) this.RotationKeys = obj["rotationKeys"].Values.Select(n =>n.AsString()).ToList();
            if (obj["alsoKnownAs"] is not null) this.AlsoKnownAs = obj["alsoKnownAs"].Values.Select(n =>n.AsString()).ToList();
            if (obj["verificationMethods"] is not null) this.VerificationMethods = obj["verificationMethods"].ToATObject();
            if (obj["services"] is not null) this.Services = obj["services"].ToATObject();
        }

        /// <summary>
        /// Gets or sets the token.
        /// <br/> A token received through com.atproto.identity.requestPlcOperationSignature
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the rotationKeys.
        /// </summary>
        [JsonPropertyName("rotationKeys")]
        public List<string>? RotationKeys { get; set; }

        /// <summary>
        /// Gets or sets the alsoKnownAs.
        /// </summary>
        [JsonPropertyName("alsoKnownAs")]
        public List<string>? AlsoKnownAs { get; set; }

        /// <summary>
        /// Gets or sets the verificationMethods.
        /// </summary>
        [JsonPropertyName("verificationMethods")]
        public ATObject? VerificationMethods { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        [JsonPropertyName("services")]
        public ATObject? Services { get; set; }

        public const string RecordType = "com.atproto.identity.signPlcOperation#SignPlcOperationInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationInput>)SourceGenerationContext.Default.ComAtprotoIdentitySignPlcOperationInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationInput>)SourceGenerationContext.Default.ComAtprotoIdentitySignPlcOperationInput);
        }

        public static new SignPlcOperationInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationInput>)SourceGenerationContext.Default.ComAtprotoIdentitySignPlcOperationInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new SignPlcOperationInput FromCBORObject(CBORObject obj)
        {
            return new SignPlcOperationInput(obj);
        }

    }
}


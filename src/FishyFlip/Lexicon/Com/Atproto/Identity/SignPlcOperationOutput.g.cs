// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Identity
{
    public partial class SignPlcOperationOutput : ATObject, ICBOREncodable<SignPlcOperationOutput>, IJsonEncodable<SignPlcOperationOutput>, IParsable<SignPlcOperationOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SignPlcOperationOutput"/> class.
        /// </summary>
        /// <param name="operation">A signed DID PLC operation.</param>
        public SignPlcOperationOutput(ATObject operation = default)
        {
            this.Operation = operation;
            this.Type = "com.atproto.identity.signPlcOperation#SignPlcOperationOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SignPlcOperationOutput"/> class.
        /// </summary>
        public SignPlcOperationOutput()
        {
            this.Type = "com.atproto.identity.signPlcOperation#SignPlcOperationOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SignPlcOperationOutput"/> class.
        /// </summary>
        public SignPlcOperationOutput(CBORObject obj)
        {
            if (obj["operation"] is not null) this.Operation = obj["operation"].ToATObject();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the operation.
        /// <br/> A signed DID PLC operation.
        /// </summary>
        [JsonPropertyName("operation")]
        [JsonRequired]
        public ATObject Operation { get; set; }

        public const string RecordType = "com.atproto.identity.signPlcOperation#SignPlcOperationOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationOutput>)SourceGenerationContext.Default.ComAtprotoIdentitySignPlcOperationOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationOutput>)SourceGenerationContext.Default.ComAtprotoIdentitySignPlcOperationOutput);
        }

        public static new SignPlcOperationOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationOutput>)SourceGenerationContext.Default.ComAtprotoIdentitySignPlcOperationOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new SignPlcOperationOutput FromCBORObject(CBORObject obj)
        {
            return new SignPlcOperationOutput(obj);
        }

        /// <inheritdoc/>
        public static SignPlcOperationOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<SignPlcOperationOutput>(s, (JsonTypeInfo<SignPlcOperationOutput>)SourceGenerationContext.Default.ComAtprotoIdentitySignPlcOperationOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out SignPlcOperationOutput result)
        {
            result = JsonSerializer.Deserialize<SignPlcOperationOutput>(s, (JsonTypeInfo<SignPlcOperationOutput>)SourceGenerationContext.Default.ComAtprotoIdentitySignPlcOperationOutput);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Identity
{
    public partial class SignPlcOperationOutput : ATObject, ICBOREncodable<SignPlcOperationOutput>, IJsonEncodable<SignPlcOperationOutput>
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

    }
}


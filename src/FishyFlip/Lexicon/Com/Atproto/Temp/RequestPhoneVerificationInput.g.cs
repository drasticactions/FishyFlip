// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Temp
{
    public partial class RequestPhoneVerificationInput : ATObject, ICBOREncodable<RequestPhoneVerificationInput>, IJsonEncodable<RequestPhoneVerificationInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPhoneVerificationInput"/> class.
        /// </summary>
        /// <param name="phoneNumber"></param>
        public RequestPhoneVerificationInput(string phoneNumber = default)
        {
            this.PhoneNumber = phoneNumber;
            this.Type = "com.atproto.temp.requestPhoneVerification#RequestPhoneVerificationInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPhoneVerificationInput"/> class.
        /// </summary>
        public RequestPhoneVerificationInput()
        {
            this.Type = "com.atproto.temp.requestPhoneVerification#RequestPhoneVerificationInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPhoneVerificationInput"/> class.
        /// </summary>
        public RequestPhoneVerificationInput(CBORObject obj)
        {
            if (obj["phoneNumber"] is not null) this.PhoneNumber = obj["phoneNumber"].AsString();
        }

        /// <summary>
        /// Gets or sets the phoneNumber.
        /// </summary>
        [JsonPropertyName("phoneNumber")]
        [JsonRequired]
        public string PhoneNumber { get; set; }

        public const string RecordType = "com.atproto.temp.requestPhoneVerification#RequestPhoneVerificationInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.RequestPhoneVerificationInput>)SourceGenerationContext.Default.ComAtprotoTempRequestPhoneVerificationInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.RequestPhoneVerificationInput>)SourceGenerationContext.Default.ComAtprotoTempRequestPhoneVerificationInput);
        }

        public static new RequestPhoneVerificationInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Temp.RequestPhoneVerificationInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.RequestPhoneVerificationInput>)SourceGenerationContext.Default.ComAtprotoTempRequestPhoneVerificationInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new RequestPhoneVerificationInput FromCBORObject(CBORObject obj)
        {
            return new RequestPhoneVerificationInput(obj);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Temp
{
    public partial class RequestPhoneVerificationInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPhoneVerificationInput"/> class.
        /// </summary>
        /// <param name="phoneNumber"></param>
        public RequestPhoneVerificationInput(string phoneNumber = default)
        {
            this.PhoneNumber = phoneNumber;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPhoneVerificationInput"/> class.
        /// </summary>
        public RequestPhoneVerificationInput()
        {
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

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.temp.requestPhoneVerification#RequestPhoneVerificationInput";

        public const string RecordType = "com.atproto.temp.requestPhoneVerification#RequestPhoneVerificationInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Temp.RequestPhoneVerificationInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.RequestPhoneVerificationInput>)SourceGenerationContext.Default.ComAtprotoTempRequestPhoneVerificationInput)!;
        }

        public static RequestPhoneVerificationInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Temp.RequestPhoneVerificationInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.RequestPhoneVerificationInput>)SourceGenerationContext.Default.ComAtprotoTempRequestPhoneVerificationInput)!;
        }
    }
}


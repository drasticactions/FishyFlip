// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class DeleteAccountInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountInput"/> class.
        /// </summary>
        /// <param name="did"></param>
        public DeleteAccountInput(FishyFlip.Models.ATDid did = default)
        {
            this.Did = did;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountInput"/> class.
        /// </summary>
        public DeleteAccountInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountInput"/> class.
        /// </summary>
        public DeleteAccountInput(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.deleteAccount#DeleteAccountInput";

        public const string RecordType = "com.atproto.admin.deleteAccount#DeleteAccountInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Admin.DeleteAccountInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.DeleteAccountInput>)SourceGenerationContext.Default.ComAtprotoAdminDeleteAccountInput)!;
        }

        public static DeleteAccountInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.DeleteAccountInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.DeleteAccountInput>)SourceGenerationContext.Default.ComAtprotoAdminDeleteAccountInput)!;
        }
    }
}


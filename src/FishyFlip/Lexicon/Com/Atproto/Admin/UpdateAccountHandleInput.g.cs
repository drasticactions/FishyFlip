// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class UpdateAccountHandleInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountHandleInput"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="handle"></param>
        public UpdateAccountHandleInput(FishyFlip.Models.ATDid did = default, FishyFlip.Models.ATHandle handle = default)
        {
            this.Did = did;
            this.Handle = handle;
            this.Type = "com.atproto.admin.updateAccountHandle#UpdateAccountHandleInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountHandleInput"/> class.
        /// </summary>
        public UpdateAccountHandleInput()
        {
            this.Type = "com.atproto.admin.updateAccountHandle#UpdateAccountHandleInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountHandleInput"/> class.
        /// </summary>
        public UpdateAccountHandleInput(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle Handle { get; set; }

        public const string RecordType = "com.atproto.admin.updateAccountHandle#UpdateAccountHandleInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateAccountHandleInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateAccountHandleInput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateAccountHandleInput)!;
        }

        public static UpdateAccountHandleInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateAccountHandleInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateAccountHandleInput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateAccountHandleInput)!;
        }
    }
}


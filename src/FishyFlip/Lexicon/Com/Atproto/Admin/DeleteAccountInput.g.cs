// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class DeleteAccountInput : ATObject, ICBOREncodable<DeleteAccountInput>, IJsonEncodable<DeleteAccountInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountInput"/> class.
        /// </summary>
        /// <param name="did"></param>
        public DeleteAccountInput(FishyFlip.Models.ATDid did = default)
        {
            this.Did = did;
            this.Type = "com.atproto.admin.deleteAccount#DeleteAccountInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountInput"/> class.
        /// </summary>
        public DeleteAccountInput()
        {
            this.Type = "com.atproto.admin.deleteAccount#DeleteAccountInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountInput"/> class.
        /// </summary>
        public DeleteAccountInput(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        public const string RecordType = "com.atproto.admin.deleteAccount#DeleteAccountInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.DeleteAccountInput>)SourceGenerationContext.Default.ComAtprotoAdminDeleteAccountInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.DeleteAccountInput>)SourceGenerationContext.Default.ComAtprotoAdminDeleteAccountInput);
        }

        public static new DeleteAccountInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.DeleteAccountInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.DeleteAccountInput>)SourceGenerationContext.Default.ComAtprotoAdminDeleteAccountInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new DeleteAccountInput FromCBORObject(CBORObject obj)
        {
            return new DeleteAccountInput(obj);
        }

    }
}


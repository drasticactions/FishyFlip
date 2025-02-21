// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class UpdateAccountEmailInput : ATObject, ICBOREncodable<UpdateAccountEmailInput>, IJsonEncodable<UpdateAccountEmailInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountEmailInput"/> class.
        /// </summary>
        /// <param name="account">The handle or DID of the repo.</param>
        /// <param name="email"></param>
        public UpdateAccountEmailInput(FishyFlip.Models.ATIdentifier account = default, string email = default)
        {
            this.Account = account;
            this.Email = email;
            this.Type = "com.atproto.admin.updateAccountEmail#UpdateAccountEmailInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountEmailInput"/> class.
        /// </summary>
        public UpdateAccountEmailInput()
        {
            this.Type = "com.atproto.admin.updateAccountEmail#UpdateAccountEmailInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountEmailInput"/> class.
        /// </summary>
        public UpdateAccountEmailInput(CBORObject obj)
        {
            if (obj["account"] is not null) this.Account = obj["account"].ToATIdentifier();
            if (obj["email"] is not null) this.Email = obj["email"].AsString();
        }

        /// <summary>
        /// Gets or sets the account.
        /// <br/> The handle or DID of the repo.
        /// </summary>
        [JsonPropertyName("account")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATIdentifierJsonConverter))]
        public FishyFlip.Models.ATIdentifier Account { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonPropertyName("email")]
        [JsonRequired]
        public string Email { get; set; }

        public const string RecordType = "com.atproto.admin.updateAccountEmail#UpdateAccountEmailInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateAccountEmailInput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateAccountEmailInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateAccountEmailInput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateAccountEmailInput);
        }

        public static new UpdateAccountEmailInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateAccountEmailInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateAccountEmailInput>)SourceGenerationContext.Default.ComAtprotoAdminUpdateAccountEmailInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new UpdateAccountEmailInput FromCBORObject(CBORObject obj)
        {
            return new UpdateAccountEmailInput(obj);
        }

    }
}


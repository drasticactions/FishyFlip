// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class AccountCodes : ATObject, ICBOREncodable<AccountCodes>, IJsonEncodable<AccountCodes>, IParsable<AccountCodes>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCodes"/> class.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="codes"></param>
        public AccountCodes(string account = default, List<string> codes = default)
        {
            this.Account = account;
            this.Codes = codes;
            this.Type = "com.atproto.server.createInviteCodes#accountCodes";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCodes"/> class.
        /// </summary>
        public AccountCodes()
        {
            this.Type = "com.atproto.server.createInviteCodes#accountCodes";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCodes"/> class.
        /// </summary>
        public AccountCodes(CBORObject obj)
        {
            if (obj["account"] is not null) this.Account = obj["account"].AsString();
            if (obj["codes"] is not null) this.Codes = obj["codes"].Values.Select(n =>n.AsString()).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        [JsonPropertyName("account")]
        [JsonRequired]
        public string Account { get; set; }

        /// <summary>
        /// Gets or sets the codes.
        /// </summary>
        [JsonPropertyName("codes")]
        [JsonRequired]
        public List<string> Codes { get; set; }

        public const string RecordType = "com.atproto.server.createInviteCodes#accountCodes";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.AccountCodes>)SourceGenerationContext.Default.ComAtprotoServerAccountCodes);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.AccountCodes>)SourceGenerationContext.Default.ComAtprotoServerAccountCodes);
        }

        public static new AccountCodes FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.AccountCodes>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.AccountCodes>)SourceGenerationContext.Default.ComAtprotoServerAccountCodes)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new AccountCodes FromCBORObject(CBORObject obj)
        {
            return new AccountCodes(obj);
        }

        /// <inheritdoc/>
        public static AccountCodes Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<AccountCodes>(s, (JsonTypeInfo<AccountCodes>)SourceGenerationContext.Default.ComAtprotoServerAccountCodes)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out AccountCodes result)
        {
            result = JsonSerializer.Deserialize<AccountCodes>(s, (JsonTypeInfo<AccountCodes>)SourceGenerationContext.Default.ComAtprotoServerAccountCodes);
            return result != null;
        }
    }
}


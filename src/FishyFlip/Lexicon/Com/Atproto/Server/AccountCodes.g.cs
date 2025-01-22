// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class AccountCodes : ATObject
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

        public static AccountCodes FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.AccountCodes>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.AccountCodes>)SourceGenerationContext.Default.ComAtprotoServerAccountCodes)!;
        }
    }
}


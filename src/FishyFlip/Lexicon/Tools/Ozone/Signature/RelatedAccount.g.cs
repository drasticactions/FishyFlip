// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Signature
{
    public partial class RelatedAccount : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedAccount"/> class.
        /// </summary>
        /// <param name="account">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.AccountView"/> (com.atproto.admin.defs#accountView)
        /// </param>
        /// <param name="similarities"></param>
        public RelatedAccount(FishyFlip.Lexicon.Com.Atproto.Admin.AccountView account = default, List<FishyFlip.Lexicon.Tools.Ozone.Signature.SigDetail>? similarities = default)
        {
            this.Account = account;
            this.Similarities = similarities;
            this.Type = "tools.ozone.signature.findRelatedAccounts#relatedAccount";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedAccount"/> class.
        /// </summary>
        public RelatedAccount()
        {
            this.Type = "tools.ozone.signature.findRelatedAccounts#relatedAccount";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedAccount"/> class.
        /// </summary>
        public RelatedAccount(CBORObject obj)
        {
            if (obj["account"] is not null) this.Account = new FishyFlip.Lexicon.Com.Atproto.Admin.AccountView(obj["account"]);
            if (obj["similarities"] is not null) this.Similarities = obj["similarities"].Values.Select(n =>new FishyFlip.Lexicon.Tools.Ozone.Signature.SigDetail(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the account.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.AccountView"/> (com.atproto.admin.defs#accountView)
        /// </summary>
        [JsonPropertyName("account")]
        [JsonRequired]
        public FishyFlip.Lexicon.Com.Atproto.Admin.AccountView Account { get; set; }

        /// <summary>
        /// Gets or sets the similarities.
        /// </summary>
        [JsonPropertyName("similarities")]
        public List<FishyFlip.Lexicon.Tools.Ozone.Signature.SigDetail>? Similarities { get; set; }

        public const string RecordType = "tools.ozone.signature.findRelatedAccounts#relatedAccount";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Signature.RelatedAccount>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Signature.RelatedAccount>)SourceGenerationContext.Default.ToolsOzoneSignatureRelatedAccount)!;
        }

        public static RelatedAccount FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Signature.RelatedAccount>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Signature.RelatedAccount>)SourceGenerationContext.Default.ToolsOzoneSignatureRelatedAccount)!;
        }
    }
}


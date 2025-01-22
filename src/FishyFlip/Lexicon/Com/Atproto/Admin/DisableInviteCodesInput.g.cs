// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class DisableInviteCodesInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DisableInviteCodesInput"/> class.
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="accounts"></param>
        public DisableInviteCodesInput(List<string>? codes = default, List<string>? accounts = default)
        {
            this.Codes = codes;
            this.Accounts = accounts;
            this.Type = "com.atproto.admin.disableInviteCodes#DisableInviteCodesInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DisableInviteCodesInput"/> class.
        /// </summary>
        public DisableInviteCodesInput()
        {
            this.Type = "com.atproto.admin.disableInviteCodes#DisableInviteCodesInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DisableInviteCodesInput"/> class.
        /// </summary>
        public DisableInviteCodesInput(CBORObject obj)
        {
            if (obj["codes"] is not null) this.Codes = obj["codes"].Values.Select(n =>n.AsString()).ToList();
            if (obj["accounts"] is not null) this.Accounts = obj["accounts"].Values.Select(n =>n.AsString()).ToList();
        }

        /// <summary>
        /// Gets or sets the codes.
        /// </summary>
        [JsonPropertyName("codes")]
        public List<string>? Codes { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        [JsonPropertyName("accounts")]
        public List<string>? Accounts { get; set; }

        public const string RecordType = "com.atproto.admin.disableInviteCodes#DisableInviteCodesInput";

        public static DisableInviteCodesInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.DisableInviteCodesInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.DisableInviteCodesInput>)SourceGenerationContext.Default.ComAtprotoAdminDisableInviteCodesInput)!;
        }
    }
}


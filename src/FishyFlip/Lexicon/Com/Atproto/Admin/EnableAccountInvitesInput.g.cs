// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class EnableAccountInvitesInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EnableAccountInvitesInput"/> class.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="note">Optional reason for enabled invites.</param>
        public EnableAccountInvitesInput(FishyFlip.Models.ATDid? account = default, string? note = default)
        {
            this.Account = account;
            this.Note = note;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EnableAccountInvitesInput"/> class.
        /// </summary>
        public EnableAccountInvitesInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EnableAccountInvitesInput"/> class.
        /// </summary>
        public EnableAccountInvitesInput(CBORObject obj)
        {
            if (obj["account"] is not null) this.Account = obj["account"].ToATDid();
            if (obj["note"] is not null) this.Note = obj["note"].AsString();
        }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        [JsonPropertyName("account")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Account { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// Optional reason for enabled invites.
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.enableAccountInvites#EnableAccountInvitesInput";

        public const string RecordType = "com.atproto.admin.enableAccountInvites#EnableAccountInvitesInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Admin.EnableAccountInvitesInput>(this, (JsonTypeInfo<Com.Atproto.Admin.EnableAccountInvitesInput>)SourceGenerationContext.Default.ComAtprotoAdminEnableAccountInvitesInput)!;
        }

        public static EnableAccountInvitesInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Admin.EnableAccountInvitesInput>(json, (JsonTypeInfo<Com.Atproto.Admin.EnableAccountInvitesInput>)SourceGenerationContext.Default.ComAtprotoAdminEnableAccountInvitesInput)!;
        }
    }
}

// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class RepoViewDetail : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoViewDetail"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="handle"></param>
        /// <param name="email"></param>
        /// <param name="relatedRecords"></param>
        /// <param name="indexedAt"></param>
        /// <param name="moderation">
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModerationDetail"/> (tools.ozone.moderation.defs#moderationDetail)
        /// </param>
        /// <param name="labels"></param>
        /// <param name="invitedBy">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Server.InviteCode"/> (com.atproto.server.defs#inviteCode)
        /// </param>
        /// <param name="invites"></param>
        /// <param name="invitesDisabled"></param>
        /// <param name="inviteNote"></param>
        /// <param name="emailConfirmedAt"></param>
        /// <param name="deactivatedAt"></param>
        /// <param name="threatSignatures"></param>
        public RepoViewDetail(FishyFlip.Models.ATDid? did = default, FishyFlip.Models.ATHandle? handle = default, string? email = default, List<ATObject>? relatedRecords = default, DateTime? indexedAt = default, Tools.Ozone.Moderation.ModerationDetail? moderation = default, List<Com.Atproto.Label.Label>? labels = default, Com.Atproto.Server.InviteCode? invitedBy = default, List<Com.Atproto.Server.InviteCode>? invites = default, bool? invitesDisabled = default, string? inviteNote = default, DateTime? emailConfirmedAt = default, DateTime? deactivatedAt = default, List<Com.Atproto.Admin.ThreatSignature>? threatSignatures = default)
        {
            this.Did = did;
            this.Handle = handle;
            this.Email = email;
            this.RelatedRecords = relatedRecords;
            this.IndexedAt = indexedAt;
            this.Moderation = moderation;
            this.Labels = labels;
            this.InvitedBy = invitedBy;
            this.Invites = invites;
            this.InvitesDisabled = invitesDisabled;
            this.InviteNote = inviteNote;
            this.EmailConfirmedAt = emailConfirmedAt;
            this.DeactivatedAt = deactivatedAt;
            this.ThreatSignatures = threatSignatures;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RepoViewDetail"/> class.
        /// </summary>
        public RepoViewDetail()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RepoViewDetail"/> class.
        /// </summary>
        public RepoViewDetail(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
            if (obj["email"] is not null) this.Email = obj["email"].AsString();
            if (obj["relatedRecords"] is not null) this.RelatedRecords = obj["relatedRecords"].Values.Select(n =>n.ToATObject()).ToList();
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
            if (obj["moderation"] is not null) this.Moderation = new Tools.Ozone.Moderation.ModerationDetail(obj["moderation"]);
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new Com.Atproto.Label.Label(n)).ToList();
            if (obj["invitedBy"] is not null) this.InvitedBy = new Com.Atproto.Server.InviteCode(obj["invitedBy"]);
            if (obj["invites"] is not null) this.Invites = obj["invites"].Values.Select(n =>new Com.Atproto.Server.InviteCode(n)).ToList();
            if (obj["invitesDisabled"] is not null) this.InvitesDisabled = obj["invitesDisabled"].AsBoolean();
            if (obj["inviteNote"] is not null) this.InviteNote = obj["inviteNote"].AsString();
            if (obj["emailConfirmedAt"] is not null) this.EmailConfirmedAt = obj["emailConfirmedAt"].ToDateTime();
            if (obj["deactivatedAt"] is not null) this.DeactivatedAt = obj["deactivatedAt"].ToDateTime();
            if (obj["threatSignatures"] is not null) this.ThreatSignatures = obj["threatSignatures"].Values.Select(n =>new Com.Atproto.Admin.ThreatSignature(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle? Handle { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the relatedRecords.
        /// </summary>
        [JsonPropertyName("relatedRecords")]
        [JsonRequired]
        public List<ATObject>? RelatedRecords { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        /// <summary>
        /// Gets or sets the moderation.
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModerationDetail"/> (tools.ozone.moderation.defs#moderationDetail)
        /// </summary>
        [JsonPropertyName("moderation")]
        [JsonRequired]
        public Tools.Ozone.Moderation.ModerationDetail? Moderation { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public List<Com.Atproto.Label.Label>? Labels { get; set; }

        /// <summary>
        /// Gets or sets the invitedBy.
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Server.InviteCode"/> (com.atproto.server.defs#inviteCode)
        /// </summary>
        [JsonPropertyName("invitedBy")]
        public Com.Atproto.Server.InviteCode? InvitedBy { get; set; }

        /// <summary>
        /// Gets or sets the invites.
        /// </summary>
        [JsonPropertyName("invites")]
        public List<Com.Atproto.Server.InviteCode>? Invites { get; set; }

        /// <summary>
        /// Gets or sets the invitesDisabled.
        /// </summary>
        [JsonPropertyName("invitesDisabled")]
        public bool? InvitesDisabled { get; set; }

        /// <summary>
        /// Gets or sets the inviteNote.
        /// </summary>
        [JsonPropertyName("inviteNote")]
        public string? InviteNote { get; set; }

        /// <summary>
        /// Gets or sets the emailConfirmedAt.
        /// </summary>
        [JsonPropertyName("emailConfirmedAt")]
        public DateTime? EmailConfirmedAt { get; set; }

        /// <summary>
        /// Gets or sets the deactivatedAt.
        /// </summary>
        [JsonPropertyName("deactivatedAt")]
        public DateTime? DeactivatedAt { get; set; }

        /// <summary>
        /// Gets or sets the threatSignatures.
        /// </summary>
        [JsonPropertyName("threatSignatures")]
        public List<Com.Atproto.Admin.ThreatSignature>? ThreatSignatures { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#repoViewDetail";

        public const string RecordType = "tools.ozone.moderation.defs#repoViewDetail";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.RepoViewDetail>(this, (JsonTypeInfo<Tools.Ozone.Moderation.RepoViewDetail>)SourceGenerationContext.Default.ToolsOzoneModerationRepoViewDetail)!;
        }

        public static RepoViewDetail FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.RepoViewDetail>(json, (JsonTypeInfo<Tools.Ozone.Moderation.RepoViewDetail>)SourceGenerationContext.Default.ToolsOzoneModerationRepoViewDetail)!;
        }
    }
}

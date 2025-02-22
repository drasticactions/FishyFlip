// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class InviteCode : ATObject, ICBOREncodable<InviteCode>, IJsonEncodable<InviteCode>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InviteCode"/> class.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="available"></param>
        /// <param name="disabled"></param>
        /// <param name="forAccount"></param>
        /// <param name="createdBy"></param>
        /// <param name="createdAt"></param>
        /// <param name="uses"></param>
        public InviteCode(string code = default, long available = default, bool disabled = default, string forAccount = default, string createdBy = default, DateTime? createdAt = default, List<FishyFlip.Lexicon.Com.Atproto.Server.InviteCodeUse> uses = default)
        {
            this.Code = code;
            this.Available = available;
            this.Disabled = disabled;
            this.ForAccount = forAccount;
            this.CreatedBy = createdBy;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Uses = uses;
            this.Type = "com.atproto.server.defs#inviteCode";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InviteCode"/> class.
        /// </summary>
        public InviteCode()
        {
            this.Type = "com.atproto.server.defs#inviteCode";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InviteCode"/> class.
        /// </summary>
        public InviteCode(CBORObject obj)
        {
            if (obj["code"] is not null) this.Code = obj["code"].AsString();
            if (obj["available"] is not null) this.Available = obj["available"].AsInt64Value();
            if (obj["disabled"] is not null) this.Disabled = obj["disabled"].AsBoolean();
            if (obj["forAccount"] is not null) this.ForAccount = obj["forAccount"].AsString();
            if (obj["createdBy"] is not null) this.CreatedBy = obj["createdBy"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["uses"] is not null) this.Uses = obj["uses"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Server.InviteCodeUse(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [JsonPropertyName("code")]
        [JsonRequired]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the available.
        /// </summary>
        [JsonPropertyName("available")]
        [JsonRequired]
        public long Available { get; set; }

        /// <summary>
        /// Gets or sets the disabled.
        /// </summary>
        [JsonPropertyName("disabled")]
        [JsonRequired]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the forAccount.
        /// </summary>
        [JsonPropertyName("forAccount")]
        [JsonRequired]
        public string ForAccount { get; set; }

        /// <summary>
        /// Gets or sets the createdBy.
        /// </summary>
        [JsonPropertyName("createdBy")]
        [JsonRequired]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonRequired]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the uses.
        /// </summary>
        [JsonPropertyName("uses")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Atproto.Server.InviteCodeUse> Uses { get; set; }

        public const string RecordType = "com.atproto.server.defs#inviteCode";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.InviteCode>)SourceGenerationContext.Default.ComAtprotoServerInviteCode);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.InviteCode>)SourceGenerationContext.Default.ComAtprotoServerInviteCode);
        }

        public static new InviteCode FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.InviteCode>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.InviteCode>)SourceGenerationContext.Default.ComAtprotoServerInviteCode)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new InviteCode FromCBORObject(CBORObject obj)
        {
            return new InviteCode(obj);
        }

    }
}


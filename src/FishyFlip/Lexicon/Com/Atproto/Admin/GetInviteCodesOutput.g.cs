// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class GetInviteCodesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInviteCodesOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="codes"></param>
        public GetInviteCodesOutput(string? cursor = default, List<Com.Atproto.Server.InviteCode>? codes = default)
        {
            this.Cursor = cursor;
            this.Codes = codes;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetInviteCodesOutput"/> class.
        /// </summary>
        public GetInviteCodesOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetInviteCodesOutput"/> class.
        /// </summary>
        public GetInviteCodesOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["codes"] is not null) this.Codes = obj["codes"].Values.Select(n =>new Com.Atproto.Server.InviteCode(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the codes.
        /// </summary>
        [JsonPropertyName("codes")]
        [JsonRequired]
        public List<Com.Atproto.Server.InviteCode>? Codes { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.getInviteCodes#GetInviteCodesOutput";

        public const string RecordType = "com.atproto.admin.getInviteCodes#GetInviteCodesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Admin.GetInviteCodesOutput>(this, (JsonTypeInfo<Com.Atproto.Admin.GetInviteCodesOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetInviteCodesOutput)!;
        }

        public static GetInviteCodesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Admin.GetInviteCodesOutput>(json, (JsonTypeInfo<Com.Atproto.Admin.GetInviteCodesOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetInviteCodesOutput)!;
        }
    }
}


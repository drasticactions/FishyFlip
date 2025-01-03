// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class GetAccountInfosOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountInfosOutput"/> class.
        /// </summary>
        /// <param name="infos"></param>
        public GetAccountInfosOutput(List<Com.Atproto.Admin.AccountView>? infos = default)
        {
            this.Infos = infos;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountInfosOutput"/> class.
        /// </summary>
        public GetAccountInfosOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountInfosOutput"/> class.
        /// </summary>
        public GetAccountInfosOutput(CBORObject obj)
        {
            if (obj["infos"] is not null) this.Infos = obj["infos"].Values.Select(n =>new Com.Atproto.Admin.AccountView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the infos.
        /// </summary>
        [JsonPropertyName("infos")]
        [JsonRequired]
        public List<Com.Atproto.Admin.AccountView>? Infos { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.getAccountInfos#GetAccountInfosOutput";

        public const string RecordType = "com.atproto.admin.getAccountInfos#GetAccountInfosOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Admin.GetAccountInfosOutput>(this, (JsonTypeInfo<Com.Atproto.Admin.GetAccountInfosOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetAccountInfosOutput)!;
        }

        public static GetAccountInfosOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Admin.GetAccountInfosOutput>(json, (JsonTypeInfo<Com.Atproto.Admin.GetAccountInfosOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetAccountInfosOutput)!;
        }
    }
}


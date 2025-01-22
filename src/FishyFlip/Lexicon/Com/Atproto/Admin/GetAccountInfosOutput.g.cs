// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class GetAccountInfosOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountInfosOutput"/> class.
        /// </summary>
        /// <param name="infos"></param>
        public GetAccountInfosOutput(List<FishyFlip.Lexicon.Com.Atproto.Admin.AccountView> infos = default)
        {
            this.Infos = infos;
            this.Type = "com.atproto.admin.getAccountInfos#GetAccountInfosOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountInfosOutput"/> class.
        /// </summary>
        public GetAccountInfosOutput()
        {
            this.Type = "com.atproto.admin.getAccountInfos#GetAccountInfosOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountInfosOutput"/> class.
        /// </summary>
        public GetAccountInfosOutput(CBORObject obj)
        {
            if (obj["infos"] is not null) this.Infos = obj["infos"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Admin.AccountView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the infos.
        /// </summary>
        [JsonPropertyName("infos")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Atproto.Admin.AccountView> Infos { get; set; }

        public const string RecordType = "com.atproto.admin.getAccountInfos#GetAccountInfosOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Admin.GetAccountInfosOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.GetAccountInfosOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetAccountInfosOutput)!;
        }

        public static GetAccountInfosOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.GetAccountInfosOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.GetAccountInfosOutput>)SourceGenerationContext.Default.ComAtprotoAdminGetAccountInfosOutput)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class RepoBlobRef : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoBlobRef"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cid"></param>
        /// <param name="recordUri"></param>
        public RepoBlobRef(FishyFlip.Models.ATDid? did = default, string? cid = default, FishyFlip.Models.ATUri? recordUri = default)
        {
            this.Did = did;
            this.Cid = cid;
            this.RecordUri = recordUri;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RepoBlobRef"/> class.
        /// </summary>
        public RepoBlobRef()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RepoBlobRef"/> class.
        /// </summary>
        public RepoBlobRef(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["recordUri"] is not null) this.RecordUri = obj["recordUri"].ToATUri();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        [JsonRequired]
        public string? Cid { get; set; }

        /// <summary>
        /// Gets or sets the recordUri.
        /// </summary>
        [JsonPropertyName("recordUri")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? RecordUri { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.defs#repoBlobRef";

        public const string RecordType = "com.atproto.admin.defs#repoBlobRef";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Admin.RepoBlobRef>(this, (JsonTypeInfo<Com.Atproto.Admin.RepoBlobRef>)SourceGenerationContext.Default.ComAtprotoAdminRepoBlobRef)!;
        }

        public static RepoBlobRef FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Admin.RepoBlobRef>(json, (JsonTypeInfo<Com.Atproto.Admin.RepoBlobRef>)SourceGenerationContext.Default.ComAtprotoAdminRepoBlobRef)!;
        }
    }
}

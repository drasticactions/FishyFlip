// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class StatusAttr : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusAttr"/> class.
        /// </summary>
        public StatusAttr()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StatusAttr"/> class.
        /// </summary>
        public StatusAttr(CBORObject obj)
        {
            if (obj["applied"] is not null) this.Applied = obj["applied"].AsBoolean();
            if (obj["ref"] is not null) this.Ref = obj["ref"].AsString();
        }

        [JsonPropertyName("applied")]
        [JsonRequired]
        public bool? Applied { get; set; }

        [JsonPropertyName("ref")]
        public string? Ref { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.admin.defs#statusAttr";

        public const string RecordType = "com.atproto.admin.defs#statusAttr";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Admin.StatusAttr>(this, (JsonTypeInfo<Com.Atproto.Admin.StatusAttr>)SourceGenerationContext.Default.ComAtprotoAdminStatusAttr)!;
        }

        public static StatusAttr FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Admin.StatusAttr>(json, (JsonTypeInfo<Com.Atproto.Admin.StatusAttr>)SourceGenerationContext.Default.ComAtprotoAdminStatusAttr)!;
        }
    }
}


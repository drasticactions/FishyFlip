// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class StatusAttr : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusAttr"/> class.
        /// </summary>
        /// <param name="applied"></param>
        /// <param name="@ref"></param>
        public StatusAttr(bool applied = default, string? @ref = default)
        {
            this.Applied = applied;
            this.Ref = @ref;
            this.Type = "com.atproto.admin.defs#statusAttr";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StatusAttr"/> class.
        /// </summary>
        public StatusAttr()
        {
            this.Type = "com.atproto.admin.defs#statusAttr";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StatusAttr"/> class.
        /// </summary>
        public StatusAttr(CBORObject obj)
        {
            if (obj["applied"] is not null) this.Applied = obj["applied"].AsBoolean();
            if (obj["ref"] is not null) this.Ref = obj["ref"].AsString();
        }

        /// <summary>
        /// Gets or sets the applied.
        /// </summary>
        [JsonPropertyName("applied")]
        [JsonRequired]
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the ref.
        /// </summary>
        [JsonPropertyName("ref")]
        public string? Ref { get; set; }

        public const string RecordType = "com.atproto.admin.defs#statusAttr";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr>)SourceGenerationContext.Default.ComAtprotoAdminStatusAttr)!;
        }

        public static StatusAttr FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr>)SourceGenerationContext.Default.ComAtprotoAdminStatusAttr)!;
        }
    }
}


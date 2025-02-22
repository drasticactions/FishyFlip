// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class StatusAttr : ATObject, ICBOREncodable<StatusAttr>, IJsonEncodable<StatusAttr>
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
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
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
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr>)SourceGenerationContext.Default.ComAtprotoAdminStatusAttr);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr>)SourceGenerationContext.Default.ComAtprotoAdminStatusAttr);
        }

        public static new StatusAttr FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr>)SourceGenerationContext.Default.ComAtprotoAdminStatusAttr)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new StatusAttr FromCBORObject(CBORObject obj)
        {
            return new StatusAttr(obj);
        }

    }
}


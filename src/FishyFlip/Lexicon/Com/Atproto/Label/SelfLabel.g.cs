// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Label
{
    /// <summary>
    /// Metadata tag on an atproto record, published by the author within the record. Note that schemas should use #selfLabels, not #selfLabel.
    /// </summary>
    public partial class SelfLabel : ATObject, ICBOREncodable<SelfLabel>, IJsonEncodable<SelfLabel>, IParsable<SelfLabel>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfLabel"/> class.
        /// </summary>
        /// <param name="val">The short string name of the value or type of this label.</param>
        public SelfLabel(string val = default)
        {
            this.Val = val;
            this.Type = "com.atproto.label.defs#selfLabel";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SelfLabel"/> class.
        /// </summary>
        public SelfLabel()
        {
            this.Type = "com.atproto.label.defs#selfLabel";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SelfLabel"/> class.
        /// </summary>
        public SelfLabel(CBORObject obj)
        {
            if (obj["val"] is not null) this.Val = obj["val"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the val.
        /// <br/> The short string name of the value or type of this label.
        /// </summary>
        [JsonPropertyName("val")]
        [JsonRequired]
        public string Val { get; set; }

        public const string RecordType = "com.atproto.label.defs#selfLabel";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Label.SelfLabel>)SourceGenerationContext.Default.ComAtprotoLabelSelfLabel);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Label.SelfLabel>)SourceGenerationContext.Default.ComAtprotoLabelSelfLabel);
        }

        public static new SelfLabel FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Label.SelfLabel>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Label.SelfLabel>)SourceGenerationContext.Default.ComAtprotoLabelSelfLabel)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new SelfLabel FromCBORObject(CBORObject obj)
        {
            return new SelfLabel(obj);
        }

        /// <inheritdoc/>
        public static SelfLabel Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<SelfLabel>(s, (JsonTypeInfo<SelfLabel>)SourceGenerationContext.Default.ComAtprotoLabelSelfLabel)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out SelfLabel result)
        {
            result = JsonSerializer.Deserialize<SelfLabel>(s, (JsonTypeInfo<SelfLabel>)SourceGenerationContext.Default.ComAtprotoLabelSelfLabel);
            return result != null;
        }
    }
}


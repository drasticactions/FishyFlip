// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Label
{
    /// <summary>
    /// Declares a label value and its expected interpretations and behaviors.
    /// </summary>
    public partial class LabelValueDefinition : ATObject, ICBOREncodable<LabelValueDefinition>, IJsonEncodable<LabelValueDefinition>, IParsable<LabelValueDefinition>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelValueDefinition"/> class.
        /// </summary>
        /// <param name="identifier">The value of the label being defined. Must only include lowercase ascii and the '-' character ([a-z-]+).</param>
        /// <param name="severity">How should a client visually convey this label? 'inform' means neutral and informational; 'alert' means negative and warning; 'none' means show nothing.
        /// <br/> Known Values: <br/>
        /// inform <br/>
        /// alert <br/>
        /// none <br/>
        /// </param>
        /// <param name="blurs">What should this label hide in the UI, if applied? 'content' hides all of the target; 'media' hides the images/video/audio; 'none' hides nothing.
        /// <br/> Known Values: <br/>
        /// content <br/>
        /// media <br/>
        /// none <br/>
        /// </param>
        /// <param name="defaultSetting">The default setting for this label.
        /// <br/> Known Values: <br/>
        /// ignore <br/>
        /// warn <br/>
        /// hide <br/>
        /// </param>
        /// <param name="adultOnly">Does the user need to have adult content enabled in order to configure this label?</param>
        /// <param name="locales"></param>
        public LabelValueDefinition(string identifier = default, string severity = default, string blurs = default, string? defaultSetting = default, bool? adultOnly = default, List<FishyFlip.Lexicon.Com.Atproto.Label.LabelValueDefinitionStrings> locales = default)
        {
            this.Identifier = identifier;
            this.Severity = severity;
            this.Blurs = blurs;
            this.DefaultSetting = defaultSetting;
            this.AdultOnly = adultOnly;
            this.Locales = locales;
            this.Type = "com.atproto.label.defs#labelValueDefinition";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LabelValueDefinition"/> class.
        /// </summary>
        public LabelValueDefinition()
        {
            this.Type = "com.atproto.label.defs#labelValueDefinition";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LabelValueDefinition"/> class.
        /// </summary>
        public LabelValueDefinition(CBORObject obj)
        {
            if (obj["identifier"] is not null) this.Identifier = obj["identifier"].AsString();
            if (obj["severity"] is not null) this.Severity = obj["severity"].AsString();
            if (obj["blurs"] is not null) this.Blurs = obj["blurs"].AsString();
            if (obj["defaultSetting"] is not null) this.DefaultSetting = obj["defaultSetting"].AsString();
            if (obj["adultOnly"] is not null) this.AdultOnly = obj["adultOnly"].AsBoolean();
            if (obj["locales"] is not null) this.Locales = obj["locales"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Label.LabelValueDefinitionStrings(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// <br/> The value of the label being defined. Must only include lowercase ascii and the '-' character ([a-z-]+).
        /// </summary>
        [JsonPropertyName("identifier")]
        [JsonRequired]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// <br/> How should a client visually convey this label? 'inform' means neutral and informational; 'alert' means negative and warning; 'none' means show nothing.
        /// <br/> Known Values: <br/>
        /// inform <br/>
        /// alert <br/>
        /// none <br/>
        /// </summary>
        [JsonPropertyName("severity")]
        [JsonRequired]
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the blurs.
        /// <br/> What should this label hide in the UI, if applied? 'content' hides all of the target; 'media' hides the images/video/audio; 'none' hides nothing.
        /// <br/> Known Values: <br/>
        /// content <br/>
        /// media <br/>
        /// none <br/>
        /// </summary>
        [JsonPropertyName("blurs")]
        [JsonRequired]
        public string Blurs { get; set; }

        /// <summary>
        /// Gets or sets the defaultSetting.
        /// <br/> The default setting for this label.
        /// <br/> Known Values: <br/>
        /// ignore <br/>
        /// warn <br/>
        /// hide <br/>
        /// </summary>
        [JsonPropertyName("defaultSetting")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DefaultSetting { get; set; } = "warn";

        /// <summary>
        /// Gets or sets the adultOnly.
        /// <br/> Does the user need to have adult content enabled in order to configure this label?
        /// </summary>
        [JsonPropertyName("adultOnly")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? AdultOnly { get; set; }

        /// <summary>
        /// Gets or sets the locales.
        /// </summary>
        [JsonPropertyName("locales")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Atproto.Label.LabelValueDefinitionStrings> Locales { get; set; }

        public const string RecordType = "com.atproto.label.defs#labelValueDefinition";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Label.LabelValueDefinition>)SourceGenerationContext.Default.ComAtprotoLabelLabelValueDefinition);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Label.LabelValueDefinition>)SourceGenerationContext.Default.ComAtprotoLabelLabelValueDefinition);
        }

        public static new LabelValueDefinition FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Label.LabelValueDefinition>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Label.LabelValueDefinition>)SourceGenerationContext.Default.ComAtprotoLabelLabelValueDefinition)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new LabelValueDefinition FromCBORObject(CBORObject obj)
        {
            return new LabelValueDefinition(obj);
        }

        /// <inheritdoc/>
        public static LabelValueDefinition Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<LabelValueDefinition>(s, (JsonTypeInfo<LabelValueDefinition>)SourceGenerationContext.Default.ComAtprotoLabelLabelValueDefinition)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out LabelValueDefinition result)
        {
            result = JsonSerializer.Deserialize<LabelValueDefinition>(s, (JsonTypeInfo<LabelValueDefinition>)SourceGenerationContext.Default.ComAtprotoLabelLabelValueDefinition);
            return result != null;
        }
    }
}


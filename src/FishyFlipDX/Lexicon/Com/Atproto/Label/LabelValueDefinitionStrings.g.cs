// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Label
{
    /// <summary>
    /// Strings which describe the label in the UI, localized into a specific language.
    /// </summary>
    public partial class LabelValueDefinitionStrings : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelValueDefinitionStrings"/> class.
        /// </summary>
        /// <param name="lang">The code of the language these strings are written in.</param>
        /// <param name="name">A short human-readable name for the label.</param>
        /// <param name="description">A longer description of what the label means and why it might be applied.</param>
        public LabelValueDefinitionStrings(string? lang = default, string? name = default, string? description = default)
        {
            this.Lang = lang;
            this.Name = name;
            this.Description = description;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LabelValueDefinitionStrings"/> class.
        /// </summary>
        public LabelValueDefinitionStrings()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LabelValueDefinitionStrings"/> class.
        /// </summary>
        public LabelValueDefinitionStrings(CBORObject obj)
        {
            if (obj["lang"] is not null) this.Lang = obj["lang"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
        }

        /// <summary>
        /// Gets or sets the lang.
        /// The code of the language these strings are written in.
        /// </summary>
        [JsonPropertyName("lang")]
        [JsonRequired]
        public string? Lang { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// A short human-readable name for the label.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// A longer description of what the label means and why it might be applied.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonRequired]
        public string? Description { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.label.defs#labelValueDefinitionStrings";

        public const string RecordType = "com.atproto.label.defs#labelValueDefinitionStrings";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Label.LabelValueDefinitionStrings>(this, (JsonTypeInfo<Com.Atproto.Label.LabelValueDefinitionStrings>)SourceGenerationContext.Default.ComAtprotoLabelLabelValueDefinitionStrings)!;
        }

        public static LabelValueDefinitionStrings FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Label.LabelValueDefinitionStrings>(json, (JsonTypeInfo<Com.Atproto.Label.LabelValueDefinitionStrings>)SourceGenerationContext.Default.ComAtprotoLabelLabelValueDefinitionStrings)!;
        }
    }
}


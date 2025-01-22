// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.My.Skylights
{
    public partial class UrlItem : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlItem"/> class.
        /// </summary>
        /// <param name="value"></param>
        public UrlItem(string value = default)
        {
            this.Value = value;
            this.Type = "my.skylights.rel#urlItem";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UrlItem"/> class.
        /// </summary>
        public UrlItem()
        {
            this.Type = "my.skylights.rel#urlItem";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UrlItem"/> class.
        /// </summary>
        public UrlItem(CBORObject obj)
        {
            if (obj["value"] is not null) this.Value = obj["value"].AsString();
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public string Value { get; set; }

        public const string RecordType = "my.skylights.rel#urlItem";

        public static UrlItem FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.My.Skylights.UrlItem>(json, (JsonTypeInfo<FishyFlip.Lexicon.My.Skylights.UrlItem>)SourceGenerationContext.Default.MySkylightsUrlItem)!;
        }
    }
}


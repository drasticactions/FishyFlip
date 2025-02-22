// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Richtext
{
    public partial class Facet : ATObject, ICBOREncodable<Facet>, IJsonEncodable<Facet>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        /// <param name="did">DID of the user posting the Bluemoji</param>
        /// <param name="name">Name of the Bluemoji in :emoji: format</param>
        /// <param name="alt"></param>
        /// <param name="adultOnly"></param>
        /// <param name="labels">Self-label values for this emoji. Effectively content warnings.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels"/> (com.atproto.label.defs#selfLabels) <br/>
        /// </param>
        /// <param name="formats">
        /// <br/> Union Types: <br/>
        /// #formats_v0 <br/>
        /// </param>
        public Facet(string did = default, string name = default, string? alt = default, bool? adultOnly = default, FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels? labels = default, FishyFlip.Lexicon.Blue.Moji.Richtext.FormatsV0 formats = default)
        {
            this.Did = did;
            this.Name = name;
            this.Alt = alt;
            this.AdultOnly = adultOnly;
            this.Labels = labels;
            this.Formats = formats;
            this.Type = "blue.moji.richtext.facet";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet()
        {
            this.Type = "blue.moji.richtext.facet";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
            if (obj["adultOnly"] is not null) this.AdultOnly = obj["adultOnly"].AsBoolean();
            if (obj["labels"] is not null) this.Labels = new FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels(obj["labels"]);
            if (obj["formats"] is not null) this.Formats = new FishyFlip.Lexicon.Blue.Moji.Richtext.FormatsV0(obj["formats"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the did.
        /// <br/> DID of the user posting the Bluemoji
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        public string Did { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// <br/> Name of the Bluemoji in :emoji: format
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alt.
        /// </summary>
        [JsonPropertyName("alt")]
        public string? Alt { get; set; }

        /// <summary>
        /// Gets or sets the adultOnly.
        /// </summary>
        [JsonPropertyName("adultOnly")]
        public bool? AdultOnly { get; set; } = false;

        /// <summary>
        /// Gets or sets the labels.
        /// <br/> Self-label values for this emoji. Effectively content warnings.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels"/> (com.atproto.label.defs#selfLabels) <br/>
        /// </summary>
        [JsonPropertyName("labels")]
        public FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels? Labels { get; set; }

        /// <summary>
        /// Gets or sets the formats.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Blue.Moji.Richtext.FormatsV0"/> (blue.moji.richtext.facet#formats_v0) <br/>
        /// </summary>
        [JsonPropertyName("formats")]
        [JsonRequired]
        public FishyFlip.Lexicon.Blue.Moji.Richtext.FormatsV0 Formats { get; set; }

        public const string RecordType = "blue.moji.richtext.facet";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Richtext.Facet>)SourceGenerationContext.Default.BlueMojiRichtextFacet);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Richtext.Facet>)SourceGenerationContext.Default.BlueMojiRichtextFacet);
        }

        public static new Facet FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Richtext.Facet>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Richtext.Facet>)SourceGenerationContext.Default.BlueMojiRichtextFacet)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Facet FromCBORObject(CBORObject obj)
        {
            return new Facet(obj);
        }

    }
}


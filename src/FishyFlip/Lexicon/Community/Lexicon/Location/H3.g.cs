// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Community.Lexicon.Location
{
    /// <summary>
    /// A physical location in the form of a H3 encoded location.
    /// </summary>
    public partial class H3 : ATObject, ICBOREncodable<H3>, IJsonEncodable<H3>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="H3"/> class.
        /// </summary>
        /// <param name="value">The h3 encoded location.</param>
        /// <param name="name">The name of the location.</param>
        public H3(string value = default, string? name = default)
        {
            this.Value = value;
            this.Name = name;
            this.Type = "community.lexicon.location.h3";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="H3"/> class.
        /// </summary>
        public H3()
        {
            this.Type = "community.lexicon.location.h3";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="H3"/> class.
        /// </summary>
        public H3(CBORObject obj)
        {
            if (obj["value"] is not null) this.Value = obj["value"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the value.
        /// <br/> The h3 encoded location.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// <br/> The name of the location.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public const string RecordType = "community.lexicon.location.h3";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Location.H3>)SourceGenerationContext.Default.CommunityLexiconLocationH3);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Location.H3>)SourceGenerationContext.Default.CommunityLexiconLocationH3);
        }

        public static new H3 FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Community.Lexicon.Location.H3>(json, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Location.H3>)SourceGenerationContext.Default.CommunityLexiconLocationH3)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new H3 FromCBORObject(CBORObject obj)
        {
            return new H3(obj);
        }

    }
}


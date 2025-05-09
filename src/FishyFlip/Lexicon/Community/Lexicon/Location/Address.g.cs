// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Community.Lexicon.Location
{
    /// <summary>
    /// A physical location in the form of a street address.
    /// </summary>
    public partial class Address : ATObject, ICBOREncodable<Address>, IJsonEncodable<Address>, IParsable<Address>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="country">The ISO 3166 country code. Preferably the 2-letter code.</param>
        /// <param name="postalCode">The postal code of the location.</param>
        /// <param name="region">The administrative region of the country. For example, a state in the USA.</param>
        /// <param name="locality">The locality of the region. For example, a city in the USA.</param>
        /// <param name="street">The street address.</param>
        /// <param name="name">The name of the location.</param>
        public Address(string country = default, string? postalCode = default, string? region = default, string? locality = default, string? street = default, string? name = default)
        {
            this.Country = country;
            this.PostalCode = postalCode;
            this.Region = region;
            this.Locality = locality;
            this.Street = street;
            this.Name = name;
            this.Type = "community.lexicon.location.address";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        public Address()
        {
            this.Type = "community.lexicon.location.address";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        public Address(CBORObject obj)
        {
            if (obj["country"] is not null) this.Country = obj["country"].AsString();
            if (obj["postalCode"] is not null) this.PostalCode = obj["postalCode"].AsString();
            if (obj["region"] is not null) this.Region = obj["region"].AsString();
            if (obj["locality"] is not null) this.Locality = obj["locality"].AsString();
            if (obj["street"] is not null) this.Street = obj["street"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the country.
        /// <br/> The ISO 3166 country code. Preferably the 2-letter code.
        /// </summary>
        [JsonPropertyName("country")]
        [JsonRequired]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postalCode.
        /// <br/> The postal code of the location.
        /// </summary>
        [JsonPropertyName("postalCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// <br/> The administrative region of the country. For example, a state in the USA.
        /// </summary>
        [JsonPropertyName("region")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets the locality.
        /// <br/> The locality of the region. For example, a city in the USA.
        /// </summary>
        [JsonPropertyName("locality")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Locality { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// <br/> The street address.
        /// </summary>
        [JsonPropertyName("street")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Street { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// <br/> The name of the location.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        public const string RecordType = "community.lexicon.location.address";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Location.Address>)SourceGenerationContext.Default.CommunityLexiconLocationAddress);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Location.Address>)SourceGenerationContext.Default.CommunityLexiconLocationAddress);
        }

        public static new Address FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Community.Lexicon.Location.Address>(json, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Location.Address>)SourceGenerationContext.Default.CommunityLexiconLocationAddress)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Address FromCBORObject(CBORObject obj)
        {
            return new Address(obj);
        }

        /// <inheritdoc/>
        public static Address Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Address>(s, (JsonTypeInfo<Address>)SourceGenerationContext.Default.CommunityLexiconLocationAddress)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Address result)
        {
            result = JsonSerializer.Deserialize<Address>(s, (JsonTypeInfo<Address>)SourceGenerationContext.Default.CommunityLexiconLocationAddress);
            return result != null;
        }
    }
}


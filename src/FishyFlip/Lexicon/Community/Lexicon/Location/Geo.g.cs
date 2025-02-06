// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Community.Lexicon.Location
{
    /// <summary>
    /// A physical location in the form of a WGS84 coordinate.
    /// </summary>
    public partial class Geo : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Geo"/> class.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="altitude"></param>
        /// <param name="name">The name of the location.</param>
        public Geo(string latitude = default, string longitude = default, string? altitude = default, string? name = default)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Altitude = altitude;
            this.Name = name;
            this.Type = "community.lexicon.location.geo";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Geo"/> class.
        /// </summary>
        public Geo()
        {
            this.Type = "community.lexicon.location.geo";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Geo"/> class.
        /// </summary>
        public Geo(CBORObject obj)
        {
            if (obj["latitude"] is not null) this.Latitude = obj["latitude"].AsString();
            if (obj["longitude"] is not null) this.Longitude = obj["longitude"].AsString();
            if (obj["altitude"] is not null) this.Altitude = obj["altitude"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [JsonPropertyName("latitude")]
        [JsonRequired]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [JsonPropertyName("longitude")]
        [JsonRequired]
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        [JsonPropertyName("altitude")]
        public string? Altitude { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// <br/> The name of the location.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public const string RecordType = "community.lexicon.location.geo";

        public static Geo FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Community.Lexicon.Location.Geo>(json, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Location.Geo>)SourceGenerationContext.Default.CommunityLexiconLocationGeo)!;
        }
    }
}


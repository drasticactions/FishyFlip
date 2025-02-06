// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Community.Lexicon.Location
{
    /// <summary>
    /// A physical location contained in the Foursquare Open Source Places dataset.
    /// </summary>
    public partial class Fsq : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Fsq"/> class.
        /// </summary>
        /// <param name="fsq_place_id">The unique identifier of a Foursquare POI.</param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="name">The name of the location.</param>
        public Fsq(string fsq_place_id = default, string? latitude = default, string? longitude = default, string? name = default)
        {
            this.FsqPlaceId = fsq_place_id;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Name = name;
            this.Type = "community.lexicon.location.fsq";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Fsq"/> class.
        /// </summary>
        public Fsq()
        {
            this.Type = "community.lexicon.location.fsq";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Fsq"/> class.
        /// </summary>
        public Fsq(CBORObject obj)
        {
            if (obj["fsq_place_id"] is not null) this.FsqPlaceId = obj["fsq_place_id"].AsString();
            if (obj["latitude"] is not null) this.Latitude = obj["latitude"].AsString();
            if (obj["longitude"] is not null) this.Longitude = obj["longitude"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
        }

        /// <summary>
        /// Gets or sets the fsq_place_id.
        /// <br/> The unique identifier of a Foursquare POI.
        /// </summary>
        [JsonPropertyName("fsq_place_id")]
        [JsonRequired]
        public string FsqPlaceId { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [JsonPropertyName("latitude")]
        public string? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [JsonPropertyName("longitude")]
        public string? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// <br/> The name of the location.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public const string RecordType = "community.lexicon.location.fsq";

        public static Fsq FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Community.Lexicon.Location.Fsq>(json, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Location.Fsq>)SourceGenerationContext.Default.CommunityLexiconLocationFsq)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    /// <summary>
    /// lists the bi-directional graph relationships between one actor (not indicated in the object), and the target actors (the DID included in the object)
    /// </summary>
    public partial class Relationship : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Relationship"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="following">if the actor follows this DID, this is the AT-URI of the follow record</param>
        /// <param name="followedBy">if the actor is followed by this DID, contains the AT-URI of the follow record</param>
        public Relationship(FishyFlip.Models.ATDid? did = default, FishyFlip.Models.ATUri? following = default, FishyFlip.Models.ATUri? followedBy = default)
        {
            this.Did = did;
            this.Following = following;
            this.FollowedBy = followedBy;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Relationship"/> class.
        /// </summary>
        public Relationship()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Relationship"/> class.
        /// </summary>
        public Relationship(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["following"] is not null) this.Following = obj["following"].ToATUri();
            if (obj["followedBy"] is not null) this.FollowedBy = obj["followedBy"].ToATUri();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Gets or sets the following.
        /// <br/> if the actor follows this DID, this is the AT-URI of the follow record
        /// </summary>
        [JsonPropertyName("following")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Following { get; set; }

        /// <summary>
        /// Gets or sets the followedBy.
        /// <br/> if the actor is followed by this DID, contains the AT-URI of the follow record
        /// </summary>
        [JsonPropertyName("followedBy")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? FollowedBy { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.defs#relationship";

        public const string RecordType = "app.bsky.graph.defs#relationship";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.Relationship>(this, (JsonTypeInfo<App.Bsky.Graph.Relationship>)SourceGenerationContext.Default.AppBskyGraphRelationship)!;
        }

        public static Relationship FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.Relationship>(json, (JsonTypeInfo<App.Bsky.Graph.Relationship>)SourceGenerationContext.Default.AppBskyGraphRelationship)!;
        }
    }
}


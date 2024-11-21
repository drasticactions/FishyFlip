// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    public partial class Repo : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Repo"/> class.
        /// </summary>
        public Repo(FishyFlip.Models.ATDid? did = default, string? head = default, string? rev = default, bool? active = default, string? status = default)
        {
            this.Did = did;
            this.Head = head;
            this.Rev = rev;
            this.Active = active;
            this.Status = status;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Repo"/> class.
        /// </summary>
        public Repo()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Repo"/> class.
        /// </summary>
        public Repo(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["head"] is not null) this.Head = obj["head"].AsString();
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
            if (obj["active"] is not null) this.Active = obj["active"].AsBoolean();
            // enum
        }

        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Current repo commit CID
        /// </summary>
        [JsonPropertyName("head")]
        [JsonRequired]
        public string? Head { get; set; }

        [JsonPropertyName("rev")]
        [JsonRequired]
        public string? Rev { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        /// <summary>
        /// If active=false, this optional field indicates a possible reason for why the account is not active. If active=false and no status is supplied, then the host makes no claim for why the repository is no longer being hosted.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.sync.listRepos#repo";

        public const string RecordType = "com.atproto.sync.listRepos#repo";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Sync.Repo>(this, (JsonTypeInfo<Com.Atproto.Sync.Repo>)SourceGenerationContext.Default.ComAtprotoSyncRepo)!;
        }

        public static Repo FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Sync.Repo>(json, (JsonTypeInfo<Com.Atproto.Sync.Repo>)SourceGenerationContext.Default.ComAtprotoSyncRepo)!;
        }
    }
}


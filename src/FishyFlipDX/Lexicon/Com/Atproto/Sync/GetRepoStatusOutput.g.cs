// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    public partial class GetRepoStatusOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRepoStatusOutput"/> class.
        /// </summary>
        public GetRepoStatusOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetRepoStatusOutput"/> class.
        /// </summary>
        public GetRepoStatusOutput(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["active"] is not null) this.Active = obj["active"].AsBoolean();
            // enum
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
        }

        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        [JsonPropertyName("active")]
        [JsonRequired]
        public bool? Active { get; set; }

        /// <summary>
        /// If active=false, this optional field indicates a possible reason for why the account is not active. If active=false and no status is supplied, then the host makes no claim for why the repository is no longer being hosted.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Optional field, the current rev of the repo, if active=true
        /// </summary>
        [JsonPropertyName("rev")]
        public string? Rev { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.sync.getRepoStatus#GetRepoStatusOutput";

        public const string RecordType = "com.atproto.sync.getRepoStatus#GetRepoStatusOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Sync.GetRepoStatusOutput>(this, (JsonTypeInfo<Com.Atproto.Sync.GetRepoStatusOutput>)SourceGenerationContext.Default.ComAtprotoSyncGetRepoStatusOutput)!;
        }

        public static GetRepoStatusOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Sync.GetRepoStatusOutput>(json, (JsonTypeInfo<Com.Atproto.Sync.GetRepoStatusOutput>)SourceGenerationContext.Default.ComAtprotoSyncGetRepoStatusOutput)!;
        }
    }
}

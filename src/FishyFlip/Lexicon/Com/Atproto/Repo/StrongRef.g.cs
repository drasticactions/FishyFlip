// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class StrongRef : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StrongRef"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        public StrongRef(FishyFlip.Models.ATUri? uri = default, string? cid = default)
        {
            this.Uri = uri;
            this.Cid = cid;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StrongRef"/> class.
        /// </summary>
        public StrongRef()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StrongRef"/> class.
        /// </summary>
        public StrongRef(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Uri { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        [JsonRequired]
        public string? Cid { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.repo.strongRef";

        public const string RecordType = "com.atproto.repo.strongRef";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Repo.StrongRef>(this, (JsonTypeInfo<Com.Atproto.Repo.StrongRef>)SourceGenerationContext.Default.ComAtprotoRepoStrongRef)!;
        }

        public static StrongRef FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Repo.StrongRef>(json, (JsonTypeInfo<Com.Atproto.Repo.StrongRef>)SourceGenerationContext.Default.ComAtprotoRepoStrongRef)!;
        }
    }
}

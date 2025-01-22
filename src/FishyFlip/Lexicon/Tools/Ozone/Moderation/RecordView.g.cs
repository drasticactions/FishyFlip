// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class RecordView : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordView"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="value"></param>
        /// <param name="blobCids"></param>
        /// <param name="indexedAt"></param>
        /// <param name="moderation">
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.Moderation"/> (tools.ozone.moderation.defs#moderation)
        /// </param>
        /// <param name="repo">
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoView"/> (tools.ozone.moderation.defs#repoView)
        /// </param>
        public RecordView(FishyFlip.Models.ATUri uri = default, string cid = default, ATObject value = default, List<string> blobCids = default, DateTime? indexedAt = default, FishyFlip.Lexicon.Tools.Ozone.Moderation.Moderation moderation = default, FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoView repo = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.Value = value;
            this.BlobCids = blobCids;
            this.IndexedAt = indexedAt;
            this.Moderation = moderation;
            this.Repo = repo;
            this.Type = "tools.ozone.moderation.defs#recordView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordView"/> class.
        /// </summary>
        public RecordView()
        {
            this.Type = "tools.ozone.moderation.defs#recordView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordView"/> class.
        /// </summary>
        public RecordView(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["value"] is not null) this.Value = obj["value"].ToATObject();
            if (obj["blobCids"] is not null) this.BlobCids = obj["blobCids"].Values.Select(n =>n.AsString()).ToList();
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
            if (obj["moderation"] is not null) this.Moderation = new FishyFlip.Lexicon.Tools.Ozone.Moderation.Moderation(obj["moderation"]);
            if (obj["repo"] is not null) this.Repo = new FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoView(obj["repo"]);
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri Uri { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        [JsonRequired]
        public string Cid { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public ATObject Value { get; set; }

        /// <summary>
        /// Gets or sets the blobCids.
        /// </summary>
        [JsonPropertyName("blobCids")]
        [JsonRequired]
        public List<string> BlobCids { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        /// <summary>
        /// Gets or sets the moderation.
        /// <br/> <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.Moderation"/> (tools.ozone.moderation.defs#moderation)
        /// </summary>
        [JsonPropertyName("moderation")]
        [JsonRequired]
        public FishyFlip.Lexicon.Tools.Ozone.Moderation.Moderation Moderation { get; set; }

        /// <summary>
        /// Gets or sets the repo.
        /// <br/> <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoView"/> (tools.ozone.moderation.defs#repoView)
        /// </summary>
        [JsonPropertyName("repo")]
        [JsonRequired]
        public FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoView Repo { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#recordView";

        public static RecordView FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordView>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordView>)SourceGenerationContext.Default.ToolsOzoneModerationRecordView)!;
        }
    }
}


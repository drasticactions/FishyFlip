// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class RecordView : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordView"/> class.
        /// </summary>
        public RecordView(FishyFlip.Models.ATUri? uri = default, string? cid = default, ATObject? value = default, List<string>? blobCids = default, DateTime? indexedAt = default, Tools.Ozone.Moderation.Moderation? moderation = default, Tools.Ozone.Moderation.RepoView? repo = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.Value = value;
            this.BlobCids = blobCids;
            this.IndexedAt = indexedAt;
            this.Moderation = moderation;
            this.Repo = repo;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordView"/> class.
        /// </summary>
        public RecordView()
        {
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
            if (obj["moderation"] is not null) this.Moderation = new Tools.Ozone.Moderation.Moderation(obj["moderation"]);
            if (obj["repo"] is not null) this.Repo = new Tools.Ozone.Moderation.RepoView(obj["repo"]);
        }

        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Uri { get; set; }

        [JsonPropertyName("cid")]
        [JsonRequired]
        public string? Cid { get; set; }

        [JsonPropertyName("value")]
        [JsonRequired]
        public ATObject? Value { get; set; }

        [JsonPropertyName("blobCids")]
        [JsonRequired]
        public List<string>? BlobCids { get; set; }

        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        [JsonPropertyName("moderation")]
        [JsonRequired]
        public Tools.Ozone.Moderation.Moderation? Moderation { get; set; }

        [JsonPropertyName("repo")]
        [JsonRequired]
        public Tools.Ozone.Moderation.RepoView? Repo { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#recordView";

        public const string RecordType = "tools.ozone.moderation.defs#recordView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.RecordView>(this, (JsonTypeInfo<Tools.Ozone.Moderation.RecordView>)SourceGenerationContext.Default.ToolsOzoneModerationRecordView)!;
        }

        public static RecordView FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.RecordView>(json, (JsonTypeInfo<Tools.Ozone.Moderation.RecordView>)SourceGenerationContext.Default.ToolsOzoneModerationRecordView)!;
        }
    }
}


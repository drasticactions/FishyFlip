// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Labeler
{
    public partial class LabelerView : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelerView"/> class.
        /// </summary>
        public LabelerView()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LabelerView"/> class.
        /// </summary>
        public LabelerView(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["creator"] is not null) this.Creator = new App.Bsky.Actor.ProfileView(obj["creator"]);
            if (obj["likeCount"] is not null) this.LikeCount = obj["likeCount"].AsInt64Value();
            if (obj["viewer"] is not null) this.Viewer = new App.Bsky.Labeler.LabelerViewerState(obj["viewer"]);
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n => n is not null ? new Com.Atproto.Label.Label(n) : null).ToList();
        }

        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Uri { get; set; }

        [JsonPropertyName("cid")]
        [JsonRequired]
        public string? Cid { get; set; }

        [JsonPropertyName("creator")]
        [JsonRequired]
        public App.Bsky.Actor.ProfileView? Creator { get; set; }

        [JsonPropertyName("likeCount")]
        public long? LikeCount { get; set; }

        [JsonPropertyName("viewer")]
        public App.Bsky.Labeler.LabelerViewerState? Viewer { get; set; }

        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        [JsonPropertyName("labels")]
        public List<Com.Atproto.Label.Label?>? Labels { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.labeler.defs#labelerView";

        public const string RecordType = "app.bsky.labeler.defs#labelerView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Labeler.LabelerView>(this, (JsonTypeInfo<App.Bsky.Labeler.LabelerView>)SourceGenerationContext.Default.AppBskyLabelerLabelerView)!;
        }

        public static LabelerView FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Labeler.LabelerView>(json, (JsonTypeInfo<App.Bsky.Labeler.LabelerView>)SourceGenerationContext.Default.AppBskyLabelerLabelerView)!;
        }
    }
}

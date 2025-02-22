// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Labeler
{
    public partial class LabelerViewDetailed : ATObject, ICBOREncodable<LabelerViewDetailed>, IJsonEncodable<LabelerViewDetailed>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelerViewDetailed"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="creator">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </param>
        /// <param name="policies">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Labeler.LabelerPolicies"/> (app.bsky.labeler.defs#labelerPolicies)
        /// </param>
        /// <param name="likeCount"></param>
        /// <param name="viewer">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewerState"/> (app.bsky.labeler.defs#labelerViewerState)
        /// </param>
        /// <param name="indexedAt"></param>
        /// <param name="labels"></param>
        public LabelerViewDetailed(FishyFlip.Models.ATUri uri = default, string cid = default, FishyFlip.Lexicon.App.Bsky.Actor.ProfileView creator = default, FishyFlip.Lexicon.App.Bsky.Labeler.LabelerPolicies policies = default, long? likeCount = default, FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewerState? viewer = default, DateTime? indexedAt = default, List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? labels = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.Creator = creator;
            this.Policies = policies;
            this.LikeCount = likeCount;
            this.Viewer = viewer;
            this.IndexedAt = indexedAt;
            this.Labels = labels;
            this.Type = "app.bsky.labeler.defs#labelerViewDetailed";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LabelerViewDetailed"/> class.
        /// </summary>
        public LabelerViewDetailed()
        {
            this.Type = "app.bsky.labeler.defs#labelerViewDetailed";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LabelerViewDetailed"/> class.
        /// </summary>
        public LabelerViewDetailed(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["creator"] is not null) this.Creator = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(obj["creator"]);
            if (obj["policies"] is not null) this.Policies = new FishyFlip.Lexicon.App.Bsky.Labeler.LabelerPolicies(obj["policies"]);
            if (obj["likeCount"] is not null) this.LikeCount = obj["likeCount"].AsInt64Value();
            if (obj["viewer"] is not null) this.Viewer = new FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewerState(obj["viewer"]);
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Label.Label(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
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
        /// Gets or sets the creator.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </summary>
        [JsonPropertyName("creator")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Actor.ProfileView Creator { get; set; }

        /// <summary>
        /// Gets or sets the policies.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Labeler.LabelerPolicies"/> (app.bsky.labeler.defs#labelerPolicies)
        /// </summary>
        [JsonPropertyName("policies")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Labeler.LabelerPolicies Policies { get; set; }

        /// <summary>
        /// Gets or sets the likeCount.
        /// </summary>
        [JsonPropertyName("likeCount")]
        public long? LikeCount { get; set; }

        /// <summary>
        /// Gets or sets the viewer.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewerState"/> (app.bsky.labeler.defs#labelerViewerState)
        /// </summary>
        [JsonPropertyName("viewer")]
        public FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewerState? Viewer { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? Labels { get; set; }

        public const string RecordType = "app.bsky.labeler.defs#labelerViewDetailed";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewDetailed>)SourceGenerationContext.Default.AppBskyLabelerLabelerViewDetailed);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewDetailed>)SourceGenerationContext.Default.AppBskyLabelerLabelerViewDetailed);
        }

        public static new LabelerViewDetailed FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewDetailed>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Labeler.LabelerViewDetailed>)SourceGenerationContext.Default.AppBskyLabelerLabelerViewDetailed)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new LabelerViewDetailed FromCBORObject(CBORObject obj)
        {
            return new LabelerViewDetailed(obj);
        }

    }
}


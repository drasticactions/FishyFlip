// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    /// <summary>
    /// Represents an update of repository state. Note that empty commits are allowed, which include no repo data changes, but an update to rev and signature.
    /// </summary>
    public partial class Commit : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Commit"/> class.
        /// </summary>
        /// <param name="seq">The stream sequence number of this message.</param>
        /// <param name="tooBig">Indicates that this commit contained too many ops, or data size was too large. Consumers will need to make a separate request to get missing data.</param>
        /// <param name="repo">The repo this event comes from.</param>
        /// <param name="commit">Repo commit object CID.</param>
        /// <param name="rev">The rev of the emitted commit. Note that this information is also in the commit object included in blocks, unless this is a tooBig event.</param>
        /// <param name="since">The rev of the last emitted commit from this repo (if any).</param>
        /// <param name="blocks">CAR file containing relevant blocks, as a diff since the previous repo state.</param>
        /// <param name="ops"></param>
        /// <param name="blobs"></param>
        /// <param name="time">Timestamp of when this message was originally broadcast.</param>
        public Commit(long? seq = default, bool? tooBig = default, FishyFlip.Models.ATDid? repo = default, Ipfs.Cid? commit = default, string? rev = default, string? since = default, byte[]? blocks = default, List<Com.Atproto.Sync.RepoOp>? ops = default, List<Ipfs.Cid>? blobs = default, DateTime? time = default)
        {
            this.Seq = seq;
            this.TooBig = tooBig;
            this.Repo = repo;
            this.CommitValue = commit;
            this.Rev = rev;
            this.Since = since;
            this.Blocks = blocks;
            this.Ops = ops;
            this.Blobs = blobs;
            this.Time = time;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Commit"/> class.
        /// </summary>
        public Commit()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Commit"/> class.
        /// </summary>
        public Commit(CBORObject obj)
        {
            if (obj["seq"] is not null) this.Seq = obj["seq"].AsInt64Value();
            if (obj["tooBig"] is not null) this.TooBig = obj["tooBig"].AsBoolean();
            if (obj["repo"] is not null) this.Repo = obj["repo"].ToATDid();
            if (obj["commit"] is not null) this.CommitValue = obj["commit"].ToATCid();
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
            if (obj["since"] is not null) this.Since = obj["since"].AsString();
            if (obj["blocks"] is not null) this.Blocks = obj["blocks"].EncodeToBytes();
            if (obj["ops"] is not null) this.Ops = obj["ops"].Values.Select(n =>new Com.Atproto.Sync.RepoOp(n)).ToList();
            if (obj["blobs"] is not null) this.Blobs = obj["blobs"].Values.Select(n =>n.ToATCid()!).ToList();
            if (obj["time"] is not null) this.Time = obj["time"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the seq.
        /// The stream sequence number of this message.
        /// </summary>
        [JsonPropertyName("seq")]
        [JsonRequired]
        public long? Seq { get; set; }

        /// <summary>
        /// Gets or sets the tooBig.
        /// Indicates that this commit contained too many ops, or data size was too large. Consumers will need to make a separate request to get missing data.
        /// </summary>
        [JsonPropertyName("tooBig")]
        [JsonRequired]
        public bool? TooBig { get; set; }

        /// <summary>
        /// Gets or sets the repo.
        /// The repo this event comes from.
        /// </summary>
        [JsonPropertyName("repo")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Repo { get; set; }

        /// <summary>
        /// Gets or sets the commit.
        /// Repo commit object CID.
        /// </summary>
        [JsonPropertyName("commit")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATCidJsonConverter))]
        public Ipfs.Cid? CommitValue { get; set; }

        /// <summary>
        /// Gets or sets the rev.
        /// The rev of the emitted commit. Note that this information is also in the commit object included in blocks, unless this is a tooBig event.
        /// </summary>
        [JsonPropertyName("rev")]
        [JsonRequired]
        public string? Rev { get; set; }

        /// <summary>
        /// Gets or sets the since.
        /// The rev of the last emitted commit from this repo (if any).
        /// </summary>
        [JsonPropertyName("since")]
        [JsonRequired]
        public string? Since { get; set; }

        /// <summary>
        /// Gets or sets the blocks.
        /// CAR file containing relevant blocks, as a diff since the previous repo state.
        /// </summary>
        [JsonPropertyName("blocks")]
        [JsonRequired]
        public byte[]? Blocks { get; set; }

        /// <summary>
        /// Gets or sets the ops.
        /// </summary>
        [JsonPropertyName("ops")]
        [JsonRequired]
        public List<Com.Atproto.Sync.RepoOp>? Ops { get; set; }

        /// <summary>
        /// Gets or sets the blobs.
        /// </summary>
        [JsonPropertyName("blobs")]
        [JsonRequired]
        public List<Ipfs.Cid>? Blobs { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// Timestamp of when this message was originally broadcast.
        /// </summary>
        [JsonPropertyName("time")]
        [JsonRequired]
        public DateTime? Time { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.sync.subscribeRepos#commit";

        public const string RecordType = "com.atproto.sync.subscribeRepos#commit";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Sync.Commit>(this, (JsonTypeInfo<Com.Atproto.Sync.Commit>)SourceGenerationContext.Default.ComAtprotoSyncCommit)!;
        }

        public static Commit FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Sync.Commit>(json, (JsonTypeInfo<Com.Atproto.Sync.Commit>)SourceGenerationContext.Default.ComAtprotoSyncCommit)!;
        }
    }
}


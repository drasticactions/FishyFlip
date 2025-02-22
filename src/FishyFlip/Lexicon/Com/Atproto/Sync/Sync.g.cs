// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    /// <summary>
    /// Updates the repo to a new state, without necessarily including that state on the firehose. Used to recover from broken commit streams, data loss incidents, or in situations where upstream host does not know recent state of the repository.
    /// </summary>
    public partial class Sync : ATObject, ICBOREncodable<Sync>, IJsonEncodable<Sync>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Sync"/> class.
        /// </summary>
        /// <param name="seq">The stream sequence number of this message.</param>
        /// <param name="did">The account this repo event corresponds to. Must match that in the commit object.</param>
        /// <param name="blocks">CAR file containing the commit, as a block. The CAR header must include the commit block CID as the first 'root'.</param>
        /// <param name="rev">The rev of the commit. This value must match that in the commit object.</param>
        /// <param name="time">Timestamp of when this message was originally broadcast.</param>
        public Sync(long seq = default, FishyFlip.Models.ATDid did = default, byte[] blocks = default, string rev = default, DateTime? time = default)
        {
            this.Seq = seq;
            this.Did = did;
            this.Blocks = blocks;
            this.Rev = rev;
            this.Time = time;
            this.Type = "com.atproto.sync.subscribeRepos#sync";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Sync"/> class.
        /// </summary>
        public Sync()
        {
            this.Type = "com.atproto.sync.subscribeRepos#sync";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Sync"/> class.
        /// </summary>
        public Sync(CBORObject obj)
        {
            if (obj["seq"] is not null) this.Seq = obj["seq"].AsInt64Value();
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["blocks"] is not null) this.Blocks = obj["blocks"].EncodeToBytes();
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
            if (obj["time"] is not null) this.Time = obj["time"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the seq.
        /// <br/> The stream sequence number of this message.
        /// </summary>
        [JsonPropertyName("seq")]
        [JsonRequired]
        public long Seq { get; set; }

        /// <summary>
        /// Gets or sets the did.
        /// <br/> The account this repo event corresponds to. Must match that in the commit object.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets or sets the blocks.
        /// <br/> CAR file containing the commit, as a block. The CAR header must include the commit block CID as the first 'root'.
        /// </summary>
        [JsonPropertyName("blocks")]
        [JsonRequired]
        public byte[] Blocks { get; set; }

        /// <summary>
        /// Gets or sets the rev.
        /// <br/> The rev of the commit. This value must match that in the commit object.
        /// </summary>
        [JsonPropertyName("rev")]
        [JsonRequired]
        public string Rev { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// <br/> Timestamp of when this message was originally broadcast.
        /// </summary>
        [JsonPropertyName("time")]
        [JsonRequired]
        public DateTime? Time { get; set; }

        public const string RecordType = "com.atproto.sync.subscribeRepos#sync";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Sync>)SourceGenerationContext.Default.ComAtprotoSyncSync);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Sync>)SourceGenerationContext.Default.ComAtprotoSyncSync);
        }

        public static new Sync FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Sync.Sync>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Sync>)SourceGenerationContext.Default.ComAtprotoSyncSync)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Sync FromCBORObject(CBORObject obj)
        {
            return new Sync(obj);
        }

    }
}


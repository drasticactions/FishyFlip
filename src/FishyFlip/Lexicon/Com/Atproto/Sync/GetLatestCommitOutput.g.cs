// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    public partial class GetLatestCommitOutput : ATObject, ICBOREncodable<GetLatestCommitOutput>, IJsonEncodable<GetLatestCommitOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestCommitOutput"/> class.
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="rev"></param>
        public GetLatestCommitOutput(string cid = default, string rev = default)
        {
            this.Cid = cid;
            this.Rev = rev;
            this.Type = "com.atproto.sync.getLatestCommit#GetLatestCommitOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestCommitOutput"/> class.
        /// </summary>
        public GetLatestCommitOutput()
        {
            this.Type = "com.atproto.sync.getLatestCommit#GetLatestCommitOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestCommitOutput"/> class.
        /// </summary>
        public GetLatestCommitOutput(CBORObject obj)
        {
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
        }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        [JsonRequired]
        public string Cid { get; set; }

        /// <summary>
        /// Gets or sets the rev.
        /// </summary>
        [JsonPropertyName("rev")]
        [JsonRequired]
        public string Rev { get; set; }

        public const string RecordType = "com.atproto.sync.getLatestCommit#GetLatestCommitOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.GetLatestCommitOutput>)SourceGenerationContext.Default.ComAtprotoSyncGetLatestCommitOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.GetLatestCommitOutput>)SourceGenerationContext.Default.ComAtprotoSyncGetLatestCommitOutput);
        }

        public static new GetLatestCommitOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Sync.GetLatestCommitOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.GetLatestCommitOutput>)SourceGenerationContext.Default.ComAtprotoSyncGetLatestCommitOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetLatestCommitOutput FromCBORObject(CBORObject obj)
        {
            return new GetLatestCommitOutput(obj);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class PutRecordOutput : ATObject, ICBOREncodable<PutRecordOutput>, IJsonEncodable<PutRecordOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PutRecordOutput"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="commit">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta"/> (com.atproto.repo.defs#commitMeta)
        /// </param>
        /// <param name="validationStatus">
        /// <br/> Known Values: <br/>
        /// valid <br/>
        /// unknown <br/>
        /// </param>
        public PutRecordOutput(FishyFlip.Models.ATUri uri = default, string cid = default, FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta? commit = default, string? validationStatus = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.Commit = commit;
            this.ValidationStatus = validationStatus;
            this.Type = "com.atproto.repo.putRecord#PutRecordOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PutRecordOutput"/> class.
        /// </summary>
        public PutRecordOutput()
        {
            this.Type = "com.atproto.repo.putRecord#PutRecordOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PutRecordOutput"/> class.
        /// </summary>
        public PutRecordOutput(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["commit"] is not null) this.Commit = new FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta(obj["commit"]);
            if (obj["validationStatus"] is not null) this.ValidationStatus = obj["validationStatus"].AsString();
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
        /// Gets or sets the commit.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta"/> (com.atproto.repo.defs#commitMeta)
        /// </summary>
        [JsonPropertyName("commit")]
        public FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta? Commit { get; set; }

        /// <summary>
        /// Gets or sets the validationStatus.
        /// <br/> Known Values: <br/>
        /// valid <br/>
        /// unknown <br/>
        /// </summary>
        [JsonPropertyName("validationStatus")]
        public string? ValidationStatus { get; set; }

        public const string RecordType = "com.atproto.repo.putRecord#PutRecordOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordOutput>)SourceGenerationContext.Default.ComAtprotoRepoPutRecordOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordOutput>)SourceGenerationContext.Default.ComAtprotoRepoPutRecordOutput);
        }

        public static new PutRecordOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordOutput>)SourceGenerationContext.Default.ComAtprotoRepoPutRecordOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new PutRecordOutput FromCBORObject(CBORObject obj)
        {
            return new PutRecordOutput(obj);
        }

    }
}


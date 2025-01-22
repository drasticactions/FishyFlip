// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class PutRecordInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PutRecordInput"/> class.
        /// </summary>
        /// <param name="repo">The handle or DID of the repo (aka, current account).</param>
        /// <param name="collection">The NSID of the record collection.</param>
        /// <param name="rkey">The Record Key.</param>
        /// <param name="validate">Can be set to 'false' to skip Lexicon schema validation of record data, 'true' to require it, or leave unset to validate only for known Lexicons.</param>
        /// <param name="record">The record to write.</param>
        /// <param name="swapRecord">Compare and swap with the previous record by CID. WARNING: nullable and optional field; may cause problems with golang implementation</param>
        /// <param name="swapCommit">Compare and swap with the previous commit by CID.</param>
        public PutRecordInput(FishyFlip.Models.ATIdentifier repo = default, string collection = default, string rkey = default, bool? validate = default, ATObject record = default, string? swapRecord = default, string? swapCommit = default)
        {
            this.Repo = repo;
            this.Collection = collection;
            this.Rkey = rkey;
            this.Validate = validate;
            this.Record = record;
            this.SwapRecord = swapRecord;
            this.SwapCommit = swapCommit;
            this.Type = "com.atproto.repo.putRecord#PutRecordInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PutRecordInput"/> class.
        /// </summary>
        public PutRecordInput()
        {
            this.Type = "com.atproto.repo.putRecord#PutRecordInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PutRecordInput"/> class.
        /// </summary>
        public PutRecordInput(CBORObject obj)
        {
            if (obj["repo"] is not null) this.Repo = obj["repo"].ToATIdentifier();
            if (obj["collection"] is not null) this.Collection = obj["collection"].AsString();
            if (obj["rkey"] is not null) this.Rkey = obj["rkey"].AsString();
            if (obj["validate"] is not null) this.Validate = obj["validate"].AsBoolean();
            if (obj["record"] is not null) this.Record = obj["record"].ToATObject();
            if (obj["swapRecord"] is not null) this.SwapRecord = obj["swapRecord"].AsString();
            if (obj["swapCommit"] is not null) this.SwapCommit = obj["swapCommit"].AsString();
        }

        /// <summary>
        /// Gets or sets the repo.
        /// <br/> The handle or DID of the repo (aka, current account).
        /// </summary>
        [JsonPropertyName("repo")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATIdentifierJsonConverter))]
        public FishyFlip.Models.ATIdentifier Repo { get; set; }

        /// <summary>
        /// Gets or sets the collection.
        /// <br/> The NSID of the record collection.
        /// </summary>
        [JsonPropertyName("collection")]
        [JsonRequired]
        public string Collection { get; set; }

        /// <summary>
        /// Gets or sets the rkey.
        /// <br/> The Record Key.
        /// </summary>
        [JsonPropertyName("rkey")]
        [JsonRequired]
        public string Rkey { get; set; }

        /// <summary>
        /// Gets or sets the validate.
        /// <br/> Can be set to 'false' to skip Lexicon schema validation of record data, 'true' to require it, or leave unset to validate only for known Lexicons.
        /// </summary>
        [JsonPropertyName("validate")]
        public bool? Validate { get; set; }

        /// <summary>
        /// Gets or sets the record.
        /// <br/> The record to write.
        /// </summary>
        [JsonPropertyName("record")]
        [JsonRequired]
        public ATObject Record { get; set; }

        /// <summary>
        /// Gets or sets the swapRecord.
        /// <br/> Compare and swap with the previous record by CID. WARNING: nullable and optional field; may cause problems with golang implementation
        /// </summary>
        [JsonPropertyName("swapRecord")]
        public string? SwapRecord { get; set; }

        /// <summary>
        /// Gets or sets the swapCommit.
        /// <br/> Compare and swap with the previous commit by CID.
        /// </summary>
        [JsonPropertyName("swapCommit")]
        public string? SwapCommit { get; set; }

        public const string RecordType = "com.atproto.repo.putRecord#PutRecordInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoPutRecordInput)!;
        }

        public static PutRecordInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoPutRecordInput)!;
        }
    }
}


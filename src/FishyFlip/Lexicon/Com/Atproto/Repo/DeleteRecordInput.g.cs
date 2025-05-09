// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class DeleteRecordInput : ATObject, ICBOREncodable<DeleteRecordInput>, IJsonEncodable<DeleteRecordInput>, IParsable<DeleteRecordInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRecordInput"/> class.
        /// </summary>
        /// <param name="repo">The handle or DID of the repo (aka, current account).</param>
        /// <param name="collection">The NSID of the record collection.</param>
        /// <param name="rkey">The Record Key.</param>
        /// <param name="swapRecord">Compare and swap with the previous record by CID.</param>
        /// <param name="swapCommit">Compare and swap with the previous commit by CID.</param>
        public DeleteRecordInput(FishyFlip.Models.ATIdentifier repo = default, string collection = default, string rkey = default, string? swapRecord = default, string? swapCommit = default)
        {
            this.Repo = repo;
            this.Collection = collection;
            this.Rkey = rkey;
            this.SwapRecord = swapRecord;
            this.SwapCommit = swapCommit;
            this.Type = "com.atproto.repo.deleteRecord#DeleteRecordInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRecordInput"/> class.
        /// </summary>
        public DeleteRecordInput()
        {
            this.Type = "com.atproto.repo.deleteRecord#DeleteRecordInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRecordInput"/> class.
        /// </summary>
        public DeleteRecordInput(CBORObject obj)
        {
            if (obj["repo"] is not null) this.Repo = obj["repo"].ToATIdentifier();
            if (obj["collection"] is not null) this.Collection = obj["collection"].AsString();
            if (obj["rkey"] is not null) this.Rkey = obj["rkey"].AsString();
            if (obj["swapRecord"] is not null) this.SwapRecord = obj["swapRecord"].AsString();
            if (obj["swapCommit"] is not null) this.SwapCommit = obj["swapCommit"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
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
        /// Gets or sets the swapRecord.
        /// <br/> Compare and swap with the previous record by CID.
        /// </summary>
        [JsonPropertyName("swapRecord")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SwapRecord { get; set; }

        /// <summary>
        /// Gets or sets the swapCommit.
        /// <br/> Compare and swap with the previous commit by CID.
        /// </summary>
        [JsonPropertyName("swapCommit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SwapCommit { get; set; }

        public const string RecordType = "com.atproto.repo.deleteRecord#DeleteRecordInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.DeleteRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoDeleteRecordInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.DeleteRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoDeleteRecordInput);
        }

        public static new DeleteRecordInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.DeleteRecordInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.DeleteRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoDeleteRecordInput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new DeleteRecordInput FromCBORObject(CBORObject obj)
        {
            return new DeleteRecordInput(obj);
        }

        /// <inheritdoc/>
        public static DeleteRecordInput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<DeleteRecordInput>(s, (JsonTypeInfo<DeleteRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoDeleteRecordInput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out DeleteRecordInput result)
        {
            result = JsonSerializer.Deserialize<DeleteRecordInput>(s, (JsonTypeInfo<DeleteRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoDeleteRecordInput);
            return result != null;
        }
    }
}


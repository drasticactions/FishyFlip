// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class CreateRecordInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRecordInput"/> class.
        /// </summary>
        public CreateRecordInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRecordInput"/> class.
        /// </summary>
        public CreateRecordInput(CBORObject obj)
        {
            if (obj["repo"] is not null) this.Repo = obj["repo"].ToATIdentifier();
            if (obj["collection"] is not null) this.Collection = obj["collection"].AsString();
            if (obj["rkey"] is not null) this.Rkey = obj["rkey"].AsString();
            if (obj["validate"] is not null) this.Validate = obj["validate"].AsBoolean();
            if (obj["record"] is not null) this.Record = obj["record"].ToATObject();
            if (obj["swapCommit"] is not null) this.SwapCommit = obj["swapCommit"].AsString();
        }

        /// <summary>
        /// The handle or DID of the repo (aka, current account).
        /// </summary>
        [JsonPropertyName("repo")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATIdentifierJsonConverter))]
        public FishyFlip.Models.ATIdentifier? Repo { get; set; }

        /// <summary>
        /// The NSID of the record collection.
        /// </summary>
        [JsonPropertyName("collection")]
        [JsonRequired]
        public string? Collection { get; set; }

        /// <summary>
        /// The Record Key.
        /// </summary>
        [JsonPropertyName("rkey")]
        public string? Rkey { get; set; }

        /// <summary>
        /// Can be set to 'false' to skip Lexicon schema validation of record data, 'true' to require it, or leave unset to validate only for known Lexicons.
        /// </summary>
        [JsonPropertyName("validate")]
        public bool? Validate { get; set; }

        /// <summary>
        /// The record itself. Must contain a $type field.
        /// </summary>
        [JsonPropertyName("record")]
        [JsonRequired]
        public ATObject? Record { get; set; }

        /// <summary>
        /// Compare and swap with the previous commit by CID.
        /// </summary>
        [JsonPropertyName("swapCommit")]
        public string? SwapCommit { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.repo.createRecord#CreateRecordInput";

        public const string RecordType = "com.atproto.repo.createRecord#CreateRecordInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Repo.CreateRecordInput>(this, (JsonTypeInfo<Com.Atproto.Repo.CreateRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoCreateRecordInput)!;
        }

        public static CreateRecordInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Repo.CreateRecordInput>(json, (JsonTypeInfo<Com.Atproto.Repo.CreateRecordInput>)SourceGenerationContext.Default.ComAtprotoRepoCreateRecordInput)!;
        }
    }
}

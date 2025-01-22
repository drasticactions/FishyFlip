// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class Record : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="value"></param>
        public Record(FishyFlip.Models.ATUri uri = default, string cid = default, ATObject value = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.Value = value;
            this.Type = "com.atproto.repo.listRecords#record";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        public Record()
        {
            this.Type = "com.atproto.repo.listRecords#record";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        public Record(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["value"] is not null) this.Value = obj["value"].ToATObject();
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
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public ATObject Value { get; set; }

        public const string RecordType = "com.atproto.repo.listRecords#record";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Repo.Record>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.Record>)SourceGenerationContext.Default.ComAtprotoRepoRecord)!;
        }

        public static Record FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.Record>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.Record>)SourceGenerationContext.Default.ComAtprotoRepoRecord)!;
        }
    }
}


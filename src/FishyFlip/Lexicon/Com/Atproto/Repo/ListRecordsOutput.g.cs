// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class ListRecordsOutput : ATObject, ICBOREncodable<ListRecordsOutput>, IJsonEncodable<ListRecordsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ListRecordsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="records"></param>
        public ListRecordsOutput(string? cursor = default, List<FishyFlip.Lexicon.Com.Atproto.Repo.Record> records = default)
        {
            this.Cursor = cursor;
            this.Records = records;
            this.Type = "com.atproto.repo.listRecords#ListRecordsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListRecordsOutput"/> class.
        /// </summary>
        public ListRecordsOutput()
        {
            this.Type = "com.atproto.repo.listRecords#ListRecordsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListRecordsOutput"/> class.
        /// </summary>
        public ListRecordsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["records"] is not null) this.Records = obj["records"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Repo.Record(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        [JsonPropertyName("records")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Atproto.Repo.Record> Records { get; set; }

        public const string RecordType = "com.atproto.repo.listRecords#ListRecordsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.ListRecordsOutput>)SourceGenerationContext.Default.ComAtprotoRepoListRecordsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.ListRecordsOutput>)SourceGenerationContext.Default.ComAtprotoRepoListRecordsOutput);
        }

        public static new ListRecordsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.ListRecordsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.ListRecordsOutput>)SourceGenerationContext.Default.ComAtprotoRepoListRecordsOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ListRecordsOutput FromCBORObject(CBORObject obj)
        {
            return new ListRecordsOutput(obj);
        }

    }
}


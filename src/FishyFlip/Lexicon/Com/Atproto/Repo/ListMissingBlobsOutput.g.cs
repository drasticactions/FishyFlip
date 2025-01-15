// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class ListMissingBlobsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMissingBlobsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="blobs"></param>
        public ListMissingBlobsOutput(string? cursor = default, List<FishyFlip.Lexicon.Com.Atproto.Repo.RecordBlob>? blobs = default)
        {
            this.Cursor = cursor;
            this.Blobs = blobs;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListMissingBlobsOutput"/> class.
        /// </summary>
        public ListMissingBlobsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListMissingBlobsOutput"/> class.
        /// </summary>
        public ListMissingBlobsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["blobs"] is not null) this.Blobs = obj["blobs"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Repo.RecordBlob(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the blobs.
        /// </summary>
        [JsonPropertyName("blobs")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Atproto.Repo.RecordBlob>? Blobs { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.repo.listMissingBlobs#ListMissingBlobsOutput";

        public const string RecordType = "com.atproto.repo.listMissingBlobs#ListMissingBlobsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Repo.ListMissingBlobsOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.ListMissingBlobsOutput>)SourceGenerationContext.Default.ComAtprotoRepoListMissingBlobsOutput)!;
        }

        public static ListMissingBlobsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.ListMissingBlobsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.ListMissingBlobsOutput>)SourceGenerationContext.Default.ComAtprotoRepoListMissingBlobsOutput)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class DescribeRepoOutput : ATObject, ICBOREncodable<DescribeRepoOutput>, IJsonEncodable<DescribeRepoOutput>, IParsable<DescribeRepoOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DescribeRepoOutput"/> class.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="did"></param>
        /// <param name="didDoc">The complete DID document for this account.</param>
        /// <param name="collections">List of all the collections (NSIDs) for which this repo contains at least one record.</param>
        /// <param name="handleIsCorrect">Indicates if handle is currently valid (resolves bi-directionally)</param>
        public DescribeRepoOutput(FishyFlip.Models.ATHandle handle = default, FishyFlip.Models.ATDid did = default, FishyFlip.Models.DidDoc didDoc = default, List<string> collections = default, bool handleIsCorrect = default)
        {
            this.Handle = handle;
            this.Did = did;
            this.DidDoc = didDoc;
            this.Collections = collections;
            this.HandleIsCorrect = handleIsCorrect;
            this.Type = "com.atproto.repo.describeRepo#DescribeRepoOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DescribeRepoOutput"/> class.
        /// </summary>
        public DescribeRepoOutput()
        {
            this.Type = "com.atproto.repo.describeRepo#DescribeRepoOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DescribeRepoOutput"/> class.
        /// </summary>
        public DescribeRepoOutput(CBORObject obj)
        {
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            // Ignore DidDoc
            if (obj["collections"] is not null) this.Collections = obj["collections"].Values.Select(n =>n.AsString()).ToList();
            if (obj["handleIsCorrect"] is not null) this.HandleIsCorrect = obj["handleIsCorrect"].AsBoolean();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle Handle { get; set; }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets or sets the didDoc.
        /// <br/> The complete DID document for this account.
        /// </summary>
        [JsonPropertyName("didDoc")]
        [JsonRequired]
        public FishyFlip.Models.DidDoc DidDoc { get; set; }

        /// <summary>
        /// Gets or sets the collections.
        /// <br/> List of all the collections (NSIDs) for which this repo contains at least one record.
        /// </summary>
        [JsonPropertyName("collections")]
        [JsonRequired]
        public List<string> Collections { get; set; }

        /// <summary>
        /// Gets or sets the handleIsCorrect.
        /// <br/> Indicates if handle is currently valid (resolves bi-directionally)
        /// </summary>
        [JsonPropertyName("handleIsCorrect")]
        [JsonRequired]
        public bool HandleIsCorrect { get; set; }

        public const string RecordType = "com.atproto.repo.describeRepo#DescribeRepoOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.DescribeRepoOutput>)SourceGenerationContext.Default.ComAtprotoRepoDescribeRepoOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.DescribeRepoOutput>)SourceGenerationContext.Default.ComAtprotoRepoDescribeRepoOutput);
        }

        public static new DescribeRepoOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.DescribeRepoOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.DescribeRepoOutput>)SourceGenerationContext.Default.ComAtprotoRepoDescribeRepoOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new DescribeRepoOutput FromCBORObject(CBORObject obj)
        {
            return new DescribeRepoOutput(obj);
        }

        /// <inheritdoc/>
        public static DescribeRepoOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<DescribeRepoOutput>(s, (JsonTypeInfo<DescribeRepoOutput>)SourceGenerationContext.Default.ComAtprotoRepoDescribeRepoOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out DescribeRepoOutput result)
        {
            result = JsonSerializer.Deserialize<DescribeRepoOutput>(s, (JsonTypeInfo<DescribeRepoOutput>)SourceGenerationContext.Default.ComAtprotoRepoDescribeRepoOutput);
            return result != null;
        }
    }
}


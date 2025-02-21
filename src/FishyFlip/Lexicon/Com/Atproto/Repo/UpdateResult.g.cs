// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class UpdateResult : ATObject, ICBOREncodable<UpdateResult>, IJsonEncodable<UpdateResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateResult"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="validationStatus">
        /// <br/> Known Values: <br/>
        /// valid <br/>
        /// unknown <br/>
        /// </param>
        public UpdateResult(FishyFlip.Models.ATUri uri = default, string cid = default, string? validationStatus = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.ValidationStatus = validationStatus;
            this.Type = "com.atproto.repo.applyWrites#updateResult";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateResult"/> class.
        /// </summary>
        public UpdateResult()
        {
            this.Type = "com.atproto.repo.applyWrites#updateResult";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateResult"/> class.
        /// </summary>
        public UpdateResult(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["validationStatus"] is not null) this.ValidationStatus = obj["validationStatus"].AsString();
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
        /// Gets or sets the validationStatus.
        /// <br/> Known Values: <br/>
        /// valid <br/>
        /// unknown <br/>
        /// </summary>
        [JsonPropertyName("validationStatus")]
        public string? ValidationStatus { get; set; }

        public const string RecordType = "com.atproto.repo.applyWrites#updateResult";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.UpdateResult>)SourceGenerationContext.Default.ComAtprotoRepoUpdateResult);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.UpdateResult>)SourceGenerationContext.Default.ComAtprotoRepoUpdateResult);
        }

        public static new UpdateResult FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.UpdateResult>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.UpdateResult>)SourceGenerationContext.Default.ComAtprotoRepoUpdateResult)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new UpdateResult FromCBORObject(CBORObject obj)
        {
            return new UpdateResult(obj);
        }

    }
}


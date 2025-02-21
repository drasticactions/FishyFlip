// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    /// <summary>
    /// Operation which creates a new record.
    /// </summary>
    public partial class Create : ATObject, ICBOREncodable<Create>, IJsonEncodable<Create>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Create"/> class.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="rkey">NOTE: maxLength is redundant with record-key format. Keeping it temporarily to ensure backwards compatibility.</param>
        /// <param name="value"></param>
        public Create(string collection = default, string? rkey = default, ATObject value = default)
        {
            this.Collection = collection;
            this.Rkey = rkey;
            this.Value = value;
            this.Type = "com.atproto.repo.applyWrites#create";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Create"/> class.
        /// </summary>
        public Create()
        {
            this.Type = "com.atproto.repo.applyWrites#create";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Create"/> class.
        /// </summary>
        public Create(CBORObject obj)
        {
            if (obj["collection"] is not null) this.Collection = obj["collection"].AsString();
            if (obj["rkey"] is not null) this.Rkey = obj["rkey"].AsString();
            if (obj["value"] is not null) this.Value = obj["value"].ToATObject();
        }

        /// <summary>
        /// Gets or sets the collection.
        /// </summary>
        [JsonPropertyName("collection")]
        [JsonRequired]
        public string Collection { get; set; }

        /// <summary>
        /// Gets or sets the rkey.
        /// <br/> NOTE: maxLength is redundant with record-key format. Keeping it temporarily to ensure backwards compatibility.
        /// </summary>
        [JsonPropertyName("rkey")]
        public string? Rkey { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public ATObject Value { get; set; }

        public const string RecordType = "com.atproto.repo.applyWrites#create";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.Create>)SourceGenerationContext.Default.ComAtprotoRepoCreate);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.Create>)SourceGenerationContext.Default.ComAtprotoRepoCreate);
        }

        public static new Create FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.Create>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.Create>)SourceGenerationContext.Default.ComAtprotoRepoCreate)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Create FromCBORObject(CBORObject obj)
        {
            return new Create(obj);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class BlobMetadata : ATObject, ICBOREncodable<BlobMetadata>, IJsonEncodable<BlobMetadata>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobMetadata"/> class.
        /// </summary>
        /// <param name="blobref"></param>
        /// <param name="name"></param>
        public BlobMetadata(Blob blobref = default, string? name = default)
        {
            this.Blobref = blobref;
            this.Name = name;
            this.Type = "com.whtwnd.blog.defs#blobMetadata";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlobMetadata"/> class.
        /// </summary>
        public BlobMetadata()
        {
            this.Type = "com.whtwnd.blog.defs#blobMetadata";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BlobMetadata"/> class.
        /// </summary>
        public BlobMetadata(CBORObject obj)
        {
            if (obj["blobref"] is not null) this.Blobref = new FishyFlip.Models.Blob(obj["blobref"]);
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the blobref.
        /// </summary>
        [JsonPropertyName("blobref")]
        [JsonRequired]
        public Blob Blobref { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public const string RecordType = "com.whtwnd.blog.defs#blobMetadata";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlobMetadata>)SourceGenerationContext.Default.ComWhtwndBlogBlobMetadata);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlobMetadata>)SourceGenerationContext.Default.ComWhtwndBlogBlobMetadata);
        }

        public static new BlobMetadata FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlobMetadata>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.BlobMetadata>)SourceGenerationContext.Default.ComWhtwndBlogBlobMetadata)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new BlobMetadata FromCBORObject(CBORObject obj)
        {
            return new BlobMetadata(obj);
        }

    }
}


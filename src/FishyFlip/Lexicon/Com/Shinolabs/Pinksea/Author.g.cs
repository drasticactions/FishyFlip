// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    /// <summary>
    /// An author for an oekaki post
    /// </summary>
    public partial class Author : ATObject, ICBOREncodable<Author>, IJsonEncodable<Author>, IParsable<Author>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        /// <param name="did">The DID of the author</param>
        /// <param name="handle">The handle of the author.</param>
        public Author(FishyFlip.Models.ATDid did = default, FishyFlip.Models.ATHandle handle = default)
        {
            this.Did = did;
            this.Handle = handle;
            this.Type = "com.shinolabs.pinksea.appViewDefs#author";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        public Author()
        {
            this.Type = "com.shinolabs.pinksea.appViewDefs#author";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        public Author(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the did.
        /// <br/> The DID of the author
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets or sets the handle.
        /// <br/> The handle of the author.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle Handle { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.appViewDefs#author";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Author>)SourceGenerationContext.Default.ComShinolabsPinkseaAuthor);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Author>)SourceGenerationContext.Default.ComShinolabsPinkseaAuthor);
        }

        public static new Author FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Author>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.Author>)SourceGenerationContext.Default.ComShinolabsPinkseaAuthor)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Author FromCBORObject(CBORObject obj)
        {
            return new Author(obj);
        }

        /// <inheritdoc/>
        public static Author Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Author>(s, (JsonTypeInfo<Author>)SourceGenerationContext.Default.ComShinolabsPinkseaAuthor)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Author result)
        {
            result = JsonSerializer.Deserialize<Author>(s, (JsonTypeInfo<Author>)SourceGenerationContext.Default.ComShinolabsPinkseaAuthor);
            return result != null;
        }
    }
}


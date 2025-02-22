// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Collection
{
    public partial class FormatsV0 : ATObject, ICBOREncodable<FormatsV0>, IJsonEncodable<FormatsV0>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatsV0"/> class.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="png_128">
        /// blue.moji.collection.defs#blob_v0 <br/>
        /// </param>
        /// <param name="apng_128">
        /// blue.moji.collection.defs#bytes_v0 <br/>
        /// </param>
        /// <param name="gif_128">
        /// blue.moji.collection.defs#blob_v0 <br/>
        /// </param>
        /// <param name="webp_128">
        /// blue.moji.collection.defs#blob_v0 <br/>
        /// </param>
        /// <param name="lottie">
        /// blue.moji.collection.defs#bytes_v0 <br/>
        /// </param>
        public FormatsV0(Blob? original = default, Blob? png_128 = default, byte[]? apng_128 = default, Blob? gif_128 = default, Blob? webp_128 = default, byte[]? lottie = default)
        {
            this.Original = original;
            this.Png128 = png_128;
            this.Apng128 = apng_128;
            this.Gif128 = gif_128;
            this.Webp128 = webp_128;
            this.Lottie = lottie;
            this.Type = "blue.moji.collection.item#formats_v0";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FormatsV0"/> class.
        /// </summary>
        public FormatsV0()
        {
            this.Type = "blue.moji.collection.item#formats_v0";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FormatsV0"/> class.
        /// </summary>
        public FormatsV0(CBORObject obj)
        {
            if (obj["original"] is not null) this.Original = new FishyFlip.Models.Blob(obj["original"]);
            if (obj["png_128"] is not null) this.Png128 = obj["png_128"].ToBlob();
            if (obj["apng_128"] is not null) this.Apng128 = obj["apng_128"].EncodeToBytes();
            if (obj["gif_128"] is not null) this.Gif128 = obj["gif_128"].ToBlob();
            if (obj["webp_128"] is not null) this.Webp128 = obj["webp_128"].ToBlob();
            if (obj["lottie"] is not null) this.Lottie = obj["lottie"].EncodeToBytes();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the original.
        /// </summary>
        [JsonPropertyName("original")]
        public Blob? Original { get; set; }

        /// <summary>
        /// Gets or sets the png_128.
        /// blue.moji.collection.defs#blob_v0 <br/>
        /// </summary>
        [JsonPropertyName("png_128")]
        public Blob? Png128 { get; set; }

        /// <summary>
        /// Gets or sets the apng_128.
        /// blue.moji.collection.defs#bytes_v0 <br/>
        /// </summary>
        [JsonPropertyName("apng_128")]
        public byte[]? Apng128 { get; set; }

        /// <summary>
        /// Gets or sets the gif_128.
        /// blue.moji.collection.defs#blob_v0 <br/>
        /// </summary>
        [JsonPropertyName("gif_128")]
        public Blob? Gif128 { get; set; }

        /// <summary>
        /// Gets or sets the webp_128.
        /// blue.moji.collection.defs#blob_v0 <br/>
        /// </summary>
        [JsonPropertyName("webp_128")]
        public Blob? Webp128 { get; set; }

        /// <summary>
        /// Gets or sets the lottie.
        /// blue.moji.collection.defs#bytes_v0 <br/>
        /// </summary>
        [JsonPropertyName("lottie")]
        public byte[]? Lottie { get; set; }

        public const string RecordType = "blue.moji.collection.item#formats_v0";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.FormatsV0>)SourceGenerationContext.Default.BlueMojiCollectionFormatsV0);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.FormatsV0>)SourceGenerationContext.Default.BlueMojiCollectionFormatsV0);
        }

        public static new FormatsV0 FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Collection.FormatsV0>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.FormatsV0>)SourceGenerationContext.Default.BlueMojiCollectionFormatsV0)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new FormatsV0 FromCBORObject(CBORObject obj)
        {
            return new FormatsV0(obj);
        }

    }
}


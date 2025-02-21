// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.My.Skylights
{
    public partial class RefItem : ATObject, ICBOREncodable<RefItem>, IJsonEncodable<RefItem>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RefItem"/> class.
        /// </summary>
        /// <param name="@ref"></param>
        /// <param name="value"></param>
        public RefItem(string @ref = default, string value = default)
        {
            this.Ref = @ref;
            this.Value = value;
            this.Type = "my.skylights.rel#refItem";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RefItem"/> class.
        /// </summary>
        public RefItem()
        {
            this.Type = "my.skylights.rel#refItem";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RefItem"/> class.
        /// </summary>
        public RefItem(CBORObject obj)
        {
            if (obj["ref"] is not null) this.Ref = obj["ref"].AsString();
            if (obj["value"] is not null) this.Value = obj["value"].AsString();
        }

        /// <summary>
        /// Gets or sets the ref.
        /// </summary>
        [JsonPropertyName("ref")]
        [JsonRequired]
        public string Ref { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public string Value { get; set; }

        public const string RecordType = "my.skylights.rel#refItem";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.My.Skylights.RefItem>)SourceGenerationContext.Default.MySkylightsRefItem);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.My.Skylights.RefItem>)SourceGenerationContext.Default.MySkylightsRefItem);
        }

        public static new RefItem FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.My.Skylights.RefItem>(json, (JsonTypeInfo<FishyFlip.Lexicon.My.Skylights.RefItem>)SourceGenerationContext.Default.MySkylightsRefItem)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new RefItem FromCBORObject(CBORObject obj)
        {
            return new RefItem(obj);
        }

    }
}


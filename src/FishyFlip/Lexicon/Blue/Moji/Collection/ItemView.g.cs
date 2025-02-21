// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Collection
{
    public partial class ItemView : ATObject, ICBOREncodable<ItemView>, IJsonEncodable<ItemView>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemView"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alt"></param>
        /// <param name="createdAt"></param>
        /// <param name="formats">
        /// blue.moji.collection.defs#formats_v0 <br/>
        /// </param>
        /// <param name="adultOnly"></param>
        public ItemView(string name = default, string? alt = default, DateTime? createdAt = default, FishyFlip.Lexicon.Blue.Moji.Collection.FormatsV0 formats = default, bool? adultOnly = default)
        {
            this.Name = name;
            this.Alt = alt;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Formats = formats;
            this.AdultOnly = adultOnly;
            this.Type = "blue.moji.collection.item#itemView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ItemView"/> class.
        /// </summary>
        public ItemView()
        {
            this.Type = "blue.moji.collection.item#itemView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ItemView"/> class.
        /// </summary>
        public ItemView(CBORObject obj)
        {
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["alt"] is not null) this.Alt = obj["alt"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["formats"] is not null) this.Formats = new FishyFlip.Lexicon.Blue.Moji.Collection.FormatsV0(obj["formats"]);
            if (obj["adultOnly"] is not null) this.AdultOnly = obj["adultOnly"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alt.
        /// </summary>
        [JsonPropertyName("alt")]
        public string? Alt { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the formats.
        /// blue.moji.collection.defs#formats_v0 <br/>
        /// </summary>
        [JsonPropertyName("formats")]
        [JsonRequired]
        public FishyFlip.Lexicon.Blue.Moji.Collection.FormatsV0 Formats { get; set; }

        /// <summary>
        /// Gets or sets the adultOnly.
        /// </summary>
        [JsonPropertyName("adultOnly")]
        public bool? AdultOnly { get; set; } = false;

        public const string RecordType = "blue.moji.collection.item#itemView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.ItemView>)SourceGenerationContext.Default.BlueMojiCollectionItemView);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.ItemView>)SourceGenerationContext.Default.BlueMojiCollectionItemView);
        }

        public static new ItemView FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Collection.ItemView>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.ItemView>)SourceGenerationContext.Default.BlueMojiCollectionItemView)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ItemView FromCBORObject(CBORObject obj)
        {
            return new ItemView(obj);
        }

    }
}


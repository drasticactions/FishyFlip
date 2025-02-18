// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Collection
{
    public partial class ListCollectionOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCollectionOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="items"></param>
        public ListCollectionOutput(string? cursor = default, List<FishyFlip.Lexicon.Blue.Moji.Collection.ItemView> items = default)
        {
            this.Cursor = cursor;
            this.Items = items;
            this.Type = "blue.moji.collection.listCollection#ListCollectionOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListCollectionOutput"/> class.
        /// </summary>
        public ListCollectionOutput()
        {
            this.Type = "blue.moji.collection.listCollection#ListCollectionOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListCollectionOutput"/> class.
        /// </summary>
        public ListCollectionOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["items"] is not null) this.Items = obj["items"].Values.Select(n =>new FishyFlip.Lexicon.Blue.Moji.Collection.ItemView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Blue.Moji.Collection.ItemView> Items { get; set; }

        public const string RecordType = "blue.moji.collection.listCollection#ListCollectionOutput";

        public static ListCollectionOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Collection.ListCollectionOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.ListCollectionOutput>)SourceGenerationContext.Default.BlueMojiCollectionListCollectionOutput)!;
        }
    }
}


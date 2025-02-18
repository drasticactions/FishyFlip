// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Collection
{
    public partial class GetItemOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetItemOutput"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="item">
        /// <see cref="FishyFlip.Lexicon.Blue.Moji.Collection.ItemView"/> (blue.moji.collection.item#itemView)
        /// </param>
        public GetItemOutput(FishyFlip.Models.ATUri uri = default, FishyFlip.Lexicon.Blue.Moji.Collection.ItemView item = default)
        {
            this.Uri = uri;
            this.Item = item;
            this.Type = "blue.moji.collection.getItem#GetItemOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetItemOutput"/> class.
        /// </summary>
        public GetItemOutput()
        {
            this.Type = "blue.moji.collection.getItem#GetItemOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetItemOutput"/> class.
        /// </summary>
        public GetItemOutput(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["item"] is not null) this.Item = new FishyFlip.Lexicon.Blue.Moji.Collection.ItemView(obj["item"]);
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri Uri { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// <br/> <see cref="FishyFlip.Lexicon.Blue.Moji.Collection.ItemView"/> (blue.moji.collection.item#itemView)
        /// </summary>
        [JsonPropertyName("item")]
        [JsonRequired]
        public FishyFlip.Lexicon.Blue.Moji.Collection.ItemView Item { get; set; }

        public const string RecordType = "blue.moji.collection.getItem#GetItemOutput";

        public static GetItemOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Collection.GetItemOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.GetItemOutput>)SourceGenerationContext.Default.BlueMojiCollectionGetItemOutput)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Linkat
{
    public partial class Card : ATObject, ICBOREncodable<Card>, IJsonEncodable<Card>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="url">URL of the link</param>
        /// <param name="text">Text of the card</param>
        /// <param name="emoji">Emoji of the card</param>
        public Card(string? url = default, string? text = default, string? emoji = default)
        {
            this.Url = url;
            this.Text = text;
            this.Emoji = emoji;
            this.Type = "blue.linkat.board#card";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card()
        {
            this.Type = "blue.linkat.board#card";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card(CBORObject obj)
        {
            if (obj["url"] is not null) this.Url = obj["url"].AsString();
            if (obj["text"] is not null) this.Text = obj["text"].AsString();
            if (obj["emoji"] is not null) this.Emoji = obj["emoji"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the url.
        /// <br/> URL of the link
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// <br/> Text of the card
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the emoji.
        /// <br/> Emoji of the card
        /// </summary>
        [JsonPropertyName("emoji")]
        public string? Emoji { get; set; }

        public const string RecordType = "blue.linkat.board#card";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Linkat.Card>)SourceGenerationContext.Default.BlueLinkatCard);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Linkat.Card>)SourceGenerationContext.Default.BlueLinkatCard);
        }

        public static new Card FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Linkat.Card>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Linkat.Card>)SourceGenerationContext.Default.BlueLinkatCard)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Card FromCBORObject(CBORObject obj)
        {
            return new Card(obj);
        }

    }
}


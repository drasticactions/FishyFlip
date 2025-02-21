// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class MessageInput : ATObject, ICBOREncodable<MessageInput>, IJsonEncodable<MessageInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageInput"/> class.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="facets">Annotations of text (mentions, URLs, hashtags, etc)</param>
        /// <param name="embed">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord"/> (app.bsky.embed.record) <br/>
        /// </param>
        public MessageInput(string text = default, List<FishyFlip.Lexicon.App.Bsky.Richtext.Facet>? facets = default, FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord? embed = default)
        {
            this.Text = text;
            this.Facets = facets;
            this.Embed = embed;
            this.Type = "chat.bsky.convo.defs#messageInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MessageInput"/> class.
        /// </summary>
        public MessageInput()
        {
            this.Type = "chat.bsky.convo.defs#messageInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MessageInput"/> class.
        /// </summary>
        public MessageInput(CBORObject obj)
        {
            if (obj["text"] is not null) this.Text = obj["text"].AsString();
            if (obj["facets"] is not null) this.Facets = obj["facets"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Richtext.Facet(n)).ToList();
            if (obj["embed"] is not null) this.Embed = new FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord(obj["embed"]);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [JsonPropertyName("text")]
        [JsonRequired]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the facets.
        /// <br/> Annotations of text (mentions, URLs, hashtags, etc)
        /// </summary>
        [JsonPropertyName("facets")]
        public List<FishyFlip.Lexicon.App.Bsky.Richtext.Facet>? Facets { get; set; }

        /// <summary>
        /// Gets or sets the embed.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord"/> (app.bsky.embed.record) <br/>
        /// </summary>
        [JsonPropertyName("embed")]
        public FishyFlip.Lexicon.App.Bsky.Embed.EmbedRecord? Embed { get; set; }

        public const string RecordType = "chat.bsky.convo.defs#messageInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageInput>)SourceGenerationContext.Default.ChatBskyConvoMessageInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageInput>)SourceGenerationContext.Default.ChatBskyConvoMessageInput);
        }

        public static new MessageInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageInput>)SourceGenerationContext.Default.ChatBskyConvoMessageInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new MessageInput FromCBORObject(CBORObject obj)
        {
            return new MessageInput(obj);
        }

    }
}


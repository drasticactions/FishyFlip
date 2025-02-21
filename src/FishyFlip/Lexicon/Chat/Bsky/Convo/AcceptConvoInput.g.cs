// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class AcceptConvoInput : ATObject, ICBOREncodable<AcceptConvoInput>, IJsonEncodable<AcceptConvoInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptConvoInput"/> class.
        /// </summary>
        /// <param name="convoId"></param>
        public AcceptConvoInput(string convoId = default)
        {
            this.ConvoId = convoId;
            this.Type = "chat.bsky.convo.acceptConvo#AcceptConvoInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptConvoInput"/> class.
        /// </summary>
        public AcceptConvoInput()
        {
            this.Type = "chat.bsky.convo.acceptConvo#AcceptConvoInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptConvoInput"/> class.
        /// </summary>
        public AcceptConvoInput(CBORObject obj)
        {
            if (obj["convoId"] is not null) this.ConvoId = obj["convoId"].AsString();
        }

        /// <summary>
        /// Gets or sets the convoId.
        /// </summary>
        [JsonPropertyName("convoId")]
        [JsonRequired]
        public string ConvoId { get; set; }

        public const string RecordType = "chat.bsky.convo.acceptConvo#AcceptConvoInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoInput>)SourceGenerationContext.Default.ChatBskyConvoAcceptConvoInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoInput>)SourceGenerationContext.Default.ChatBskyConvoAcceptConvoInput);
        }

        public static new AcceptConvoInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoInput>)SourceGenerationContext.Default.ChatBskyConvoAcceptConvoInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new AcceptConvoInput FromCBORObject(CBORObject obj)
        {
            return new AcceptConvoInput(obj);
        }

    }
}


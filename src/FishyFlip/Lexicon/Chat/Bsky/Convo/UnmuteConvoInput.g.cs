// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class UnmuteConvoInput : ATObject, ICBOREncodable<UnmuteConvoInput>, IJsonEncodable<UnmuteConvoInput>, IParsable<UnmuteConvoInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UnmuteConvoInput"/> class.
        /// </summary>
        /// <param name="convoId"></param>
        public UnmuteConvoInput(string convoId = default)
        {
            this.ConvoId = convoId;
            this.Type = "chat.bsky.convo.unmuteConvo#UnmuteConvoInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnmuteConvoInput"/> class.
        /// </summary>
        public UnmuteConvoInput()
        {
            this.Type = "chat.bsky.convo.unmuteConvo#UnmuteConvoInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnmuteConvoInput"/> class.
        /// </summary>
        public UnmuteConvoInput(CBORObject obj)
        {
            if (obj["convoId"] is not null) this.ConvoId = obj["convoId"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the convoId.
        /// </summary>
        [JsonPropertyName("convoId")]
        [JsonRequired]
        public string ConvoId { get; set; }

        public const string RecordType = "chat.bsky.convo.unmuteConvo#UnmuteConvoInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoInput>)SourceGenerationContext.Default.ChatBskyConvoUnmuteConvoInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoInput>)SourceGenerationContext.Default.ChatBskyConvoUnmuteConvoInput);
        }

        public static new UnmuteConvoInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.UnmuteConvoInput>)SourceGenerationContext.Default.ChatBskyConvoUnmuteConvoInput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new UnmuteConvoInput FromCBORObject(CBORObject obj)
        {
            return new UnmuteConvoInput(obj);
        }

        /// <inheritdoc/>
        public static UnmuteConvoInput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<UnmuteConvoInput>(s, (JsonTypeInfo<UnmuteConvoInput>)SourceGenerationContext.Default.ChatBskyConvoUnmuteConvoInput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out UnmuteConvoInput result)
        {
            result = JsonSerializer.Deserialize<UnmuteConvoInput>(s, (JsonTypeInfo<UnmuteConvoInput>)SourceGenerationContext.Default.ChatBskyConvoUnmuteConvoInput);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class MuteConvoInput : ATObject, ICBOREncodable<MuteConvoInput>, IJsonEncodable<MuteConvoInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MuteConvoInput"/> class.
        /// </summary>
        /// <param name="convoId"></param>
        public MuteConvoInput(string convoId = default)
        {
            this.ConvoId = convoId;
            this.Type = "chat.bsky.convo.muteConvo#MuteConvoInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteConvoInput"/> class.
        /// </summary>
        public MuteConvoInput()
        {
            this.Type = "chat.bsky.convo.muteConvo#MuteConvoInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteConvoInput"/> class.
        /// </summary>
        public MuteConvoInput(CBORObject obj)
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

        public const string RecordType = "chat.bsky.convo.muteConvo#MuteConvoInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoInput>)SourceGenerationContext.Default.ChatBskyConvoMuteConvoInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoInput>)SourceGenerationContext.Default.ChatBskyConvoMuteConvoInput);
        }

        public static new MuteConvoInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoInput>)SourceGenerationContext.Default.ChatBskyConvoMuteConvoInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new MuteConvoInput FromCBORObject(CBORObject obj)
        {
            return new MuteConvoInput(obj);
        }

    }
}


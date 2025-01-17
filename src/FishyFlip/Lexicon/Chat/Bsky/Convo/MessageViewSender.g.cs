// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class MessageViewSender : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageViewSender"/> class.
        /// </summary>
        /// <param name="did"></param>
        public MessageViewSender(FishyFlip.Models.ATDid did = default)
        {
            this.Did = did;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MessageViewSender"/> class.
        /// </summary>
        public MessageViewSender()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MessageViewSender"/> class.
        /// </summary>
        public MessageViewSender(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "chat.bsky.convo.defs#messageViewSender";

        public const string RecordType = "chat.bsky.convo.defs#messageViewSender";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageViewSender>(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageViewSender>)SourceGenerationContext.Default.ChatBskyConvoMessageViewSender)!;
        }

        public static MessageViewSender FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageViewSender>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MessageViewSender>)SourceGenerationContext.Default.ChatBskyConvoMessageViewSender)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class MuteConvoOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MuteConvoOutput"/> class.
        /// </summary>
        /// <param name="convo">
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView"/> (chat.bsky.convo.defs#convoView)
        /// </param>
        public MuteConvoOutput(FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView convo = default)
        {
            this.Convo = convo;
            this.Type = "chat.bsky.convo.muteConvo#MuteConvoOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteConvoOutput"/> class.
        /// </summary>
        public MuteConvoOutput()
        {
            this.Type = "chat.bsky.convo.muteConvo#MuteConvoOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteConvoOutput"/> class.
        /// </summary>
        public MuteConvoOutput(CBORObject obj)
        {
            if (obj["convo"] is not null) this.Convo = new FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView(obj["convo"]);
        }

        /// <summary>
        /// Gets or sets the convo.
        /// <br/> <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView"/> (chat.bsky.convo.defs#convoView)
        /// </summary>
        [JsonPropertyName("convo")]
        [JsonRequired]
        public FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView Convo { get; set; }

        public const string RecordType = "chat.bsky.convo.muteConvo#MuteConvoOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoMuteConvoOutput)!;
        }

        public static MuteConvoOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.MuteConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoMuteConvoOutput)!;
        }
    }
}


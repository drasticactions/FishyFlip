// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class LeaveConvoInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveConvoInput"/> class.
        /// </summary>
        /// <param name="convoId"></param>
        public LeaveConvoInput(string? convoId = default)
        {
            this.ConvoId = convoId;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveConvoInput"/> class.
        /// </summary>
        public LeaveConvoInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveConvoInput"/> class.
        /// </summary>
        public LeaveConvoInput(CBORObject obj)
        {
            if (obj["convoId"] is not null) this.ConvoId = obj["convoId"].AsString();
        }

        /// <summary>
        /// Gets or sets the convoId.
        /// </summary>
        [JsonPropertyName("convoId")]
        [JsonRequired]
        public string? ConvoId { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "chat.bsky.convo.leaveConvo#LeaveConvoInput";

        public const string RecordType = "chat.bsky.convo.leaveConvo#LeaveConvoInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoInput>)SourceGenerationContext.Default.ChatBskyConvoLeaveConvoInput)!;
        }

        public static LeaveConvoInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoInput>)SourceGenerationContext.Default.ChatBskyConvoLeaveConvoInput)!;
        }
    }
}


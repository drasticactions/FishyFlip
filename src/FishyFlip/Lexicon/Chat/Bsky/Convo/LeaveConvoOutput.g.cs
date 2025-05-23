// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class LeaveConvoOutput : ATObject, ICBOREncodable<LeaveConvoOutput>, IJsonEncodable<LeaveConvoOutput>, IParsable<LeaveConvoOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveConvoOutput"/> class.
        /// </summary>
        /// <param name="convoId"></param>
        /// <param name="rev"></param>
        public LeaveConvoOutput(string convoId = default, string rev = default)
        {
            this.ConvoId = convoId;
            this.Rev = rev;
            this.Type = "chat.bsky.convo.leaveConvo#LeaveConvoOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveConvoOutput"/> class.
        /// </summary>
        public LeaveConvoOutput()
        {
            this.Type = "chat.bsky.convo.leaveConvo#LeaveConvoOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveConvoOutput"/> class.
        /// </summary>
        public LeaveConvoOutput(CBORObject obj)
        {
            if (obj["convoId"] is not null) this.ConvoId = obj["convoId"].AsString();
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the convoId.
        /// </summary>
        [JsonPropertyName("convoId")]
        [JsonRequired]
        public string ConvoId { get; set; }

        /// <summary>
        /// Gets or sets the rev.
        /// </summary>
        [JsonPropertyName("rev")]
        [JsonRequired]
        public string Rev { get; set; }

        public const string RecordType = "chat.bsky.convo.leaveConvo#LeaveConvoOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoLeaveConvoOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoLeaveConvoOutput);
        }

        public static new LeaveConvoOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.LeaveConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoLeaveConvoOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new LeaveConvoOutput FromCBORObject(CBORObject obj)
        {
            return new LeaveConvoOutput(obj);
        }

        /// <inheritdoc/>
        public static LeaveConvoOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<LeaveConvoOutput>(s, (JsonTypeInfo<LeaveConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoLeaveConvoOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out LeaveConvoOutput result)
        {
            result = JsonSerializer.Deserialize<LeaveConvoOutput>(s, (JsonTypeInfo<LeaveConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoLeaveConvoOutput);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class GetLogOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLogOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="logs">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LogBeginConvo"/> (chat.bsky.convo.defs#logBeginConvo) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LogLeaveConvo"/> (chat.bsky.convo.defs#logLeaveConvo) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LogCreateMessage"/> (chat.bsky.convo.defs#logCreateMessage) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LogDeleteMessage"/> (chat.bsky.convo.defs#logDeleteMessage) <br/>
        /// </param>
        public GetLogOutput(string? cursor = default, List<ATObject> logs = default)
        {
            this.Cursor = cursor;
            this.Logs = logs;
            this.Type = "chat.bsky.convo.getLog#GetLogOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetLogOutput"/> class.
        /// </summary>
        public GetLogOutput()
        {
            this.Type = "chat.bsky.convo.getLog#GetLogOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetLogOutput"/> class.
        /// </summary>
        public GetLogOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["logs"] is not null) this.Logs = obj["logs"].Values.Select(n =>n.ToATObject()).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the logs.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LogBeginConvo"/> (chat.bsky.convo.defs#logBeginConvo) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LogLeaveConvo"/> (chat.bsky.convo.defs#logLeaveConvo) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LogCreateMessage"/> (chat.bsky.convo.defs#logCreateMessage) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.LogDeleteMessage"/> (chat.bsky.convo.defs#logDeleteMessage) <br/>
        /// </summary>
        [JsonPropertyName("logs")]
        [JsonRequired]
        public List<ATObject> Logs { get; set; }

        public const string RecordType = "chat.bsky.convo.getLog#GetLogOutput";

        public static GetLogOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.GetLogOutput>)SourceGenerationContext.Default.ChatBskyConvoGetLogOutput)!;
        }
    }
}


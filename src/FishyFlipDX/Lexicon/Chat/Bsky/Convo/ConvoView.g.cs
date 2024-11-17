// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class ConvoView : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvoView"/> class.
        /// </summary>
        public ConvoView()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConvoView"/> class.
        /// </summary>
        public ConvoView(CBORObject obj)
        {
            if (obj["id"] is not null) this.Id = obj["id"].AsString();
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
            if (obj["members"] is not null) this.Members = obj["members"].Values.Select(n => n is not null ? new Chat.Bsky.Actor.ProfileViewBasic(n) : null).ToList();
            if (obj["lastMessage"] is not null) this.LastMessage = obj["lastMessage"].ToATObject();
            if (obj["muted"] is not null) this.Muted = obj["muted"].AsBoolean();
            if (obj["opened"] is not null) this.Opened = obj["opened"].AsBoolean();
            if (obj["unreadCount"] is not null) this.UnreadCount = obj["unreadCount"].AsInt64Value();
        }

        [JsonPropertyName("id")]
        [JsonRequired]
        public string? Id { get; set; }

        [JsonPropertyName("rev")]
        [JsonRequired]
        public string? Rev { get; set; }

        [JsonPropertyName("members")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Chat.Bsky.Actor.ProfileViewBasic?>? Members { get; set; }

        [JsonPropertyName("lastMessage")]
        public ATObject? LastMessage { get; set; }

        [JsonPropertyName("muted")]
        [JsonRequired]
        public bool? Muted { get; set; }

        [JsonPropertyName("opened")]
        public bool? Opened { get; set; }

        [JsonPropertyName("unreadCount")]
        [JsonRequired]
        public long? UnreadCount { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "chat.bsky.convo.defs#convoView";

        public const string RecordType = "chat.bsky.convo.defs#convoView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Chat.Bsky.Convo.ConvoView>(this, (JsonTypeInfo<Chat.Bsky.Convo.ConvoView>)SourceGenerationContext.Default.ChatBskyConvoConvoView)!;
        }

        public static ConvoView FromJson(string json)
        {
            return JsonSerializer.Deserialize<Chat.Bsky.Convo.ConvoView>(json, (JsonTypeInfo<Chat.Bsky.Convo.ConvoView>)SourceGenerationContext.Default.ChatBskyConvoConvoView)!;
        }
    }
}


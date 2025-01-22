// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class ConvoView : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvoView"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rev"></param>
        /// <param name="members"></param>
        /// <param name="lastMessage">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView"/> (chat.bsky.convo.defs#messageView) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView"/> (chat.bsky.convo.defs#deletedMessageView) <br/>
        /// </param>
        /// <param name="muted"></param>
        /// <param name="opened"></param>
        /// <param name="unreadCount"></param>
        public ConvoView(string id = default, string rev = default, List<FishyFlip.Lexicon.Chat.Bsky.Actor.ProfileViewBasic> members = default, ATObject? lastMessage = default, bool muted = default, bool? opened = default, long unreadCount = default)
        {
            this.Id = id;
            this.Rev = rev;
            this.Members = members;
            this.LastMessage = lastMessage;
            this.Muted = muted;
            this.Opened = opened;
            this.UnreadCount = unreadCount;
            this.Type = "chat.bsky.convo.defs#convoView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConvoView"/> class.
        /// </summary>
        public ConvoView()
        {
            this.Type = "chat.bsky.convo.defs#convoView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConvoView"/> class.
        /// </summary>
        public ConvoView(CBORObject obj)
        {
            if (obj["id"] is not null) this.Id = obj["id"].AsString();
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
            if (obj["members"] is not null) this.Members = obj["members"].Values.Select(n =>new FishyFlip.Lexicon.Chat.Bsky.Actor.ProfileViewBasic(n)).ToList();
            if (obj["lastMessage"] is not null) this.LastMessage = obj["lastMessage"].ToATObject();
            if (obj["muted"] is not null) this.Muted = obj["muted"].AsBoolean();
            if (obj["opened"] is not null) this.Opened = obj["opened"].AsBoolean();
            if (obj["unreadCount"] is not null) this.UnreadCount = obj["unreadCount"].AsInt64Value();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonRequired]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the rev.
        /// </summary>
        [JsonPropertyName("rev")]
        [JsonRequired]
        public string Rev { get; set; }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        [JsonPropertyName("members")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Chat.Bsky.Actor.ProfileViewBasic> Members { get; set; }

        /// <summary>
        /// Gets or sets the lastMessage.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.MessageView"/> (chat.bsky.convo.defs#messageView) <br/>
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.DeletedMessageView"/> (chat.bsky.convo.defs#deletedMessageView) <br/>
        /// </summary>
        [JsonPropertyName("lastMessage")]
        public ATObject? LastMessage { get; set; }

        /// <summary>
        /// Gets or sets the muted.
        /// </summary>
        [JsonPropertyName("muted")]
        [JsonRequired]
        public bool Muted { get; set; }

        /// <summary>
        /// Gets or sets the opened.
        /// </summary>
        [JsonPropertyName("opened")]
        public bool? Opened { get; set; }

        /// <summary>
        /// Gets or sets the unreadCount.
        /// </summary>
        [JsonPropertyName("unreadCount")]
        [JsonRequired]
        public long UnreadCount { get; set; }

        public const string RecordType = "chat.bsky.convo.defs#convoView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView>(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView>)SourceGenerationContext.Default.ChatBskyConvoConvoView)!;
        }

        public static ConvoView FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView>)SourceGenerationContext.Default.ChatBskyConvoConvoView)!;
        }
    }
}


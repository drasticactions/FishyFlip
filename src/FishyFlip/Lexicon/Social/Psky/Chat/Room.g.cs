// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Social.Psky.Chat
{
    /// <summary>
    /// A Picosky room belonging to the user.
    /// </summary>
    public partial class Room : ATObject, ICBOREncodable<Room>, IJsonEncodable<Room>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="languages"></param>
        /// <param name="topic">Topic title of the room.</param>
        /// <param name="tags"></param>
        /// <param name="allowlist">List of users allowed to send messages in the room.
        /// social.psky.chat.defs#modlistRef <br/>
        /// </param>
        /// <param name="denylist">List of users disallowed to send messages in the room.
        /// social.psky.chat.defs#modlistRef <br/>
        /// </param>
        public Room(string? name, List<string>? languages = default, string? topic = default, List<string>? tags = default, FishyFlip.Lexicon.Social.Psky.Chat.ModlistRef? allowlist = default, FishyFlip.Lexicon.Social.Psky.Chat.ModlistRef? denylist = default)
        {
            this.Name = name;
            this.Languages = languages;
            this.Topic = topic;
            this.Tags = tags;
            this.Allowlist = allowlist;
            this.Denylist = denylist;
            this.Type = "social.psky.chat.room";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        public Room()
        {
            this.Type = "social.psky.chat.room";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        public Room(CBORObject obj)
        {
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["languages"] is not null) this.Languages = obj["languages"].Values.Select(n =>n.AsString()).ToList();
            if (obj["topic"] is not null) this.Topic = obj["topic"].AsString();
            if (obj["tags"] is not null) this.Tags = obj["tags"].Values.Select(n =>n.AsString()).ToList();
            if (obj["allowlist"] is not null) this.Allowlist = new FishyFlip.Lexicon.Social.Psky.Chat.ModlistRef(obj["allowlist"]);
            if (obj["denylist"] is not null) this.Denylist = new FishyFlip.Lexicon.Social.Psky.Chat.ModlistRef(obj["denylist"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        [JsonPropertyName("languages")]
        public List<string>? Languages { get; set; }

        /// <summary>
        /// Gets or sets the topic.
        /// <br/> Topic title of the room.
        /// </summary>
        [JsonPropertyName("topic")]
        public string? Topic { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Gets or sets the allowlist.
        /// <br/> List of users allowed to send messages in the room.
        /// social.psky.chat.defs#modlistRef <br/>
        /// </summary>
        [JsonPropertyName("allowlist")]
        public FishyFlip.Lexicon.Social.Psky.Chat.ModlistRef? Allowlist { get; set; }

        /// <summary>
        /// Gets or sets the denylist.
        /// <br/> List of users disallowed to send messages in the room.
        /// social.psky.chat.defs#modlistRef <br/>
        /// </summary>
        [JsonPropertyName("denylist")]
        public FishyFlip.Lexicon.Social.Psky.Chat.ModlistRef? Denylist { get; set; }

        public const string RecordType = "social.psky.chat.room";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Chat.Room>)SourceGenerationContext.Default.SocialPskyChatRoom);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Chat.Room>)SourceGenerationContext.Default.SocialPskyChatRoom);
        }

        public static new Room FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Social.Psky.Chat.Room>(json, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Chat.Room>)SourceGenerationContext.Default.SocialPskyChatRoom)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Room FromCBORObject(CBORObject obj)
        {
            return new Room(obj);
        }

    }
}


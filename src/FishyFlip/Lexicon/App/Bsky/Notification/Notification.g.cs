// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Notification
{
    public partial class Notification : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="author">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </param>
        /// <param name="reason">Expected values are 'like', 'repost', 'follow', 'mention', 'reply', 'quote', and 'starterpack-joined'.
        /// <br/> Known Values: <br/>
        /// like <br/>
        /// repost <br/>
        /// follow <br/>
        /// mention <br/>
        /// reply <br/>
        /// quote <br/>
        /// starterpack-joined <br/>
        /// </param>
        /// <param name="reasonSubject"></param>
        /// <param name="record"></param>
        /// <param name="isRead"></param>
        /// <param name="indexedAt"></param>
        /// <param name="labels"></param>
        public Notification(FishyFlip.Models.ATUri? uri = default, string? cid = default, FishyFlip.Lexicon.App.Bsky.Actor.ProfileView? author = default, string? reason = default, FishyFlip.Models.ATUri? reasonSubject = default, ATObject? record = default, bool? isRead = default, DateTime? indexedAt = default, List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? labels = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.Author = author;
            this.Reason = reason;
            this.ReasonSubject = reasonSubject;
            this.Record = record;
            this.IsRead = isRead;
            this.IndexedAt = indexedAt;
            this.Labels = labels;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        public Notification()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        public Notification(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["author"] is not null) this.Author = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(obj["author"]);
            if (obj["reason"] is not null) this.Reason = obj["reason"].AsString();
            if (obj["reasonSubject"] is not null) this.ReasonSubject = obj["reasonSubject"].ToATUri();
            if (obj["record"] is not null) this.Record = obj["record"].ToATObject();
            if (obj["isRead"] is not null) this.IsRead = obj["isRead"].AsBoolean();
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Label.Label(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Uri { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        [JsonRequired]
        public string? Cid { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </summary>
        [JsonPropertyName("author")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Actor.ProfileView? Author { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// <br/> Expected values are 'like', 'repost', 'follow', 'mention', 'reply', 'quote', and 'starterpack-joined'.
        /// <br/> Known Values: <br/>
        /// like <br/>
        /// repost <br/>
        /// follow <br/>
        /// mention <br/>
        /// reply <br/>
        /// quote <br/>
        /// starterpack-joined <br/>
        /// </summary>
        [JsonPropertyName("reason")]
        [JsonRequired]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets the reasonSubject.
        /// </summary>
        [JsonPropertyName("reasonSubject")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? ReasonSubject { get; set; }

        /// <summary>
        /// Gets or sets the record.
        /// </summary>
        [JsonPropertyName("record")]
        [JsonRequired]
        public ATObject? Record { get; set; }

        /// <summary>
        /// Gets or sets the isRead.
        /// </summary>
        [JsonPropertyName("isRead")]
        [JsonRequired]
        public bool? IsRead { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? Labels { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.notification.listNotifications#notification";

        public const string RecordType = "app.bsky.notification.listNotifications#notification";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Notification.Notification>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Notification.Notification>)SourceGenerationContext.Default.AppBskyNotificationNotification)!;
        }

        public static Notification FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Notification.Notification>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Notification.Notification>)SourceGenerationContext.Default.AppBskyNotificationNotification)!;
        }
    }
}


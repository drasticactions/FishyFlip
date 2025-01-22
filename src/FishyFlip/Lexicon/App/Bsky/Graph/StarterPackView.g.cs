// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class StarterPackView : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StarterPackView"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="record"></param>
        /// <param name="creator">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic"/> (app.bsky.actor.defs#profileViewBasic)
        /// </param>
        /// <param name="list">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.ListViewBasic"/> (app.bsky.graph.defs#listViewBasic)
        /// </param>
        /// <param name="listItemsSample"></param>
        /// <param name="feeds"></param>
        /// <param name="joinedWeekCount"></param>
        /// <param name="joinedAllTimeCount"></param>
        /// <param name="labels"></param>
        /// <param name="indexedAt"></param>
        public StarterPackView(FishyFlip.Models.ATUri uri = default, string cid = default, ATObject record = default, FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic creator = default, FishyFlip.Lexicon.App.Bsky.Graph.ListViewBasic? list = default, List<FishyFlip.Lexicon.App.Bsky.Graph.ListItemView>? listItemsSample = default, List<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView>? feeds = default, long? joinedWeekCount = default, long? joinedAllTimeCount = default, List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? labels = default, DateTime? indexedAt = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.Record = record;
            this.Creator = creator;
            this.List = list;
            this.ListItemsSample = listItemsSample;
            this.Feeds = feeds;
            this.JoinedWeekCount = joinedWeekCount;
            this.JoinedAllTimeCount = joinedAllTimeCount;
            this.Labels = labels;
            this.IndexedAt = indexedAt;
            this.Type = "app.bsky.graph.defs#starterPackView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StarterPackView"/> class.
        /// </summary>
        public StarterPackView()
        {
            this.Type = "app.bsky.graph.defs#starterPackView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StarterPackView"/> class.
        /// </summary>
        public StarterPackView(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["record"] is not null) this.Record = obj["record"].ToATObject();
            if (obj["creator"] is not null) this.Creator = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic(obj["creator"]);
            if (obj["list"] is not null) this.List = new FishyFlip.Lexicon.App.Bsky.Graph.ListViewBasic(obj["list"]);
            if (obj["listItemsSample"] is not null) this.ListItemsSample = obj["listItemsSample"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Graph.ListItemView(n)).ToList();
            if (obj["feeds"] is not null) this.Feeds = obj["feeds"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView(n)).ToList();
            if (obj["joinedWeekCount"] is not null) this.JoinedWeekCount = obj["joinedWeekCount"].AsInt64Value();
            if (obj["joinedAllTimeCount"] is not null) this.JoinedAllTimeCount = obj["joinedAllTimeCount"].AsInt64Value();
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Label.Label(n)).ToList();
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri Uri { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        [JsonRequired]
        public string Cid { get; set; }

        /// <summary>
        /// Gets or sets the record.
        /// </summary>
        [JsonPropertyName("record")]
        [JsonRequired]
        public ATObject Record { get; set; }

        /// <summary>
        /// Gets or sets the creator.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic"/> (app.bsky.actor.defs#profileViewBasic)
        /// </summary>
        [JsonPropertyName("creator")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic Creator { get; set; }

        /// <summary>
        /// Gets or sets the list.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Graph.ListViewBasic"/> (app.bsky.graph.defs#listViewBasic)
        /// </summary>
        [JsonPropertyName("list")]
        public FishyFlip.Lexicon.App.Bsky.Graph.ListViewBasic? List { get; set; }

        /// <summary>
        /// Gets or sets the listItemsSample.
        /// </summary>
        [JsonPropertyName("listItemsSample")]
        public List<FishyFlip.Lexicon.App.Bsky.Graph.ListItemView>? ListItemsSample { get; set; }

        /// <summary>
        /// Gets or sets the feeds.
        /// </summary>
        [JsonPropertyName("feeds")]
        public List<FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView>? Feeds { get; set; }

        /// <summary>
        /// Gets or sets the joinedWeekCount.
        /// </summary>
        [JsonPropertyName("joinedWeekCount")]
        public long? JoinedWeekCount { get; set; }

        /// <summary>
        /// Gets or sets the joinedAllTimeCount.
        /// </summary>
        [JsonPropertyName("joinedAllTimeCount")]
        public long? JoinedAllTimeCount { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? Labels { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        public const string RecordType = "app.bsky.graph.defs#starterPackView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Graph.StarterPackView>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.StarterPackView>)SourceGenerationContext.Default.AppBskyGraphStarterPackView)!;
        }

        public static StarterPackView FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.StarterPackView>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.StarterPackView>)SourceGenerationContext.Default.AppBskyGraphStarterPackView)!;
        }
    }
}


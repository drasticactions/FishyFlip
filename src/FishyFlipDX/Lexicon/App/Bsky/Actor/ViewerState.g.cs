// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    /// <summary>
    /// Metadata about the requesting account's relationship with the subject account. Only has meaningful content for authed requests.
    /// </summary>
    public partial class ViewerState : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerState"/> class.
        /// </summary>
        public ViewerState()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerState"/> class.
        /// </summary>
        public ViewerState(CBORObject obj)
        {
            if (obj["muted"] is not null) this.Muted = obj["muted"].AsBoolean();
            if (obj["mutedByList"] is not null) this.MutedByList = new App.Bsky.Graph.ListViewBasic(obj["mutedByList"]);
            if (obj["blockedBy"] is not null) this.BlockedBy = obj["blockedBy"].AsBoolean();
            if (obj["blocking"] is not null) this.Blocking = obj["blocking"].ToATUri();
            if (obj["blockingByList"] is not null) this.BlockingByList = new App.Bsky.Graph.ListViewBasic(obj["blockingByList"]);
            if (obj["following"] is not null) this.Following = obj["following"].ToATUri();
            if (obj["followedBy"] is not null) this.FollowedBy = obj["followedBy"].ToATUri();
            if (obj["knownFollowers"] is not null) this.KnownFollowers = new App.Bsky.Actor.KnownFollowers(obj["knownFollowers"]);
        }

        [JsonPropertyName("muted")]
        public bool? Muted { get; set; }

        [JsonPropertyName("mutedByList")]
        public App.Bsky.Graph.ListViewBasic? MutedByList { get; set; }

        [JsonPropertyName("blockedBy")]
        public bool? BlockedBy { get; set; }

        [JsonPropertyName("blocking")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Blocking { get; set; }

        [JsonPropertyName("blockingByList")]
        public App.Bsky.Graph.ListViewBasic? BlockingByList { get; set; }

        [JsonPropertyName("following")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Following { get; set; }

        [JsonPropertyName("followedBy")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? FollowedBy { get; set; }

        [JsonPropertyName("knownFollowers")]
        public App.Bsky.Actor.KnownFollowers? KnownFollowers { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.actor.defs#viewerState";

        public const string RecordType = "app.bsky.actor.defs#viewerState";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Actor.ViewerState>(this, (JsonTypeInfo<App.Bsky.Actor.ViewerState>)SourceGenerationContext.Default.AppBskyActorViewerState)!;
        }

        public static ViewerState FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Actor.ViewerState>(json, (JsonTypeInfo<App.Bsky.Actor.ViewerState>)SourceGenerationContext.Default.AppBskyActorViewerState)!;
        }
    }
}


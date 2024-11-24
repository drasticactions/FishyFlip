// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class ProfileViewBasic : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewBasic"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="handle"></param>
        /// <param name="displayName"></param>
        /// <param name="avatar"></param>
        /// <param name="associated">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated"/> (app.bsky.actor.defs#profileAssociated)
        /// </param>
        /// <param name="viewer">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ViewerState"/> (app.bsky.actor.defs#viewerState)
        /// </param>
        /// <param name="labels"></param>
        /// <param name="createdAt"></param>
        public ProfileViewBasic(FishyFlip.Models.ATDid? did = default, FishyFlip.Models.ATHandle? handle = default, string? displayName = default, string? avatar = default, App.Bsky.Actor.ProfileAssociated? associated = default, App.Bsky.Actor.ViewerState? viewer = default, List<Com.Atproto.Label.Label>? labels = default, DateTime? createdAt = default)
        {
            this.Did = did;
            this.Handle = handle;
            this.DisplayName = displayName;
            this.Avatar = avatar;
            this.Associated = associated;
            this.Viewer = viewer;
            this.Labels = labels;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewBasic"/> class.
        /// </summary>
        public ProfileViewBasic()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewBasic"/> class.
        /// </summary>
        public ProfileViewBasic(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["avatar"] is not null) this.Avatar = obj["avatar"].AsString();
            if (obj["associated"] is not null) this.Associated = new App.Bsky.Actor.ProfileAssociated(obj["associated"]);
            if (obj["viewer"] is not null) this.Viewer = new App.Bsky.Actor.ViewerState(obj["viewer"]);
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new Com.Atproto.Label.Label(n)).ToList();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle? Handle { get; set; }

        /// <summary>
        /// Gets or sets the displayName.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the associated.
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated"/> (app.bsky.actor.defs#profileAssociated)
        /// </summary>
        [JsonPropertyName("associated")]
        public App.Bsky.Actor.ProfileAssociated? Associated { get; set; }

        /// <summary>
        /// Gets or sets the viewer.
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ViewerState"/> (app.bsky.actor.defs#viewerState)
        /// </summary>
        [JsonPropertyName("viewer")]
        public App.Bsky.Actor.ViewerState? Viewer { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public List<Com.Atproto.Label.Label>? Labels { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.actor.defs#profileViewBasic";

        public const string RecordType = "app.bsky.actor.defs#profileViewBasic";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Actor.ProfileViewBasic>(this, (JsonTypeInfo<App.Bsky.Actor.ProfileViewBasic>)SourceGenerationContext.Default.AppBskyActorProfileViewBasic)!;
        }

        public static ProfileViewBasic FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Actor.ProfileViewBasic>(json, (JsonTypeInfo<App.Bsky.Actor.ProfileViewBasic>)SourceGenerationContext.Default.AppBskyActorProfileViewBasic)!;
        }
    }
}

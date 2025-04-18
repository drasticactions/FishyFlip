// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class ProfileViewDetailed : ATObject, ICBOREncodable<ProfileViewDetailed>, IJsonEncodable<ProfileViewDetailed>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewDetailed"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="handle"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="avatar"></param>
        /// <param name="banner"></param>
        /// <param name="followersCount"></param>
        /// <param name="followsCount"></param>
        /// <param name="postsCount"></param>
        /// <param name="associated">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated"/> (app.bsky.actor.defs#profileAssociated)
        /// </param>
        /// <param name="joinedViaStarterPack">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic"/> (app.bsky.graph.defs#starterPackViewBasic)
        /// </param>
        /// <param name="indexedAt"></param>
        /// <param name="createdAt"></param>
        /// <param name="viewer">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ViewerState"/> (app.bsky.actor.defs#viewerState)
        /// </param>
        /// <param name="labels"></param>
        /// <param name="pinnedPost">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="verification">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.VerificationState"/> (app.bsky.actor.defs#verificationState)
        /// </param>
        public ProfileViewDetailed(FishyFlip.Models.ATDid did = default, FishyFlip.Models.ATHandle handle = default, string? displayName = default, string? description = default, string? avatar = default, string? banner = default, long? followersCount = default, long? followsCount = default, long? postsCount = default, FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated? associated = default, FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic? joinedViaStarterPack = default, DateTime? indexedAt = default, DateTime? createdAt = default, FishyFlip.Lexicon.App.Bsky.Actor.ViewerState? viewer = default, List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? labels = default, Com.Atproto.Repo.StrongRef? pinnedPost = default, FishyFlip.Lexicon.App.Bsky.Actor.VerificationState? verification = default)
        {
            this.Did = did;
            this.Handle = handle;
            this.DisplayName = displayName;
            this.Description = description;
            this.Avatar = avatar;
            this.Banner = banner;
            this.FollowersCount = followersCount;
            this.FollowsCount = followsCount;
            this.PostsCount = postsCount;
            this.Associated = associated;
            this.JoinedViaStarterPack = joinedViaStarterPack;
            this.IndexedAt = indexedAt;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Viewer = viewer;
            this.Labels = labels;
            this.PinnedPost = pinnedPost;
            this.Verification = verification;
            this.Type = "app.bsky.actor.defs#profileViewDetailed";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewDetailed"/> class.
        /// </summary>
        public ProfileViewDetailed()
        {
            this.Type = "app.bsky.actor.defs#profileViewDetailed";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewDetailed"/> class.
        /// </summary>
        public ProfileViewDetailed(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["avatar"] is not null) this.Avatar = obj["avatar"].AsString();
            if (obj["banner"] is not null) this.Banner = obj["banner"].AsString();
            if (obj["followersCount"] is not null) this.FollowersCount = obj["followersCount"].AsInt64Value();
            if (obj["followsCount"] is not null) this.FollowsCount = obj["followsCount"].AsInt64Value();
            if (obj["postsCount"] is not null) this.PostsCount = obj["postsCount"].AsInt64Value();
            if (obj["associated"] is not null) this.Associated = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated(obj["associated"]);
            if (obj["joinedViaStarterPack"] is not null) this.JoinedViaStarterPack = new FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic(obj["joinedViaStarterPack"]);
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["viewer"] is not null) this.Viewer = new FishyFlip.Lexicon.App.Bsky.Actor.ViewerState(obj["viewer"]);
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Label.Label(n)).ToList();
            if (obj["pinnedPost"] is not null) this.PinnedPost = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["pinnedPost"]);
            if (obj["verification"] is not null) this.Verification = new FishyFlip.Lexicon.App.Bsky.Actor.VerificationState(obj["verification"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle Handle { get; set; }

        /// <summary>
        /// Gets or sets the displayName.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the banner.
        /// </summary>
        [JsonPropertyName("banner")]
        public string? Banner { get; set; }

        /// <summary>
        /// Gets or sets the followersCount.
        /// </summary>
        [JsonPropertyName("followersCount")]
        public long? FollowersCount { get; set; }

        /// <summary>
        /// Gets or sets the followsCount.
        /// </summary>
        [JsonPropertyName("followsCount")]
        public long? FollowsCount { get; set; }

        /// <summary>
        /// Gets or sets the postsCount.
        /// </summary>
        [JsonPropertyName("postsCount")]
        public long? PostsCount { get; set; }

        /// <summary>
        /// Gets or sets the associated.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated"/> (app.bsky.actor.defs#profileAssociated)
        /// </summary>
        [JsonPropertyName("associated")]
        public FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated? Associated { get; set; }

        /// <summary>
        /// Gets or sets the joinedViaStarterPack.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic"/> (app.bsky.graph.defs#starterPackViewBasic)
        /// </summary>
        [JsonPropertyName("joinedViaStarterPack")]
        public FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic? JoinedViaStarterPack { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        public DateTime? IndexedAt { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the viewer.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ViewerState"/> (app.bsky.actor.defs#viewerState)
        /// </summary>
        [JsonPropertyName("viewer")]
        public FishyFlip.Lexicon.App.Bsky.Actor.ViewerState? Viewer { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? Labels { get; set; }

        /// <summary>
        /// Gets or sets the pinnedPost.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("pinnedPost")]
        public Com.Atproto.Repo.StrongRef? PinnedPost { get; set; }

        /// <summary>
        /// Gets or sets the verification.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.VerificationState"/> (app.bsky.actor.defs#verificationState)
        /// </summary>
        [JsonPropertyName("verification")]
        public FishyFlip.Lexicon.App.Bsky.Actor.VerificationState? Verification { get; set; }

        public const string RecordType = "app.bsky.actor.defs#profileViewDetailed";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed>)SourceGenerationContext.Default.AppBskyActorProfileViewDetailed);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed>)SourceGenerationContext.Default.AppBskyActorProfileViewDetailed);
        }

        public static new ProfileViewDetailed FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed>)SourceGenerationContext.Default.AppBskyActorProfileViewDetailed)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ProfileViewDetailed FromCBORObject(CBORObject obj)
        {
            return new ProfileViewDetailed(obj);
        }

    }
}


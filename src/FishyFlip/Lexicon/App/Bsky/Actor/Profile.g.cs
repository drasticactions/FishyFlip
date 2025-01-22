// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    /// <summary>
    /// A declaration of a Bluesky account profile.
    /// </summary>
    public partial class Profile : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="description">Free-form profile description text.</param>
        /// <param name="avatar">Small image to be displayed next to posts from account. AKA, 'profile picture'</param>
        /// <param name="banner">Larger horizontal image to display behind profile view.</param>
        /// <param name="labels">Self-label values, specific to the Bluesky application, on the overall account.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels"/> (com.atproto.label.defs#selfLabels) <br/>
        /// </param>
        /// <param name="joinedViaStarterPack">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="pinnedPost">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="createdAt"></param>
        public Profile(string? displayName = default, string? description = default, Blob? avatar = default, Blob? banner = default, FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels? labels = default, Com.Atproto.Repo.StrongRef? joinedViaStarterPack = default, Com.Atproto.Repo.StrongRef? pinnedPost = default, DateTime? createdAt = default)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.Avatar = avatar;
            this.Banner = banner;
            this.Labels = labels;
            this.JoinedViaStarterPack = joinedViaStarterPack;
            this.PinnedPost = pinnedPost;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "app.bsky.actor.profile";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        {
            this.Type = "app.bsky.actor.profile";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile(CBORObject obj)
        {
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["avatar"] is not null) this.Avatar = new FishyFlip.Models.Blob(obj["avatar"]);
            if (obj["banner"] is not null) this.Banner = new FishyFlip.Models.Blob(obj["banner"]);
            if (obj["labels"] is not null) this.Labels = new FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels(obj["labels"]);
            if (obj["joinedViaStarterPack"] is not null) this.JoinedViaStarterPack = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["joinedViaStarterPack"]);
            if (obj["pinnedPost"] is not null) this.PinnedPost = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["pinnedPost"]);
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the displayName.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// <br/> Free-form profile description text.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// <br/> Small image to be displayed next to posts from account. AKA, 'profile picture'
        /// </summary>
        [JsonPropertyName("avatar")]
        public Blob? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the banner.
        /// <br/> Larger horizontal image to display behind profile view.
        /// </summary>
        [JsonPropertyName("banner")]
        public Blob? Banner { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// <br/> Self-label values, specific to the Bluesky application, on the overall account.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels"/> (com.atproto.label.defs#selfLabels) <br/>
        /// </summary>
        [JsonPropertyName("labels")]
        public FishyFlip.Lexicon.Com.Atproto.Label.SelfLabels? Labels { get; set; }

        /// <summary>
        /// Gets or sets the joinedViaStarterPack.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("joinedViaStarterPack")]
        public Com.Atproto.Repo.StrongRef? JoinedViaStarterPack { get; set; }

        /// <summary>
        /// Gets or sets the pinnedPost.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("pinnedPost")]
        public Com.Atproto.Repo.StrongRef? PinnedPost { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "app.bsky.actor.profile";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Actor.Profile>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.Profile>)SourceGenerationContext.Default.AppBskyActorProfile)!;
        }

        public static Profile FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.Profile>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.Profile>)SourceGenerationContext.Default.AppBskyActorProfile)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

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
        public Profile(string? displayName = default, string? description = default, Blob? avatar = default, Blob? banner = default, Com.Atproto.Label.SelfLabels? labels = default, Com.Atproto.Repo.StrongRef? joinedViaStarterPack = default, Com.Atproto.Repo.StrongRef? pinnedPost = default, DateTime? createdAt = default)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.Avatar = avatar;
            this.Banner = banner;
            this.Labels = labels;
            this.JoinedViaStarterPack = joinedViaStarterPack;
            this.PinnedPost = pinnedPost;
            this.CreatedAt = createdAt;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        {
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
            if (obj["labels"] is not null) this.Labels = new Com.Atproto.Label.SelfLabels(obj["labels"]);
            if (obj["joinedViaStarterPack"] is not null) this.JoinedViaStarterPack = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["joinedViaStarterPack"]);
            if (obj["pinnedPost"] is not null) this.PinnedPost = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["pinnedPost"]);
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Free-form profile description text.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Small image to be displayed next to posts from account. AKA, 'profile picture'
        /// </summary>
        [JsonPropertyName("avatar")]
        public Blob? Avatar { get; set; }

        /// <summary>
        /// Larger horizontal image to display behind profile view.
        /// </summary>
        [JsonPropertyName("banner")]
        public Blob? Banner { get; set; }

        /// <summary>
        /// Self-label values, specific to the Bluesky application, on the overall account.
        /// </summary>
        [JsonPropertyName("labels")]
        public Com.Atproto.Label.SelfLabels? Labels { get; set; }

        [JsonPropertyName("joinedViaStarterPack")]
        public Com.Atproto.Repo.StrongRef? JoinedViaStarterPack { get; set; }

        [JsonPropertyName("pinnedPost")]
        public Com.Atproto.Repo.StrongRef? PinnedPost { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.actor.profile";

        public const string RecordType = "app.bsky.actor.profile";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Actor.Profile>(this, (JsonTypeInfo<App.Bsky.Actor.Profile>)SourceGenerationContext.Default.AppBskyActorProfile)!;
        }

        public static Profile FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Actor.Profile>(json, (JsonTypeInfo<App.Bsky.Actor.Profile>)SourceGenerationContext.Default.AppBskyActorProfile)!;
        }
    }
}


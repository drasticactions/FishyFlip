// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Fm.Teal.Alpha.Actor
{
    /// <summary>
    /// This lexicon is in a not officially released state. It is subject to change. | A declaration of a teal.fm account profile.
    /// </summary>
    public partial class Profile : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="description">Free-form profile description text.</param>
        /// <param name="descriptionFacets">Annotations of text in the profile description (mentions, URLs, hashtags, etc).</param>
        /// <param name="featuredItem">The user's most recent item featured on their profile.
        /// fm.teal.alpha.actor.defs#featuredItem <br/>
        /// </param>
        /// <param name="avatar">Small image to be displayed next to posts from account. AKA, 'profile picture'</param>
        /// <param name="banner">Larger horizontal image to display behind profile view.</param>
        /// <param name="createdAt"></param>
        public Profile(string? displayName = default, string? description = default, List<FishyFlip.Lexicon.App.Bsky.Richtext.Facet>? descriptionFacets = default, FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem? featuredItem = default, Blob? avatar = default, Blob? banner = default, DateTime? createdAt = default)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.DescriptionFacets = descriptionFacets;
            this.FeaturedItem = featuredItem;
            this.Avatar = avatar;
            this.Banner = banner;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Type = "fm.teal.alpha.actor.profile";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        {
            this.Type = "fm.teal.alpha.actor.profile";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile(CBORObject obj)
        {
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["descriptionFacets"] is not null) this.DescriptionFacets = obj["descriptionFacets"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Richtext.Facet(n)).ToList();
            if (obj["featuredItem"] is not null) this.FeaturedItem = new FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem(obj["featuredItem"]);
            if (obj["avatar"] is not null) this.Avatar = new FishyFlip.Models.Blob(obj["avatar"]);
            if (obj["banner"] is not null) this.Banner = new FishyFlip.Models.Blob(obj["banner"]);
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
        /// Gets or sets the descriptionFacets.
        /// <br/> Annotations of text in the profile description (mentions, URLs, hashtags, etc).
        /// </summary>
        [JsonPropertyName("descriptionFacets")]
        public List<FishyFlip.Lexicon.App.Bsky.Richtext.Facet>? DescriptionFacets { get; set; }

        /// <summary>
        /// Gets or sets the featuredItem.
        /// <br/> The user's most recent item featured on their profile.
        /// fm.teal.alpha.actor.defs#featuredItem <br/>
        /// </summary>
        [JsonPropertyName("featuredItem")]
        public FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.FeaturedItem? FeaturedItem { get; set; }

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
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public const string RecordType = "fm.teal.alpha.actor.profile";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.Profile>(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.Profile>)SourceGenerationContext.Default.FmTealAlphaActorProfile)!;
        }

        public static Profile FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.Profile>(json, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.Profile>)SourceGenerationContext.Default.FmTealAlphaActorProfile)!;
        }
    }
}


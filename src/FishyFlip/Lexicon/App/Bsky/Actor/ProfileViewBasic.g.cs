// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class ProfileViewBasic : ATObject, ICBOREncodable<ProfileViewBasic>, IJsonEncodable<ProfileViewBasic>
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
        /// <param name="verification">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.VerificationState"/> (app.bsky.actor.defs#verificationState)
        /// </param>
        public ProfileViewBasic(FishyFlip.Models.ATDid did = default, FishyFlip.Models.ATHandle handle = default, string? displayName = default, string? avatar = default, FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated? associated = default, FishyFlip.Lexicon.App.Bsky.Actor.ViewerState? viewer = default, List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? labels = default, DateTime? createdAt = default, FishyFlip.Lexicon.App.Bsky.Actor.VerificationState? verification = default)
        {
            this.Did = did;
            this.Handle = handle;
            this.DisplayName = displayName;
            this.Avatar = avatar;
            this.Associated = associated;
            this.Viewer = viewer;
            this.Labels = labels;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Verification = verification;
            this.Type = "app.bsky.actor.defs#profileViewBasic";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewBasic"/> class.
        /// </summary>
        public ProfileViewBasic()
        {
            this.Type = "app.bsky.actor.defs#profileViewBasic";
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
            if (obj["associated"] is not null) this.Associated = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated(obj["associated"]);
            if (obj["viewer"] is not null) this.Viewer = new FishyFlip.Lexicon.App.Bsky.Actor.ViewerState(obj["viewer"]);
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Label.Label(n)).ToList();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
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
        /// Gets or sets the avatar.
        /// </summary>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the associated.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated"/> (app.bsky.actor.defs#profileAssociated)
        /// </summary>
        [JsonPropertyName("associated")]
        public FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociated? Associated { get; set; }

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
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the verification.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.VerificationState"/> (app.bsky.actor.defs#verificationState)
        /// </summary>
        [JsonPropertyName("verification")]
        public FishyFlip.Lexicon.App.Bsky.Actor.VerificationState? Verification { get; set; }

        public const string RecordType = "app.bsky.actor.defs#profileViewBasic";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic>)SourceGenerationContext.Default.AppBskyActorProfileViewBasic);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic>)SourceGenerationContext.Default.AppBskyActorProfileViewBasic);
        }

        public static new ProfileViewBasic FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic>)SourceGenerationContext.Default.AppBskyActorProfileViewBasic)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ProfileViewBasic FromCBORObject(CBORObject obj)
        {
            return new ProfileViewBasic(obj);
        }

    }
}


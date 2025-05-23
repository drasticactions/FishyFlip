// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Social.Psky.Actor
{
    /// <summary>
    /// A declaration of a Picosky account profile.
    /// </summary>
    public partial class Profile : ATObject, ICBOREncodable<Profile>, IJsonEncodable<Profile>, IParsable<Profile>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        /// <param name="nickname"></param>
        public Profile(string? nickname = default)
        {
            this.Nickname = nickname;
            this.Type = "social.psky.actor.profile";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        {
            this.Type = "social.psky.actor.profile";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile(CBORObject obj)
        {
            if (obj["nickname"] is not null) this.Nickname = obj["nickname"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        [JsonPropertyName("nickname")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Nickname { get; set; }

        public const string RecordType = "social.psky.actor.profile";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Actor.Profile>)SourceGenerationContext.Default.SocialPskyActorProfile);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Actor.Profile>)SourceGenerationContext.Default.SocialPskyActorProfile);
        }

        public static new Profile FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Social.Psky.Actor.Profile>(json, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Actor.Profile>)SourceGenerationContext.Default.SocialPskyActorProfile)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Profile FromCBORObject(CBORObject obj)
        {
            return new Profile(obj);
        }

        /// <inheritdoc/>
        public static Profile Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Profile>(s, (JsonTypeInfo<Profile>)SourceGenerationContext.Default.SocialPskyActorProfile)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Profile result)
        {
            result = JsonSerializer.Deserialize<Profile>(s, (JsonTypeInfo<Profile>)SourceGenerationContext.Default.SocialPskyActorProfile);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Buzz.Bookhive
{
    public partial class Profile : ATObject, ICBOREncodable<Profile>, IJsonEncodable<Profile>, IParsable<Profile>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="handle"></param>
        /// <param name="avatar"></param>
        /// <param name="description"></param>
        /// <param name="booksRead"></param>
        /// <param name="reviews"></param>
        public Profile(string displayName = default, string handle = default, string? avatar = default, string? description = default, long booksRead = default, long reviews = default)
        {
            this.DisplayName = displayName;
            this.Handle = handle;
            this.Avatar = avatar;
            this.Description = description;
            this.BooksRead = booksRead;
            this.Reviews = reviews;
            this.Type = "buzz.bookhive.defs#profile";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        {
            this.Type = "buzz.bookhive.defs#profile";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile(CBORObject obj)
        {
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["handle"] is not null) this.Handle = obj["handle"].AsString();
            if (obj["avatar"] is not null) this.Avatar = obj["avatar"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["booksRead"] is not null) this.BooksRead = obj["booksRead"].AsInt64Value();
            if (obj["reviews"] is not null) this.Reviews = obj["reviews"].AsInt64Value();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the displayName.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonRequired]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        public string Handle { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        [JsonPropertyName("avatar")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the booksRead.
        /// </summary>
        [JsonPropertyName("booksRead")]
        [JsonRequired]
        public long BooksRead { get; set; }

        /// <summary>
        /// Gets or sets the reviews.
        /// </summary>
        [JsonPropertyName("reviews")]
        [JsonRequired]
        public long Reviews { get; set; }

        public const string RecordType = "buzz.bookhive.defs#profile";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.Profile>)SourceGenerationContext.Default.BuzzBookhiveProfile);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.Profile>)SourceGenerationContext.Default.BuzzBookhiveProfile);
        }

        public static new Profile FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Buzz.Bookhive.Profile>(json, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.Profile>)SourceGenerationContext.Default.BuzzBookhiveProfile)!;
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
            return JsonSerializer.Deserialize<Profile>(s, (JsonTypeInfo<Profile>)SourceGenerationContext.Default.BuzzBookhiveProfile)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Profile result)
        {
            result = JsonSerializer.Deserialize<Profile>(s, (JsonTypeInfo<Profile>)SourceGenerationContext.Default.BuzzBookhiveProfile);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Buzz.Bookhive
{
    public partial class GetProfileOutput : ATObject, ICBOREncodable<GetProfileOutput>, IJsonEncodable<GetProfileOutput>, IParsable<GetProfileOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileOutput"/> class.
        /// </summary>
        /// <param name="books">All books in the user's library</param>
        /// <param name="profile">The user's profile
        /// <see cref="FishyFlip.Lexicon.Buzz.Bookhive.Profile"/> (buzz.bookhive.defs#profile)
        /// </param>
        /// <param name="activity">The user's activity</param>
        public GetProfileOutput(List<FishyFlip.Lexicon.Buzz.Bookhive.UserBook> books = default, FishyFlip.Lexicon.Buzz.Bookhive.Profile profile = default, List<FishyFlip.Lexicon.Buzz.Bookhive.Activity> activity = default)
        {
            this.Books = books;
            this.Profile = profile;
            this.Activity = activity;
            this.Type = "buzz.bookhive.getProfile#GetProfileOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileOutput"/> class.
        /// </summary>
        public GetProfileOutput()
        {
            this.Type = "buzz.bookhive.getProfile#GetProfileOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileOutput"/> class.
        /// </summary>
        public GetProfileOutput(CBORObject obj)
        {
            if (obj["books"] is not null) this.Books = obj["books"].Values.Select(n =>new FishyFlip.Lexicon.Buzz.Bookhive.UserBook(n)).ToList();
            if (obj["profile"] is not null) this.Profile = new FishyFlip.Lexicon.Buzz.Bookhive.Profile(obj["profile"]);
            if (obj["activity"] is not null) this.Activity = obj["activity"].Values.Select(n =>new FishyFlip.Lexicon.Buzz.Bookhive.Activity(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the books.
        /// <br/> All books in the user's library
        /// </summary>
        [JsonPropertyName("books")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Buzz.Bookhive.UserBook> Books { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// <br/> The user's profile
        /// <br/> <see cref="FishyFlip.Lexicon.Buzz.Bookhive.Profile"/> (buzz.bookhive.defs#profile)
        /// </summary>
        [JsonPropertyName("profile")]
        [JsonRequired]
        public FishyFlip.Lexicon.Buzz.Bookhive.Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets the activity.
        /// <br/> The user's activity
        /// </summary>
        [JsonPropertyName("activity")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Buzz.Bookhive.Activity> Activity { get; set; }

        public const string RecordType = "buzz.bookhive.getProfile#GetProfileOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.GetProfileOutput>)SourceGenerationContext.Default.BuzzBookhiveGetProfileOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.GetProfileOutput>)SourceGenerationContext.Default.BuzzBookhiveGetProfileOutput);
        }

        public static new GetProfileOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Buzz.Bookhive.GetProfileOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.GetProfileOutput>)SourceGenerationContext.Default.BuzzBookhiveGetProfileOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetProfileOutput FromCBORObject(CBORObject obj)
        {
            return new GetProfileOutput(obj);
        }

        /// <inheritdoc/>
        public static GetProfileOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetProfileOutput>(s, (JsonTypeInfo<GetProfileOutput>)SourceGenerationContext.Default.BuzzBookhiveGetProfileOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetProfileOutput result)
        {
            result = JsonSerializer.Deserialize<GetProfileOutput>(s, (JsonTypeInfo<GetProfileOutput>)SourceGenerationContext.Default.BuzzBookhiveGetProfileOutput);
            return result != null;
        }
    }
}


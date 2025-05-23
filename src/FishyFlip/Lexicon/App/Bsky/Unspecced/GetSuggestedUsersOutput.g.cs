// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class GetSuggestedUsersOutput : ATObject, ICBOREncodable<GetSuggestedUsersOutput>, IJsonEncodable<GetSuggestedUsersOutput>, IParsable<GetSuggestedUsersOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedUsersOutput"/> class.
        /// </summary>
        /// <param name="actors"></param>
        public GetSuggestedUsersOutput(List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> actors = default)
        {
            this.Actors = actors;
            this.Type = "app.bsky.unspecced.getSuggestedUsers#GetSuggestedUsersOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedUsersOutput"/> class.
        /// </summary>
        public GetSuggestedUsersOutput()
        {
            this.Type = "app.bsky.unspecced.getSuggestedUsers#GetSuggestedUsersOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedUsersOutput"/> class.
        /// </summary>
        public GetSuggestedUsersOutput(CBORObject obj)
        {
            if (obj["actors"] is not null) this.Actors = obj["actors"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the actors.
        /// </summary>
        [JsonPropertyName("actors")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> Actors { get; set; }

        public const string RecordType = "app.bsky.unspecced.getSuggestedUsers#GetSuggestedUsersOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestedUsersOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetSuggestedUsersOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestedUsersOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetSuggestedUsersOutput);
        }

        public static new GetSuggestedUsersOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestedUsersOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestedUsersOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetSuggestedUsersOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetSuggestedUsersOutput FromCBORObject(CBORObject obj)
        {
            return new GetSuggestedUsersOutput(obj);
        }

        /// <inheritdoc/>
        public static GetSuggestedUsersOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetSuggestedUsersOutput>(s, (JsonTypeInfo<GetSuggestedUsersOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetSuggestedUsersOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetSuggestedUsersOutput result)
        {
            result = JsonSerializer.Deserialize<GetSuggestedUsersOutput>(s, (JsonTypeInfo<GetSuggestedUsersOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetSuggestedUsersOutput);
            return result != null;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class GetTaggedSuggestionsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTaggedSuggestionsOutput"/> class.
        /// </summary>
        /// <param name="suggestions"></param>
        public GetTaggedSuggestionsOutput(List<FishyFlip.Lexicon.App.Bsky.Unspecced.Suggestion> suggestions = default)
        {
            this.Suggestions = suggestions;
            this.Type = "app.bsky.unspecced.getTaggedSuggestions#GetTaggedSuggestionsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTaggedSuggestionsOutput"/> class.
        /// </summary>
        public GetTaggedSuggestionsOutput()
        {
            this.Type = "app.bsky.unspecced.getTaggedSuggestions#GetTaggedSuggestionsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTaggedSuggestionsOutput"/> class.
        /// </summary>
        public GetTaggedSuggestionsOutput(CBORObject obj)
        {
            if (obj["suggestions"] is not null) this.Suggestions = obj["suggestions"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Unspecced.Suggestion(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the suggestions.
        /// </summary>
        [JsonPropertyName("suggestions")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Unspecced.Suggestion> Suggestions { get; set; }

        public const string RecordType = "app.bsky.unspecced.getTaggedSuggestions#GetTaggedSuggestionsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTaggedSuggestionsOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTaggedSuggestionsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTaggedSuggestionsOutput)!;
        }

        public static GetTaggedSuggestionsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTaggedSuggestionsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTaggedSuggestionsOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetTaggedSuggestionsOutput)!;
        }
    }
}


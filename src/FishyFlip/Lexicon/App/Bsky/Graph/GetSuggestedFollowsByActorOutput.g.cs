// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetSuggestedFollowsByActorOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedFollowsByActorOutput"/> class.
        /// </summary>
        /// <param name="suggestions"></param>
        /// <param name="isFallback">If true, response has fallen-back to generic results, and is not scoped using relativeToDid</param>
        public GetSuggestedFollowsByActorOutput(List<App.Bsky.Actor.ProfileView>? suggestions = default, bool? isFallback = default)
        {
            this.Suggestions = suggestions;
            this.IsFallback = isFallback;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedFollowsByActorOutput"/> class.
        /// </summary>
        public GetSuggestedFollowsByActorOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedFollowsByActorOutput"/> class.
        /// </summary>
        public GetSuggestedFollowsByActorOutput(CBORObject obj)
        {
            if (obj["suggestions"] is not null) this.Suggestions = obj["suggestions"].Values.Select(n =>new App.Bsky.Actor.ProfileView(n)).ToList();
            if (obj["isFallback"] is not null) this.IsFallback = obj["isFallback"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the suggestions.
        /// </summary>
        [JsonPropertyName("suggestions")]
        [JsonRequired]
        public List<App.Bsky.Actor.ProfileView>? Suggestions { get; set; }

        /// <summary>
        /// Gets or sets the isFallback.
        /// <br/> If true, response has fallen-back to generic results, and is not scoped using relativeToDid
        /// </summary>
        [JsonPropertyName("isFallback")]
        public bool? IsFallback { get; set; } = false;

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.getSuggestedFollowsByActor#GetSuggestedFollowsByActorOutput";

        public const string RecordType = "app.bsky.graph.getSuggestedFollowsByActor#GetSuggestedFollowsByActorOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.GetSuggestedFollowsByActorOutput>(this, (JsonTypeInfo<App.Bsky.Graph.GetSuggestedFollowsByActorOutput>)SourceGenerationContext.Default.AppBskyGraphGetSuggestedFollowsByActorOutput)!;
        }

        public static GetSuggestedFollowsByActorOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.GetSuggestedFollowsByActorOutput>(json, (JsonTypeInfo<App.Bsky.Graph.GetSuggestedFollowsByActorOutput>)SourceGenerationContext.Default.AppBskyGraphGetSuggestedFollowsByActorOutput)!;
        }
    }
}


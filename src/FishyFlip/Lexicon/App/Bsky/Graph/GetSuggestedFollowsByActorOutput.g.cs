// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetSuggestedFollowsByActorOutput : ATObject, ICBOREncodable<GetSuggestedFollowsByActorOutput>, IJsonEncodable<GetSuggestedFollowsByActorOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedFollowsByActorOutput"/> class.
        /// </summary>
        /// <param name="suggestions"></param>
        /// <param name="isFallback">If true, response has fallen-back to generic results, and is not scoped using relativeToDid</param>
        /// <param name="recId">Snowflake for this recommendation, use when submitting recommendation events.</param>
        public GetSuggestedFollowsByActorOutput(List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> suggestions = default, bool? isFallback = default, long? recId = default)
        {
            this.Suggestions = suggestions;
            this.IsFallback = isFallback;
            this.RecId = recId;
            this.Type = "app.bsky.graph.getSuggestedFollowsByActor#GetSuggestedFollowsByActorOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedFollowsByActorOutput"/> class.
        /// </summary>
        public GetSuggestedFollowsByActorOutput()
        {
            this.Type = "app.bsky.graph.getSuggestedFollowsByActor#GetSuggestedFollowsByActorOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedFollowsByActorOutput"/> class.
        /// </summary>
        public GetSuggestedFollowsByActorOutput(CBORObject obj)
        {
            if (obj["suggestions"] is not null) this.Suggestions = obj["suggestions"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(n)).ToList();
            if (obj["isFallback"] is not null) this.IsFallback = obj["isFallback"].AsBoolean();
            if (obj["recId"] is not null) this.RecId = obj["recId"].AsInt64Value();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the suggestions.
        /// </summary>
        [JsonPropertyName("suggestions")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> Suggestions { get; set; }

        /// <summary>
        /// Gets or sets the isFallback.
        /// <br/> If true, response has fallen-back to generic results, and is not scoped using relativeToDid
        /// </summary>
        [JsonPropertyName("isFallback")]
        public bool? IsFallback { get; set; } = false;

        /// <summary>
        /// Gets or sets the recId.
        /// <br/> Snowflake for this recommendation, use when submitting recommendation events.
        /// </summary>
        [JsonPropertyName("recId")]
        public long? RecId { get; set; }

        public const string RecordType = "app.bsky.graph.getSuggestedFollowsByActor#GetSuggestedFollowsByActorOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetSuggestedFollowsByActorOutput>)SourceGenerationContext.Default.AppBskyGraphGetSuggestedFollowsByActorOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetSuggestedFollowsByActorOutput>)SourceGenerationContext.Default.AppBskyGraphGetSuggestedFollowsByActorOutput);
        }

        public static new GetSuggestedFollowsByActorOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetSuggestedFollowsByActorOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetSuggestedFollowsByActorOutput>)SourceGenerationContext.Default.AppBskyGraphGetSuggestedFollowsByActorOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetSuggestedFollowsByActorOutput FromCBORObject(CBORObject obj)
        {
            return new GetSuggestedFollowsByActorOutput(obj);
        }

    }
}


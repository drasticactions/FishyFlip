// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    /// <summary>
    /// indicates that a handle or DID could not be resolved
    /// </summary>
    public partial class NotFoundActor : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundActor"/> class.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="notFound"></param>
        public NotFoundActor(FishyFlip.Models.ATIdentifier? actor = default, bool? notFound = default)
        {
            this.Actor = actor;
            this.NotFound = notFound;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundActor"/> class.
        /// </summary>
        public NotFoundActor()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundActor"/> class.
        /// </summary>
        public NotFoundActor(CBORObject obj)
        {
            if (obj["actor"] is not null) this.Actor = obj["actor"].ToATIdentifier();
            if (obj["notFound"] is not null) this.NotFound = obj["notFound"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the actor.
        /// </summary>
        [JsonPropertyName("actor")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATIdentifierJsonConverter))]
        public FishyFlip.Models.ATIdentifier? Actor { get; set; }

        /// <summary>
        /// Gets or sets the notFound.
        /// </summary>
        [JsonPropertyName("notFound")]
        [JsonRequired]
        public bool? NotFound { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.defs#notFoundActor";

        public const string RecordType = "app.bsky.graph.defs#notFoundActor";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Graph.NotFoundActor>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.NotFoundActor>)SourceGenerationContext.Default.AppBskyGraphNotFoundActor)!;
        }

        public static NotFoundActor FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.NotFoundActor>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.NotFoundActor>)SourceGenerationContext.Default.AppBskyGraphNotFoundActor)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetRelationshipsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRelationshipsOutput"/> class.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="relationships">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.Relationship"/> (app.bsky.graph.defs#relationship) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.NotFoundActor"/> (app.bsky.graph.defs#notFoundActor) <br/>
        /// </param>
        public GetRelationshipsOutput(FishyFlip.Models.ATDid? actor = default, List<ATObject> relationships = default)
        {
            this.Actor = actor;
            this.Relationships = relationships;
            this.Type = "app.bsky.graph.getRelationships#GetRelationshipsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetRelationshipsOutput"/> class.
        /// </summary>
        public GetRelationshipsOutput()
        {
            this.Type = "app.bsky.graph.getRelationships#GetRelationshipsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetRelationshipsOutput"/> class.
        /// </summary>
        public GetRelationshipsOutput(CBORObject obj)
        {
            if (obj["actor"] is not null) this.Actor = obj["actor"].ToATDid();
            if (obj["relationships"] is not null) this.Relationships = obj["relationships"].Values.Select(n =>n.ToATObject()).ToList();
        }

        /// <summary>
        /// Gets or sets the actor.
        /// </summary>
        [JsonPropertyName("actor")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Actor { get; set; }

        /// <summary>
        /// Gets or sets the relationships.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.Relationship"/> (app.bsky.graph.defs#relationship) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.NotFoundActor"/> (app.bsky.graph.defs#notFoundActor) <br/>
        /// </summary>
        [JsonPropertyName("relationships")]
        [JsonRequired]
        public List<ATObject> Relationships { get; set; }

        public const string RecordType = "app.bsky.graph.getRelationships#GetRelationshipsOutput";

        public static GetRelationshipsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetRelationshipsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetRelationshipsOutput>)SourceGenerationContext.Default.AppBskyGraphGetRelationshipsOutput)!;
        }
    }
}


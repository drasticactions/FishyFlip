// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetRelationshipsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRelationshipsOutput"/> class.
        /// </summary>
        public GetRelationshipsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetRelationshipsOutput"/> class.
        /// </summary>
        public GetRelationshipsOutput(CBORObject obj)
        {
            if (obj["actor"] is not null) this.Actor = obj["actor"].ToATDid();
            if (obj["relationships"] is not null) this.Relationships = obj["relationships"].Values.Select(n => n is not null ? n.ToATObject() : null).ToList();
        }

        [JsonPropertyName("actor")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Actor { get; set; }

        [JsonPropertyName("relationships")]
        [JsonRequired]
        public List<ATObject?>? Relationships { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.getRelationships#GetRelationshipsOutput";

        public const string RecordType = "app.bsky.graph.getRelationships#GetRelationshipsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.GetRelationshipsOutput>(this, (JsonTypeInfo<App.Bsky.Graph.GetRelationshipsOutput>)SourceGenerationContext.Default.AppBskyGraphGetRelationshipsOutput)!;
        }

        public static GetRelationshipsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.GetRelationshipsOutput>(json, (JsonTypeInfo<App.Bsky.Graph.GetRelationshipsOutput>)SourceGenerationContext.Default.AppBskyGraphGetRelationshipsOutput)!;
        }
    }
}


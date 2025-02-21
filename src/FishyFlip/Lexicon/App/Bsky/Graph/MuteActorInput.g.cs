// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class MuteActorInput : ATObject, ICBOREncodable<MuteActorInput>, IJsonEncodable<MuteActorInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MuteActorInput"/> class.
        /// </summary>
        /// <param name="actor"></param>
        public MuteActorInput(FishyFlip.Models.ATIdentifier actor = default)
        {
            this.Actor = actor;
            this.Type = "app.bsky.graph.muteActor#MuteActorInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteActorInput"/> class.
        /// </summary>
        public MuteActorInput()
        {
            this.Type = "app.bsky.graph.muteActor#MuteActorInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteActorInput"/> class.
        /// </summary>
        public MuteActorInput(CBORObject obj)
        {
            if (obj["actor"] is not null) this.Actor = obj["actor"].ToATIdentifier();
        }

        /// <summary>
        /// Gets or sets the actor.
        /// </summary>
        [JsonPropertyName("actor")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATIdentifierJsonConverter))]
        public FishyFlip.Models.ATIdentifier Actor { get; set; }

        public const string RecordType = "app.bsky.graph.muteActor#MuteActorInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.MuteActorInput>)SourceGenerationContext.Default.AppBskyGraphMuteActorInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.MuteActorInput>)SourceGenerationContext.Default.AppBskyGraphMuteActorInput);
        }

        public static new MuteActorInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.MuteActorInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.MuteActorInput>)SourceGenerationContext.Default.AppBskyGraphMuteActorInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new MuteActorInput FromCBORObject(CBORObject obj)
        {
            return new MuteActorInput(obj);
        }

    }
}


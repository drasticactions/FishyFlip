// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Maril.Stellar
{
    public partial class ActorReaction : ATObject, ICBOREncodable<ActorReaction>, IJsonEncodable<ActorReaction>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorReaction"/> class.
        /// </summary>
        /// <param name="subject">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="reaction">
        /// <see cref="FishyFlip.Lexicon.Blue.Maril.Stellar.ReactionDef"/> (blue.maril.stellar.getReactions#reaction)
        /// </param>
        public ActorReaction(Com.Atproto.Repo.StrongRef subject = default, FishyFlip.Lexicon.Blue.Maril.Stellar.ReactionDef reaction = default)
        {
            this.Subject = subject;
            this.Reaction = reaction;
            this.Type = "blue.maril.stellar.getActorReactions#actorReaction";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ActorReaction"/> class.
        /// </summary>
        public ActorReaction()
        {
            this.Type = "blue.maril.stellar.getActorReactions#actorReaction";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ActorReaction"/> class.
        /// </summary>
        public ActorReaction(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["subject"]);
            if (obj["reaction"] is not null) this.Reaction = new FishyFlip.Lexicon.Blue.Maril.Stellar.ReactionDef(obj["reaction"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public Com.Atproto.Repo.StrongRef Subject { get; set; }

        /// <summary>
        /// Gets or sets the reaction.
        /// <br/> <see cref="FishyFlip.Lexicon.Blue.Maril.Stellar.ReactionDef"/> (blue.maril.stellar.getReactions#reaction)
        /// </summary>
        [JsonPropertyName("reaction")]
        [JsonRequired]
        public FishyFlip.Lexicon.Blue.Maril.Stellar.ReactionDef Reaction { get; set; }

        public const string RecordType = "blue.maril.stellar.getActorReactions#actorReaction";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction>)SourceGenerationContext.Default.BlueMarilStellarActorReaction);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction>)SourceGenerationContext.Default.BlueMarilStellarActorReaction);
        }

        public static new ActorReaction FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction>)SourceGenerationContext.Default.BlueMarilStellarActorReaction)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ActorReaction FromCBORObject(CBORObject obj)
        {
            return new ActorReaction(obj);
        }

    }
}


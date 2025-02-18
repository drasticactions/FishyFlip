// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Maril.Stellar
{
    public partial class ActorReaction : ATObject
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

        public static ActorReaction FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Maril.Stellar.ActorReaction>)SourceGenerationContext.Default.BlueMarilStellarActorReaction)!;
        }
    }
}


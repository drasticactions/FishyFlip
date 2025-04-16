// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Fm.Teal.Alpha.Actor
{
    public partial class GetProfileOutput : ATObject, ICBOREncodable<GetProfileOutput>, IJsonEncodable<GetProfileOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileOutput"/> class.
        /// </summary>
        /// <param name="actor">
        /// <see cref="FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.ProfileView"/> (fm.teal.alpha.actor.defs#profileView)
        /// </param>
        public GetProfileOutput(FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.ProfileView actor = default)
        {
            this.Actor = actor;
            this.Type = "fm.teal.alpha.actor.getProfile#GetProfileOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileOutput"/> class.
        /// </summary>
        public GetProfileOutput()
        {
            this.Type = "fm.teal.alpha.actor.getProfile#GetProfileOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileOutput"/> class.
        /// </summary>
        public GetProfileOutput(CBORObject obj)
        {
            if (obj["actor"] is not null) this.Actor = new FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.ProfileView(obj["actor"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the actor.
        /// <br/> <see cref="FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.ProfileView"/> (fm.teal.alpha.actor.defs#profileView)
        /// </summary>
        [JsonPropertyName("actor")]
        [JsonRequired]
        public FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.ProfileView Actor { get; set; }

        public const string RecordType = "fm.teal.alpha.actor.getProfile#GetProfileOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.GetProfileOutput>)SourceGenerationContext.Default.FmTealAlphaActorGetProfileOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.GetProfileOutput>)SourceGenerationContext.Default.FmTealAlphaActorGetProfileOutput);
        }

        public static new GetProfileOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.GetProfileOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.GetProfileOutput>)SourceGenerationContext.Default.FmTealAlphaActorGetProfileOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetProfileOutput FromCBORObject(CBORObject obj)
        {
            return new GetProfileOutput(obj);
        }

    }
}


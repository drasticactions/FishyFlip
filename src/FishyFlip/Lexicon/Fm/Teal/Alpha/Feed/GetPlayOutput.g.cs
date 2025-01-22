// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Fm.Teal.Alpha.Feed
{
    public partial class GetPlayOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPlayOutput"/> class.
        /// </summary>
        /// <param name="play">
        /// <see cref="FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.PlayView"/> (fm.teal.alpha.feed.defs#playView)
        /// </param>
        public GetPlayOutput(FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.PlayView play = default)
        {
            this.Play = play;
            this.Type = "fm.teal.alpha.feed.getPlay#GetPlayOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPlayOutput"/> class.
        /// </summary>
        public GetPlayOutput()
        {
            this.Type = "fm.teal.alpha.feed.getPlay#GetPlayOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPlayOutput"/> class.
        /// </summary>
        public GetPlayOutput(CBORObject obj)
        {
            if (obj["play"] is not null) this.Play = new FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.PlayView(obj["play"]);
        }

        /// <summary>
        /// Gets or sets the play.
        /// <br/> <see cref="FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.PlayView"/> (fm.teal.alpha.feed.defs#playView)
        /// </summary>
        [JsonPropertyName("play")]
        [JsonRequired]
        public FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.PlayView Play { get; set; }

        public const string RecordType = "fm.teal.alpha.feed.getPlay#GetPlayOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.GetPlayOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.GetPlayOutput>)SourceGenerationContext.Default.FmTealAlphaFeedGetPlayOutput)!;
        }

        public static GetPlayOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.GetPlayOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.GetPlayOutput>)SourceGenerationContext.Default.FmTealAlphaFeedGetPlayOutput)!;
        }
    }
}


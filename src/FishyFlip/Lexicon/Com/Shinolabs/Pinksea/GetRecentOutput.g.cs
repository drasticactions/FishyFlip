// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    public partial class GetRecentOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRecentOutput"/> class.
        /// </summary>
        /// <param name="oekaki"></param>
        public GetRecentOutput(List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki> oekaki = default)
        {
            this.Oekaki = oekaki;
            this.Type = "com.shinolabs.pinksea.getRecent#GetRecentOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetRecentOutput"/> class.
        /// </summary>
        public GetRecentOutput()
        {
            this.Type = "com.shinolabs.pinksea.getRecent#GetRecentOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetRecentOutput"/> class.
        /// </summary>
        public GetRecentOutput(CBORObject obj)
        {
            if (obj["oekaki"] is not null) this.Oekaki = obj["oekaki"].Values.Select(n =>new FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the oekaki.
        /// </summary>
        [JsonPropertyName("oekaki")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki> Oekaki { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.getRecent#GetRecentOutput";

        public static GetRecentOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetRecentOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetRecentOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetRecentOutput)!;
        }
    }
}


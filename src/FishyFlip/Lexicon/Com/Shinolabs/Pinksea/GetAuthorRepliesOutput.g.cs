// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    public partial class GetAuthorRepliesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorRepliesOutput"/> class.
        /// </summary>
        /// <param name="oekaki"></param>
        public GetAuthorRepliesOutput(List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki>? oekaki = default)
        {
            this.Oekaki = oekaki;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorRepliesOutput"/> class.
        /// </summary>
        public GetAuthorRepliesOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorRepliesOutput"/> class.
        /// </summary>
        public GetAuthorRepliesOutput(CBORObject obj)
        {
            if (obj["oekaki"] is not null) this.Oekaki = obj["oekaki"].Values.Select(n =>new FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the oekaki.
        /// </summary>
        [JsonPropertyName("oekaki")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki>? Oekaki { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.shinolabs.pinksea.getAuthorReplies#GetAuthorRepliesOutput";

        public const string RecordType = "com.shinolabs.pinksea.getAuthorReplies#GetAuthorRepliesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorRepliesOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorRepliesOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetAuthorRepliesOutput)!;
        }

        public static GetAuthorRepliesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorRepliesOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorRepliesOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetAuthorRepliesOutput)!;
        }
    }
}


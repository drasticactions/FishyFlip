// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    public partial class GetTagFeedOutput : ATObject, ICBOREncodable<GetTagFeedOutput>, IJsonEncodable<GetTagFeedOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagFeedOutput"/> class.
        /// </summary>
        /// <param name="oekaki"></param>
        public GetTagFeedOutput(List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki> oekaki = default)
        {
            this.Oekaki = oekaki;
            this.Type = "com.shinolabs.pinksea.getTagFeed#GetTagFeedOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagFeedOutput"/> class.
        /// </summary>
        public GetTagFeedOutput()
        {
            this.Type = "com.shinolabs.pinksea.getTagFeed#GetTagFeedOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagFeedOutput"/> class.
        /// </summary>
        public GetTagFeedOutput(CBORObject obj)
        {
            if (obj["oekaki"] is not null) this.Oekaki = obj["oekaki"].Values.Select(n =>new FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the oekaki.
        /// </summary>
        [JsonPropertyName("oekaki")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki> Oekaki { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.getTagFeed#GetTagFeedOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetTagFeedOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetTagFeedOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetTagFeedOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetTagFeedOutput);
        }

        public static new GetTagFeedOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetTagFeedOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetTagFeedOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetTagFeedOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetTagFeedOutput FromCBORObject(CBORObject obj)
        {
            return new GetTagFeedOutput(obj);
        }

    }
}


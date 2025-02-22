// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    public partial class GetAuthorFeedOutput : ATObject, ICBOREncodable<GetAuthorFeedOutput>, IJsonEncodable<GetAuthorFeedOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorFeedOutput"/> class.
        /// </summary>
        /// <param name="oekaki"></param>
        public GetAuthorFeedOutput(List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki> oekaki = default)
        {
            this.Oekaki = oekaki;
            this.Type = "com.shinolabs.pinksea.getAuthorFeed#GetAuthorFeedOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorFeedOutput"/> class.
        /// </summary>
        public GetAuthorFeedOutput()
        {
            this.Type = "com.shinolabs.pinksea.getAuthorFeed#GetAuthorFeedOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorFeedOutput"/> class.
        /// </summary>
        public GetAuthorFeedOutput(CBORObject obj)
        {
            if (obj["oekaki"] is not null) this.Oekaki = obj["oekaki"].Values.Select(n =>new FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the oekaki.
        /// </summary>
        [JsonPropertyName("oekaki")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki> Oekaki { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.getAuthorFeed#GetAuthorFeedOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorFeedOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetAuthorFeedOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorFeedOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetAuthorFeedOutput);
        }

        public static new GetAuthorFeedOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorFeedOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetAuthorFeedOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetAuthorFeedOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetAuthorFeedOutput FromCBORObject(CBORObject obj)
        {
            return new GetAuthorFeedOutput(obj);
        }

    }
}


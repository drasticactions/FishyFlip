// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    public partial class GetOekakiOutput : ATObject, ICBOREncodable<GetOekakiOutput>, IJsonEncodable<GetOekakiOutput>, IParsable<GetOekakiOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOekakiOutput"/> class.
        /// </summary>
        /// <param name="parent">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki"/> (com.shinolabs.pinksea.appViewDefs#hydratedOekaki) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.OekakiTombstone"/> (com.shinolabs.pinksea.appViewDefs#oekakiTombstone) <br/>
        /// </param>
        /// <param name="children"></param>
        public GetOekakiOutput(ATObject parent = default, List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki> children = default)
        {
            this.Parent = parent;
            this.Children = children;
            this.Type = "com.shinolabs.pinksea.getOekaki#GetOekakiOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetOekakiOutput"/> class.
        /// </summary>
        public GetOekakiOutput()
        {
            this.Type = "com.shinolabs.pinksea.getOekaki#GetOekakiOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetOekakiOutput"/> class.
        /// </summary>
        public GetOekakiOutput(CBORObject obj)
        {
            if (obj["parent"] is not null) this.Parent = obj["parent"].ToATObject();
            if (obj["children"] is not null) this.Children = obj["children"].Values.Select(n =>new FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the parent.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki"/> (com.shinolabs.pinksea.appViewDefs#hydratedOekaki) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Shinolabs.Pinksea.OekakiTombstone"/> (com.shinolabs.pinksea.appViewDefs#oekakiTombstone) <br/>
        /// </summary>
        [JsonPropertyName("parent")]
        [JsonRequired]
        public ATObject Parent { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        [JsonPropertyName("children")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.HydratedOekaki> Children { get; set; }

        public const string RecordType = "com.shinolabs.pinksea.getOekaki#GetOekakiOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetOekakiOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetOekakiOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetOekakiOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetOekakiOutput);
        }

        public static new GetOekakiOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetOekakiOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Shinolabs.Pinksea.GetOekakiOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetOekakiOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetOekakiOutput FromCBORObject(CBORObject obj)
        {
            return new GetOekakiOutput(obj);
        }

        /// <inheritdoc/>
        public static GetOekakiOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetOekakiOutput>(s, (JsonTypeInfo<GetOekakiOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetOekakiOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetOekakiOutput result)
        {
            result = JsonSerializer.Deserialize<GetOekakiOutput>(s, (JsonTypeInfo<GetOekakiOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetOekakiOutput);
            return result != null;
        }
    }
}


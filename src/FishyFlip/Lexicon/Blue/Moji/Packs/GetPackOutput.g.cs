// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Packs
{
    public partial class GetPackOutput : ATObject, ICBOREncodable<GetPackOutput>, IJsonEncodable<GetPackOutput>, IParsable<GetPackOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPackOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="pack">
        /// <see cref="FishyFlip.Lexicon.Blue.Moji.Packs.PackView"/> (blue.moji.packs.defs#packView)
        /// </param>
        /// <param name="items"></param>
        public GetPackOutput(string? cursor = default, FishyFlip.Lexicon.Blue.Moji.Packs.PackView pack = default, List<FishyFlip.Lexicon.Blue.Moji.Packs.PackItemView> items = default)
        {
            this.Cursor = cursor;
            this.Pack = pack;
            this.Items = items;
            this.Type = "blue.moji.packs.getPack#GetPackOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPackOutput"/> class.
        /// </summary>
        public GetPackOutput()
        {
            this.Type = "blue.moji.packs.getPack#GetPackOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPackOutput"/> class.
        /// </summary>
        public GetPackOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["pack"] is not null) this.Pack = new FishyFlip.Lexicon.Blue.Moji.Packs.PackView(obj["pack"]);
            if (obj["items"] is not null) this.Items = obj["items"].Values.Select(n =>new FishyFlip.Lexicon.Blue.Moji.Packs.PackItemView(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the pack.
        /// <br/> <see cref="FishyFlip.Lexicon.Blue.Moji.Packs.PackView"/> (blue.moji.packs.defs#packView)
        /// </summary>
        [JsonPropertyName("pack")]
        [JsonRequired]
        public FishyFlip.Lexicon.Blue.Moji.Packs.PackView Pack { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Blue.Moji.Packs.PackItemView> Items { get; set; }

        public const string RecordType = "blue.moji.packs.getPack#GetPackOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput>)SourceGenerationContext.Default.BlueMojiPacksGetPackOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput>)SourceGenerationContext.Default.BlueMojiPacksGetPackOutput);
        }

        public static new GetPackOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput>)SourceGenerationContext.Default.BlueMojiPacksGetPackOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetPackOutput FromCBORObject(CBORObject obj)
        {
            return new GetPackOutput(obj);
        }

        /// <inheritdoc/>
        public static GetPackOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetPackOutput>(s, (JsonTypeInfo<GetPackOutput>)SourceGenerationContext.Default.BlueMojiPacksGetPackOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetPackOutput result)
        {
            result = JsonSerializer.Deserialize<GetPackOutput>(s, (JsonTypeInfo<GetPackOutput>)SourceGenerationContext.Default.BlueMojiPacksGetPackOutput);
            return result != null;
        }
    }
}


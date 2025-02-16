// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Packs
{
    public partial class GetPackOutput : ATObject, IBatchItem
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
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
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

        public static GetPackOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput>)SourceGenerationContext.Default.BlueMojiPacksGetPackOutput)!;
        }
    }
}


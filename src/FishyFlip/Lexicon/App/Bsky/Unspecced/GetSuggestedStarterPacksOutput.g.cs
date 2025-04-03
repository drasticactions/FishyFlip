// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class GetSuggestedStarterPacksOutput : ATObject, ICBOREncodable<GetSuggestedStarterPacksOutput>, IJsonEncodable<GetSuggestedStarterPacksOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedStarterPacksOutput"/> class.
        /// </summary>
        /// <param name="starterPacks"></param>
        public GetSuggestedStarterPacksOutput(List<FishyFlip.Lexicon.App.Bsky.Graph.StarterPackView> starterPacks = default)
        {
            this.StarterPacks = starterPacks;
            this.Type = "app.bsky.unspecced.getSuggestedStarterPacks#GetSuggestedStarterPacksOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedStarterPacksOutput"/> class.
        /// </summary>
        public GetSuggestedStarterPacksOutput()
        {
            this.Type = "app.bsky.unspecced.getSuggestedStarterPacks#GetSuggestedStarterPacksOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetSuggestedStarterPacksOutput"/> class.
        /// </summary>
        public GetSuggestedStarterPacksOutput(CBORObject obj)
        {
            if (obj["starterPacks"] is not null) this.StarterPacks = obj["starterPacks"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Graph.StarterPackView(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the starterPacks.
        /// </summary>
        [JsonPropertyName("starterPacks")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Graph.StarterPackView> StarterPacks { get; set; }

        public const string RecordType = "app.bsky.unspecced.getSuggestedStarterPacks#GetSuggestedStarterPacksOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestedStarterPacksOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetSuggestedStarterPacksOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestedStarterPacksOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetSuggestedStarterPacksOutput);
        }

        public static new GetSuggestedStarterPacksOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestedStarterPacksOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestedStarterPacksOutput>)SourceGenerationContext.Default.AppBskyUnspeccedGetSuggestedStarterPacksOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetSuggestedStarterPacksOutput FromCBORObject(CBORObject obj)
        {
            return new GetSuggestedStarterPacksOutput(obj);
        }

    }
}


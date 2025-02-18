// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Packs
{
    public partial class PackViewerState : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PackViewerState"/> class.
        /// </summary>
        /// <param name="savedToCollection"></param>
        public PackViewerState(bool? savedToCollection = default)
        {
            this.SavedToCollection = savedToCollection;
            this.Type = "blue.moji.packs.defs#packViewerState";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PackViewerState"/> class.
        /// </summary>
        public PackViewerState()
        {
            this.Type = "blue.moji.packs.defs#packViewerState";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PackViewerState"/> class.
        /// </summary>
        public PackViewerState(CBORObject obj)
        {
            if (obj["savedToCollection"] is not null) this.SavedToCollection = obj["savedToCollection"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the savedToCollection.
        /// </summary>
        [JsonPropertyName("savedToCollection")]
        public bool? SavedToCollection { get; set; }

        public const string RecordType = "blue.moji.packs.defs#packViewerState";

        public static PackViewerState FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Packs.PackViewerState>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Packs.PackViewerState>)SourceGenerationContext.Default.BlueMojiPacksPackViewerState)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Social.Psky.Richtext
{
    /// <summary>
    /// Specifies the sub-string range a facet feature applies to. Start index is inclusive, end index is exclusive. Indices are zero-indexed, counting bytes of the UTF-8 encoded text. NOTE: some languages, like Javascript, use UTF-16 or Unicode codepoints for string slice indexing; in these languages, convert to byte arrays before working with facets.
    /// </summary>
    public partial class ByteSlice : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteSlice"/> class.
        /// </summary>
        /// <param name="byteStart"></param>
        /// <param name="byteEnd"></param>
        public ByteSlice(long byteStart = default, long byteEnd = default)
        {
            this.ByteStart = byteStart;
            this.ByteEnd = byteEnd;
            this.Type = "social.psky.richtext.facet#byteSlice";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ByteSlice"/> class.
        /// </summary>
        public ByteSlice()
        {
            this.Type = "social.psky.richtext.facet#byteSlice";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ByteSlice"/> class.
        /// </summary>
        public ByteSlice(CBORObject obj)
        {
            if (obj["byteStart"] is not null) this.ByteStart = obj["byteStart"].AsInt64Value();
            if (obj["byteEnd"] is not null) this.ByteEnd = obj["byteEnd"].AsInt64Value();
        }

        /// <summary>
        /// Gets or sets the byteStart.
        /// </summary>
        [JsonPropertyName("byteStart")]
        [JsonRequired]
        public long ByteStart { get; set; }

        /// <summary>
        /// Gets or sets the byteEnd.
        /// </summary>
        [JsonPropertyName("byteEnd")]
        [JsonRequired]
        public long ByteEnd { get; set; }

        public const string RecordType = "social.psky.richtext.facet#byteSlice";

        public static ByteSlice FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Social.Psky.Richtext.ByteSlice>(json, (JsonTypeInfo<FishyFlip.Lexicon.Social.Psky.Richtext.ByteSlice>)SourceGenerationContext.Default.SocialPskyRichtextByteSlice)!;
        }
    }
}


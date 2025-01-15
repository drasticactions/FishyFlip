// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Lexicon
{
    /// <summary>
    /// Representation of Lexicon schemas themselves, when published as atproto records. Note that the schema language is not defined in Lexicon; this meta schema currently only includes a single version field ('lexicon'). See the atproto specifications for description of the other expected top-level fields ('id', 'defs', etc).
    /// </summary>
    public partial class Schema : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="lexicon">Indicates the 'version' of the Lexicon language. Must be '1' for the current atproto/Lexicon schema system.</param>
        public Schema(long? lexicon)
        {
            this.Lexicon = lexicon;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        public Schema()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        public Schema(CBORObject obj)
        {
            if (obj["lexicon"] is not null) this.Lexicon = obj["lexicon"].AsInt64Value();
        }

        /// <summary>
        /// Gets or sets the lexicon.
        /// <br/> Indicates the 'version' of the Lexicon language. Must be '1' for the current atproto/Lexicon schema system.
        /// </summary>
        [JsonPropertyName("lexicon")]
        public long? Lexicon { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.lexicon.schema";

        public const string RecordType = "com.atproto.lexicon.schema";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Lexicon.Schema>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Lexicon.Schema>)SourceGenerationContext.Default.ComAtprotoLexiconSchema)!;
        }

        public static Schema FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Lexicon.Schema>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Lexicon.Schema>)SourceGenerationContext.Default.ComAtprotoLexiconSchema)!;
        }
    }
}


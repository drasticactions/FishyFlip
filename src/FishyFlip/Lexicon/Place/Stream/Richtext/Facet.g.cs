// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Place.Stream.Richtext
{
    /// <summary>
    /// Annotation of a sub-string within rich text.
    /// </summary>
    public partial class Facet : ATObject, ICBOREncodable<Facet>, IJsonEncodable<Facet>, IParsable<Facet>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        /// <param name="index">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Richtext.ByteSlice"/> (app.bsky.richtext.facet#byteSlice)
        /// </param>
        /// <param name="features">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Richtext.Mention"/> (app.bsky.richtext.facet#mention) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Richtext.Link"/> (app.bsky.richtext.facet#link) <br/>
        /// </param>
        public Facet(FishyFlip.Lexicon.App.Bsky.Richtext.ByteSlice index = default, List<ATObject> features = default)
        {
            this.Index = index;
            this.Features = features;
            this.Type = "place.stream.richtext.facet";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet()
        {
            this.Type = "place.stream.richtext.facet";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// </summary>
        public Facet(CBORObject obj)
        {
            if (obj["index"] is not null) this.Index = new FishyFlip.Lexicon.App.Bsky.Richtext.ByteSlice(obj["index"]);
            if (obj["features"] is not null) this.Features = obj["features"].Values.Select(n =>n.ToATObject()).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the index.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Richtext.ByteSlice"/> (app.bsky.richtext.facet#byteSlice)
        /// </summary>
        [JsonPropertyName("index")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Richtext.ByteSlice Index { get; set; }

        /// <summary>
        /// Gets or sets the features.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Richtext.Mention"/> (app.bsky.richtext.facet#mention) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Richtext.Link"/> (app.bsky.richtext.facet#link) <br/>
        /// </summary>
        [JsonPropertyName("features")]
        [JsonRequired]
        public List<ATObject> Features { get; set; }

        public const string RecordType = "place.stream.richtext.facet";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Richtext.Facet>)SourceGenerationContext.Default.PlaceStreamRichtextFacet);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Richtext.Facet>)SourceGenerationContext.Default.PlaceStreamRichtextFacet);
        }

        public static new Facet FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Place.Stream.Richtext.Facet>(json, (JsonTypeInfo<FishyFlip.Lexicon.Place.Stream.Richtext.Facet>)SourceGenerationContext.Default.PlaceStreamRichtextFacet)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Facet FromCBORObject(CBORObject obj)
        {
            return new Facet(obj);
        }

        /// <inheritdoc/>
        public static Facet Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Facet>(s, (JsonTypeInfo<Facet>)SourceGenerationContext.Default.PlaceStreamRichtextFacet)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Facet result)
        {
            result = JsonSerializer.Deserialize<Facet>(s, (JsonTypeInfo<Facet>)SourceGenerationContext.Default.PlaceStreamRichtextFacet);
            return result != null;
        }
    }
}


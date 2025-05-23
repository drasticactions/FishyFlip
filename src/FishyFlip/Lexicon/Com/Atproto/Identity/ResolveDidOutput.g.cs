// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Identity
{
    public partial class ResolveDidOutput : ATObject, ICBOREncodable<ResolveDidOutput>, IJsonEncodable<ResolveDidOutput>, IParsable<ResolveDidOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveDidOutput"/> class.
        /// </summary>
        /// <param name="didDoc">The complete DID document for the identity.</param>
        public ResolveDidOutput(FishyFlip.Models.DidDoc didDoc = default)
        {
            this.DidDoc = didDoc;
            this.Type = "com.atproto.identity.resolveDid#ResolveDidOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveDidOutput"/> class.
        /// </summary>
        public ResolveDidOutput()
        {
            this.Type = "com.atproto.identity.resolveDid#ResolveDidOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveDidOutput"/> class.
        /// </summary>
        public ResolveDidOutput(CBORObject obj)
        {
            // Ignore DidDoc
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the didDoc.
        /// <br/> The complete DID document for the identity.
        /// </summary>
        [JsonPropertyName("didDoc")]
        [JsonRequired]
        public FishyFlip.Models.DidDoc DidDoc { get; set; }

        public const string RecordType = "com.atproto.identity.resolveDid#ResolveDidOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.ResolveDidOutput>)SourceGenerationContext.Default.ComAtprotoIdentityResolveDidOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.ResolveDidOutput>)SourceGenerationContext.Default.ComAtprotoIdentityResolveDidOutput);
        }

        public static new ResolveDidOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Identity.ResolveDidOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Identity.ResolveDidOutput>)SourceGenerationContext.Default.ComAtprotoIdentityResolveDidOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new ResolveDidOutput FromCBORObject(CBORObject obj)
        {
            return new ResolveDidOutput(obj);
        }

        /// <inheritdoc/>
        public static ResolveDidOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<ResolveDidOutput>(s, (JsonTypeInfo<ResolveDidOutput>)SourceGenerationContext.Default.ComAtprotoIdentityResolveDidOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out ResolveDidOutput result)
        {
            result = JsonSerializer.Deserialize<ResolveDidOutput>(s, (JsonTypeInfo<ResolveDidOutput>)SourceGenerationContext.Default.ComAtprotoIdentityResolveDidOutput);
            return result != null;
        }
    }
}


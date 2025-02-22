// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class ReserveSigningKeyInput : ATObject, ICBOREncodable<ReserveSigningKeyInput>, IJsonEncodable<ReserveSigningKeyInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReserveSigningKeyInput"/> class.
        /// </summary>
        /// <param name="did">The DID to reserve a key for.</param>
        public ReserveSigningKeyInput(FishyFlip.Models.ATDid? did = default)
        {
            this.Did = did;
            this.Type = "com.atproto.server.reserveSigningKey#ReserveSigningKeyInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReserveSigningKeyInput"/> class.
        /// </summary>
        public ReserveSigningKeyInput()
        {
            this.Type = "com.atproto.server.reserveSigningKey#ReserveSigningKeyInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReserveSigningKeyInput"/> class.
        /// </summary>
        public ReserveSigningKeyInput(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the did.
        /// <br/> The DID to reserve a key for.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        public const string RecordType = "com.atproto.server.reserveSigningKey#ReserveSigningKeyInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ReserveSigningKeyInput>)SourceGenerationContext.Default.ComAtprotoServerReserveSigningKeyInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ReserveSigningKeyInput>)SourceGenerationContext.Default.ComAtprotoServerReserveSigningKeyInput);
        }

        public static new ReserveSigningKeyInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.ReserveSigningKeyInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ReserveSigningKeyInput>)SourceGenerationContext.Default.ComAtprotoServerReserveSigningKeyInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ReserveSigningKeyInput FromCBORObject(CBORObject obj)
        {
            return new ReserveSigningKeyInput(obj);
        }

    }
}


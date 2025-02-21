// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class RevokeAppPasswordInput : ATObject, ICBOREncodable<RevokeAppPasswordInput>, IJsonEncodable<RevokeAppPasswordInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeAppPasswordInput"/> class.
        /// </summary>
        /// <param name="name"></param>
        public RevokeAppPasswordInput(string name = default)
        {
            this.Name = name;
            this.Type = "com.atproto.server.revokeAppPassword#RevokeAppPasswordInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeAppPasswordInput"/> class.
        /// </summary>
        public RevokeAppPasswordInput()
        {
            this.Type = "com.atproto.server.revokeAppPassword#RevokeAppPasswordInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeAppPasswordInput"/> class.
        /// </summary>
        public RevokeAppPasswordInput(CBORObject obj)
        {
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string Name { get; set; }

        public const string RecordType = "com.atproto.server.revokeAppPassword#RevokeAppPasswordInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RevokeAppPasswordInput>)SourceGenerationContext.Default.ComAtprotoServerRevokeAppPasswordInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RevokeAppPasswordInput>)SourceGenerationContext.Default.ComAtprotoServerRevokeAppPasswordInput);
        }

        public static new RevokeAppPasswordInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.RevokeAppPasswordInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RevokeAppPasswordInput>)SourceGenerationContext.Default.ComAtprotoServerRevokeAppPasswordInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new RevokeAppPasswordInput FromCBORObject(CBORObject obj)
        {
            return new RevokeAppPasswordInput(obj);
        }

    }
}


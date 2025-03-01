// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class GetServiceAuthOutput : ATObject, ICBOREncodable<GetServiceAuthOutput>, IJsonEncodable<GetServiceAuthOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceAuthOutput"/> class.
        /// </summary>
        /// <param name="token"></param>
        public GetServiceAuthOutput(string token = default)
        {
            this.Token = token;
            this.Type = "com.atproto.server.getServiceAuth#GetServiceAuthOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceAuthOutput"/> class.
        /// </summary>
        public GetServiceAuthOutput()
        {
            this.Type = "com.atproto.server.getServiceAuth#GetServiceAuthOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceAuthOutput"/> class.
        /// </summary>
        public GetServiceAuthOutput(CBORObject obj)
        {
            if (obj["token"] is not null) this.Token = obj["token"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [JsonPropertyName("token")]
        [JsonRequired]
        public string Token { get; set; }

        public const string RecordType = "com.atproto.server.getServiceAuth#GetServiceAuthOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.GetServiceAuthOutput>)SourceGenerationContext.Default.ComAtprotoServerGetServiceAuthOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.GetServiceAuthOutput>)SourceGenerationContext.Default.ComAtprotoServerGetServiceAuthOutput);
        }

        public static new GetServiceAuthOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.GetServiceAuthOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.GetServiceAuthOutput>)SourceGenerationContext.Default.ComAtprotoServerGetServiceAuthOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetServiceAuthOutput FromCBORObject(CBORObject obj)
        {
            return new GetServiceAuthOutput(obj);
        }

    }
}


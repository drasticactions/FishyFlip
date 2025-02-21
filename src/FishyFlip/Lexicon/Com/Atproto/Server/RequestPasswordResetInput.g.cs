// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class RequestPasswordResetInput : ATObject, ICBOREncodable<RequestPasswordResetInput>, IJsonEncodable<RequestPasswordResetInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPasswordResetInput"/> class.
        /// </summary>
        /// <param name="email"></param>
        public RequestPasswordResetInput(string email = default)
        {
            this.Email = email;
            this.Type = "com.atproto.server.requestPasswordReset#RequestPasswordResetInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPasswordResetInput"/> class.
        /// </summary>
        public RequestPasswordResetInput()
        {
            this.Type = "com.atproto.server.requestPasswordReset#RequestPasswordResetInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPasswordResetInput"/> class.
        /// </summary>
        public RequestPasswordResetInput(CBORObject obj)
        {
            if (obj["email"] is not null) this.Email = obj["email"].AsString();
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonPropertyName("email")]
        [JsonRequired]
        public string Email { get; set; }

        public const string RecordType = "com.atproto.server.requestPasswordReset#RequestPasswordResetInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RequestPasswordResetInput>)SourceGenerationContext.Default.ComAtprotoServerRequestPasswordResetInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RequestPasswordResetInput>)SourceGenerationContext.Default.ComAtprotoServerRequestPasswordResetInput);
        }

        public static new RequestPasswordResetInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.RequestPasswordResetInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RequestPasswordResetInput>)SourceGenerationContext.Default.ComAtprotoServerRequestPasswordResetInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new RequestPasswordResetInput FromCBORObject(CBORObject obj)
        {
            return new RequestPasswordResetInput(obj);
        }

    }
}


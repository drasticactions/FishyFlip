// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class ResetPasswordInput : ATObject, ICBOREncodable<ResetPasswordInput>, IJsonEncodable<ResetPasswordInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordInput"/> class.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="password"></param>
        public ResetPasswordInput(string token = default, string password = default)
        {
            this.Token = token;
            this.Password = password;
            this.Type = "com.atproto.server.resetPassword#ResetPasswordInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordInput"/> class.
        /// </summary>
        public ResetPasswordInput()
        {
            this.Type = "com.atproto.server.resetPassword#ResetPasswordInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordInput"/> class.
        /// </summary>
        public ResetPasswordInput(CBORObject obj)
        {
            if (obj["token"] is not null) this.Token = obj["token"].AsString();
            if (obj["password"] is not null) this.Password = obj["password"].AsString();
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [JsonPropertyName("token")]
        [JsonRequired]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonPropertyName("password")]
        [JsonRequired]
        public string Password { get; set; }

        public const string RecordType = "com.atproto.server.resetPassword#ResetPasswordInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ResetPasswordInput>)SourceGenerationContext.Default.ComAtprotoServerResetPasswordInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ResetPasswordInput>)SourceGenerationContext.Default.ComAtprotoServerResetPasswordInput);
        }

        public static new ResetPasswordInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.ResetPasswordInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.ResetPasswordInput>)SourceGenerationContext.Default.ComAtprotoServerResetPasswordInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ResetPasswordInput FromCBORObject(CBORObject obj)
        {
            return new ResetPasswordInput(obj);
        }

    }
}


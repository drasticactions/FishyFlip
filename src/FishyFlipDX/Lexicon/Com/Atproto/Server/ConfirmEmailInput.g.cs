// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class ConfirmEmailInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmEmailInput"/> class.
        /// </summary>
        public ConfirmEmailInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmEmailInput"/> class.
        /// </summary>
        public ConfirmEmailInput(CBORObject obj)
        {
            if (obj["email"] is not null) this.Email = obj["email"].AsString();
            if (obj["token"] is not null) this.Token = obj["token"].AsString();
        }

        [JsonPropertyName("email")]
        [JsonRequired]
        public string? Email { get; set; }

        [JsonPropertyName("token")]
        [JsonRequired]
        public string? Token { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.server.confirmEmail#ConfirmEmailInput";

        public const string RecordType = "com.atproto.server.confirmEmail#ConfirmEmailInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Server.ConfirmEmailInput>(this, (JsonTypeInfo<Com.Atproto.Server.ConfirmEmailInput>)SourceGenerationContext.Default.ComAtprotoServerConfirmEmailInput)!;
        }

        public static ConfirmEmailInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Server.ConfirmEmailInput>(json, (JsonTypeInfo<Com.Atproto.Server.ConfirmEmailInput>)SourceGenerationContext.Default.ComAtprotoServerConfirmEmailInput)!;
        }
    }
}

// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Notification
{
    public partial class RegisterPushInput : ATObject, ICBOREncodable<RegisterPushInput>, IJsonEncodable<RegisterPushInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterPushInput"/> class.
        /// </summary>
        /// <param name="serviceDid"></param>
        /// <param name="token"></param>
        /// <param name="platform">
        /// <br/> Known Values: <br/>
        /// ios <br/>
        /// android <br/>
        /// web <br/>
        /// </param>
        /// <param name="appId"></param>
        public RegisterPushInput(FishyFlip.Models.ATDid serviceDid = default, string token = default, string platform = default, string appId = default)
        {
            this.ServiceDid = serviceDid;
            this.Token = token;
            this.Platform = platform;
            this.AppId = appId;
            this.Type = "app.bsky.notification.registerPush#RegisterPushInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterPushInput"/> class.
        /// </summary>
        public RegisterPushInput()
        {
            this.Type = "app.bsky.notification.registerPush#RegisterPushInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterPushInput"/> class.
        /// </summary>
        public RegisterPushInput(CBORObject obj)
        {
            if (obj["serviceDid"] is not null) this.ServiceDid = obj["serviceDid"].ToATDid();
            if (obj["token"] is not null) this.Token = obj["token"].AsString();
            if (obj["platform"] is not null) this.Platform = obj["platform"].AsString();
            if (obj["appId"] is not null) this.AppId = obj["appId"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the serviceDid.
        /// </summary>
        [JsonPropertyName("serviceDid")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid ServiceDid { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [JsonPropertyName("token")]
        [JsonRequired]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// <br/> Known Values: <br/>
        /// ios <br/>
        /// android <br/>
        /// web <br/>
        /// </summary>
        [JsonPropertyName("platform")]
        [JsonRequired]
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the appId.
        /// </summary>
        [JsonPropertyName("appId")]
        [JsonRequired]
        public string AppId { get; set; }

        public const string RecordType = "app.bsky.notification.registerPush#RegisterPushInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Notification.RegisterPushInput>)SourceGenerationContext.Default.AppBskyNotificationRegisterPushInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Notification.RegisterPushInput>)SourceGenerationContext.Default.AppBskyNotificationRegisterPushInput);
        }

        public static new RegisterPushInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Notification.RegisterPushInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Notification.RegisterPushInput>)SourceGenerationContext.Default.AppBskyNotificationRegisterPushInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new RegisterPushInput FromCBORObject(CBORObject obj)
        {
            return new RegisterPushInput(obj);
        }

    }
}


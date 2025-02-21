// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    public partial class Info : ATObject, ICBOREncodable<Info>, IJsonEncodable<Info>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Info"/> class.
        /// </summary>
        /// <param name="name">
        /// <br/> Known Values: <br/>
        /// OutdatedCursor <br/>
        /// </param>
        /// <param name="message"></param>
        public Info(string name = default, string? message = default)
        {
            this.Name = name;
            this.Message = message;
            this.Type = "com.atproto.sync.subscribeRepos#info";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Info"/> class.
        /// </summary>
        public Info()
        {
            this.Type = "com.atproto.sync.subscribeRepos#info";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Info"/> class.
        /// </summary>
        public Info(CBORObject obj)
        {
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["message"] is not null) this.Message = obj["message"].AsString();
        }

        /// <summary>
        /// Gets or sets the name.
        /// <br/> Known Values: <br/>
        /// OutdatedCursor <br/>
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        public const string RecordType = "com.atproto.sync.subscribeRepos#info";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Info>)SourceGenerationContext.Default.ComAtprotoSyncInfo);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Info>)SourceGenerationContext.Default.ComAtprotoSyncInfo);
        }

        public static new Info FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Sync.Info>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Info>)SourceGenerationContext.Default.ComAtprotoSyncInfo)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Info FromCBORObject(CBORObject obj)
        {
            return new Info(obj);
        }

    }
}


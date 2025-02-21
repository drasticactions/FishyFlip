// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    /// <summary>
    /// Represents a change to an account's identity. Could be an updated handle, signing key, or pds hosting endpoint. Serves as a prod to all downstream services to refresh their identity cache.
    /// </summary>
    public partial class Identity : ATObject, ICBOREncodable<Identity>, IJsonEncodable<Identity>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="did"></param>
        /// <param name="time"></param>
        /// <param name="handle">The current handle for the account, or 'handle.invalid' if validation fails. This field is optional, might have been validated or passed-through from an upstream source. Semantics and behaviors for PDS vs Relay may evolve in the future; see atproto specs for more details.</param>
        public Identity(long seq = default, FishyFlip.Models.ATDid did = default, DateTime? time = default, FishyFlip.Models.ATHandle? handle = default)
        {
            this.Seq = seq;
            this.Did = did;
            this.Time = time;
            this.Handle = handle;
            this.Type = "com.atproto.sync.subscribeRepos#identity";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        public Identity()
        {
            this.Type = "com.atproto.sync.subscribeRepos#identity";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        public Identity(CBORObject obj)
        {
            if (obj["seq"] is not null) this.Seq = obj["seq"].AsInt64Value();
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["time"] is not null) this.Time = obj["time"].ToDateTime();
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
        }

        /// <summary>
        /// Gets or sets the seq.
        /// </summary>
        [JsonPropertyName("seq")]
        [JsonRequired]
        public long Seq { get; set; }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        [JsonPropertyName("time")]
        [JsonRequired]
        public DateTime? Time { get; set; }

        /// <summary>
        /// Gets or sets the handle.
        /// <br/> The current handle for the account, or 'handle.invalid' if validation fails. This field is optional, might have been validated or passed-through from an upstream source. Semantics and behaviors for PDS vs Relay may evolve in the future; see atproto specs for more details.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle? Handle { get; set; }

        public const string RecordType = "com.atproto.sync.subscribeRepos#identity";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Identity>)SourceGenerationContext.Default.ComAtprotoSyncIdentity);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Identity>)SourceGenerationContext.Default.ComAtprotoSyncIdentity);
        }

        public static new Identity FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Sync.Identity>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.Identity>)SourceGenerationContext.Default.ComAtprotoSyncIdentity)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Identity FromCBORObject(CBORObject obj)
        {
            return new Identity(obj);
        }

    }
}


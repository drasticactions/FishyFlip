// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    public partial class NotifyOfUpdateInput : ATObject, ICBOREncodable<NotifyOfUpdateInput>, IJsonEncodable<NotifyOfUpdateInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfUpdateInput"/> class.
        /// </summary>
        /// <param name="hostname">Hostname of the current service (usually a PDS) that is notifying of update.</param>
        public NotifyOfUpdateInput(string hostname = default)
        {
            this.Hostname = hostname;
            this.Type = "com.atproto.sync.notifyOfUpdate#NotifyOfUpdateInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfUpdateInput"/> class.
        /// </summary>
        public NotifyOfUpdateInput()
        {
            this.Type = "com.atproto.sync.notifyOfUpdate#NotifyOfUpdateInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfUpdateInput"/> class.
        /// </summary>
        public NotifyOfUpdateInput(CBORObject obj)
        {
            if (obj["hostname"] is not null) this.Hostname = obj["hostname"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the hostname.
        /// <br/> Hostname of the current service (usually a PDS) that is notifying of update.
        /// </summary>
        [JsonPropertyName("hostname")]
        [JsonRequired]
        public string Hostname { get; set; }

        public const string RecordType = "com.atproto.sync.notifyOfUpdate#NotifyOfUpdateInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.NotifyOfUpdateInput>)SourceGenerationContext.Default.ComAtprotoSyncNotifyOfUpdateInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.NotifyOfUpdateInput>)SourceGenerationContext.Default.ComAtprotoSyncNotifyOfUpdateInput);
        }

        public static new NotifyOfUpdateInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Sync.NotifyOfUpdateInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.NotifyOfUpdateInput>)SourceGenerationContext.Default.ComAtprotoSyncNotifyOfUpdateInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new NotifyOfUpdateInput FromCBORObject(CBORObject obj)
        {
            return new NotifyOfUpdateInput(obj);
        }

    }
}


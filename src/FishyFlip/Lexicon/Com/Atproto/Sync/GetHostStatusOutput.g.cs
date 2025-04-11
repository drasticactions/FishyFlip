// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    public partial class GetHostStatusOutput : ATObject, ICBOREncodable<GetHostStatusOutput>, IJsonEncodable<GetHostStatusOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetHostStatusOutput"/> class.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="seq">Recent repo stream event sequence number. May be delayed from actual stream processing (eg, persisted cursor not in-memory cursor).</param>
        /// <param name="accountCount">Number of accounts on the server which are associated with the upstream host. Note that the upstream may actually have more accounts.</param>
        /// <param name="status">
        /// <br/> Known Values: <br/>
        /// active <br/>
        /// idle <br/>
        /// offline <br/>
        /// throttled <br/>
        /// banned <br/>
        /// </param>
        public GetHostStatusOutput(string hostname = default, long? seq = default, long? accountCount = default, string? status = default)
        {
            this.Hostname = hostname;
            this.Seq = seq;
            this.AccountCount = accountCount;
            this.Status = status;
            this.Type = "com.atproto.sync.getHostStatus#GetHostStatusOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetHostStatusOutput"/> class.
        /// </summary>
        public GetHostStatusOutput()
        {
            this.Type = "com.atproto.sync.getHostStatus#GetHostStatusOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetHostStatusOutput"/> class.
        /// </summary>
        public GetHostStatusOutput(CBORObject obj)
        {
            if (obj["hostname"] is not null) this.Hostname = obj["hostname"].AsString();
            if (obj["seq"] is not null) this.Seq = obj["seq"].AsInt64Value();
            if (obj["accountCount"] is not null) this.AccountCount = obj["accountCount"].AsInt64Value();
            if (obj["status"] is not null) this.Status = obj["status"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the hostname.
        /// </summary>
        [JsonPropertyName("hostname")]
        [JsonRequired]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the seq.
        /// <br/> Recent repo stream event sequence number. May be delayed from actual stream processing (eg, persisted cursor not in-memory cursor).
        /// </summary>
        [JsonPropertyName("seq")]
        public long? Seq { get; set; }

        /// <summary>
        /// Gets or sets the accountCount.
        /// <br/> Number of accounts on the server which are associated with the upstream host. Note that the upstream may actually have more accounts.
        /// </summary>
        [JsonPropertyName("accountCount")]
        public long? AccountCount { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// <br/> Known Values: <br/>
        /// active <br/>
        /// idle <br/>
        /// offline <br/>
        /// throttled <br/>
        /// banned <br/>
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        public const string RecordType = "com.atproto.sync.getHostStatus#GetHostStatusOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.GetHostStatusOutput>)SourceGenerationContext.Default.ComAtprotoSyncGetHostStatusOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.GetHostStatusOutput>)SourceGenerationContext.Default.ComAtprotoSyncGetHostStatusOutput);
        }

        public static new GetHostStatusOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Sync.GetHostStatusOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.GetHostStatusOutput>)SourceGenerationContext.Default.ComAtprotoSyncGetHostStatusOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetHostStatusOutput FromCBORObject(CBORObject obj)
        {
            return new GetHostStatusOutput(obj);
        }

    }
}


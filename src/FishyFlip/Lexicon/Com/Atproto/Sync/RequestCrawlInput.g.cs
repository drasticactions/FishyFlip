// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    public partial class RequestCrawlInput : ATObject, ICBOREncodable<RequestCrawlInput>, IJsonEncodable<RequestCrawlInput>, IParsable<RequestCrawlInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestCrawlInput"/> class.
        /// </summary>
        /// <param name="hostname">Hostname of the current service (eg, PDS) that is requesting to be crawled.</param>
        public RequestCrawlInput(string hostname = default)
        {
            this.Hostname = hostname;
            this.Type = "com.atproto.sync.requestCrawl#RequestCrawlInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestCrawlInput"/> class.
        /// </summary>
        public RequestCrawlInput()
        {
            this.Type = "com.atproto.sync.requestCrawl#RequestCrawlInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestCrawlInput"/> class.
        /// </summary>
        public RequestCrawlInput(CBORObject obj)
        {
            if (obj["hostname"] is not null) this.Hostname = obj["hostname"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the hostname.
        /// <br/> Hostname of the current service (eg, PDS) that is requesting to be crawled.
        /// </summary>
        [JsonPropertyName("hostname")]
        [JsonRequired]
        public string Hostname { get; set; }

        public const string RecordType = "com.atproto.sync.requestCrawl#RequestCrawlInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.RequestCrawlInput>)SourceGenerationContext.Default.ComAtprotoSyncRequestCrawlInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.RequestCrawlInput>)SourceGenerationContext.Default.ComAtprotoSyncRequestCrawlInput);
        }

        public static new RequestCrawlInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Sync.RequestCrawlInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.RequestCrawlInput>)SourceGenerationContext.Default.ComAtprotoSyncRequestCrawlInput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new RequestCrawlInput FromCBORObject(CBORObject obj)
        {
            return new RequestCrawlInput(obj);
        }

        /// <inheritdoc/>
        public static RequestCrawlInput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<RequestCrawlInput>(s, (JsonTypeInfo<RequestCrawlInput>)SourceGenerationContext.Default.ComAtprotoSyncRequestCrawlInput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out RequestCrawlInput result)
        {
            result = JsonSerializer.Deserialize<RequestCrawlInput>(s, (JsonTypeInfo<RequestCrawlInput>)SourceGenerationContext.Default.ComAtprotoSyncRequestCrawlInput);
            return result != null;
        }
    }
}


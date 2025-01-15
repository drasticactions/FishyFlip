// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class GetEntryMetadataByNameOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntryMetadataByNameOutput"/> class.
        /// </summary>
        /// <param name="entryUri"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="cid"></param>
        public GetEntryMetadataByNameOutput(FishyFlip.Models.ATUri entryUri = default, DateTime? lastUpdate = default, string? cid = default)
        {
            this.EntryUri = entryUri;
            this.LastUpdate = lastUpdate;
            this.Cid = cid;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntryMetadataByNameOutput"/> class.
        /// </summary>
        public GetEntryMetadataByNameOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntryMetadataByNameOutput"/> class.
        /// </summary>
        public GetEntryMetadataByNameOutput(CBORObject obj)
        {
            if (obj["entryUri"] is not null) this.EntryUri = obj["entryUri"].ToATUri();
            if (obj["lastUpdate"] is not null) this.LastUpdate = obj["lastUpdate"].ToDateTime();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
        }

        /// <summary>
        /// Gets or sets the entryUri.
        /// </summary>
        [JsonPropertyName("entryUri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri EntryUri { get; set; }

        /// <summary>
        /// Gets or sets the lastUpdate.
        /// </summary>
        [JsonPropertyName("lastUpdate")]
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        public string? Cid { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.whtwnd.blog.getEntryMetadataByName#GetEntryMetadataByNameOutput";

        public const string RecordType = "com.whtwnd.blog.getEntryMetadataByName#GetEntryMetadataByNameOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetEntryMetadataByNameOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetEntryMetadataByNameOutput>)SourceGenerationContext.Default.ComWhtwndBlogGetEntryMetadataByNameOutput)!;
        }

        public static GetEntryMetadataByNameOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetEntryMetadataByNameOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetEntryMetadataByNameOutput>)SourceGenerationContext.Default.ComWhtwndBlogGetEntryMetadataByNameOutput)!;
        }
    }
}


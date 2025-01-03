// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class NotifyOfNewEntryInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfNewEntryInput"/> class.
        /// </summary>
        /// <param name="entryUri"></param>
        public NotifyOfNewEntryInput(FishyFlip.Models.ATUri? entryUri = default)
        {
            this.EntryUri = entryUri;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfNewEntryInput"/> class.
        /// </summary>
        public NotifyOfNewEntryInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfNewEntryInput"/> class.
        /// </summary>
        public NotifyOfNewEntryInput(CBORObject obj)
        {
            if (obj["entryUri"] is not null) this.EntryUri = obj["entryUri"].ToATUri();
        }

        /// <summary>
        /// Gets or sets the entryUri.
        /// </summary>
        [JsonPropertyName("entryUri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? EntryUri { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.whtwnd.blog.notifyOfNewEntry#NotifyOfNewEntryInput";

        public const string RecordType = "com.whtwnd.blog.notifyOfNewEntry#NotifyOfNewEntryInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Whtwnd.Blog.NotifyOfNewEntryInput>(this, (JsonTypeInfo<Com.Whtwnd.Blog.NotifyOfNewEntryInput>)SourceGenerationContext.Default.ComWhtwndBlogNotifyOfNewEntryInput)!;
        }

        public static NotifyOfNewEntryInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Whtwnd.Blog.NotifyOfNewEntryInput>(json, (JsonTypeInfo<Com.Whtwnd.Blog.NotifyOfNewEntryInput>)SourceGenerationContext.Default.ComWhtwndBlogNotifyOfNewEntryInput)!;
        }
    }
}


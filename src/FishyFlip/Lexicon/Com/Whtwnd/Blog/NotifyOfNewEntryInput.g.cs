// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class NotifyOfNewEntryInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfNewEntryInput"/> class.
        /// </summary>
        /// <param name="entryUri"></param>
        public NotifyOfNewEntryInput(FishyFlip.Models.ATUri entryUri = default)
        {
            this.EntryUri = entryUri;
            this.Type = "com.whtwnd.blog.notifyOfNewEntry#NotifyOfNewEntryInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfNewEntryInput"/> class.
        /// </summary>
        public NotifyOfNewEntryInput()
        {
            this.Type = "com.whtwnd.blog.notifyOfNewEntry#NotifyOfNewEntryInput";
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
        public FishyFlip.Models.ATUri EntryUri { get; set; }

        public const string RecordType = "com.whtwnd.blog.notifyOfNewEntry#NotifyOfNewEntryInput";

        public static NotifyOfNewEntryInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.NotifyOfNewEntryInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.NotifyOfNewEntryInput>)SourceGenerationContext.Default.ComWhtwndBlogNotifyOfNewEntryInput)!;
        }
    }
}


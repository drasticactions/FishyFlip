// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class NotifyOfNewEntryOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfNewEntryOutput"/> class.
        /// </summary>
        public NotifyOfNewEntryOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyOfNewEntryOutput"/> class.
        /// </summary>
        public NotifyOfNewEntryOutput(CBORObject obj)
        {
        }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.whtwnd.blog.notifyOfNewEntry#NotifyOfNewEntryOutput";

        public const string RecordType = "com.whtwnd.blog.notifyOfNewEntry#NotifyOfNewEntryOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.NotifyOfNewEntryOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.NotifyOfNewEntryOutput>)SourceGenerationContext.Default.ComWhtwndBlogNotifyOfNewEntryOutput)!;
        }

        public static NotifyOfNewEntryOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.NotifyOfNewEntryOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.NotifyOfNewEntryOutput>)SourceGenerationContext.Default.ComWhtwndBlogNotifyOfNewEntryOutput)!;
        }
    }
}


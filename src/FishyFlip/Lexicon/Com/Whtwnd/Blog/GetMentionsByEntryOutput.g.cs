// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Whtwnd.Blog
{
    public partial class GetMentionsByEntryOutput : ATObject, ICBOREncodable<GetMentionsByEntryOutput>, IJsonEncodable<GetMentionsByEntryOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMentionsByEntryOutput"/> class.
        /// </summary>
        /// <param name="mentions"></param>
        public GetMentionsByEntryOutput(List<FishyFlip.Models.ATUri> mentions = default)
        {
            this.Mentions = mentions;
            this.Type = "com.whtwnd.blog.getMentionsByEntry#GetMentionsByEntryOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetMentionsByEntryOutput"/> class.
        /// </summary>
        public GetMentionsByEntryOutput()
        {
            this.Type = "com.whtwnd.blog.getMentionsByEntry#GetMentionsByEntryOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetMentionsByEntryOutput"/> class.
        /// </summary>
        public GetMentionsByEntryOutput(CBORObject obj)
        {
            if (obj["mentions"] is not null) this.Mentions = obj["mentions"].Values.Select(n =>n.ToATUri()!).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the mentions.
        /// </summary>
        [JsonPropertyName("mentions")]
        [JsonRequired]
        public List<FishyFlip.Models.ATUri> Mentions { get; set; }

        public const string RecordType = "com.whtwnd.blog.getMentionsByEntry#GetMentionsByEntryOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetMentionsByEntryOutput>)SourceGenerationContext.Default.ComWhtwndBlogGetMentionsByEntryOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetMentionsByEntryOutput>)SourceGenerationContext.Default.ComWhtwndBlogGetMentionsByEntryOutput);
        }

        public static new GetMentionsByEntryOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetMentionsByEntryOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Whtwnd.Blog.GetMentionsByEntryOutput>)SourceGenerationContext.Default.ComWhtwndBlogGetMentionsByEntryOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetMentionsByEntryOutput FromCBORObject(CBORObject obj)
        {
            return new GetMentionsByEntryOutput(obj);
        }

    }
}


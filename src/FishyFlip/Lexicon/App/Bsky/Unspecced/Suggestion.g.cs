// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{
    public partial class Suggestion : ATObject, ICBOREncodable<Suggestion>, IJsonEncodable<Suggestion>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Suggestion"/> class.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="subjectType">
        /// <br/> Known Values: <br/>
        /// actor <br/>
        /// feed <br/>
        /// </param>
        /// <param name="subject"></param>
        public Suggestion(string tag = default, string subjectType = default, string subject = default)
        {
            this.Tag = tag;
            this.SubjectType = subjectType;
            this.Subject = subject;
            this.Type = "app.bsky.unspecced.getTaggedSuggestions#suggestion";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Suggestion"/> class.
        /// </summary>
        public Suggestion()
        {
            this.Type = "app.bsky.unspecced.getTaggedSuggestions#suggestion";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Suggestion"/> class.
        /// </summary>
        public Suggestion(CBORObject obj)
        {
            if (obj["tag"] is not null) this.Tag = obj["tag"].AsString();
            if (obj["subjectType"] is not null) this.SubjectType = obj["subjectType"].AsString();
            if (obj["subject"] is not null) this.Subject = obj["subject"].AsString();
        }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        [JsonPropertyName("tag")]
        [JsonRequired]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the subjectType.
        /// <br/> Known Values: <br/>
        /// actor <br/>
        /// feed <br/>
        /// </summary>
        [JsonPropertyName("subjectType")]
        [JsonRequired]
        public string SubjectType { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public string Subject { get; set; }

        public const string RecordType = "app.bsky.unspecced.getTaggedSuggestions#suggestion";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.Suggestion>)SourceGenerationContext.Default.AppBskyUnspeccedSuggestion);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.Suggestion>)SourceGenerationContext.Default.AppBskyUnspeccedSuggestion);
        }

        public static new Suggestion FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Unspecced.Suggestion>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Unspecced.Suggestion>)SourceGenerationContext.Default.AppBskyUnspeccedSuggestion)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Suggestion FromCBORObject(CBORObject obj)
        {
            return new Suggestion(obj);
        }

    }
}


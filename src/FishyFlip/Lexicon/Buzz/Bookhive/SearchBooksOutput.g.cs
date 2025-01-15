// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Buzz.Bookhive
{
    public partial class SearchBooksOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBooksOutput"/> class.
        /// </summary>
        /// <param name="offset">The next offset to use for pagination (result of limit + offset)</param>
        /// <param name="books"></param>
        public SearchBooksOutput(long? offset = default, List<FishyFlip.Lexicon.Buzz.Bookhive.HiveBook>? books = default)
        {
            this.Offset = offset;
            this.Books = books;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBooksOutput"/> class.
        /// </summary>
        public SearchBooksOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBooksOutput"/> class.
        /// </summary>
        public SearchBooksOutput(CBORObject obj)
        {
            if (obj["offset"] is not null) this.Offset = obj["offset"].AsInt64Value();
            if (obj["books"] is not null) this.Books = obj["books"].Values.Select(n =>new FishyFlip.Lexicon.Buzz.Bookhive.HiveBook(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the offset.
        /// <br/> The next offset to use for pagination (result of limit + offset)
        /// </summary>
        [JsonPropertyName("offset")]
        public long? Offset { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        [JsonPropertyName("books")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Buzz.Bookhive.HiveBook>? Books { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "buzz.bookhive.searchBooks#SearchBooksOutput";

        public const string RecordType = "buzz.bookhive.searchBooks#SearchBooksOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Buzz.Bookhive.SearchBooksOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.SearchBooksOutput>)SourceGenerationContext.Default.BuzzBookhiveSearchBooksOutput)!;
        }

        public static SearchBooksOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Buzz.Bookhive.SearchBooksOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Buzz.Bookhive.SearchBooksOutput>)SourceGenerationContext.Default.BuzzBookhiveSearchBooksOutput)!;
        }
    }
}


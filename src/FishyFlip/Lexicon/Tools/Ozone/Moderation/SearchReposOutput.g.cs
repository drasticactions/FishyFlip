// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class SearchReposOutput : ATObject, ICBOREncodable<SearchReposOutput>, IJsonEncodable<SearchReposOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchReposOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="repos"></param>
        public SearchReposOutput(string? cursor = default, List<FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoView> repos = default)
        {
            this.Cursor = cursor;
            this.Repos = repos;
            this.Type = "tools.ozone.moderation.searchRepos#SearchReposOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchReposOutput"/> class.
        /// </summary>
        public SearchReposOutput()
        {
            this.Type = "tools.ozone.moderation.searchRepos#SearchReposOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchReposOutput"/> class.
        /// </summary>
        public SearchReposOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["repos"] is not null) this.Repos = obj["repos"].Values.Select(n =>new FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the repos.
        /// </summary>
        [JsonPropertyName("repos")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoView> Repos { get; set; }

        public const string RecordType = "tools.ozone.moderation.searchRepos#SearchReposOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.SearchReposOutput>)SourceGenerationContext.Default.ToolsOzoneModerationSearchReposOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.SearchReposOutput>)SourceGenerationContext.Default.ToolsOzoneModerationSearchReposOutput);
        }

        public static new SearchReposOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.SearchReposOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.SearchReposOutput>)SourceGenerationContext.Default.ToolsOzoneModerationSearchReposOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new SearchReposOutput FromCBORObject(CBORObject obj)
        {
            return new SearchReposOutput(obj);
        }

    }
}


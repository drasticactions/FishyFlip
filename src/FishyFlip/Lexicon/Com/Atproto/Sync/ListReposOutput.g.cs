// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{
    public partial class ListReposOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ListReposOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="repos"></param>
        public ListReposOutput(string? cursor = default, List<FishyFlip.Lexicon.Com.Atproto.Sync.Repo> repos = default)
        {
            this.Cursor = cursor;
            this.Repos = repos;
            this.Type = "com.atproto.sync.listRepos#ListReposOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListReposOutput"/> class.
        /// </summary>
        public ListReposOutput()
        {
            this.Type = "com.atproto.sync.listRepos#ListReposOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListReposOutput"/> class.
        /// </summary>
        public ListReposOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["repos"] is not null) this.Repos = obj["repos"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Sync.Repo(n)).ToList();
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
        public List<FishyFlip.Lexicon.Com.Atproto.Sync.Repo> Repos { get; set; }

        public const string RecordType = "com.atproto.sync.listRepos#ListReposOutput";

        public static ListReposOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Sync.ListReposOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Sync.ListReposOutput>)SourceGenerationContext.Default.ComAtprotoSyncListReposOutput)!;
        }
    }
}


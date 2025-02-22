// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class GetReposOutput : ATObject, ICBOREncodable<GetReposOutput>, IJsonEncodable<GetReposOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetReposOutput"/> class.
        /// </summary>
        /// <param name="repos">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoViewDetail"/> (tools.ozone.moderation.defs#repoViewDetail) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoViewNotFound"/> (tools.ozone.moderation.defs#repoViewNotFound) <br/>
        /// </param>
        public GetReposOutput(List<ATObject> repos = default)
        {
            this.Repos = repos;
            this.Type = "tools.ozone.moderation.getRepos#GetReposOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetReposOutput"/> class.
        /// </summary>
        public GetReposOutput()
        {
            this.Type = "tools.ozone.moderation.getRepos#GetReposOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetReposOutput"/> class.
        /// </summary>
        public GetReposOutput(CBORObject obj)
        {
            if (obj["repos"] is not null) this.Repos = obj["repos"].Values.Select(n =>n.ToATObject()).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the repos.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoViewDetail"/> (tools.ozone.moderation.defs#repoViewDetail) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoViewNotFound"/> (tools.ozone.moderation.defs#repoViewNotFound) <br/>
        /// </summary>
        [JsonPropertyName("repos")]
        [JsonRequired]
        public List<ATObject> Repos { get; set; }

        public const string RecordType = "tools.ozone.moderation.getRepos#GetReposOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReposOutput>)SourceGenerationContext.Default.ToolsOzoneModerationGetReposOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReposOutput>)SourceGenerationContext.Default.ToolsOzoneModerationGetReposOutput);
        }

        public static new GetReposOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReposOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReposOutput>)SourceGenerationContext.Default.ToolsOzoneModerationGetReposOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetReposOutput FromCBORObject(CBORObject obj)
        {
            return new GetReposOutput(obj);
        }

    }
}


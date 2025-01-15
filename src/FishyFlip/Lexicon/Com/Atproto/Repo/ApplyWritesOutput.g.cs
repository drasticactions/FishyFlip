// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class ApplyWritesOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplyWritesOutput"/> class.
        /// </summary>
        /// <param name="commit">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta"/> (com.atproto.repo.defs#commitMeta)
        /// </param>
        /// <param name="results">
        /// <br/> Union Types: <br/>
        /// #createResult <br/>
        /// #updateResult <br/>
        /// #deleteResult <br/>
        /// </param>
        public ApplyWritesOutput(FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta? commit = default, List<ATObject>? results = default)
        {
            this.Commit = commit;
            this.Results = results;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ApplyWritesOutput"/> class.
        /// </summary>
        public ApplyWritesOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ApplyWritesOutput"/> class.
        /// </summary>
        public ApplyWritesOutput(CBORObject obj)
        {
            if (obj["commit"] is not null) this.Commit = new FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta(obj["commit"]);
            if (obj["results"] is not null) this.Results = obj["results"].Values.Select(n =>n.ToATObject()).ToList();
        }

        /// <summary>
        /// Gets or sets the commit.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta"/> (com.atproto.repo.defs#commitMeta)
        /// </summary>
        [JsonPropertyName("commit")]
        public FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta? Commit { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.CreateResult"/> (com.atproto.repo.applyWrites#createResult) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.UpdateResult"/> (com.atproto.repo.applyWrites#updateResult) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.DeleteResult"/> (com.atproto.repo.applyWrites#deleteResult) <br/>
        /// </summary>
        [JsonPropertyName("results")]
        public List<ATObject>? Results { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.repo.applyWrites#ApplyWritesOutput";

        public const string RecordType = "com.atproto.repo.applyWrites#ApplyWritesOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Repo.ApplyWritesOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.ApplyWritesOutput>)SourceGenerationContext.Default.ComAtprotoRepoApplyWritesOutput)!;
        }

        public static ApplyWritesOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Repo.ApplyWritesOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Repo.ApplyWritesOutput>)SourceGenerationContext.Default.ComAtprotoRepoApplyWritesOutput)!;
        }
    }
}


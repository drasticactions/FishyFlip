// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{
    public partial class DeleteRecordOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRecordOutput"/> class.
        /// </summary>
        /// <param name="commit">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta"/> (com.atproto.repo.defs#commitMeta)
        /// </param>
        public DeleteRecordOutput(Com.Atproto.Repo.CommitMeta? commit = default)
        {
            this.Commit = commit;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRecordOutput"/> class.
        /// </summary>
        public DeleteRecordOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRecordOutput"/> class.
        /// </summary>
        public DeleteRecordOutput(CBORObject obj)
        {
            if (obj["commit"] is not null) this.Commit = new Com.Atproto.Repo.CommitMeta(obj["commit"]);
        }

        /// <summary>
        /// Gets or sets the commit.
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.CommitMeta"/> (com.atproto.repo.defs#commitMeta)
        /// </summary>
        [JsonPropertyName("commit")]
        public Com.Atproto.Repo.CommitMeta? Commit { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.repo.deleteRecord#DeleteRecordOutput";

        public const string RecordType = "com.atproto.repo.deleteRecord#DeleteRecordOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Repo.DeleteRecordOutput>(this, (JsonTypeInfo<Com.Atproto.Repo.DeleteRecordOutput>)SourceGenerationContext.Default.ComAtprotoRepoDeleteRecordOutput)!;
        }

        public static DeleteRecordOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Repo.DeleteRecordOutput>(json, (JsonTypeInfo<Com.Atproto.Repo.DeleteRecordOutput>)SourceGenerationContext.Default.ComAtprotoRepoDeleteRecordOutput)!;
        }
    }
}

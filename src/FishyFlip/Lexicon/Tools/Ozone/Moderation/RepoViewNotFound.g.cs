// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class RepoViewNotFound : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoViewNotFound"/> class.
        /// </summary>
        /// <param name="did"></param>
        public RepoViewNotFound(FishyFlip.Models.ATDid? did = default)
        {
            this.Did = did;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RepoViewNotFound"/> class.
        /// </summary>
        public RepoViewNotFound()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RepoViewNotFound"/> class.
        /// </summary>
        public RepoViewNotFound(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#repoViewNotFound";

        public const string RecordType = "tools.ozone.moderation.defs#repoViewNotFound";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Moderation.RepoViewNotFound>(this, (JsonTypeInfo<Tools.Ozone.Moderation.RepoViewNotFound>)SourceGenerationContext.Default.ToolsOzoneModerationRepoViewNotFound)!;
        }

        public static RepoViewNotFound FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Moderation.RepoViewNotFound>(json, (JsonTypeInfo<Tools.Ozone.Moderation.RepoViewNotFound>)SourceGenerationContext.Default.ToolsOzoneModerationRepoViewNotFound)!;
        }
    }
}


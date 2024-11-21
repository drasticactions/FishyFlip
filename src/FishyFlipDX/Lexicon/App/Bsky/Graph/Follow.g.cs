// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    /// <summary>
    /// Record declaring a social 'follow' relationship of another account. Duplicate follows will be ignored by the AppView.
    /// </summary>
    public partial class Follow : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Follow"/> class.
        /// </summary>
        public Follow(FishyFlip.Models.ATDid? subject, DateTime? createdAt)
        {
            this.Subject = subject;
            this.CreatedAt = createdAt;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Follow"/> class.
        /// </summary>
        public Follow()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Follow"/> class.
        /// </summary>
        public Follow(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATDid();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        [JsonPropertyName("subject")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Subject { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.follow";

        public const string RecordType = "app.bsky.graph.follow";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.Follow>(this, (JsonTypeInfo<App.Bsky.Graph.Follow>)SourceGenerationContext.Default.AppBskyGraphFollow)!;
        }

        public static Follow FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.Follow>(json, (JsonTypeInfo<App.Bsky.Graph.Follow>)SourceGenerationContext.Default.AppBskyGraphFollow)!;
        }
    }
}


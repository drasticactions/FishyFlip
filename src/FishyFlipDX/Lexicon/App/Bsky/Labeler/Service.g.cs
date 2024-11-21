// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Labeler
{
    /// <summary>
    /// A declaration of the existence of labeler service.
    /// </summary>
    public partial class Service : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service(App.Bsky.Labeler.LabelerPolicies? policies, DateTime? createdAt, Com.Atproto.Label.SelfLabels? labels = default)
        {
            this.Policies = policies;
            this.Labels = labels;
            this.CreatedAt = createdAt;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service(CBORObject obj)
        {
            if (obj["policies"] is not null) this.Policies = new App.Bsky.Labeler.LabelerPolicies(obj["policies"]);
            if (obj["labels"] is not null) this.Labels = new Com.Atproto.Label.SelfLabels(obj["labels"]);
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
        }

        [JsonPropertyName("policies")]
        public App.Bsky.Labeler.LabelerPolicies? Policies { get; set; }

        [JsonPropertyName("labels")]
        public Com.Atproto.Label.SelfLabels? Labels { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.labeler.service";

        public const string RecordType = "app.bsky.labeler.service";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Labeler.Service>(this, (JsonTypeInfo<App.Bsky.Labeler.Service>)SourceGenerationContext.Default.AppBskyLabelerService)!;
        }

        public static Service FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Labeler.Service>(json, (JsonTypeInfo<App.Bsky.Labeler.Service>)SourceGenerationContext.Default.AppBskyLabelerService)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Communication
{
    public partial class TemplateView : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateView"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">Name of the template.</param>
        /// <param name="subject">Content of the template, can contain markdown and variable placeholders.</param>
        /// <param name="contentMarkdown">Subject of the message, used in emails.</param>
        /// <param name="disabled"></param>
        /// <param name="lang">Message language.</param>
        /// <param name="lastUpdatedBy">DID of the user who last updated the template.</param>
        /// <param name="createdAt"></param>
        /// <param name="updatedAt"></param>
        public TemplateView(string? id = default, string? name = default, string? subject = default, string? contentMarkdown = default, bool? disabled = default, string? lang = default, FishyFlip.Models.ATDid? lastUpdatedBy = default, DateTime? createdAt = default, DateTime? updatedAt = default)
        {
            this.Id = id;
            this.Name = name;
            this.Subject = subject;
            this.ContentMarkdown = contentMarkdown;
            this.Disabled = disabled;
            this.Lang = lang;
            this.LastUpdatedBy = lastUpdatedBy;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.UpdatedAt = updatedAt;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateView"/> class.
        /// </summary>
        public TemplateView()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateView"/> class.
        /// </summary>
        public TemplateView(CBORObject obj)
        {
            if (obj["id"] is not null) this.Id = obj["id"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["subject"] is not null) this.Subject = obj["subject"].AsString();
            if (obj["contentMarkdown"] is not null) this.ContentMarkdown = obj["contentMarkdown"].AsString();
            if (obj["disabled"] is not null) this.Disabled = obj["disabled"].AsBoolean();
            if (obj["lang"] is not null) this.Lang = obj["lang"].AsString();
            if (obj["lastUpdatedBy"] is not null) this.LastUpdatedBy = obj["lastUpdatedBy"].ToATDid();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["updatedAt"] is not null) this.UpdatedAt = obj["updatedAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonRequired]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// Name of the template.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// Content of the template, can contain markdown and variable placeholders.
        /// </summary>
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the contentMarkdown.
        /// Subject of the message, used in emails.
        /// </summary>
        [JsonPropertyName("contentMarkdown")]
        [JsonRequired]
        public string? ContentMarkdown { get; set; }

        /// <summary>
        /// Gets or sets the disabled.
        /// </summary>
        [JsonPropertyName("disabled")]
        [JsonRequired]
        public bool? Disabled { get; set; }

        /// <summary>
        /// Gets or sets the lang.
        /// Message language.
        /// </summary>
        [JsonPropertyName("lang")]
        public string? Lang { get; set; }

        /// <summary>
        /// Gets or sets the lastUpdatedBy.
        /// DID of the user who last updated the template.
        /// </summary>
        [JsonPropertyName("lastUpdatedBy")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonRequired]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the updatedAt.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        [JsonRequired]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.communication.defs#templateView";

        public const string RecordType = "tools.ozone.communication.defs#templateView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Communication.TemplateView>(this, (JsonTypeInfo<Tools.Ozone.Communication.TemplateView>)SourceGenerationContext.Default.ToolsOzoneCommunicationTemplateView)!;
        }

        public static TemplateView FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Communication.TemplateView>(json, (JsonTypeInfo<Tools.Ozone.Communication.TemplateView>)SourceGenerationContext.Default.ToolsOzoneCommunicationTemplateView)!;
        }
    }
}


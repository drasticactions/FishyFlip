// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Communication
{
    public partial class UpdateTemplateInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTemplateInput"/> class.
        /// </summary>
        /// <param name="id">ID of the template to be updated.</param>
        /// <param name="name">Name of the template.</param>
        /// <param name="lang">Message language.</param>
        /// <param name="contentMarkdown">Content of the template, markdown supported, can contain variable placeholders.</param>
        /// <param name="subject">Subject of the message, used in emails.</param>
        /// <param name="updatedBy">DID of the user who is updating the template.</param>
        /// <param name="disabled"></param>
        public UpdateTemplateInput(string? id = default, string? name = default, string? lang = default, string? contentMarkdown = default, string? subject = default, FishyFlip.Models.ATDid? updatedBy = default, bool? disabled = default)
        {
            this.Id = id;
            this.Name = name;
            this.Lang = lang;
            this.ContentMarkdown = contentMarkdown;
            this.Subject = subject;
            this.UpdatedBy = updatedBy;
            this.Disabled = disabled;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTemplateInput"/> class.
        /// </summary>
        public UpdateTemplateInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTemplateInput"/> class.
        /// </summary>
        public UpdateTemplateInput(CBORObject obj)
        {
            if (obj["id"] is not null) this.Id = obj["id"].AsString();
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["lang"] is not null) this.Lang = obj["lang"].AsString();
            if (obj["contentMarkdown"] is not null) this.ContentMarkdown = obj["contentMarkdown"].AsString();
            if (obj["subject"] is not null) this.Subject = obj["subject"].AsString();
            if (obj["updatedBy"] is not null) this.UpdatedBy = obj["updatedBy"].ToATDid();
            if (obj["disabled"] is not null) this.Disabled = obj["disabled"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the id.
        /// ID of the template to be updated.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonRequired]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// Name of the template.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the lang.
        /// Message language.
        /// </summary>
        [JsonPropertyName("lang")]
        public string? Lang { get; set; }

        /// <summary>
        /// Gets or sets the contentMarkdown.
        /// Content of the template, markdown supported, can contain variable placeholders.
        /// </summary>
        [JsonPropertyName("contentMarkdown")]
        public string? ContentMarkdown { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// Subject of the message, used in emails.
        /// </summary>
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the updatedBy.
        /// DID of the user who is updating the template.
        /// </summary>
        [JsonPropertyName("updatedBy")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the disabled.
        /// </summary>
        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.communication.updateTemplate#UpdateTemplateInput";

        public const string RecordType = "tools.ozone.communication.updateTemplate#UpdateTemplateInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Communication.UpdateTemplateInput>(this, (JsonTypeInfo<Tools.Ozone.Communication.UpdateTemplateInput>)SourceGenerationContext.Default.ToolsOzoneCommunicationUpdateTemplateInput)!;
        }

        public static UpdateTemplateInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Communication.UpdateTemplateInput>(json, (JsonTypeInfo<Tools.Ozone.Communication.UpdateTemplateInput>)SourceGenerationContext.Default.ToolsOzoneCommunicationUpdateTemplateInput)!;
        }
    }
}


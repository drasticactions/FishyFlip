// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Setting
{
    public partial class UpsertOptionInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpsertOptionInput"/> class.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="scope">
        /// <br/> Known Values: <br/>
        /// instance <br/>
        /// personal <br/>
        /// </param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <param name="managerRole">
        /// <br/> Known Values: <br/>
        /// roleModerator - Moderator role. Can perform most actions. <br/>
        /// roleTriage - Triage role. Mostly intended for monitoring and escalating issues. <br/>
        /// roleAdmin - Admin role. Highest level of access, can perform all actions. <br/>
        /// </param>
        public UpsertOptionInput(string key = default, string scope = default, ATObject value = default, string? description = default, string? managerRole = default)
        {
            this.Key = key;
            this.Scope = scope;
            this.Value = value;
            this.Description = description;
            this.ManagerRole = managerRole;
            this.Type = "tools.ozone.setting.upsertOption#UpsertOptionInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpsertOptionInput"/> class.
        /// </summary>
        public UpsertOptionInput()
        {
            this.Type = "tools.ozone.setting.upsertOption#UpsertOptionInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpsertOptionInput"/> class.
        /// </summary>
        public UpsertOptionInput(CBORObject obj)
        {
            if (obj["key"] is not null) this.Key = obj["key"].AsString();
            if (obj["scope"] is not null) this.Scope = obj["scope"].AsString();
            if (obj["value"] is not null) this.Value = obj["value"].ToATObject();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["managerRole"] is not null) this.ManagerRole = obj["managerRole"].AsString();
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [JsonPropertyName("key")]
        [JsonRequired]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// <br/> Known Values: <br/>
        /// instance <br/>
        /// personal <br/>
        /// </summary>
        [JsonPropertyName("scope")]
        [JsonRequired]
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public ATObject Value { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the managerRole.
        /// <br/> Known Values: <br/>
        /// roleModerator - Moderator role. Can perform most actions. <br/>
        /// roleTriage - Triage role. Mostly intended for monitoring and escalating issues. <br/>
        /// roleAdmin - Admin role. Highest level of access, can perform all actions. <br/>
        /// </summary>
        [JsonPropertyName("managerRole")]
        public string? ManagerRole { get; set; }

        public const string RecordType = "tools.ozone.setting.upsertOption#UpsertOptionInput";

        public static UpsertOptionInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Setting.UpsertOptionInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Setting.UpsertOptionInput>)SourceGenerationContext.Default.ToolsOzoneSettingUpsertOptionInput)!;
        }
    }
}


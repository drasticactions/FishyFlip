// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Team
{
    public partial class UpdateMemberInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMemberInput"/> class.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="disabled"></param>
        /// <param name="role">
        /// Known Values:
        /// roleAdmin - Admin role. Highest level of access, can perform all actions.
        /// roleModerator - Moderator role. Can perform most actions.
        /// roleTriage - Triage role. Mostly intended for monitoring and escalating issues.
        /// </param>
        public UpdateMemberInput(FishyFlip.Models.ATDid? did = default, bool? disabled = default, string? role = default)
        {
            this.Did = did;
            this.Disabled = disabled;
            this.Role = role;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMemberInput"/> class.
        /// </summary>
        public UpdateMemberInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMemberInput"/> class.
        /// </summary>
        public UpdateMemberInput(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["disabled"] is not null) this.Disabled = obj["disabled"].AsBoolean();
            if (obj["role"] is not null) this.Role = obj["role"].AsString();
        }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// Gets or sets the disabled.
        /// </summary>
        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// Known Values:
        /// roleAdmin - Admin role. Highest level of access, can perform all actions.
        /// roleModerator - Moderator role. Can perform most actions.
        /// roleTriage - Triage role. Mostly intended for monitoring and escalating issues.
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.team.updateMember#UpdateMemberInput";

        public const string RecordType = "tools.ozone.team.updateMember#UpdateMemberInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Team.UpdateMemberInput>(this, (JsonTypeInfo<Tools.Ozone.Team.UpdateMemberInput>)SourceGenerationContext.Default.ToolsOzoneTeamUpdateMemberInput)!;
        }

        public static UpdateMemberInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Team.UpdateMemberInput>(json, (JsonTypeInfo<Tools.Ozone.Team.UpdateMemberInput>)SourceGenerationContext.Default.ToolsOzoneTeamUpdateMemberInput)!;
        }
    }
}

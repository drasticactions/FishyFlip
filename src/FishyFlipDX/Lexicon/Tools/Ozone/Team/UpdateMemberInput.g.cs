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
            // enum
        }

        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }

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


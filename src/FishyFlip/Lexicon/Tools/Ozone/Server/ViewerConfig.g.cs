// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Server
{
    public partial class ViewerConfig : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerConfig"/> class.
        /// </summary>
        /// <param name="role">
        /// <br/> Known Values: <br/>
        /// roleAdmin - Admin role. Highest level of access, can perform all actions. <br/>
        /// roleModerator - Moderator role. Can perform most actions. <br/>
        /// roleTriage - Triage role. Mostly intended for monitoring and escalating issues. <br/>
        /// </param>
        public ViewerConfig(string? role = default)
        {
            this.Role = role;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerConfig"/> class.
        /// </summary>
        public ViewerConfig()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerConfig"/> class.
        /// </summary>
        public ViewerConfig(CBORObject obj)
        {
            if (obj["role"] is not null) this.Role = obj["role"].AsString();
        }

        /// <summary>
        /// Gets or sets the role.
        /// <br/> Known Values: <br/>
        /// roleAdmin - Admin role. Highest level of access, can perform all actions. <br/>
        /// roleModerator - Moderator role. Can perform most actions. <br/>
        /// roleTriage - Triage role. Mostly intended for monitoring and escalating issues. <br/>
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.server.getConfig#viewerConfig";

        public const string RecordType = "tools.ozone.server.getConfig#viewerConfig";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Server.ViewerConfig>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Server.ViewerConfig>)SourceGenerationContext.Default.ToolsOzoneServerViewerConfig)!;
        }

        public static ViewerConfig FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Server.ViewerConfig>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Server.ViewerConfig>)SourceGenerationContext.Default.ToolsOzoneServerViewerConfig)!;
        }
    }
}


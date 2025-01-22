// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Server
{
    public partial class ServiceConfig : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfig"/> class.
        /// </summary>
        /// <param name="url"></param>
        public ServiceConfig(string? url = default)
        {
            this.Url = url;
            this.Type = "tools.ozone.server.getConfig#serviceConfig";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfig"/> class.
        /// </summary>
        public ServiceConfig()
        {
            this.Type = "tools.ozone.server.getConfig#serviceConfig";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfig"/> class.
        /// </summary>
        public ServiceConfig(CBORObject obj)
        {
            if (obj["url"] is not null) this.Url = obj["url"].AsString();
        }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        public const string RecordType = "tools.ozone.server.getConfig#serviceConfig";

        public static ServiceConfig FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig>)SourceGenerationContext.Default.ToolsOzoneServerServiceConfig)!;
        }
    }
}


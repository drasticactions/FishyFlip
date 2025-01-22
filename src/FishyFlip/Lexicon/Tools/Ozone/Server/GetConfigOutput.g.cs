// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Server
{
    public partial class GetConfigOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigOutput"/> class.
        /// </summary>
        /// <param name="appview">
        /// tools.ozone.server.defs#serviceConfig <br/>
        /// </param>
        /// <param name="pds">
        /// tools.ozone.server.defs#serviceConfig <br/>
        /// </param>
        /// <param name="blobDivert">
        /// tools.ozone.server.defs#serviceConfig <br/>
        /// </param>
        /// <param name="chat">
        /// tools.ozone.server.defs#serviceConfig <br/>
        /// </param>
        /// <param name="viewer">
        /// tools.ozone.server.defs#viewerConfig <br/>
        /// </param>
        public GetConfigOutput(FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig? appview = default, FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig? pds = default, FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig? blobDivert = default, FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig? chat = default, FishyFlip.Lexicon.Tools.Ozone.Server.ViewerConfig? viewer = default)
        {
            this.Appview = appview;
            this.Pds = pds;
            this.BlobDivert = blobDivert;
            this.Chat = chat;
            this.Viewer = viewer;
            this.Type = "tools.ozone.server.getConfig#GetConfigOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigOutput"/> class.
        /// </summary>
        public GetConfigOutput()
        {
            this.Type = "tools.ozone.server.getConfig#GetConfigOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigOutput"/> class.
        /// </summary>
        public GetConfigOutput(CBORObject obj)
        {
            if (obj["appview"] is not null) this.Appview = new FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig(obj["appview"]);
            if (obj["pds"] is not null) this.Pds = new FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig(obj["pds"]);
            if (obj["blobDivert"] is not null) this.BlobDivert = new FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig(obj["blobDivert"]);
            if (obj["chat"] is not null) this.Chat = new FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig(obj["chat"]);
            if (obj["viewer"] is not null) this.Viewer = new FishyFlip.Lexicon.Tools.Ozone.Server.ViewerConfig(obj["viewer"]);
        }

        /// <summary>
        /// Gets or sets the appview.
        /// tools.ozone.server.defs#serviceConfig <br/>
        /// </summary>
        [JsonPropertyName("appview")]
        public FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig? Appview { get; set; }

        /// <summary>
        /// Gets or sets the pds.
        /// tools.ozone.server.defs#serviceConfig <br/>
        /// </summary>
        [JsonPropertyName("pds")]
        public FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig? Pds { get; set; }

        /// <summary>
        /// Gets or sets the blobDivert.
        /// tools.ozone.server.defs#serviceConfig <br/>
        /// </summary>
        [JsonPropertyName("blobDivert")]
        public FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig? BlobDivert { get; set; }

        /// <summary>
        /// Gets or sets the chat.
        /// tools.ozone.server.defs#serviceConfig <br/>
        /// </summary>
        [JsonPropertyName("chat")]
        public FishyFlip.Lexicon.Tools.Ozone.Server.ServiceConfig? Chat { get; set; }

        /// <summary>
        /// Gets or sets the viewer.
        /// tools.ozone.server.defs#viewerConfig <br/>
        /// </summary>
        [JsonPropertyName("viewer")]
        public FishyFlip.Lexicon.Tools.Ozone.Server.ViewerConfig? Viewer { get; set; }

        public const string RecordType = "tools.ozone.server.getConfig#GetConfigOutput";

        public static GetConfigOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Server.GetConfigOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Server.GetConfigOutput>)SourceGenerationContext.Default.ToolsOzoneServerGetConfigOutput)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class RecordViewNotFound : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordViewNotFound"/> class.
        /// </summary>
        /// <param name="uri"></param>
        public RecordViewNotFound(FishyFlip.Models.ATUri uri = default)
        {
            this.Uri = uri;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordViewNotFound"/> class.
        /// </summary>
        public RecordViewNotFound()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RecordViewNotFound"/> class.
        /// </summary>
        public RecordViewNotFound(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri Uri { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.moderation.defs#recordViewNotFound";

        public const string RecordType = "tools.ozone.moderation.defs#recordViewNotFound";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordViewNotFound>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordViewNotFound>)SourceGenerationContext.Default.ToolsOzoneModerationRecordViewNotFound)!;
        }

        public static RecordViewNotFound FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordViewNotFound>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordViewNotFound>)SourceGenerationContext.Default.ToolsOzoneModerationRecordViewNotFound)!;
        }
    }
}


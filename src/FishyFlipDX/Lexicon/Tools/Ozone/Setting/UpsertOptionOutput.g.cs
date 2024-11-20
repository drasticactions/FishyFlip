// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Setting
{
    public partial class UpsertOptionOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpsertOptionOutput"/> class.
        /// </summary>
        public UpsertOptionOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpsertOptionOutput"/> class.
        /// </summary>
        public UpsertOptionOutput(CBORObject obj)
        {
            if (obj["option"] is not null) this.Option = new Tools.Ozone.Setting.Option(obj["option"]);
        }

        [JsonPropertyName("option")]
        [JsonRequired]
        public Tools.Ozone.Setting.Option? Option { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.setting.upsertOption#UpsertOptionOutput";

        public const string RecordType = "tools.ozone.setting.upsertOption#UpsertOptionOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Setting.UpsertOptionOutput>(this, (JsonTypeInfo<Tools.Ozone.Setting.UpsertOptionOutput>)SourceGenerationContext.Default.ToolsOzoneSettingUpsertOptionOutput)!;
        }

        public static UpsertOptionOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Setting.UpsertOptionOutput>(json, (JsonTypeInfo<Tools.Ozone.Setting.UpsertOptionOutput>)SourceGenerationContext.Default.ToolsOzoneSettingUpsertOptionOutput)!;
        }
    }
}

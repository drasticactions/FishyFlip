// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Set
{
    public partial class DeleteSetOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSetOutput"/> class.
        /// </summary>
        public DeleteSetOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSetOutput"/> class.
        /// </summary>
        public DeleteSetOutput(CBORObject obj)
        {
        }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.set.deleteSet#DeleteSetOutput";

        public const string RecordType = "tools.ozone.set.deleteSet#DeleteSetOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Set.DeleteSetOutput>(this, (JsonTypeInfo<Tools.Ozone.Set.DeleteSetOutput>)SourceGenerationContext.Default.ToolsOzoneSetDeleteSetOutput)!;
        }

        public static DeleteSetOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Set.DeleteSetOutput>(json, (JsonTypeInfo<Tools.Ozone.Set.DeleteSetOutput>)SourceGenerationContext.Default.ToolsOzoneSetDeleteSetOutput)!;
        }
    }
}

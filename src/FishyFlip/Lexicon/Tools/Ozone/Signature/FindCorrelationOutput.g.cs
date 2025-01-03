// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Signature
{
    public partial class FindCorrelationOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCorrelationOutput"/> class.
        /// </summary>
        /// <param name="details"></param>
        public FindCorrelationOutput(List<Tools.Ozone.Signature.SigDetail>? details = default)
        {
            this.Details = details;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FindCorrelationOutput"/> class.
        /// </summary>
        public FindCorrelationOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FindCorrelationOutput"/> class.
        /// </summary>
        public FindCorrelationOutput(CBORObject obj)
        {
            if (obj["details"] is not null) this.Details = obj["details"].Values.Select(n =>new Tools.Ozone.Signature.SigDetail(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        [JsonPropertyName("details")]
        [JsonRequired]
        public List<Tools.Ozone.Signature.SigDetail>? Details { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "tools.ozone.signature.findCorrelation#FindCorrelationOutput";

        public const string RecordType = "tools.ozone.signature.findCorrelation#FindCorrelationOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Tools.Ozone.Signature.FindCorrelationOutput>(this, (JsonTypeInfo<Tools.Ozone.Signature.FindCorrelationOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureFindCorrelationOutput)!;
        }

        public static FindCorrelationOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Tools.Ozone.Signature.FindCorrelationOutput>(json, (JsonTypeInfo<Tools.Ozone.Signature.FindCorrelationOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureFindCorrelationOutput)!;
        }
    }
}


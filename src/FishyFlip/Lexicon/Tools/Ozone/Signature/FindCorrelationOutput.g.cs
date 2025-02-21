// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Signature
{
    public partial class FindCorrelationOutput : ATObject, ICBOREncodable<FindCorrelationOutput>, IJsonEncodable<FindCorrelationOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCorrelationOutput"/> class.
        /// </summary>
        /// <param name="details"></param>
        public FindCorrelationOutput(List<FishyFlip.Lexicon.Tools.Ozone.Signature.SigDetail> details = default)
        {
            this.Details = details;
            this.Type = "tools.ozone.signature.findCorrelation#FindCorrelationOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FindCorrelationOutput"/> class.
        /// </summary>
        public FindCorrelationOutput()
        {
            this.Type = "tools.ozone.signature.findCorrelation#FindCorrelationOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FindCorrelationOutput"/> class.
        /// </summary>
        public FindCorrelationOutput(CBORObject obj)
        {
            if (obj["details"] is not null) this.Details = obj["details"].Values.Select(n =>new FishyFlip.Lexicon.Tools.Ozone.Signature.SigDetail(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        [JsonPropertyName("details")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Tools.Ozone.Signature.SigDetail> Details { get; set; }

        public const string RecordType = "tools.ozone.signature.findCorrelation#FindCorrelationOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Signature.FindCorrelationOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureFindCorrelationOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Signature.FindCorrelationOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureFindCorrelationOutput);
        }

        public static new FindCorrelationOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Signature.FindCorrelationOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Signature.FindCorrelationOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureFindCorrelationOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new FindCorrelationOutput FromCBORObject(CBORObject obj)
        {
            return new FindCorrelationOutput(obj);
        }

    }
}


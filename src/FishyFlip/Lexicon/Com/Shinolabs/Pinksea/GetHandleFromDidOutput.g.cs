// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Shinolabs.Pinksea
{
    public partial class GetHandleFromDidOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetHandleFromDidOutput"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public GetHandleFromDidOutput(FishyFlip.Models.ATHandle? handle = default)
        {
            this.Handle = handle;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetHandleFromDidOutput"/> class.
        /// </summary>
        public GetHandleFromDidOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetHandleFromDidOutput"/> class.
        /// </summary>
        public GetHandleFromDidOutput(CBORObject obj)
        {
            if (obj["handle"] is not null) this.Handle = obj["handle"].ToATHandle();
        }

        /// <summary>
        /// Gets or sets the handle.
        /// <br/> The handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]
        public FishyFlip.Models.ATHandle? Handle { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.shinolabs.pinksea.getHandleFromDid#GetHandleFromDidOutput";

        public const string RecordType = "com.shinolabs.pinksea.getHandleFromDid#GetHandleFromDidOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Shinolabs.Pinksea.GetHandleFromDidOutput>(this, (JsonTypeInfo<Com.Shinolabs.Pinksea.GetHandleFromDidOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetHandleFromDidOutput)!;
        }

        public static GetHandleFromDidOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Shinolabs.Pinksea.GetHandleFromDidOutput>(json, (JsonTypeInfo<Com.Shinolabs.Pinksea.GetHandleFromDidOutput>)SourceGenerationContext.Default.ComShinolabsPinkseaGetHandleFromDidOutput)!;
        }
    }
}

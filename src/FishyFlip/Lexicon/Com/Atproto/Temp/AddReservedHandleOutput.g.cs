// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Temp
{
    public partial class AddReservedHandleOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AddReservedHandleOutput"/> class.
        /// </summary>
        public AddReservedHandleOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AddReservedHandleOutput"/> class.
        /// </summary>
        public AddReservedHandleOutput(CBORObject obj)
        {
        }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.temp.addReservedHandle#AddReservedHandleOutput";

        public const string RecordType = "com.atproto.temp.addReservedHandle#AddReservedHandleOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleOutput)!;
        }

        public static AddReservedHandleOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleOutput)!;
        }
    }
}


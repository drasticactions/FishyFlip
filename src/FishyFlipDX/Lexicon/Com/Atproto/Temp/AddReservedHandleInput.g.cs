// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Temp
{
    public partial class AddReservedHandleInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AddReservedHandleInput"/> class.
        /// </summary>
        /// <param name="handle"></param>
        public AddReservedHandleInput(string? handle = default)
        {
            this.Handle = handle;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AddReservedHandleInput"/> class.
        /// </summary>
        public AddReservedHandleInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AddReservedHandleInput"/> class.
        /// </summary>
        public AddReservedHandleInput(CBORObject obj)
        {
            if (obj["handle"] is not null) this.Handle = obj["handle"].AsString();
        }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        [JsonRequired]
        public string? Handle { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.temp.addReservedHandle#AddReservedHandleInput";

        public const string RecordType = "com.atproto.temp.addReservedHandle#AddReservedHandleInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Com.Atproto.Temp.AddReservedHandleInput>(this, (JsonTypeInfo<Com.Atproto.Temp.AddReservedHandleInput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleInput)!;
        }

        public static AddReservedHandleInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Temp.AddReservedHandleInput>(json, (JsonTypeInfo<Com.Atproto.Temp.AddReservedHandleInput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleInput)!;
        }
    }
}


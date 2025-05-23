// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Temp
{
    public partial class AddReservedHandleOutput : ATObject, ICBOREncodable<AddReservedHandleOutput>, IJsonEncodable<AddReservedHandleOutput>, IParsable<AddReservedHandleOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AddReservedHandleOutput"/> class.
        /// </summary>
        public AddReservedHandleOutput()
        {
            this.Type = "com.atproto.temp.addReservedHandle#AddReservedHandleOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AddReservedHandleOutput"/> class.
        /// </summary>
        public AddReservedHandleOutput(CBORObject obj)
        {
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        public const string RecordType = "com.atproto.temp.addReservedHandle#AddReservedHandleOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleOutput);
        }

        public static new AddReservedHandleOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new AddReservedHandleOutput FromCBORObject(CBORObject obj)
        {
            return new AddReservedHandleOutput(obj);
        }

        /// <inheritdoc/>
        public static AddReservedHandleOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<AddReservedHandleOutput>(s, (JsonTypeInfo<AddReservedHandleOutput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out AddReservedHandleOutput result)
        {
            result = JsonSerializer.Deserialize<AddReservedHandleOutput>(s, (JsonTypeInfo<AddReservedHandleOutput>)SourceGenerationContext.Default.ComAtprotoTempAddReservedHandleOutput);
            return result != null;
        }
    }
}


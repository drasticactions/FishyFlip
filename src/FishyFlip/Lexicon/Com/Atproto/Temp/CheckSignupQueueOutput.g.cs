// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Temp
{
    public partial class CheckSignupQueueOutput : ATObject, ICBOREncodable<CheckSignupQueueOutput>, IJsonEncodable<CheckSignupQueueOutput>, IParsable<CheckSignupQueueOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckSignupQueueOutput"/> class.
        /// </summary>
        /// <param name="activated"></param>
        /// <param name="placeInQueue"></param>
        /// <param name="estimatedTimeMs"></param>
        public CheckSignupQueueOutput(bool activated = default, long? placeInQueue = default, long? estimatedTimeMs = default)
        {
            this.Activated = activated;
            this.PlaceInQueue = placeInQueue;
            this.EstimatedTimeMs = estimatedTimeMs;
            this.Type = "com.atproto.temp.checkSignupQueue#CheckSignupQueueOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CheckSignupQueueOutput"/> class.
        /// </summary>
        public CheckSignupQueueOutput()
        {
            this.Type = "com.atproto.temp.checkSignupQueue#CheckSignupQueueOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CheckSignupQueueOutput"/> class.
        /// </summary>
        public CheckSignupQueueOutput(CBORObject obj)
        {
            if (obj["activated"] is not null) this.Activated = obj["activated"].AsBoolean();
            if (obj["placeInQueue"] is not null) this.PlaceInQueue = obj["placeInQueue"].AsInt64Value();
            if (obj["estimatedTimeMs"] is not null) this.EstimatedTimeMs = obj["estimatedTimeMs"].AsInt64Value();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the activated.
        /// </summary>
        [JsonPropertyName("activated")]
        [JsonRequired]
        public bool Activated { get; set; }

        /// <summary>
        /// Gets or sets the placeInQueue.
        /// </summary>
        [JsonPropertyName("placeInQueue")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? PlaceInQueue { get; set; }

        /// <summary>
        /// Gets or sets the estimatedTimeMs.
        /// </summary>
        [JsonPropertyName("estimatedTimeMs")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? EstimatedTimeMs { get; set; }

        public const string RecordType = "com.atproto.temp.checkSignupQueue#CheckSignupQueueOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.CheckSignupQueueOutput>)SourceGenerationContext.Default.ComAtprotoTempCheckSignupQueueOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.CheckSignupQueueOutput>)SourceGenerationContext.Default.ComAtprotoTempCheckSignupQueueOutput);
        }

        public static new CheckSignupQueueOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Temp.CheckSignupQueueOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Temp.CheckSignupQueueOutput>)SourceGenerationContext.Default.ComAtprotoTempCheckSignupQueueOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new CheckSignupQueueOutput FromCBORObject(CBORObject obj)
        {
            return new CheckSignupQueueOutput(obj);
        }

        /// <inheritdoc/>
        public static CheckSignupQueueOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<CheckSignupQueueOutput>(s, (JsonTypeInfo<CheckSignupQueueOutput>)SourceGenerationContext.Default.ComAtprotoTempCheckSignupQueueOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out CheckSignupQueueOutput result)
        {
            result = JsonSerializer.Deserialize<CheckSignupQueueOutput>(s, (JsonTypeInfo<CheckSignupQueueOutput>)SourceGenerationContext.Default.ComAtprotoTempCheckSignupQueueOutput);
            return result != null;
        }
    }
}


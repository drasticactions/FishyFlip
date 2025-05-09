// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Zio.Atfile
{
    /// <summary>
    /// A reference to a locked file.
    /// </summary>
    public partial class Lock : ATObject, ICBOREncodable<Lock>, IJsonEncodable<Lock>, IParsable<Lock>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Lock"/> class.
        /// </summary>
        /// <param name="@lock"></param>
        public Lock(bool? @lock = default)
        {
            this.LockValue = @lock;
            this.Type = "blue.zio.atfile.lock";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Lock"/> class.
        /// </summary>
        public Lock()
        {
            this.Type = "blue.zio.atfile.lock";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Lock"/> class.
        /// </summary>
        public Lock(CBORObject obj)
        {
            if (obj["lock"] is not null) this.LockValue = obj["lock"].AsBoolean();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the lock.
        /// </summary>
        [JsonPropertyName("lock")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? LockValue { get; set; }

        public const string RecordType = "blue.zio.atfile.lock";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Zio.Atfile.Lock>)SourceGenerationContext.Default.BlueZioAtfileLock);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Zio.Atfile.Lock>)SourceGenerationContext.Default.BlueZioAtfileLock);
        }

        public static new Lock FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Zio.Atfile.Lock>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Zio.Atfile.Lock>)SourceGenerationContext.Default.BlueZioAtfileLock)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new Lock FromCBORObject(CBORObject obj)
        {
            return new Lock(obj);
        }

        /// <inheritdoc/>
        public static Lock Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<Lock>(s, (JsonTypeInfo<Lock>)SourceGenerationContext.Default.BlueZioAtfileLock)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out Lock result)
        {
            result = JsonSerializer.Deserialize<Lock>(s, (JsonTypeInfo<Lock>)SourceGenerationContext.Default.BlueZioAtfileLock);
            return result != null;
        }
    }
}


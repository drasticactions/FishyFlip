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
    public partial class Lock : ATObject
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
        }

        /// <summary>
        /// Gets or sets the lock.
        /// </summary>
        [JsonPropertyName("lock")]
        public bool? LockValue { get; set; }

        public const string RecordType = "blue.zio.atfile.lock";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Blue.Zio.Atfile.Lock>(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Zio.Atfile.Lock>)SourceGenerationContext.Default.BlueZioAtfileLock)!;
        }

        public static Lock FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Zio.Atfile.Lock>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Zio.Atfile.Lock>)SourceGenerationContext.Default.BlueZioAtfileLock)!;
        }
    }
}


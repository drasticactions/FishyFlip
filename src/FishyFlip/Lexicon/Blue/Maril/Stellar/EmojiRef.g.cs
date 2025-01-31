// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Maril.Stellar
{
    public partial class EmojiRef : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmojiRef"/> class.
        /// </summary>
        /// <param name="rkey"></param>
        /// <param name="repo"></param>
        public EmojiRef(string rkey = default, FishyFlip.Models.ATDid repo = default)
        {
            this.Rkey = rkey;
            this.Repo = repo;
            this.Type = "blue.maril.stellar.reaction#emojiRef";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmojiRef"/> class.
        /// </summary>
        public EmojiRef()
        {
            this.Type = "blue.maril.stellar.reaction#emojiRef";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmojiRef"/> class.
        /// </summary>
        public EmojiRef(CBORObject obj)
        {
            if (obj["rkey"] is not null) this.Rkey = obj["rkey"].AsString();
            if (obj["repo"] is not null) this.Repo = obj["repo"].ToATDid();
        }

        /// <summary>
        /// Gets or sets the rkey.
        /// </summary>
        [JsonPropertyName("rkey")]
        [JsonRequired]
        public string Rkey { get; set; }

        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        [JsonPropertyName("repo")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Repo { get; set; }

        public const string RecordType = "blue.maril.stellar.reaction#emojiRef";

        public static EmojiRef FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Maril.Stellar.EmojiRef>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Maril.Stellar.EmojiRef>)SourceGenerationContext.Default.BlueMarilStellarEmojiRef)!;
        }
    }
}


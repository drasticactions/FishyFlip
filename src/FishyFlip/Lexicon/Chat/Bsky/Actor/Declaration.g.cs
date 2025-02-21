// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Actor
{
    /// <summary>
    /// A declaration of a Bluesky chat account.
    /// </summary>
    public partial class Declaration : ATObject, ICBOREncodable<Declaration>, IJsonEncodable<Declaration>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Declaration"/> class.
        /// </summary>
        /// <param name="allowIncoming">
        /// <br/> Known Values: <br/>
        /// all <br/>
        /// none <br/>
        /// following <br/>
        /// </param>
        public Declaration(string? allowIncoming)
        {
            this.AllowIncoming = allowIncoming;
            this.Type = "chat.bsky.actor.declaration";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Declaration"/> class.
        /// </summary>
        public Declaration()
        {
            this.Type = "chat.bsky.actor.declaration";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Declaration"/> class.
        /// </summary>
        public Declaration(CBORObject obj)
        {
            if (obj["allowIncoming"] is not null) this.AllowIncoming = obj["allowIncoming"].AsString();
        }

        /// <summary>
        /// Gets or sets the allowIncoming.
        /// <br/> Known Values: <br/>
        /// all <br/>
        /// none <br/>
        /// following <br/>
        /// </summary>
        [JsonPropertyName("allowIncoming")]
        public string? AllowIncoming { get; set; }

        public const string RecordType = "chat.bsky.actor.declaration";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Actor.Declaration>)SourceGenerationContext.Default.ChatBskyActorDeclaration);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Actor.Declaration>)SourceGenerationContext.Default.ChatBskyActorDeclaration);
        }

        public static new Declaration FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Actor.Declaration>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Actor.Declaration>)SourceGenerationContext.Default.ChatBskyActorDeclaration)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new Declaration FromCBORObject(CBORObject obj)
        {
            return new Declaration(obj);
        }

    }
}


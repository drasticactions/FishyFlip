// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class AcceptConvoOutput : ATObject, ICBOREncodable<AcceptConvoOutput>, IJsonEncodable<AcceptConvoOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptConvoOutput"/> class.
        /// </summary>
        /// <param name="rev">Rev when the convo was accepted. If not present, the convo was already accepted.</param>
        public AcceptConvoOutput(string? rev = default)
        {
            this.Rev = rev;
            this.Type = "chat.bsky.convo.acceptConvo#AcceptConvoOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptConvoOutput"/> class.
        /// </summary>
        public AcceptConvoOutput()
        {
            this.Type = "chat.bsky.convo.acceptConvo#AcceptConvoOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptConvoOutput"/> class.
        /// </summary>
        public AcceptConvoOutput(CBORObject obj)
        {
            if (obj["rev"] is not null) this.Rev = obj["rev"].AsString();
        }

        /// <summary>
        /// Gets or sets the rev.
        /// <br/> Rev when the convo was accepted. If not present, the convo was already accepted.
        /// </summary>
        [JsonPropertyName("rev")]
        public string? Rev { get; set; }

        public const string RecordType = "chat.bsky.convo.acceptConvo#AcceptConvoOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoAcceptConvoOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoAcceptConvoOutput);
        }

        public static new AcceptConvoOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.AcceptConvoOutput>)SourceGenerationContext.Default.ChatBskyConvoAcceptConvoOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new AcceptConvoOutput FromCBORObject(CBORObject obj)
        {
            return new AcceptConvoOutput(obj);
        }

    }
}


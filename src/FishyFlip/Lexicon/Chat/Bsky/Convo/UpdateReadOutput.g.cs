// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class UpdateReadOutput : ATObject, ICBOREncodable<UpdateReadOutput>, IJsonEncodable<UpdateReadOutput>, IParsable<UpdateReadOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateReadOutput"/> class.
        /// </summary>
        /// <param name="convo">
        /// <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView"/> (chat.bsky.convo.defs#convoView)
        /// </param>
        public UpdateReadOutput(FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView convo = default)
        {
            this.Convo = convo;
            this.Type = "chat.bsky.convo.updateRead#UpdateReadOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateReadOutput"/> class.
        /// </summary>
        public UpdateReadOutput()
        {
            this.Type = "chat.bsky.convo.updateRead#UpdateReadOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateReadOutput"/> class.
        /// </summary>
        public UpdateReadOutput(CBORObject obj)
        {
            if (obj["convo"] is not null) this.Convo = new FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView(obj["convo"]);
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the convo.
        /// <br/> <see cref="FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView"/> (chat.bsky.convo.defs#convoView)
        /// </summary>
        [JsonPropertyName("convo")]
        [JsonRequired]
        public FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView Convo { get; set; }

        public const string RecordType = "chat.bsky.convo.updateRead#UpdateReadOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput>)SourceGenerationContext.Default.ChatBskyConvoUpdateReadOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput>)SourceGenerationContext.Default.ChatBskyConvoUpdateReadOutput);
        }

        public static new UpdateReadOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.UpdateReadOutput>)SourceGenerationContext.Default.ChatBskyConvoUpdateReadOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new UpdateReadOutput FromCBORObject(CBORObject obj)
        {
            return new UpdateReadOutput(obj);
        }

        /// <inheritdoc/>
        public static UpdateReadOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<UpdateReadOutput>(s, (JsonTypeInfo<UpdateReadOutput>)SourceGenerationContext.Default.ChatBskyConvoUpdateReadOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out UpdateReadOutput result)
        {
            result = JsonSerializer.Deserialize<UpdateReadOutput>(s, (JsonTypeInfo<UpdateReadOutput>)SourceGenerationContext.Default.ChatBskyConvoUpdateReadOutput);
            return result != null;
        }
    }
}


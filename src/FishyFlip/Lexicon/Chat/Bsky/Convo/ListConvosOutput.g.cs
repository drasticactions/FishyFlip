// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    public partial class ListConvosOutput : ATObject, IBatchItem
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ListConvosOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="convos"></param>
        public ListConvosOutput(string? cursor = default, List<FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView> convos = default)
        {
            this.Cursor = cursor;
            this.Convos = convos;
            this.Type = "chat.bsky.convo.listConvos#ListConvosOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListConvosOutput"/> class.
        /// </summary>
        public ListConvosOutput()
        {
            this.Type = "chat.bsky.convo.listConvos#ListConvosOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListConvosOutput"/> class.
        /// </summary>
        public ListConvosOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["convos"] is not null) this.Convos = obj["convos"].Values.Select(n =>new FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the convos.
        /// </summary>
        [JsonPropertyName("convos")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Chat.Bsky.Convo.ConvoView> Convos { get; set; }

        public const string RecordType = "chat.bsky.convo.listConvos#ListConvosOutput";

        public static ListConvosOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Convo.ListConvosOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Convo.ListConvosOutput>)SourceGenerationContext.Default.ChatBskyConvoListConvosOutput)!;
        }
    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Chat.Bsky.Actor
{
    public partial class DeleteAccountOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountOutput"/> class.
        /// </summary>
        public DeleteAccountOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountOutput"/> class.
        /// </summary>
        public DeleteAccountOutput(CBORObject obj)
        {
        }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "chat.bsky.actor.deleteAccount#DeleteAccountOutput";

        public const string RecordType = "chat.bsky.actor.deleteAccount#DeleteAccountOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<Chat.Bsky.Actor.DeleteAccountOutput>(this, (JsonTypeInfo<Chat.Bsky.Actor.DeleteAccountOutput>)SourceGenerationContext.Default.ChatBskyActorDeleteAccountOutput)!;
        }

        public static DeleteAccountOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<Chat.Bsky.Actor.DeleteAccountOutput>(json, (JsonTypeInfo<Chat.Bsky.Actor.DeleteAccountOutput>)SourceGenerationContext.Default.ChatBskyActorDeleteAccountOutput)!;
        }
    }
}

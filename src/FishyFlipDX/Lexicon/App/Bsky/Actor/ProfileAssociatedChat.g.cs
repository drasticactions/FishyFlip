// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class ProfileAssociatedChat : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileAssociatedChat"/> class.
        /// </summary>
        public ProfileAssociatedChat(string? allowIncoming = default)
        {
            this.AllowIncoming = allowIncoming;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileAssociatedChat"/> class.
        /// </summary>
        public ProfileAssociatedChat()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileAssociatedChat"/> class.
        /// </summary>
        public ProfileAssociatedChat(CBORObject obj)
        {
            // enum
        }

        [JsonPropertyName("allowIncoming")]
        [JsonRequired]
        public string? AllowIncoming { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.actor.defs#profileAssociatedChat";

        public const string RecordType = "app.bsky.actor.defs#profileAssociatedChat";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Actor.ProfileAssociatedChat>(this, (JsonTypeInfo<App.Bsky.Actor.ProfileAssociatedChat>)SourceGenerationContext.Default.AppBskyActorProfileAssociatedChat)!;
        }

        public static ProfileAssociatedChat FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Actor.ProfileAssociatedChat>(json, (JsonTypeInfo<App.Bsky.Actor.ProfileAssociatedChat>)SourceGenerationContext.Default.AppBskyActorProfileAssociatedChat)!;
        }
    }
}


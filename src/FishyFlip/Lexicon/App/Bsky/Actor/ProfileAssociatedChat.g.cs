// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class ProfileAssociatedChat : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileAssociatedChat"/> class.
        /// </summary>
        /// <param name="allowIncoming">
        /// <br/> Known Values: <br/>
        /// all <br/>
        /// none <br/>
        /// following <br/>
        /// </param>
        public ProfileAssociatedChat(string allowIncoming = default)
        {
            this.AllowIncoming = allowIncoming;
            this.Type = "app.bsky.actor.defs#profileAssociatedChat";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileAssociatedChat"/> class.
        /// </summary>
        public ProfileAssociatedChat()
        {
            this.Type = "app.bsky.actor.defs#profileAssociatedChat";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileAssociatedChat"/> class.
        /// </summary>
        public ProfileAssociatedChat(CBORObject obj)
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
        [JsonRequired]
        public string AllowIncoming { get; set; }

        public const string RecordType = "app.bsky.actor.defs#profileAssociatedChat";

        public static ProfileAssociatedChat FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociatedChat>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ProfileAssociatedChat>)SourceGenerationContext.Default.AppBskyActorProfileAssociatedChat)!;
        }
    }
}


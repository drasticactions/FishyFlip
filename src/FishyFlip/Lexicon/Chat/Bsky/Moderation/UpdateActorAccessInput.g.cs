// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Moderation
{
    public partial class UpdateActorAccessInput : ATObject, ICBOREncodable<UpdateActorAccessInput>, IJsonEncodable<UpdateActorAccessInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActorAccessInput"/> class.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="allowAccess"></param>
        /// <param name="@ref"></param>
        public UpdateActorAccessInput(FishyFlip.Models.ATDid actor = default, bool allowAccess = default, string? @ref = default)
        {
            this.Actor = actor;
            this.AllowAccess = allowAccess;
            this.Ref = @ref;
            this.Type = "chat.bsky.moderation.updateActorAccess#UpdateActorAccessInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActorAccessInput"/> class.
        /// </summary>
        public UpdateActorAccessInput()
        {
            this.Type = "chat.bsky.moderation.updateActorAccess#UpdateActorAccessInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActorAccessInput"/> class.
        /// </summary>
        public UpdateActorAccessInput(CBORObject obj)
        {
            if (obj["actor"] is not null) this.Actor = obj["actor"].ToATDid();
            if (obj["allowAccess"] is not null) this.AllowAccess = obj["allowAccess"].AsBoolean();
            if (obj["ref"] is not null) this.Ref = obj["ref"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the actor.
        /// </summary>
        [JsonPropertyName("actor")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Actor { get; set; }

        /// <summary>
        /// Gets or sets the allowAccess.
        /// </summary>
        [JsonPropertyName("allowAccess")]
        [JsonRequired]
        public bool AllowAccess { get; set; }

        /// <summary>
        /// Gets or sets the ref.
        /// </summary>
        [JsonPropertyName("ref")]
        public string? Ref { get; set; }

        public const string RecordType = "chat.bsky.moderation.updateActorAccess#UpdateActorAccessInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.UpdateActorAccessInput>)SourceGenerationContext.Default.ChatBskyModerationUpdateActorAccessInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.UpdateActorAccessInput>)SourceGenerationContext.Default.ChatBskyModerationUpdateActorAccessInput);
        }

        public static new UpdateActorAccessInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Chat.Bsky.Moderation.UpdateActorAccessInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Chat.Bsky.Moderation.UpdateActorAccessInput>)SourceGenerationContext.Default.ChatBskyModerationUpdateActorAccessInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new UpdateActorAccessInput FromCBORObject(CBORObject obj)
        {
            return new UpdateActorAccessInput(obj);
        }

    }
}


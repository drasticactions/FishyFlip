// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Fm.Teal.Alpha.Actor
{
    public partial class MiniProfileView : ATObject, ICBOREncodable<MiniProfileView>, IJsonEncodable<MiniProfileView>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MiniProfileView"/> class.
        /// </summary>
        /// <param name="did">The decentralized identifier of the actor</param>
        /// <param name="displayName"></param>
        /// <param name="handle"></param>
        /// <param name="avatar">IPLD of the avatar</param>
        public MiniProfileView(string? did = default, string? displayName = default, string? handle = default, string? avatar = default)
        {
            this.Did = did;
            this.DisplayName = displayName;
            this.Handle = handle;
            this.Avatar = avatar;
            this.Type = "fm.teal.alpha.actor.defs#miniProfileView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MiniProfileView"/> class.
        /// </summary>
        public MiniProfileView()
        {
            this.Type = "fm.teal.alpha.actor.defs#miniProfileView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MiniProfileView"/> class.
        /// </summary>
        public MiniProfileView(CBORObject obj)
        {
            if (obj["did"] is not null) this.Did = obj["did"].AsString();
            if (obj["displayName"] is not null) this.DisplayName = obj["displayName"].AsString();
            if (obj["handle"] is not null) this.Handle = obj["handle"].AsString();
            if (obj["avatar"] is not null) this.Avatar = obj["avatar"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the did.
        /// <br/> The decentralized identifier of the actor
        /// </summary>
        [JsonPropertyName("did")]
        public string? Did { get; set; }

        /// <summary>
        /// Gets or sets the displayName.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [JsonPropertyName("handle")]
        public string? Handle { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// <br/> IPLD of the avatar
        /// </summary>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        public const string RecordType = "fm.teal.alpha.actor.defs#miniProfileView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.MiniProfileView>)SourceGenerationContext.Default.FmTealAlphaActorMiniProfileView);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.MiniProfileView>)SourceGenerationContext.Default.FmTealAlphaActorMiniProfileView);
        }

        public static new MiniProfileView FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.MiniProfileView>(json, (JsonTypeInfo<FishyFlip.Lexicon.Fm.Teal.Alpha.Actor.MiniProfileView>)SourceGenerationContext.Default.FmTealAlphaActorMiniProfileView)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new MiniProfileView FromCBORObject(CBORObject obj)
        {
            return new MiniProfileView(obj);
        }

    }
}


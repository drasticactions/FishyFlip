// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class HiddenPostsPref : ATObject, ICBOREncodable<HiddenPostsPref>, IJsonEncodable<HiddenPostsPref>, IParsable<HiddenPostsPref>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HiddenPostsPref"/> class.
        /// </summary>
        /// <param name="items">A list of URIs of posts the account owner has hidden.</param>
        public HiddenPostsPref(List<FishyFlip.Models.ATUri> items = default)
        {
            this.Items = items;
            this.Type = "app.bsky.actor.defs#hiddenPostsPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="HiddenPostsPref"/> class.
        /// </summary>
        public HiddenPostsPref()
        {
            this.Type = "app.bsky.actor.defs#hiddenPostsPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="HiddenPostsPref"/> class.
        /// </summary>
        public HiddenPostsPref(CBORObject obj)
        {
            if (obj["items"] is not null) this.Items = obj["items"].Values.Select(n =>n.ToATUri()!).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the items.
        /// <br/> A list of URIs of posts the account owner has hidden.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<FishyFlip.Models.ATUri> Items { get; set; }

        public const string RecordType = "app.bsky.actor.defs#hiddenPostsPref";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.HiddenPostsPref>)SourceGenerationContext.Default.AppBskyActorHiddenPostsPref);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.HiddenPostsPref>)SourceGenerationContext.Default.AppBskyActorHiddenPostsPref);
        }

        public static new HiddenPostsPref FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.HiddenPostsPref>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.HiddenPostsPref>)SourceGenerationContext.Default.AppBskyActorHiddenPostsPref)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new HiddenPostsPref FromCBORObject(CBORObject obj)
        {
            return new HiddenPostsPref(obj);
        }

        /// <inheritdoc/>
        public static HiddenPostsPref Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<HiddenPostsPref>(s, (JsonTypeInfo<HiddenPostsPref>)SourceGenerationContext.Default.AppBskyActorHiddenPostsPref)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out HiddenPostsPref result)
        {
            result = JsonSerializer.Deserialize<HiddenPostsPref>(s, (JsonTypeInfo<HiddenPostsPref>)SourceGenerationContext.Default.AppBskyActorHiddenPostsPref);
            return result != null;
        }
    }
}


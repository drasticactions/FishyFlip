// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class HiddenPostsPref : ATObject
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
        }

        /// <summary>
        /// Gets or sets the items.
        /// <br/> A list of URIs of posts the account owner has hidden.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<FishyFlip.Models.ATUri> Items { get; set; }

        public const string RecordType = "app.bsky.actor.defs#hiddenPostsPref";

        public static HiddenPostsPref FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.HiddenPostsPref>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.HiddenPostsPref>)SourceGenerationContext.Default.AppBskyActorHiddenPostsPref)!;
        }
    }
}


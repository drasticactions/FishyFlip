// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Collection
{
    public partial class CollectionView : ATObject, ICBOREncodable<CollectionView>, IJsonEncodable<CollectionView>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionView"/> class.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="creator">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="descriptionFacets"></param>
        /// <param name="avatar"></param>
        /// <param name="collectionItemCount"></param>
        /// <param name="labels"></param>
        /// <param name="indexedAt"></param>
        public CollectionView(FishyFlip.Models.ATUri uri = default, string cid = default, FishyFlip.Lexicon.App.Bsky.Actor.ProfileView creator = default, string name = default, string? description = default, List<FishyFlip.Lexicon.App.Bsky.Richtext.Facet>? descriptionFacets = default, string? avatar = default, long? collectionItemCount = default, List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? labels = default, DateTime? indexedAt = default)
        {
            this.Uri = uri;
            this.Cid = cid;
            this.Creator = creator;
            this.Name = name;
            this.Description = description;
            this.DescriptionFacets = descriptionFacets;
            this.Avatar = avatar;
            this.CollectionItemCount = collectionItemCount;
            this.Labels = labels;
            this.IndexedAt = indexedAt;
            this.Type = "blue.moji.collection.defs#collectionView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionView"/> class.
        /// </summary>
        public CollectionView()
        {
            this.Type = "blue.moji.collection.defs#collectionView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionView"/> class.
        /// </summary>
        public CollectionView(CBORObject obj)
        {
            if (obj["uri"] is not null) this.Uri = obj["uri"].ToATUri();
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["creator"] is not null) this.Creator = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(obj["creator"]);
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["descriptionFacets"] is not null) this.DescriptionFacets = obj["descriptionFacets"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Richtext.Facet(n)).ToList();
            if (obj["avatar"] is not null) this.Avatar = obj["avatar"].AsString();
            if (obj["collectionItemCount"] is not null) this.CollectionItemCount = obj["collectionItemCount"].AsInt64Value();
            if (obj["labels"] is not null) this.Labels = obj["labels"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Label.Label(n)).ToList();
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [JsonPropertyName("uri")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri Uri { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        [JsonRequired]
        public string Cid { get; set; }

        /// <summary>
        /// Gets or sets the creator.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </summary>
        [JsonPropertyName("creator")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Actor.ProfileView Creator { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the descriptionFacets.
        /// </summary>
        [JsonPropertyName("descriptionFacets")]
        public List<FishyFlip.Lexicon.App.Bsky.Richtext.Facet>? DescriptionFacets { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the collectionItemCount.
        /// </summary>
        [JsonPropertyName("collectionItemCount")]
        public long? CollectionItemCount { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public List<FishyFlip.Lexicon.Com.Atproto.Label.Label>? Labels { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        public const string RecordType = "blue.moji.collection.defs#collectionView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.CollectionView>)SourceGenerationContext.Default.BlueMojiCollectionCollectionView);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.CollectionView>)SourceGenerationContext.Default.BlueMojiCollectionCollectionView);
        }

        public static new CollectionView FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Moji.Collection.CollectionView>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Moji.Collection.CollectionView>)SourceGenerationContext.Default.BlueMojiCollectionCollectionView)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new CollectionView FromCBORObject(CBORObject obj)
        {
            return new CollectionView(obj);
        }

    }
}


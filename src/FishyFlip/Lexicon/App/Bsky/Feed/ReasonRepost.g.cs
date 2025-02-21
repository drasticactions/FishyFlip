// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class ReasonRepost : ATObject, ICBOREncodable<ReasonRepost>, IJsonEncodable<ReasonRepost>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonRepost"/> class.
        /// </summary>
        /// <param name="by">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic"/> (app.bsky.actor.defs#profileViewBasic)
        /// </param>
        /// <param name="indexedAt"></param>
        public ReasonRepost(FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic by = default, DateTime? indexedAt = default)
        {
            this.By = by;
            this.IndexedAt = indexedAt;
            this.Type = "app.bsky.feed.defs#reasonRepost";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonRepost"/> class.
        /// </summary>
        public ReasonRepost()
        {
            this.Type = "app.bsky.feed.defs#reasonRepost";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReasonRepost"/> class.
        /// </summary>
        public ReasonRepost(CBORObject obj)
        {
            if (obj["by"] is not null) this.By = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic(obj["by"]);
            if (obj["indexedAt"] is not null) this.IndexedAt = obj["indexedAt"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the by.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic"/> (app.bsky.actor.defs#profileViewBasic)
        /// </summary>
        [JsonPropertyName("by")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic By { get; set; }

        /// <summary>
        /// Gets or sets the indexedAt.
        /// </summary>
        [JsonPropertyName("indexedAt")]
        [JsonRequired]
        public DateTime? IndexedAt { get; set; }

        public const string RecordType = "app.bsky.feed.defs#reasonRepost";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.ReasonRepost>)SourceGenerationContext.Default.AppBskyFeedReasonRepost);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.ReasonRepost>)SourceGenerationContext.Default.AppBskyFeedReasonRepost);
        }

        public static new ReasonRepost FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.ReasonRepost>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.ReasonRepost>)SourceGenerationContext.Default.AppBskyFeedReasonRepost)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ReasonRepost FromCBORObject(CBORObject obj)
        {
            return new ReasonRepost(obj);
        }

    }
}


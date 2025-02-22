// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class FeedViewPost : ATObject, ICBOREncodable<FeedViewPost>, IJsonEncodable<FeedViewPost>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedViewPost"/> class.
        /// </summary>
        /// <param name="post">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.PostView"/> (app.bsky.feed.defs#postView)
        /// </param>
        /// <param name="reply">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ReplyRef"/> (app.bsky.feed.defs#replyRef)
        /// </param>
        /// <param name="reason">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ReasonRepost"/> (app.bsky.feed.defs#reasonRepost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ReasonPin"/> (app.bsky.feed.defs#reasonPin) <br/>
        /// </param>
        /// <param name="feedContext">Context provided by feed generator that may be passed back alongside interactions.</param>
        public FeedViewPost(FishyFlip.Lexicon.App.Bsky.Feed.PostView post = default, FishyFlip.Lexicon.App.Bsky.Feed.ReplyRef? reply = default, ATObject? reason = default, string? feedContext = default)
        {
            this.Post = post;
            this.Reply = reply;
            this.Reason = reason;
            this.FeedContext = feedContext;
            this.Type = "app.bsky.feed.defs#feedViewPost";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeedViewPost"/> class.
        /// </summary>
        public FeedViewPost()
        {
            this.Type = "app.bsky.feed.defs#feedViewPost";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeedViewPost"/> class.
        /// </summary>
        public FeedViewPost(CBORObject obj)
        {
            if (obj["post"] is not null) this.Post = new FishyFlip.Lexicon.App.Bsky.Feed.PostView(obj["post"]);
            if (obj["reply"] is not null) this.Reply = new FishyFlip.Lexicon.App.Bsky.Feed.ReplyRef(obj["reply"]);
            if (obj["reason"] is not null) this.Reason = obj["reason"].ToATObject();
            if (obj["feedContext"] is not null) this.FeedContext = obj["feedContext"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the post.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Feed.PostView"/> (app.bsky.feed.defs#postView)
        /// </summary>
        [JsonPropertyName("post")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Feed.PostView Post { get; set; }

        /// <summary>
        /// Gets or sets the reply.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ReplyRef"/> (app.bsky.feed.defs#replyRef)
        /// </summary>
        [JsonPropertyName("reply")]
        public FishyFlip.Lexicon.App.Bsky.Feed.ReplyRef? Reply { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ReasonRepost"/> (app.bsky.feed.defs#reasonRepost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ReasonPin"/> (app.bsky.feed.defs#reasonPin) <br/>
        /// </summary>
        [JsonPropertyName("reason")]
        public ATObject? Reason { get; set; }

        /// <summary>
        /// Gets or sets the feedContext.
        /// <br/> Context provided by feed generator that may be passed back alongside interactions.
        /// </summary>
        [JsonPropertyName("feedContext")]
        public string? FeedContext { get; set; }

        public const string RecordType = "app.bsky.feed.defs#feedViewPost";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.FeedViewPost>)SourceGenerationContext.Default.AppBskyFeedFeedViewPost);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.FeedViewPost>)SourceGenerationContext.Default.AppBskyFeedFeedViewPost);
        }

        public static new FeedViewPost FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.FeedViewPost>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.FeedViewPost>)SourceGenerationContext.Default.AppBskyFeedFeedViewPost)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new FeedViewPost FromCBORObject(CBORObject obj)
        {
            return new FeedViewPost(obj);
        }

    }
}


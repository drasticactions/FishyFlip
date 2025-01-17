// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class ThreadViewPost : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadViewPost"/> class.
        /// </summary>
        /// <param name="post">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.PostView"/> (app.bsky.feed.defs#postView)
        /// </param>
        /// <param name="parent">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost"/> (app.bsky.feed.defs#threadViewPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.NotFoundPost"/> (app.bsky.feed.defs#notFoundPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost"/> (app.bsky.feed.defs#blockedPost) <br/>
        /// </param>
        /// <param name="replies">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost"/> (app.bsky.feed.defs#threadViewPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.NotFoundPost"/> (app.bsky.feed.defs#notFoundPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost"/> (app.bsky.feed.defs#blockedPost) <br/>
        /// </param>
        public ThreadViewPost(FishyFlip.Lexicon.App.Bsky.Feed.PostView post = default, ATObject? parent = default, List<ATObject>? replies = default)
        {
            this.Post = post;
            this.Parent = parent;
            this.Replies = replies;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadViewPost"/> class.
        /// </summary>
        public ThreadViewPost()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadViewPost"/> class.
        /// </summary>
        public ThreadViewPost(CBORObject obj)
        {
            if (obj["post"] is not null) this.Post = new FishyFlip.Lexicon.App.Bsky.Feed.PostView(obj["post"]);
            if (obj["parent"] is not null) this.Parent = obj["parent"].ToATObject();
            if (obj["replies"] is not null) this.Replies = obj["replies"].Values.Select(n =>n.ToATObject()).ToList();
        }

        /// <summary>
        /// Gets or sets the post.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Feed.PostView"/> (app.bsky.feed.defs#postView)
        /// </summary>
        [JsonPropertyName("post")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Feed.PostView Post { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost"/> (app.bsky.feed.defs#threadViewPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.NotFoundPost"/> (app.bsky.feed.defs#notFoundPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost"/> (app.bsky.feed.defs#blockedPost) <br/>
        /// </summary>
        [JsonPropertyName("parent")]
        public ATObject? Parent { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost"/> (app.bsky.feed.defs#threadViewPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.NotFoundPost"/> (app.bsky.feed.defs#notFoundPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost"/> (app.bsky.feed.defs#blockedPost) <br/>
        /// </summary>
        [JsonPropertyName("replies")]
        public List<ATObject>? Replies { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.defs#threadViewPost";

        public const string RecordType = "app.bsky.feed.defs#threadViewPost";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost>)SourceGenerationContext.Default.AppBskyFeedThreadViewPost)!;
        }

        public static ThreadViewPost FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost>)SourceGenerationContext.Default.AppBskyFeedThreadViewPost)!;
        }
    }
}


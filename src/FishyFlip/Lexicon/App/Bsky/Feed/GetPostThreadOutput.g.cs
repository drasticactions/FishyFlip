// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class GetPostThreadOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPostThreadOutput"/> class.
        /// </summary>
        /// <param name="thread">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost"/> (app.bsky.feed.defs#threadViewPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.NotFoundPost"/> (app.bsky.feed.defs#notFoundPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost"/> (app.bsky.feed.defs#blockedPost) <br/>
        /// </param>
        /// <param name="threadgate">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ThreadgateView"/> (app.bsky.feed.defs#threadgateView)
        /// </param>
        public GetPostThreadOutput(ATObject thread = default, FishyFlip.Lexicon.App.Bsky.Feed.ThreadgateView? threadgate = default)
        {
            this.Thread = thread;
            this.Threadgate = threadgate;
            this.Type = "app.bsky.feed.getPostThread#GetPostThreadOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPostThreadOutput"/> class.
        /// </summary>
        public GetPostThreadOutput()
        {
            this.Type = "app.bsky.feed.getPostThread#GetPostThreadOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetPostThreadOutput"/> class.
        /// </summary>
        public GetPostThreadOutput(CBORObject obj)
        {
            if (obj["thread"] is not null) this.Thread = obj["thread"].ToATObject();
            if (obj["threadgate"] is not null) this.Threadgate = new FishyFlip.Lexicon.App.Bsky.Feed.ThreadgateView(obj["threadgate"]);
        }

        /// <summary>
        /// Gets or sets the thread.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ThreadViewPost"/> (app.bsky.feed.defs#threadViewPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.NotFoundPost"/> (app.bsky.feed.defs#notFoundPost) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.BlockedPost"/> (app.bsky.feed.defs#blockedPost) <br/>
        /// </summary>
        [JsonPropertyName("thread")]
        [JsonRequired]
        public ATObject Thread { get; set; }

        /// <summary>
        /// Gets or sets the threadgate.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Feed.ThreadgateView"/> (app.bsky.feed.defs#threadgateView)
        /// </summary>
        [JsonPropertyName("threadgate")]
        public FishyFlip.Lexicon.App.Bsky.Feed.ThreadgateView? Threadgate { get; set; }

        public const string RecordType = "app.bsky.feed.getPostThread#GetPostThreadOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Feed.GetPostThreadOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetPostThreadOutput>)SourceGenerationContext.Default.AppBskyFeedGetPostThreadOutput)!;
        }

        public static GetPostThreadOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.GetPostThreadOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.GetPostThreadOutput>)SourceGenerationContext.Default.AppBskyFeedGetPostThreadOutput)!;
        }
    }
}


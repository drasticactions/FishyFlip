// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class Interaction : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Interaction"/> class.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="@event">
        /// <br/> Known Values: <br/>
        /// requestLess - Request that less content like the given feed item be shown in the feed <br/>
        /// requestMore - Request that more content like the given feed item be shown in the feed <br/>
        /// clickthroughItem - User clicked through to the feed item <br/>
        /// clickthroughAuthor - User clicked through to the author of the feed item <br/>
        /// clickthroughReposter - User clicked through to the reposter of the feed item <br/>
        /// clickthroughEmbed - User clicked through to the embedded content of the feed item <br/>
        /// interactionSeen - Feed item was seen by user <br/>
        /// interactionLike - User liked the feed item <br/>
        /// interactionRepost - User reposted the feed item <br/>
        /// interactionReply - User replied to the feed item <br/>
        /// interactionQuote - User quoted the feed item <br/>
        /// interactionShare - User shared the feed item <br/>
        /// </param>
        /// <param name="feedContext">Context on a feed item that was originally supplied by the feed generator on getFeedSkeleton.</param>
        public Interaction(FishyFlip.Models.ATUri? item = default, string? @event = default, string? feedContext = default)
        {
            this.Item = item;
            this.Event = @event;
            this.FeedContext = feedContext;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Interaction"/> class.
        /// </summary>
        public Interaction()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Interaction"/> class.
        /// </summary>
        public Interaction(CBORObject obj)
        {
            if (obj["item"] is not null) this.Item = obj["item"].ToATUri();
            if (obj["event"] is not null) this.Event = obj["event"].AsString();
            if (obj["feedContext"] is not null) this.FeedContext = obj["feedContext"].AsString();
        }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        [JsonPropertyName("item")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri? Item { get; set; }

        /// <summary>
        /// Gets or sets the event.
        /// <br/> Known Values: <br/>
        /// requestLess - Request that less content like the given feed item be shown in the feed <br/>
        /// requestMore - Request that more content like the given feed item be shown in the feed <br/>
        /// clickthroughItem - User clicked through to the feed item <br/>
        /// clickthroughAuthor - User clicked through to the author of the feed item <br/>
        /// clickthroughReposter - User clicked through to the reposter of the feed item <br/>
        /// clickthroughEmbed - User clicked through to the embedded content of the feed item <br/>
        /// interactionSeen - Feed item was seen by user <br/>
        /// interactionLike - User liked the feed item <br/>
        /// interactionRepost - User reposted the feed item <br/>
        /// interactionReply - User replied to the feed item <br/>
        /// interactionQuote - User quoted the feed item <br/>
        /// interactionShare - User shared the feed item <br/>
        /// </summary>
        [JsonPropertyName("event")]
        public string? Event { get; set; }

        /// <summary>
        /// Gets or sets the feedContext.
        /// <br/> Context on a feed item that was originally supplied by the feed generator on getFeedSkeleton.
        /// </summary>
        [JsonPropertyName("feedContext")]
        public string? FeedContext { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.feed.defs#interaction";

        public const string RecordType = "app.bsky.feed.defs#interaction";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Feed.Interaction>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Interaction>)SourceGenerationContext.Default.AppBskyFeedInteraction)!;
        }

        public static Interaction FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.Interaction>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.Interaction>)SourceGenerationContext.Default.AppBskyFeedInteraction)!;
        }
    }
}


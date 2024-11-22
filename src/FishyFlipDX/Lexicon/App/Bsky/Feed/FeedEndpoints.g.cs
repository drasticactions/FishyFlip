// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Feed
{

    /// <summary>
    /// app.bsky.feed Endpoint Group.
    /// </summary>
    public static class FeedEndpoints
    {

       public const string DescribeFeedGenerator = "/xrpc/app.bsky.feed.describeFeedGenerator";

       public const string GetActorFeeds = "/xrpc/app.bsky.feed.getActorFeeds";

       public const string GetActorLikes = "/xrpc/app.bsky.feed.getActorLikes";

       public const string GetAuthorFeed = "/xrpc/app.bsky.feed.getAuthorFeed";

       public const string GetFeed = "/xrpc/app.bsky.feed.getFeed";

       public const string GetFeedGenerator = "/xrpc/app.bsky.feed.getFeedGenerator";

       public const string GetFeedGenerators = "/xrpc/app.bsky.feed.getFeedGenerators";

       public const string GetFeedSkeleton = "/xrpc/app.bsky.feed.getFeedSkeleton";

       public const string GetLikes = "/xrpc/app.bsky.feed.getLikes";

       public const string GetListFeed = "/xrpc/app.bsky.feed.getListFeed";

       public const string GetPosts = "/xrpc/app.bsky.feed.getPosts";

       public const string GetPostThread = "/xrpc/app.bsky.feed.getPostThread";

       public const string GetQuotes = "/xrpc/app.bsky.feed.getQuotes";

       public const string GetRepostedBy = "/xrpc/app.bsky.feed.getRepostedBy";

       public const string GetSuggestedFeeds = "/xrpc/app.bsky.feed.getSuggestedFeeds";

       public const string GetTimeline = "/xrpc/app.bsky.feed.getTimeline";

       public const string SearchPosts = "/xrpc/app.bsky.feed.searchPosts";

       public const string SendInteractions = "/xrpc/app.bsky.feed.sendInteractions";


        /// <summary>
        /// Get information about a feed generator, including policies and offered feed URIs. Does not require auth; implemented by Feed Generator services (not App View).
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.DescribeFeedGeneratorOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.DescribeFeedGeneratorOutput?>> DescribeFeedGeneratorAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = DescribeFeedGenerator.ToString();
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.DescribeFeedGeneratorOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedDescribeFeedGeneratorOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a list of feeds (feed generator records) created by the actor (in the actor's repo).
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetActorFeedsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetActorFeedsOutput?>> GetActorFeedsAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetActorFeeds.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("actor=" + actor);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetActorFeedsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetActorFeedsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a list of posts liked by an actor. Requires auth, actor must be the requesting account.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetActorLikesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetActorLikesOutput?>> GetActorLikesAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetActorLikes.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("actor=" + actor);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetActorLikesOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetActorLikesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a view of an actor's 'author feed' (post and reposts by the author). Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="filter"></param>
        /// <param name="includePins"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetAuthorFeedOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetAuthorFeedOutput?>> GetAuthorFeedAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, string? filter = default, bool? includePins = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetAuthorFeed.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("actor=" + actor);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            if (filter != null)
            {
                queryStrings.Add("filter=" + filter);
            }

            if (includePins != null)
            {
                queryStrings.Add("includePins=" + includePins);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetAuthorFeedOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetAuthorFeedOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a hydrated feed from an actor's selected feed generator. Implemented by App View.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="feed"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetFeedOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedOutput?>> GetFeedAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri feed, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetFeed.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("feed=" + feed);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetFeedOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get information about a feed generator. Implemented by AppView.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="feed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorOutput?>> GetFeedGeneratorAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri feed, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetFeedGenerator.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("feed=" + feed);

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetFeedGeneratorOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get information about a list of feed generators.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="feeds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorsOutput?>> GetFeedGeneratorsAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATUri> feeds, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetFeedGenerators.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", feeds.Select(n => "feeds=" + n)));

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedGeneratorsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetFeedGeneratorsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a skeleton of a feed provided by a feed generator. Auth is optional, depending on provider requirements, and provides the DID of the requester. Implemented by Feed Generator Service.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="feed"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetFeedSkeletonOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedSkeletonOutput?>> GetFeedSkeletonAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri feed, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetFeedSkeleton.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("feed=" + feed);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetFeedSkeletonOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetFeedSkeletonOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get like records which reference a subject (by AT-URI and CID).
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetLikesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetLikesOutput?>> GetLikesAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri uri, string? cid = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetLikes.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("uri=" + uri);

            if (cid != null)
            {
                queryStrings.Add("cid=" + cid);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetLikesOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetLikesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a feed of recent posts from a list (posts and reposts from any actors on the list). Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="list"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetListFeedOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetListFeedOutput?>> GetListFeedAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri list, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetListFeed.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("list=" + list);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetListFeedOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetListFeedOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Gets post views for a specified list of posts (by AT-URI). This is sometimes referred to as 'hydrating' a 'feed skeleton'.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uris"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetPostsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetPostsOutput?>> GetPostsAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATUri> uris, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetPosts.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", uris.Select(n => "uris=" + n)));

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetPostsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetPostsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get posts in a thread. Does not require auth, but additional metadata and filtering will be applied for authed requests.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uri"></param>
        /// <param name="depth"></param>
        /// <param name="parentHeight"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetPostThreadOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetPostThreadOutput?>> GetPostThreadAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri uri, int? depth = 6, int? parentHeight = 80, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetPostThread.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("uri=" + uri);

            if (depth != null)
            {
                queryStrings.Add("depth=" + depth);
            }

            if (parentHeight != null)
            {
                queryStrings.Add("parentHeight=" + parentHeight);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetPostThreadOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetPostThreadOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a list of quotes for a given post.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetQuotesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetQuotesOutput?>> GetQuotesAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri uri, string? cid = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetQuotes.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("uri=" + uri);

            if (cid != null)
            {
                queryStrings.Add("cid=" + cid);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetQuotesOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetQuotesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a list of reposts for a given post.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetRepostedByOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetRepostedByOutput?>> GetRepostedByAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri uri, string? cid = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRepostedBy.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("uri=" + uri);

            if (cid != null)
            {
                queryStrings.Add("cid=" + cid);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetRepostedByOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetRepostedByOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a list of suggested feeds (feed generators) for the requesting account.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetSuggestedFeedsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetSuggestedFeedsOutput?>> GetSuggestedFeedsAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetSuggestedFeeds.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetSuggestedFeedsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetSuggestedFeedsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a view of the requesting account's home timeline. This is expected to be some form of reverse-chronological feed.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="algorithm"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GetTimelineOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.GetTimelineOutput?>> GetTimelineAsync (this FishyFlip.ATProtocol atp, string? algorithm = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetTimeline.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (algorithm != null)
            {
                queryStrings.Add("algorithm=" + algorithm);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.GetTimelineOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedGetTimelineOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Find posts matching search criteria, returning views of those posts.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="q"></param>
        /// <param name="sort"></param>
        /// <param name="since"></param>
        /// <param name="until"></param>
        /// <param name="mentions"></param>
        /// <param name="author"></param>
        /// <param name="lang"></param>
        /// <param name="domain"></param>
        /// <param name="url"></param>
        /// <param name="tag"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.SearchPostsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.SearchPostsOutput?>> SearchPostsAsync (this FishyFlip.ATProtocol atp, string q, string? sort = default, string? since = default, string? until = default, FishyFlip.Models.ATIdentifier? mentions = default, FishyFlip.Models.ATIdentifier? author = default, string? lang = default, string? domain = default, string? url = default, List<string>? tag = default, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchPosts.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("q=" + q);

            if (sort != null)
            {
                queryStrings.Add("sort=" + sort);
            }

            if (since != null)
            {
                queryStrings.Add("since=" + since);
            }

            if (until != null)
            {
                queryStrings.Add("until=" + until);
            }

            if (mentions != null)
            {
                queryStrings.Add("mentions=" + mentions);
            }

            if (author != null)
            {
                queryStrings.Add("author=" + author);
            }

            if (lang != null)
            {
                queryStrings.Add("lang=" + lang);
            }

            if (domain != null)
            {
                queryStrings.Add("domain=" + domain);
            }

            if (url != null)
            {
                queryStrings.Add("url=" + url);
            }

            if (tag != null)
            {
                queryStrings.Add(string.Join("&", tag.Select(n => "tag=" + n)));
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Feed.SearchPostsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedSearchPostsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Send information about interactions with feed items back to the feed generator that served them.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="interactions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Feed.SendInteractionsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Feed.SendInteractionsOutput?>> SendInteractionsAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Lexicon.App.Bsky.Feed.Interaction> interactions, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SendInteractions.ToString();
            var inputItem = new SendInteractionsInput();
            inputItem.Interactions = interactions;
            return atp.Client.Post<SendInteractionsInput, FishyFlip.Lexicon.App.Bsky.Feed.SendInteractionsOutput?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyFeedSendInteractionsInput!, atp.Options.SourceGenerationContext.AppBskyFeedSendInteractionsOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }

    }
}


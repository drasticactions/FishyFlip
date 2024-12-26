// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Unspecced
{

    /// <summary>
    /// app.bsky.unspecced Endpoint Group.
    /// </summary>
    public static class UnspeccedEndpoints
    {

       public const string GetConfig = "/xrpc/app.bsky.unspecced.getConfig";

       public const string GetPopularFeedGenerators = "/xrpc/app.bsky.unspecced.getPopularFeedGenerators";

       public const string GetSuggestionsSkeleton = "/xrpc/app.bsky.unspecced.getSuggestionsSkeleton";

       public const string GetTaggedSuggestions = "/xrpc/app.bsky.unspecced.getTaggedSuggestions";

       public const string GetTrendingTopics = "/xrpc/app.bsky.unspecced.getTrendingTopics";

       public const string SearchActorsSkeleton = "/xrpc/app.bsky.unspecced.searchActorsSkeleton";

       public const string SearchPostsSkeleton = "/xrpc/app.bsky.unspecced.searchPostsSkeleton";

       public const string SearchStarterPacksSkeleton = "/xrpc/app.bsky.unspecced.searchStarterPacksSkeleton";


        /// <summary>
        /// Get miscellaneous runtime configuration.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Unspecced.GetConfigOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Unspecced.GetConfigOutput?>> GetConfigAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetConfig.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            return atp.Get<FishyFlip.Lexicon.App.Bsky.Unspecced.GetConfigOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyUnspeccedGetConfigOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// An unspecced view of globally popular feed generators.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Unspecced.GetPopularFeedGeneratorsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Unspecced.GetPopularFeedGeneratorsOutput?>> GetPopularFeedGeneratorsAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, string? query = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetPopularFeedGenerators.ToString();
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

            if (query != null)
            {
                queryStrings.Add("query=" + query);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.App.Bsky.Unspecced.GetPopularFeedGeneratorsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyUnspeccedGetPopularFeedGeneratorsOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Get a skeleton of suggested actors. Intended to be called and then hydrated through app.bsky.actor.getSuggestions
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="viewer"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="relativeToDid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestionsSkeletonOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestionsSkeletonOutput?>> GetSuggestionsSkeletonAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid? viewer = default, int? limit = 50, string? cursor = default, FishyFlip.Models.ATDid? relativeToDid = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetSuggestionsSkeleton.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (viewer != null)
            {
                queryStrings.Add("viewer=" + viewer);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            if (relativeToDid != null)
            {
                queryStrings.Add("relativeToDid=" + relativeToDid);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.App.Bsky.Unspecced.GetSuggestionsSkeletonOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyUnspeccedGetSuggestionsSkeletonOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Get a list of suggestions (feeds and users) tagged with categories
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Unspecced.GetTaggedSuggestionsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTaggedSuggestionsOutput?>> GetTaggedSuggestionsAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetTaggedSuggestions.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            return atp.Get<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTaggedSuggestionsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyUnspeccedGetTaggedSuggestionsOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Get a list of trending topics
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="viewer"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendingTopicsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendingTopicsOutput?>> GetTrendingTopicsAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid? viewer = default, int? limit = 10, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetTrendingTopics.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (viewer != null)
            {
                queryStrings.Add("viewer=" + viewer);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.App.Bsky.Unspecced.GetTrendingTopicsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyUnspeccedGetTrendingTopicsOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Backend Actors (profile) search, returns only skeleton.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.BadQueryStringError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="q"></param>
        /// <param name="viewer"></param>
        /// <param name="typeahead"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Unspecced.SearchActorsSkeletonOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Unspecced.SearchActorsSkeletonOutput?>> SearchActorsSkeletonAsync (this FishyFlip.ATProtocol atp, string q, FishyFlip.Models.ATDid? viewer = default, bool? typeahead = default, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchActorsSkeleton.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("q=" + q);

            if (viewer != null)
            {
                queryStrings.Add("viewer=" + viewer);
            }

            if (typeahead != null)
            {
                queryStrings.Add("typeahead=" + (typeahead.Value ? "true" : "false"));
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.App.Bsky.Unspecced.SearchActorsSkeletonOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyUnspeccedSearchActorsSkeletonOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Backend Posts search, returns only skeleton
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.BadQueryStringError"/>  <br/>
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
        /// <param name="viewer"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Unspecced.SearchPostsSkeletonOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Unspecced.SearchPostsSkeletonOutput?>> SearchPostsSkeletonAsync (this FishyFlip.ATProtocol atp, string q, string? sort = default, string? since = default, string? until = default, FishyFlip.Models.ATIdentifier? mentions = default, FishyFlip.Models.ATIdentifier? author = default, string? lang = default, string? domain = default, string? url = default, List<string>? tag = default, FishyFlip.Models.ATDid? viewer = default, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchPostsSkeleton.ToString();
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

            if (viewer != null)
            {
                queryStrings.Add("viewer=" + viewer);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.App.Bsky.Unspecced.SearchPostsSkeletonOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyUnspeccedSearchPostsSkeletonOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Backend Starter Pack search, returns only skeleton.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.BadQueryStringError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="q"></param>
        /// <param name="viewer"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Unspecced.SearchStarterPacksSkeletonOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Unspecced.SearchStarterPacksSkeletonOutput?>> SearchStarterPacksSkeletonAsync (this FishyFlip.ATProtocol atp, string q, FishyFlip.Models.ATDid? viewer = default, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchStarterPacksSkeleton.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("q=" + q);

            if (viewer != null)
            {
                queryStrings.Add("viewer=" + viewer);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.App.Bsky.Unspecced.SearchStarterPacksSkeletonOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyUnspeccedSearchStarterPacksSkeletonOutput!, cancellationToken, headers);
        }

    }
}


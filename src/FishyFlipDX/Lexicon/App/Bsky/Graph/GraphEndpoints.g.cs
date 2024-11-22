// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{

    /// <summary>
    /// app.bsky.graph Endpoint Group.
    /// </summary>
    public static class GraphEndpoints
    {

       public const string GetActorStarterPacks = "/xrpc/app.bsky.graph.getActorStarterPacks";

       public const string GetBlocks = "/xrpc/app.bsky.graph.getBlocks";

       public const string GetFollowers = "/xrpc/app.bsky.graph.getFollowers";

       public const string GetFollows = "/xrpc/app.bsky.graph.getFollows";

       public const string GetKnownFollowers = "/xrpc/app.bsky.graph.getKnownFollowers";

       public const string GetList = "/xrpc/app.bsky.graph.getList";

       public const string GetListBlocks = "/xrpc/app.bsky.graph.getListBlocks";

       public const string GetListMutes = "/xrpc/app.bsky.graph.getListMutes";

       public const string GetLists = "/xrpc/app.bsky.graph.getLists";

       public const string GetMutes = "/xrpc/app.bsky.graph.getMutes";

       public const string GetRelationships = "/xrpc/app.bsky.graph.getRelationships";

       public const string GetStarterPack = "/xrpc/app.bsky.graph.getStarterPack";

       public const string GetStarterPacks = "/xrpc/app.bsky.graph.getStarterPacks";

       public const string GetSuggestedFollowsByActor = "/xrpc/app.bsky.graph.getSuggestedFollowsByActor";

       public const string MuteActor = "/xrpc/app.bsky.graph.muteActor";

       public const string MuteActorList = "/xrpc/app.bsky.graph.muteActorList";

       public const string MuteThread = "/xrpc/app.bsky.graph.muteThread";

       public const string SearchStarterPacks = "/xrpc/app.bsky.graph.searchStarterPacks";

       public const string UnmuteActor = "/xrpc/app.bsky.graph.unmuteActor";

       public const string UnmuteActorList = "/xrpc/app.bsky.graph.unmuteActorList";

       public const string UnmuteThread = "/xrpc/app.bsky.graph.unmuteThread";


        /// <summary>
        /// Get a list of starter packs created by the actor.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetActorStarterPacksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetActorStarterPacksOutput?>> GetActorStarterPacksAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetActorStarterPacks.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetActorStarterPacksOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetActorStarterPacksOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates which accounts the requesting account is currently blocking. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetBlocksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetBlocksOutput?>> GetBlocksAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetBlocks.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetBlocksOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetBlocksOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates accounts which follow a specified account (actor).
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetFollowersOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowersOutput?>> GetFollowersAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetFollowers.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowersOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetFollowersOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates accounts which a specified account (actor) follows.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetFollowsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowsOutput?>> GetFollowsAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetFollows.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetFollowsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates accounts which follow a specified account (actor) and are followed by the viewer.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetKnownFollowersOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetKnownFollowersOutput?>> GetKnownFollowersAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetKnownFollowers.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetKnownFollowersOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetKnownFollowersOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Gets a 'view' (with additional context) of a specified list.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="list"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetListOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetListOutput?>> GetListAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri list, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetList.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetListOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetListOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get mod lists that the requesting account (actor) is blocking. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetListBlocksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetListBlocksOutput?>> GetListBlocksAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetListBlocks.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetListBlocksOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetListBlocksOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates mod lists that the requesting account (actor) currently has muted. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetListMutesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetListMutesOutput?>> GetListMutesAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetListMutes.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetListMutesOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetListMutesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates the lists created by a specified account (actor).
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput?>> GetListsAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetLists.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetListsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates accounts that the requesting account (actor) currently has muted. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetMutesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetMutesOutput?>> GetMutesAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetMutes.ToString();
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
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetMutesOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetMutesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates public relationships between one account, and a list of other accounts. Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="others"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetRelationshipsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetRelationshipsOutput?>> GetRelationshipsAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, List<FishyFlip.Models.ATIdentifier>? others = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRelationships.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("actor=" + actor);

            if (others != null)
            {
                queryStrings.Add(string.Join("&", others.Select(n => "others=" + n)));
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetRelationshipsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetRelationshipsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Gets a view of a starter pack.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="starterPack"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPackOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPackOutput?>> GetStarterPackAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri starterPack, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetStarterPack.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("starterPack=" + starterPack);

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPackOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetStarterPackOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get views for a list of starter packs.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uris"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPacksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPacksOutput?>> GetStarterPacksAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATUri> uris, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetStarterPacks.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", uris.Select(n => "uris=" + n)));

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPacksOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetStarterPacksOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Enumerates follows similar to a given account (actor). Expected use is to recommend additional accounts immediately after following one account.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.GetSuggestedFollowsByActorOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetSuggestedFollowsByActorOutput?>> GetSuggestedFollowsByActorAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetSuggestedFollowsByActor.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("actor=" + actor);

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.GetSuggestedFollowsByActorOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphGetSuggestedFollowsByActorOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Creates a mute relationship for the specified account. Mutes are private in Bluesky. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> MuteActorAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default)
        {
            var endpointUrl = MuteActor.ToString();
            var inputItem = new MuteActorInput();
            inputItem.Actor = actor;
            return atp.Client.Post<MuteActorInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphMuteActorInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Creates a mute relationship for the specified list of accounts. Mutes are private in Bluesky. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="list"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> MuteActorListAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri list, CancellationToken cancellationToken = default)
        {
            var endpointUrl = MuteActorList.ToString();
            var inputItem = new MuteActorListInput();
            inputItem.List = list;
            return atp.Client.Post<MuteActorListInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphMuteActorListInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Mutes a thread preventing notifications from the thread and any of its children. Mutes are private in Bluesky. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="root"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> MuteThreadAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri root, CancellationToken cancellationToken = default)
        {
            var endpointUrl = MuteThread.ToString();
            var inputItem = new MuteThreadInput();
            inputItem.Root = root;
            return atp.Client.Post<MuteThreadInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphMuteThreadInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Find starter packs matching search criteria. Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="q"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Graph.SearchStarterPacksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.SearchStarterPacksOutput?>> SearchStarterPacksAsync (this FishyFlip.ATProtocol atp, string q, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchStarterPacks.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("q=" + q);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Graph.SearchStarterPacksOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphSearchStarterPacksOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Unmutes the specified account. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> UnmuteActorAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UnmuteActor.ToString();
            var inputItem = new UnmuteActorInput();
            inputItem.Actor = actor;
            return atp.Client.Post<UnmuteActorInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphUnmuteActorInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Unmutes the specified list of accounts. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="list"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> UnmuteActorListAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri list, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UnmuteActorList.ToString();
            var inputItem = new UnmuteActorListInput();
            inputItem.List = list;
            return atp.Client.Post<UnmuteActorListInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphUnmuteActorListInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Unmutes the specified thread. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="root"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> UnmuteThreadAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri root, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UnmuteThread.ToString();
            var inputItem = new UnmuteThreadInput();
            inputItem.Root = root;
            return atp.Client.Post<UnmuteThreadInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyGraphUnmuteThreadInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }

    }
}


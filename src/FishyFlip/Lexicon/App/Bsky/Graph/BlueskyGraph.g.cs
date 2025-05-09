// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{

    /// <summary>
    /// app.bsky.graph Endpoint Class.
    /// </summary>
    public sealed class BlueskyGraph
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueskyGraph"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal BlueskyGraph(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Get a list of starter packs created by the actor.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetActorStarterPacksOutput?>> GetActorStarterPacksAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetActorStarterPacksAsync(actor, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Get a list of starter packs created by the actor.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetActorStarterPacksOutputCollection GetActorStarterPacksCollectionAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetActorStarterPacksOutputCollection(atp, actor, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Enumerates which accounts the requesting account is currently blocking. Requires auth.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetBlocksOutput?>> GetBlocksAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetBlocksAsync(limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Enumerates which accounts the requesting account is currently blocking. Requires auth.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetBlocksOutputCollection GetBlocksCollectionAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetBlocksOutputCollection(atp, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Enumerates accounts which follow a specified account (actor).
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowersOutput?>> GetFollowersAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetFollowersAsync(actor, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Enumerates accounts which follow a specified account (actor).
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetFollowersOutputCollection GetFollowersCollectionAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetFollowersOutputCollection(atp, actor, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Enumerates accounts which a specified account (actor) follows.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowsOutput?>> GetFollowsAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetFollowsAsync(actor, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Enumerates accounts which a specified account (actor) follows.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetFollowsOutputCollection GetFollowsCollectionAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetFollowsOutputCollection(atp, actor, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Enumerates accounts which follow a specified account (actor) and are followed by the viewer.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetKnownFollowersOutput?>> GetKnownFollowersAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetKnownFollowersAsync(actor, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Enumerates accounts which follow a specified account (actor) and are followed by the viewer.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetKnownFollowersOutputCollection GetKnownFollowersCollectionAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetKnownFollowersOutputCollection(atp, actor, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Gets a 'view' (with additional context) of a specified list.
        /// </summary>
        /// <param name="list">Reference (AT-URI) of the list record to hydrate.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetListOutput?>> GetListAsync (FishyFlip.Models.ATUri list, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetListAsync(list, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Gets a 'view' (with additional context) of a specified list.
        /// </summary>
        /// <param name="list">Reference (AT-URI) of the list record to hydrate.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetListOutputCollection GetListCollectionAsync (FishyFlip.Models.ATUri list, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetListOutputCollection(atp, list, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Get mod lists that the requesting account (actor) is blocking. Requires auth.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetListBlocksOutput?>> GetListBlocksAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetListBlocksAsync(limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Get mod lists that the requesting account (actor) is blocking. Requires auth.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetListBlocksOutputCollection GetListBlocksCollectionAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetListBlocksOutputCollection(atp, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Enumerates mod lists that the requesting account (actor) currently has muted. Requires auth.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetListMutesOutput?>> GetListMutesAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetListMutesAsync(limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Enumerates mod lists that the requesting account (actor) currently has muted. Requires auth.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetListMutesOutputCollection GetListMutesCollectionAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetListMutesOutputCollection(atp, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Enumerates the lists created by a specified account (actor).
        /// </summary>
        /// <param name="actor">The account (actor) to enumerate lists from.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput?>> GetListsAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetListsAsync(actor, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Enumerates the lists created by a specified account (actor).
        /// </summary>
        /// <param name="actor">The account (actor) to enumerate lists from.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetListsOutputCollection GetListsCollectionAsync (FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetListsOutputCollection(atp, actor, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Enumerates accounts that the requesting account (actor) currently has muted. Requires auth.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetMutesOutput?>> GetMutesAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetMutesAsync(limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Enumerates accounts that the requesting account (actor) currently has muted. Requires auth.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetMutesOutputCollection GetMutesCollectionAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetMutesOutputCollection(atp, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Enumerates public relationships between one account, and a list of other accounts. Does not require auth.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.ActorNotFoundError"/> the primary actor at-identifier could not be resolved <br/>
        /// </summary>
        /// <param name="actor">Primary account requesting relationships for.</param>
        /// <param name="others">List of 'other' accounts to be related back to the primary.</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetRelationshipsOutput?>> GetRelationshipsAsync (FishyFlip.Models.ATIdentifier actor, List<FishyFlip.Models.ATIdentifier>? others = default, CancellationToken cancellationToken = default)
        {
            return atp.GetRelationshipsAsync(actor, others, cancellationToken);
        }


        /// <summary>
        /// Gets a view of a starter pack.
        /// </summary>
        /// <param name="starterPack">Reference (AT-URI) of the starter pack record.</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPackOutput?>> GetStarterPackAsync (FishyFlip.Models.ATUri starterPack, CancellationToken cancellationToken = default)
        {
            return atp.GetStarterPackAsync(starterPack, cancellationToken);
        }


        /// <summary>
        /// Get views for a list of starter packs.
        /// </summary>
        /// <param name="uris"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetStarterPacksOutput?>> GetStarterPacksAsync (List<FishyFlip.Models.ATUri> uris, CancellationToken cancellationToken = default)
        {
            return atp.GetStarterPacksAsync(uris, cancellationToken);
        }


        /// <summary>
        /// Enumerates follows similar to a given account (actor). Expected use is to recommend additional accounts immediately after following one account.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.GetSuggestedFollowsByActorOutput?>> GetSuggestedFollowsByActorAsync (FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default)
        {
            return atp.GetSuggestedFollowsByActorAsync(actor, cancellationToken);
        }


        /// <summary>
        /// Creates a mute relationship for the specified account. Mutes are private in Bluesky. Requires auth.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> MuteActorAsync (FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default)
        {
            return atp.MuteActorAsync(actor, cancellationToken);
        }


        /// <summary>
        /// Creates a mute relationship for the specified list of accounts. Mutes are private in Bluesky. Requires auth.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> MuteActorListAsync (FishyFlip.Models.ATUri list, CancellationToken cancellationToken = default)
        {
            return atp.MuteActorListAsync(list, cancellationToken);
        }


        /// <summary>
        /// Mutes a thread preventing notifications from the thread and any of its children. Mutes are private in Bluesky. Requires auth.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> MuteThreadAsync (FishyFlip.Models.ATUri root, CancellationToken cancellationToken = default)
        {
            return atp.MuteThreadAsync(root, cancellationToken);
        }


        /// <summary>
        /// Find starter packs matching search criteria. Does not require auth.
        /// </summary>
        /// <param name="q">Search query string. Syntax, phrase, boolean, and faceting is unspecified, but Lucene query syntax is recommended.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Graph.SearchStarterPacksOutput?>> SearchStarterPacksAsync (string q, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.SearchStarterPacksAsync(q, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Find starter packs matching search criteria. Does not require auth.
        /// </summary>
        /// <param name="q">Search query string. Syntax, phrase, boolean, and faceting is unspecified, but Lucene query syntax is recommended.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public SearchStarterPacksOutputCollection SearchStarterPacksCollectionAsync (string q, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new SearchStarterPacksOutputCollection(atp, q, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Unmutes the specified account. Requires auth.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> UnmuteActorAsync (FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default)
        {
            return atp.UnmuteActorAsync(actor, cancellationToken);
        }


        /// <summary>
        /// Unmutes the specified list of accounts. Requires auth.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> UnmuteActorListAsync (FishyFlip.Models.ATUri list, CancellationToken cancellationToken = default)
        {
            return atp.UnmuteActorListAsync(list, cancellationToken);
        }


        /// <summary>
        /// Unmutes the specified thread. Requires auth.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> UnmuteThreadAsync (FishyFlip.Models.ATUri root, CancellationToken cancellationToken = default)
        {
            return atp.UnmuteThreadAsync(root, cancellationToken);
        }

    }
}


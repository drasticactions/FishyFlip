// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Sync
{

    /// <summary>
    /// com.atproto.sync Endpoint Group.
    /// </summary>
    public static class SyncEndpoints
    {

       public const string GetBlob = "/xrpc/com.atproto.sync.getBlob";

       public const string GetBlocks = "/xrpc/com.atproto.sync.getBlocks";

       public const string GetLatestCommit = "/xrpc/com.atproto.sync.getLatestCommit";

       public const string GetRecord = "/xrpc/com.atproto.sync.getRecord";

       public const string GetRepo = "/xrpc/com.atproto.sync.getRepo";

       public const string GetRepoStatus = "/xrpc/com.atproto.sync.getRepoStatus";

       public const string ListBlobs = "/xrpc/com.atproto.sync.listBlobs";

       public const string ListRepos = "/xrpc/com.atproto.sync.listRepos";

       public const string ListReposByCollection = "/xrpc/com.atproto.sync.listReposByCollection";

       public const string RequestCrawl = "/xrpc/com.atproto.sync.requestCrawl";


        /// <summary>
        /// Get a blob associated with a given account. Returns the full blob as originally uploaded. Does not require auth; implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.BlobNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoTakendownError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoSuspendedError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoDeactivatedError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="did">The DID of the account.</param>
        /// <param name="cid">The CID of the blob to fetch</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="byte[]?"/></returns>
        public static Task<Result<byte[]?>> GetBlobAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, string cid, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetBlob.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            queryStrings.Add("cid=" + cid);

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.GetBlob(endpointUrl, cancellationToken);
        }


        /// <summary>
        /// Get data blocks from a given repo, by CID. For example, intermediate MST nodes, or records. Does not require auth; implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.BlockNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoTakendownError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoSuspendedError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoDeactivatedError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="did">The DID of the repo.</param>
        /// <param name="cids"></param>
        /// <param name="onDecoded"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="CarResponse?"/></returns>
        public static Task<Result<CarResponse?>> GetBlocksAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, List<string> cids, OnCarDecoded? onDecoded = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetBlocks.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            queryStrings.Add(string.Join("&", cids.Select(n => "cids=" + n)));

            if (onDecoded != null)
            {
                queryStrings.Add("onDecoded=" + onDecoded);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.GetCarAsync(endpointUrl, cancellationToken, onDecoded);
        }


        /// <summary>
        /// Get the current commit CID & revision of the specified repo. Does not require auth.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RepoNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoTakendownError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoSuspendedError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoDeactivatedError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="did">The DID of the repo.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Sync.GetLatestCommitOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Sync.GetLatestCommitOutput?>> GetLatestCommitAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetLatestCommit.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.GetLatestCommitOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoSyncGetLatestCommitOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Get data blocks needed to prove the existence or non-existence of record in the current version of repo. Does not require auth.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RecordNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoTakendownError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoSuspendedError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoDeactivatedError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="did">The DID of the repo.</param>
        /// <param name="collection"></param>
        /// <param name="rkey">Record Key</param>
        /// <param name="onDecoded"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="CarResponse?"/></returns>
        public static Task<Result<CarResponse?>> GetRecordAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, string collection, string rkey, OnCarDecoded? onDecoded = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRecord.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            queryStrings.Add("collection=" + collection);

            queryStrings.Add("rkey=" + rkey);

            if (onDecoded != null)
            {
                queryStrings.Add("onDecoded=" + onDecoded);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.GetCarAsync(endpointUrl, cancellationToken, onDecoded);
        }


        /// <summary>
        /// Download a repository export as CAR file. Optionally only a 'diff' since a previous revision. Does not require auth; implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RepoNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoTakendownError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoSuspendedError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoDeactivatedError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="did">The DID of the repo.</param>
        /// <param name="since">The revision ('rev') of the repo to create a diff from.</param>
        /// <param name="onDecoded"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="CarResponse?"/></returns>
        public static Task<Result<CarResponse?>> GetRepoAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, string? since = default, OnCarDecoded? onDecoded = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRepo.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            if (since != null)
            {
                queryStrings.Add("since=" + since);
            }

            if (onDecoded != null)
            {
                queryStrings.Add("onDecoded=" + onDecoded);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.GetCarAsync(endpointUrl, cancellationToken, onDecoded);
        }


        /// <summary>
        /// Get the hosting status for a repository, on this server. Expected to be implemented by PDS and Relay.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RepoNotFoundError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="did">The DID of the repo.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Sync.GetRepoStatusOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Sync.GetRepoStatusOutput?>> GetRepoStatusAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRepoStatus.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.GetRepoStatusOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoSyncGetRepoStatusOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// List blob CIDs for an account, since some repo revision. Does not require auth; implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RepoNotFoundError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoTakendownError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoSuspendedError"/>  <br/>
        /// <see cref="FishyFlip.Lexicon.RepoDeactivatedError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="did">The DID of the repo.</param>
        /// <param name="since">Optional revision of the repo to list blobs since.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Sync.ListBlobsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Sync.ListBlobsOutput?>> ListBlobsAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, string? since = default, int? limit = 500, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ListBlobs.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            if (since != null)
            {
                queryStrings.Add("since=" + since);
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
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.ListBlobsOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoSyncListBlobsOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Enumerates all the DID, rev, and commit CID for all repos hosted by this service. Does not require auth; implemented by PDS and Relay.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Sync.ListReposOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Sync.ListReposOutput?>> ListReposAsync (this FishyFlip.ATProtocol atp, int? limit = 500, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ListRepos.ToString();
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

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.ListReposOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoSyncListReposOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Enumerates all the DIDs which have records with the given collection NSID.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="collection"></param>
        /// <param name="limit">Maximum size of response set. Recommend setting a large maximum (1000+) when enumerating large DID lists.</param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Sync.ListReposByCollectionOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Sync.ListReposByCollectionOutput?>> ListReposByCollectionAsync (this FishyFlip.ATProtocol atp, string collection, int? limit = 500, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ListReposByCollection.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("collection=" + collection);

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
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.ListReposByCollectionOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoSyncListReposByCollectionOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Request a service to persistently crawl hosted repos. Expected use is new PDS instances declaring their existence to Relays. Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="hostname">Hostname of the current service (eg, PDS) that is requesting to be crawled.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> RequestCrawlAsync (this FishyFlip.ATProtocol atp, string hostname, CancellationToken cancellationToken = default)
        {
            var endpointUrl = RequestCrawl.ToString();
            var headers = new Dictionary<string, string>();
            var inputItem = new RequestCrawlInput();
            inputItem.Hostname = hostname;
            return atp.Post<RequestCrawlInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoSyncRequestCrawlInput!, atp.Options.SourceGenerationContext.Success!, inputItem, cancellationToken, headers);
        }

    }
}


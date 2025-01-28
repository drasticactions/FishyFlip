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

       public const string NotifyOfUpdate = "/xrpc/com.atproto.sync.notifyOfUpdate";

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
        /// <param name="did"></param>
        /// <param name="cid"></param>
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
        /// <param name="did"></param>
        /// <param name="cids"></param>
        /// <param name="onDecoded"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> GetBlocksAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, List<string> cids, OnCarDecoded onDecoded, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetBlocks.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            queryStrings.Add(string.Join("&", cids.Select(n => "cids=" + n)));

            queryStrings.Add("onDecoded=" + onDecoded);

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
        /// <param name="did"></param>
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
            JsonTypeInfo<Com.Atproto.Sync.GetLatestCommitOutput> jsonTypeInfo = (JsonTypeInfo<Com.Atproto.Sync.GetLatestCommitOutput>)atp.Options.SourceGenerationContext.GetTypeInfo(typeof(Com.Atproto.Sync.GetLatestCommitOutput), atp.Options.JsonSerializerOptions)!;
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.GetLatestCommitOutput>(endpointUrl, jsonTypeInfo, cancellationToken, headers);
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
        /// <param name="did"></param>
        /// <param name="collection"></param>
        /// <param name="rkey"></param>
        /// <param name="onDecoded"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> GetRecordAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, string collection, string rkey, OnCarDecoded onDecoded, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRecord.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            queryStrings.Add("collection=" + collection);

            queryStrings.Add("rkey=" + rkey);

            queryStrings.Add("onDecoded=" + onDecoded);

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
        /// <param name="did"></param>
        /// <param name="onDecoded"></param>
        /// <param name="since"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> GetRepoAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, OnCarDecoded onDecoded, string? since = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRepo.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            queryStrings.Add("onDecoded=" + onDecoded);

            if (since != null)
            {
                queryStrings.Add("since=" + since);
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
        /// <param name="did"></param>
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
            JsonTypeInfo<Com.Atproto.Sync.GetRepoStatusOutput> jsonTypeInfo = (JsonTypeInfo<Com.Atproto.Sync.GetRepoStatusOutput>)atp.Options.SourceGenerationContext.GetTypeInfo(typeof(Com.Atproto.Sync.GetRepoStatusOutput), atp.Options.JsonSerializerOptions)!;
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.GetRepoStatusOutput>(endpointUrl, jsonTypeInfo, cancellationToken, headers);
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
        /// <param name="did"></param>
        /// <param name="since"></param>
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
            JsonTypeInfo<Com.Atproto.Sync.ListBlobsOutput> jsonTypeInfo = (JsonTypeInfo<Com.Atproto.Sync.ListBlobsOutput>)atp.Options.SourceGenerationContext.GetTypeInfo(typeof(Com.Atproto.Sync.ListBlobsOutput), atp.Options.JsonSerializerOptions)!;
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.ListBlobsOutput>(endpointUrl, jsonTypeInfo, cancellationToken, headers);
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
            JsonTypeInfo<Com.Atproto.Sync.ListReposOutput> jsonTypeInfo = (JsonTypeInfo<Com.Atproto.Sync.ListReposOutput>)atp.Options.SourceGenerationContext.GetTypeInfo(typeof(Com.Atproto.Sync.ListReposOutput), atp.Options.JsonSerializerOptions)!;
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Sync.ListReposOutput>(endpointUrl, jsonTypeInfo, cancellationToken, headers);
        }


        /// <summary>
        /// Notify a crawling service of a recent update, and that crawling should resume. Intended use is after a gap between repo stream events caused the crawling service to disconnect. Does not require auth; implemented by Relay.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="hostname"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> NotifyOfUpdateAsync (this FishyFlip.ATProtocol atp, string hostname, CancellationToken cancellationToken = default)
        {
            var endpointUrl = NotifyOfUpdate.ToString();
            var headers = new Dictionary<string, string>();
            var inputItem = new NotifyOfUpdateInput();
            inputItem.Hostname = hostname;
            JsonTypeInfo<Com.Atproto.Sync.NotifyOfUpdateInput> jsonTypeInfo = (JsonTypeInfo<Com.Atproto.Sync.NotifyOfUpdateInput>)atp.Options.SourceGenerationContext.GetTypeInfo(typeof(Com.Atproto.Sync.NotifyOfUpdateInput), atp.Options.JsonSerializerOptions)!;
            JsonTypeInfo<Success> jsonTypeInfo2 = (JsonTypeInfo<Success>)atp.Options.SourceGenerationContext.GetTypeInfo(typeof(Success), atp.Options.JsonSerializerOptions)!;
            return atp.Post<NotifyOfUpdateInput, Success?>(endpointUrl, jsonTypeInfo, jsonTypeInfo2, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Request a service to persistently crawl hosted repos. Expected use is new PDS instances declaring their existence to Relays. Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="hostname"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> RequestCrawlAsync (this FishyFlip.ATProtocol atp, string hostname, CancellationToken cancellationToken = default)
        {
            var endpointUrl = RequestCrawl.ToString();
            var headers = new Dictionary<string, string>();
            var inputItem = new RequestCrawlInput();
            inputItem.Hostname = hostname;
            JsonTypeInfo<Com.Atproto.Sync.RequestCrawlInput> jsonTypeInfo = (JsonTypeInfo<Com.Atproto.Sync.RequestCrawlInput>)atp.Options.SourceGenerationContext.GetTypeInfo(typeof(Com.Atproto.Sync.RequestCrawlInput), atp.Options.JsonSerializerOptions)!;
            JsonTypeInfo<Success> jsonTypeInfo2 = (JsonTypeInfo<Success>)atp.Options.SourceGenerationContext.GetTypeInfo(typeof(Success), atp.Options.JsonSerializerOptions)!;
            return atp.Post<RequestCrawlInput, Success?>(endpointUrl, jsonTypeInfo, jsonTypeInfo2, inputItem, cancellationToken, headers);
        }

    }
}


// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{

    /// <summary>
    /// com.atproto.repo Endpoint Group.
    /// </summary>
    public static class RepoEndpoints
    {

       public const string ApplyWrites = "/xrpc/com.atproto.repo.applyWrites";

       public const string CreateRecord = "/xrpc/com.atproto.repo.createRecord";

       public const string DeleteRecord = "/xrpc/com.atproto.repo.deleteRecord";

       public const string DescribeRepo = "/xrpc/com.atproto.repo.describeRepo";

       public const string GetRecord = "/xrpc/com.atproto.repo.getRecord";

       public const string ImportRepo = "/xrpc/com.atproto.repo.importRepo";

       public const string ListMissingBlobs = "/xrpc/com.atproto.repo.listMissingBlobs";

       public const string ListRecords = "/xrpc/com.atproto.repo.listRecords";

       public const string PutRecord = "/xrpc/com.atproto.repo.putRecord";

       public const string UploadBlob = "/xrpc/com.atproto.repo.uploadBlob";


        /// <summary>
        /// Apply a batch transaction of repository creates, updates, and deletes. Requires auth, implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.InvalidSwapError"/> Indicates that the 'swapCommit' parameter did not match current commit. <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="writes"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.ApplyWritesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.ApplyWritesOutput?>> ApplyWritesAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, List<ATObject> writes, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ApplyWrites.ToString();
            var inputItem = new ApplyWritesInput();
            inputItem.Repo = repo;
            inputItem.Writes = writes;
            inputItem.Validate = validate;
            inputItem.SwapCommit = swapCommit;
            return atp.Post<ApplyWritesInput, FishyFlip.Lexicon.Com.Atproto.Repo.ApplyWritesOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoApplyWritesInput!, atp.Options.SourceGenerationContext.ComAtprotoRepoApplyWritesOutput!, inputItem, cancellationToken);
        }


        /// <summary>
        /// Create a single new repository record. Requires auth, implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.InvalidSwapError"/> Indicates that 'swapCommit' didn't match current repo commit. <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.CreateRecordOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.CreateRecordOutput?>> CreateRecordAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string collection, ATObject record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = CreateRecord.ToString();
            var inputItem = new CreateRecordInput();
            inputItem.Repo = repo;
            inputItem.Collection = collection;
            inputItem.Record = record;
            inputItem.Rkey = rkey;
            inputItem.Validate = validate;
            inputItem.SwapCommit = swapCommit;
            return atp.Post<CreateRecordInput, FishyFlip.Lexicon.Com.Atproto.Repo.CreateRecordOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoCreateRecordInput!, atp.Options.SourceGenerationContext.ComAtprotoRepoCreateRecordOutput!, inputItem, cancellationToken);
        }


        /// <summary>
        /// Delete a repository record, or ensure it doesn't exist. Requires auth, implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.InvalidSwapError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="rkey"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.DeleteRecordOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.DeleteRecordOutput?>> DeleteRecordAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string collection, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = DeleteRecord.ToString();
            var inputItem = new DeleteRecordInput();
            inputItem.Repo = repo;
            inputItem.Collection = collection;
            inputItem.Rkey = rkey;
            inputItem.SwapRecord = swapRecord;
            inputItem.SwapCommit = swapCommit;
            return atp.Post<DeleteRecordInput, FishyFlip.Lexicon.Com.Atproto.Repo.DeleteRecordOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoDeleteRecordInput!, atp.Options.SourceGenerationContext.ComAtprotoRepoDeleteRecordOutput!, inputItem, cancellationToken);
        }


        /// <summary>
        /// Get information about an account and repository, including the list of collections. Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.DescribeRepoOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.DescribeRepoOutput?>> DescribeRepoAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, CancellationToken cancellationToken = default)
        {
            var endpointUrl = DescribeRepo.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("repo=" + repo);

            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Repo.DescribeRepoOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoDescribeRepoOutput!, cancellationToken);
        }


        /// <summary>
        /// Get a single record from a repository. Does not require auth.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RecordNotFoundError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.GetRecordOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.GetRecordOutput?>> GetRecordAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string collection, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRecord.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("repo=" + repo);

            queryStrings.Add("collection=" + collection);

            queryStrings.Add("rkey=" + rkey);

            if (cid != null)
            {
                queryStrings.Add("cid=" + cid);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Repo.GetRecordOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoGetRecordOutput!, cancellationToken);
        }


        /// <summary>
        /// Import a repo in the form of a CAR file. Requires Content-Length HTTP header to be set.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> ImportRepoAsync (this FishyFlip.ATProtocol atp, StreamContent content, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ImportRepo.ToString();
            return atp.Post<Success?>(endpointUrl, atp.Options.SourceGenerationContext.Success!, content, cancellationToken);
        }


        /// <summary>
        /// Returns a list of missing blobs for the requesting account. Intended to be used in the account migration flow.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.ListMissingBlobsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.ListMissingBlobsOutput?>> ListMissingBlobsAsync (this FishyFlip.ATProtocol atp, int? limit = 500, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ListMissingBlobs.ToString();
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
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Repo.ListMissingBlobsOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoListMissingBlobsOutput!, cancellationToken);
        }


        /// <summary>
        /// List a range of records in a repository, matching a specific collection. Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.ListRecordsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.ListRecordsOutput?>> ListRecordsAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string collection, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ListRecords.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("repo=" + repo);

            queryStrings.Add("collection=" + collection);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            if (reverse != null)
            {
                queryStrings.Add("reverse=" + reverse);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Repo.ListRecordsOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoListRecordsOutput!, cancellationToken);
        }


        /// <summary>
        /// Write a repository record, creating or updating it as needed. Requires auth, implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.InvalidSwapError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="rkey"></param>
        /// <param name="record"></param>
        /// <param name="validate"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordOutput?>> PutRecordAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string collection, string rkey, ATObject record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = PutRecord.ToString();
            var inputItem = new PutRecordInput();
            inputItem.Repo = repo;
            inputItem.Collection = collection;
            inputItem.Rkey = rkey;
            inputItem.Record = record;
            inputItem.Validate = validate;
            inputItem.SwapRecord = swapRecord;
            inputItem.SwapCommit = swapCommit;
            return atp.Post<PutRecordInput, FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoPutRecordInput!, atp.Options.SourceGenerationContext.ComAtprotoRepoPutRecordOutput!, inputItem, cancellationToken);
        }


        /// <summary>
        /// Upload a new blob, to be referenced from a repository record. The blob will be deleted if it is not referenced within a time window (eg, minutes). Blob restrictions (mimetype, size, etc) are enforced when the reference is created. Requires auth, implemented by PDS.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.UploadBlobOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.UploadBlobOutput?>> UploadBlobAsync (this FishyFlip.ATProtocol atp, StreamContent content, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UploadBlob.ToString();
            return atp.Post<FishyFlip.Lexicon.Com.Atproto.Repo.UploadBlobOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoRepoUploadBlobOutput!, content, cancellationToken);
        }

    }
}


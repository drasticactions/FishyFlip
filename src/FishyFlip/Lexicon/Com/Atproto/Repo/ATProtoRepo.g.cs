// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Repo
{

    /// <summary>
    /// com.atproto.repo Endpoint Class.
    /// </summary>
    public sealed class ATProtoRepo
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ATProtoRepo"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal ATProtoRepo(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Apply a batch transaction of repository creates, updates, and deletes. Requires auth, implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.InvalidSwapError"/> Indicates that the 'swapCommit' parameter did not match current commit. <br/>
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="writes"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.ApplyWritesOutput?>> ApplyWritesAsync (FishyFlip.Models.ATIdentifier repo, List<ATObject> writes, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ApplyWritesAsync(repo, writes, validate, swapCommit, cancellationToken);
        }


        /// <summary>
        /// Create a single new repository record. Requires auth, implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.InvalidSwapError"/> Indicates that 'swapCommit' didn't match current repo commit. <br/>
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.CreateRecordOutput?>> CreateRecordAsync (FishyFlip.Models.ATIdentifier repo, string collection, ATObject record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.CreateRecordAsync(repo, collection, record, rkey, validate, swapCommit, cancellationToken);
        }


        /// <summary>
        /// Delete a repository record, or ensure it doesn't exist. Requires auth, implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.InvalidSwapError"/>  <br/>
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="rkey"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.DeleteRecordOutput?>> DeleteRecordAsync (FishyFlip.Models.ATIdentifier repo, string collection, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.DeleteRecordAsync(repo, collection, rkey, swapRecord, swapCommit, cancellationToken);
        }


        /// <summary>
        /// Get information about an account and repository, including the list of collections. Does not require auth.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.DescribeRepoOutput?>> DescribeRepoAsync (FishyFlip.Models.ATIdentifier repo, CancellationToken cancellationToken = default)
        {
            return atp.DescribeRepoAsync(repo, cancellationToken);
        }


        /// <summary>
        /// Get a single record from a repository. Does not require auth.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RecordNotFoundError"/>  <br/>
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.GetRecordOutput?>> GetRecordAsync (FishyFlip.Models.ATIdentifier repo, string collection, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.GetRecordAsync(repo, collection, rkey, cid, cancellationToken);
        }


        /// <summary>
        /// Import a repo in the form of a CAR file. Requires Content-Length HTTP header to be set.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> ImportRepoAsync (StreamContent content, CancellationToken cancellationToken = default)
        {
            return atp.ImportRepoAsync(content, cancellationToken);
        }


        /// <summary>
        /// Returns a list of missing blobs for the requesting account. Intended to be used in the account migration flow.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.ListMissingBlobsOutput?>> ListMissingBlobsAsync (int? limit = 500, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.ListMissingBlobsAsync(limit, cursor, cancellationToken);
        }


        /// <summary>
        /// List a range of records in a repository, matching a specific collection. Does not require auth.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.ListRecordsOutput?>> ListRecordsAsync (FishyFlip.Models.ATIdentifier repo, string collection, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ListRecordsAsync(repo, collection, limit, cursor, reverse, cancellationToken);
        }


        /// <summary>
        /// Write a repository record, creating or updating it as needed. Requires auth, implemented by PDS.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.InvalidSwapError"/>  <br/>
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="rkey"></param>
        /// <param name="record"></param>
        /// <param name="validate"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.PutRecordOutput?>> PutRecordAsync (FishyFlip.Models.ATIdentifier repo, string collection, string rkey, ATObject record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.PutRecordAsync(repo, collection, rkey, record, validate, swapRecord, swapCommit, cancellationToken);
        }


        /// <summary>
        /// Upload a new blob, to be referenced from a repository record. The blob will be deleted if it is not referenced within a time window (eg, minutes). Blob restrictions (mimetype, size, etc) are enforced when the reference is created. Requires auth, implemented by PDS.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Repo.UploadBlobOutput?>> UploadBlobAsync (StreamContent content, CancellationToken cancellationToken = default)
        {
            return atp.UploadBlobAsync(content, cancellationToken);
        }

    }
}


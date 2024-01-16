// <copyright file="ATProtoSync.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// AT Proto Sync.
/// </summary>
public sealed class ATProtoSync
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtoSync"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal ATProtoSync(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Get a Blob.
    /// </summary>
    /// <param name="did"><see cref="ATDid"/>.</param>
    /// <param name="cid"><see cref="Cid"/>.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of Blob.</returns>
    public async Task<Result<Blob?>> GetBlobAsync(ATDid did, Cid cid, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.ATProtoSync.GetBlob}?did={did}&cid={cid}";
        return await this.Client.GetBlob(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Get Head of Repo.
    /// </summary>
    /// <param name="did"><see cref="ATDid"/>.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of Head.</returns>
    [Obsolete("Deprecated in favor of GetLatestCommitAsync")]
    public Task<Result<Head?>> GetHeadAsync(ATDid did, CancellationToken cancellationToken = default)
        => this.Client.Get<Head>(
            $"{Constants.Urls.ATProtoSync.GetHead}?did={did}",
            this.Options.JsonSerializerOptions,
            cancellationToken,
            this.Options.Logger);

    /// <summary>
    /// Get latest commit for a repo.
    /// </summary>
    /// <param name="did"><see cref="ATDid"/>.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of LatestCommit.</returns>
    public Task<Result<LatestCommit?>> GetLatestCommitAsync(ATDid did, CancellationToken cancellationToken = default)
    => this.Client.Get<LatestCommit>(
        $"{Constants.Urls.ATProtoSync.GetLatestCommit}?did={did}",
        this.Options.JsonSerializerOptions,
        cancellationToken,
        this.Options.Logger);

    /// <summary>
    /// Notify of update for given hostname.
    /// </summary>
    /// <param name="hostname">Hostname.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of Success.</returns>
    public Task<Result<Success?>> NotifyOfUpdateAsync(string hostname, CancellationToken cancellationToken = default)
        => this.Client.Get<Success>($"{Constants.Urls.ATProtoSync.NotifyOfUpdate}?hostname={hostname}", this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    /// <summary>
    /// Request crawl for given hostname.
    /// </summary>
    /// <param name="hostname">Hostname.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of Success.</returns>
    public Task<Result<Success?>> RequestCrawlAsync(string hostname, CancellationToken cancellationToken = default)
        => this.Client.Get<Success>($"{Constants.Urls.ATProtoSync.RequestCrawl}?hostname={hostname}", this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    /// <summary>
    /// Get Repo.
    /// Uses OnCarDecoded to decode CAR file while downloading.
    /// </summary>
    /// <param name="repo"><see cref="ATDid"/> of repo.</param>
    /// <param name="onDecoded">Callback method for decoding CAR file.</param>
    /// <param name="since">Optional Since value.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of Success.</returns>
    public Task<Result<Success?>> GetRepoAsync(ATDid repo, OnCarDecoded onDecoded, string? since = default, CancellationToken cancellationToken = default)
    {
        var url = since is not null
            ? $"{Constants.Urls.ATProtoSync.GetRepo}?did={repo}&since={since}"
            : $"{Constants.Urls.ATProtoSync.GetRepo}?did={repo}";
        return this.Client.GetCarAsync(
            url,
            this.Options.JsonSerializerOptions,
            cancellationToken,
            this.Options.Logger,
            onDecoded);
    }

    /// <summary>
    /// Download Repo as CAR file.
    /// </summary>
    /// <param name="repo">Repo.</param>
    /// <param name="path">Path to file.</param>
    /// <param name="filename">Filename.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Result of Success.</returns>
    public Task<Result<Success?>> DownloadRepoAsync(ATDid repo, string? path = default, string? filename = default, CancellationToken cancellationToken = default)
    {
        filename ??= $"{repo}-repo.car";
        return this.Client.DownloadCarAsync(
            $"{Constants.Urls.ATProtoSync.GetRepo}?did={repo}",
            path ?? Directory.GetCurrentDirectory(),
            filename,
            this.Options.JsonSerializerOptions,
            cancellationToken,
            this.Options.Logger);
    }

    /// <summary>
    /// Get Commit Path.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="latest">Latest Cid Commit.</param>
    /// <param name="earliest">Earliest Cid Commit.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>result of CommitPath.</returns>
    [Obsolete("Deprecated in Repo V3. This should no longer work.")]
    public async Task<Result<CommitPath?>> GetCommitPathAsync(ATDid did, Cid? latest = default, Cid? earliest = default, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetCommitPath}&did={did}";
        if (latest is not null)
        {
            url += $"&latest={latest}";
        }

        if (earliest is not null)
        {
            url += $"&earliest={earliest}";
        }

        return await this.Client.Get<CommitPath>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Get Blocks.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="commits">Array of Cid Commits.</param>
    /// <param name="onDecoded">OnCarDecoded callback.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Blocks.</returns>
    public Task<Result<Success?>> GetBlocksAsync(ATDid did, Cid[] commits, OnCarDecoded onDecoded, CancellationToken cancellationToken = default)
    {
        var commitList = string.Join("&", commits.Select(n => $"cids={n}"));
        var url = $"{Constants.Urls.ATProtoSync.GetBlocks}?did={did}&{commitList}";

        return this.Client.GetCarAsync(
            url,
            this.Options.JsonSerializerOptions,
            cancellationToken,
            this.Options.Logger,
            onDecoded);
    }

    /// <summary>
    /// Download Blocks as CAR file.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="commits">Array of Cid Commits.</param>
    /// <param name="path">Path to file.</param>
    /// <param name="filename">Filename.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Blocks.</returns>
    public Task<Result<Success?>> DownloadBlocksAsync(ATDid did, Cid[] commits, string? path = default, string? filename = default, CancellationToken cancellationToken = default)
    {
        var commitList = string.Join("&", commits.Select(n => $"cids={n}"));
        var url = $"{Constants.Urls.ATProtoSync.GetBlocks}?did={did}&{commitList}";
        filename ??= $"{did}-blocks.car";
        return this.Client.DownloadCarAsync(
            url,
            path ?? Directory.GetCurrentDirectory(),
            filename,
            this.Options.JsonSerializerOptions,
            cancellationToken,
            this.Options.Logger);
    }

    /// <summary>
    /// Get Checkout.
    /// Uses OnCarDecoded to decode CAR file while downloading.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="onDecoded">OnCarDecoded callback.</param>
    /// <param name="commit">Cid Commit.</param>
    /// <param name="cancellationToken">Optional Cancellation token.</param>
    /// <returns>Result of success.</returns>
    [Obsolete("Deprecated in favor of GetRepo")]
    public Task<Result<Success?>> GetCheckoutAsync(ATDid did, OnCarDecoded onDecoded, Cid? commit = default, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetCheckout}?did={did}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        return this.Client.GetCarAsync(
            url,
            this.Options.JsonSerializerOptions,
            cancellationToken,
            this.Options.Logger,
            onDecoded);
    }

    /// <summary>
    /// Download Checkout as CAR file.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="commit">CID Commit.</param>
    /// <param name="path">Path for file.</param>
    /// <param name="filename">Filename.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Result of success.</returns>
    public Task<Result<Success?>> DownloadCheckoutAsync(ATDid did, Cid? commit = default, string? path = default, string? filename = default, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetCheckout}?did={did}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        filename ??= $"{did}-checkout.car";

        return this.Client.DownloadCarAsync(url, path ?? Directory.GetCurrentDirectory(), filename, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Get Record.
    /// Uses OnCarDecoded to decode CAR file while downloading.
    /// </summary>
    /// <param name="collection">Collection name.</param>
    /// <param name="repo">Repo.</param>
    /// <param name="rkey">Rkey.</param>
    /// <param name="onDecoded">On decoded callback.</param>
    /// <param name="commit">Commit.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of success.</returns>
    public Task<Result<Success?>> GetRecordAsync(string collection, ATDid repo, string rkey, OnCarDecoded onDecoded, Cid? commit = default, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetRecord}?collection={collection}&did={repo}&rkey={rkey}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        return this.Client.GetCarAsync(
            url,
            this.Options.JsonSerializerOptions,
            cancellationToken,
            this.Options.Logger,
            onDecoded);
    }

    /// <summary>
    /// Download Record as CAR file.
    /// </summary>
    /// <param name="collection">Collection name.</param>
    /// <param name="repo">Repo.</param>
    /// <param name="rkey">Rkey.</param>
    /// <param name="commit">Commit.</param>
    /// <param name="path">File path.</param>
    /// <param name="filename">File name.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of success.</returns>
    public Task<Result<Success?>> DownloadRecordAsync(
        string collection,
        ATDid repo,
        string rkey,
        Cid? commit = default,
        string? path = default,
        string? filename = default,
        CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetRecord}?collection={collection}&did={repo}&rkey={rkey}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        filename ??= $"{repo}-{rkey}.car";

        return this.Client.DownloadCarAsync(
            url,
            path ?? Directory.GetCurrentDirectory(),
            filename,
            this.Options.JsonSerializerOptions,
            cancellationToken,
            this.Options.Logger);
    }

    /// <summary>
    /// List Repos.
    /// </summary>
    /// <param name="limit">Limit, defaults to 50.</param>
    /// <param name="cursor">Optional Cursor. Used to continue response.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Result of RepoList.</returns>
    public async Task<Result<RepoList?>> ListReposAsync(
        int limit = 50,
        string? cursor = default,
        CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.ATProtoSync.ListRepos + $"?limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<RepoList>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}

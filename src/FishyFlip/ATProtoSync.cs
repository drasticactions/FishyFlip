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
    /// <param name="cid"><see cref="ATCid"/>.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of Blob.</returns>
    public async Task<Result<Blob?>> GetBlobAsync(ATDid did, ATCid cid, CancellationToken cancellationToken = default)
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
            this.Options.SourceGenerationContext.Head,
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
        this.Options.SourceGenerationContext.LatestCommit,
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
        => this.Client.Get<Success>($"{Constants.Urls.ATProtoSync.NotifyOfUpdate}?hostname={hostname}", this.Options.SourceGenerationContext.Success, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    /// <summary>
    /// Request crawl for given hostname.
    /// </summary>
    /// <param name="hostname">Hostname.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of Success.</returns>
    public Task<Result<Success?>> RequestCrawlAsync(string hostname, CancellationToken cancellationToken = default)
        => this.Client.Get<Success>($"{Constants.Urls.ATProtoSync.RequestCrawl}?hostname={hostname}", this.Options.SourceGenerationContext.Success, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    /// <summary>
    /// Get Repo.
    /// Uses OnCarDecoded to decode CAR file while downloading.
    /// </summary>
    /// <param name="repo"><see cref="ATDid"/> of repo.</param>
    /// <param name="onDecoded">Callback method for decoding CAR file.</param>
    /// <param name="since">Optional Since value.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>Result of Success.</returns>
    public async Task<Result<Success?>> GetRepoAsync(ATIdentifier repo, OnCarDecoded onDecoded, string? since = default, CancellationToken cancellationToken = default)
    {
        var (protocol, did) = await this.GenerateClientFromATIdentifierAsync(repo, cancellationToken);
        var url = since is not null
            ? $"{Constants.Urls.ATProtoSync.GetRepo}?did={did}&since={since}"
            : $"{Constants.Urls.ATProtoSync.GetRepo}?did={did}";
        try
        {
            return await protocol.Client.GetCarAsync(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, onDecoded);
        }
        finally
        {
            protocol.Dispose();
        }
    }

    /// <summary>
    /// Download Repo as CAR file.
    /// </summary>
    /// <param name="repo">Repo.</param>
    /// <param name="path">Path to file.</param>
    /// <param name="filename">Filename.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Result of Success.</returns>
    public async Task<Result<Success?>> DownloadRepoAsync(ATIdentifier repo, string? path = default, string? filename = default, CancellationToken cancellationToken = default)
    {
        var (protocol, did) = await this.GenerateClientFromATIdentifierAsync(repo, cancellationToken);
        filename ??= $"{repo}-repo.car";
        try
        {
            return await protocol.Client.DownloadCarAsync(
                $"{Constants.Urls.ATProtoSync.GetRepo}?did={repo}",
                path ?? Directory.GetCurrentDirectory(),
                filename,
                this.Options.JsonSerializerOptions,
                cancellationToken,
                this.Options.Logger);
        }
        finally
        {
            protocol.Dispose();
        }
    }

    /// <summary>
    /// Get Commit Path.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="latest">Latest ATCid Commit.</param>
    /// <param name="earliest">Earliest ATCid Commit.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>result of CommitPath.</returns>
    [Obsolete("Deprecated in Repo V3. This should no longer work.")]
    public async Task<Result<CommitPath?>> GetCommitPathAsync(ATDid did, ATCid? latest = default, ATCid? earliest = default, CancellationToken cancellationToken = default)
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

        return await this.Client.Get<CommitPath>(url, this.Options.SourceGenerationContext.CommitPath, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Get Blocks.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="commits">Array of ATCid Commits.</param>
    /// <param name="onDecoded">OnCarDecoded callback.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Blocks.</returns>
    public async Task<Result<Success?>> GetBlocksAsync(ATIdentifier did, ATCid[] commits, OnCarDecoded onDecoded, CancellationToken cancellationToken = default)
    {
        var (protocol, repo) = await this.GenerateClientFromATIdentifierAsync(did, cancellationToken);
        var commitList = string.Join("&", commits.Select(n => $"cids={n}"));
        var url = $"{Constants.Urls.ATProtoSync.GetBlocks}?did={repo}&{commitList}";
        try
        {
            return await protocol.Client.GetCarAsync(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, onDecoded);
        }
        finally
        {
            protocol.Dispose();
        }
    }

    /// <summary>
    /// Download Blocks as CAR file.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="commits">Array of ATCid Commits.</param>
    /// <param name="path">Path to file.</param>
    /// <param name="filename">Filename.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Blocks.</returns>
    public async Task<Result<Success?>> DownloadBlocksAsync(ATIdentifier did, ATCid[] commits, string? path = default, string? filename = default, CancellationToken cancellationToken = default)
    {
        var (protocol, repo) = await this.GenerateClientFromATIdentifierAsync(did, cancellationToken);
        var commitList = string.Join("&", commits.Select(n => $"cids={n}"));
        var url = $"{Constants.Urls.ATProtoSync.GetBlocks}?did={repo}&{commitList}";
        filename ??= $"{did}-blocks.car";

        try
        {
            return await protocol.Client.DownloadCarAsync(url, path ?? Directory.GetCurrentDirectory(), filename, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        }
        finally
        {
            protocol.Dispose();
        }
    }

    /// <summary>
    /// Get Checkout.
    /// Uses OnCarDecoded to decode CAR file while downloading.
    /// </summary>
    /// <param name="did">Actor ATDid.</param>
    /// <param name="onDecoded">OnCarDecoded callback.</param>
    /// <param name="commit">ATCid Commit.</param>
    /// <param name="cancellationToken">Optional Cancellation token.</param>
    /// <returns>Result of success.</returns>
    [Obsolete("Deprecated in favor of GetRepo")]
    public Task<Result<Success?>> GetCheckoutAsync(ATDid did, OnCarDecoded onDecoded, ATCid? commit = default, CancellationToken cancellationToken = default)
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
    public async Task<Result<Success?>> DownloadCheckoutAsync(ATIdentifier did, ATCid? commit = default, string? path = default, string? filename = default, CancellationToken cancellationToken = default)
    {
        var (protocol, repo) = await this.GenerateClientFromATIdentifierAsync(did, cancellationToken);
        var url = $"{Constants.Urls.ATProtoSync.GetCheckout}?did={repo}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        filename ??= $"{did}-checkout.car";

        try
        {
            return await protocol.Client.DownloadCarAsync(url, path ?? Directory.GetCurrentDirectory(), filename, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        }
        finally
        {
            protocol.Dispose();
        }
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
    public async Task<Result<Success?>> GetRecordAsync(string collection, ATIdentifier repo, string rkey, OnCarDecoded onDecoded, ATCid? commit = default, CancellationToken cancellationToken = default)
    {
        var (protocol, did) = await this.GenerateClientFromATIdentifierAsync(repo, cancellationToken);
        var url = $"{Constants.Urls.ATProtoSync.GetRecord}?collection={collection}&did={did}&rkey={rkey}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        try
        {
            return await protocol.Client.GetCarAsync(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, onDecoded);
        }
        finally
        {
            protocol.Dispose();
        }
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
    public async Task<Result<Success?>> DownloadRecordAsync(
        string collection,
        ATIdentifier repo,
        string rkey,
        ATCid? commit = default,
        string? path = default,
        string? filename = default,
        CancellationToken cancellationToken = default)
    {
        var (protocol, did) = await this.GenerateClientFromATIdentifierAsync(repo, cancellationToken);
        var url = $"{Constants.Urls.ATProtoSync.GetRecord}?collection={collection}&did={did}&rkey={rkey}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        filename ??= $"{repo}-{rkey}.car";

        try
        {
            return await protocol.Client.DownloadCarAsync(url, path ?? Directory.GetCurrentDirectory(), filename, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        }
        finally
        {
            protocol.Dispose();
        }
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

        return await this.Client.Get<RepoList>(url, this.Options.SourceGenerationContext.RepoList, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// List Blobs for a given Did.
    /// </summary>
    /// <param name="repo">Repo Did.</param>
    /// <param name="limit">Limit, defaults to 500.</param>
    /// <param name="since">Optional revision of the repo to list blobs since.</param>
    /// <param name="cursor">Optional Cursor. Used to continue response.</param>
    /// <param name="cancellationToken">Optional Cancellation Token.</param>
    /// <returns>Result of ATCids.</returns>
    public async Task<Result<ListBlobs?>> ListBlobsAsync(ATIdentifier repo, int limit = 500, string? since = default, string? cursor = default, CancellationToken cancellationToken = default)
    {
        var (protocol, did) = await this.GenerateClientFromATIdentifierAsync(repo, cancellationToken);
        var url = Constants.Urls.ATProtoSync.ListBlobs + $"?did={did}&limit={limit}";

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        if (since is not null)
        {
            url += $"&since={since}";
        }

        try
        {
            return await protocol.Client.Get<ListBlobs>(url, this.Options.SourceGenerationContext.ListBlobs, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        }
        finally
        {
            protocol.Dispose();
        }
    }

    private async Task<(ATProtocol Proto, ATDid Did)> GenerateClientFromATIdentifierAsync(ATIdentifier identifier, CancellationToken? token = default)
    {
        var (repo, atError) = await this.proto.Repo.DescribeRepoAsync(identifier, cancellationToken: token ?? CancellationToken.None);
        if (atError is not null)
        {
            this.Options.Logger?.LogError($"ATError: {atError.StatusCode} {atError.Detail?.Error} {atError.Detail?.Message}");
            throw new ATNetworkErrorException(atError);
        }

        var uri = new Uri(repo!.DidDoc.Service[0].ServiceEndpoint);
        var protocolBuilder = new ATProtocolBuilder().WithInstanceUrl(uri);
        return (protocolBuilder.Build(), repo.Did!);
    }
}

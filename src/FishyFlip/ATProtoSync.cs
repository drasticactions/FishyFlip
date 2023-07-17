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

    public async Task<Result<Blob?>> GetBlobAsync(ATDid did, Cid cid, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.ATProtoSync.GetBlob}?did={did}&cid={cid}";
        return await this.Client.GetBlob(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public Task<Result<Head?>> GetHeadAsync(ATDid did, CancellationToken cancellationToken = default)
        => this.Client.Get<Head>($"{Constants.Urls.ATProtoSync.GetHead}?did={did}", this.Options.JsonSerializerOptions,
            cancellationToken, this.Options.Logger);

    public Task<Result<Success?>> NotifyOfUpdateAsync(string hostname, CancellationToken cancellationToken = default)
        => this.Client.Get<Success>($"{Constants.Urls.ATProtoSync.NotifyOfUpdate}?hostname={hostname}", this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    public Task<Result<Success?>> RequestCrawlAsync(string hostname, CancellationToken cancellationToken = default)
        => this.Client.Get<Success>($"{Constants.Urls.ATProtoSync.RequestCrawl}?hostname={hostname}", this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    public Task<Result<Success>> GetRepoAsync(ATDid repo, OnCarDecoded onDecoded,
        CancellationToken cancellationToken = default)
    {
        return this.Client.GetCarAsync(
            $"{Constants.Urls.ATProtoSync.GetRepo}?did={repo}",
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, onDecoded);
    }

    public Task<Result<Success>> DownloadRepoAsync(ATDid repo, string? path = default, string? filename = default,
        CancellationToken cancellationToken = default)
    {
        filename ??= $"{repo}-repo.car";
        return this.Client.DownloadCarAsync(
            $"{Constants.Urls.ATProtoSync.GetRepo}?did={repo}",
            path ?? Directory.GetCurrentDirectory(), filename,
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

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

    public Task<Result<Success>> GetBlocksAsync(ATDid did, Cid[] commits, OnCarDecoded onDecoded,
        CancellationToken cancellationToken = default)
    {
        var commitList = string.Join(",", commits.Select(n => n.ToString()));
        var url = $"{Constants.Urls.ATProtoSync.GetBlocks}?did={did}&commits={commitList}";

        return this.Client.GetCarAsync(
            url,
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, onDecoded);
    }

    public Task<Result<Success>> DownloadBlocksAsync(ATDid did, Cid[] commits, string? path = default, string? filename = default,
        CancellationToken cancellationToken = default)
    {
        var commitList = string.Join(",", commits.Select(n => n.ToString()));
        var url = $"{Constants.Urls.ATProtoSync.GetBlocks}?did={did}&commits={commitList}";
        filename ??= $"{did}-blocks.car";
        return this.Client.DownloadCarAsync(
            url,
            path ?? Directory.GetCurrentDirectory(), filename,
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public Task<Result<Success>> GetCheckoutAsync(ATDid did, OnCarDecoded onDecoded, Cid? commit = default,
        CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetCheckout}?did={did}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        return this.Client.GetCarAsync(
            url,
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, onDecoded);
    }

    public Task<Result<Success>> DownloadCheckoutAsync(ATDid did, Cid? commit = default, string? path = default, string? filename = default,
               CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetCheckout}?did={did}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        filename ??= $"{did}-checkout.car";

        return this.Client.DownloadCarAsync(url, path ?? Directory.GetCurrentDirectory(), filename, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public Task<Result<Success>> GetRecordAsync(string collection, ATDid repo, string rkey, OnCarDecoded onDecoded, Cid? commit = default,
        CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetRecord}?collection={collection}&did={repo}&rkey={rkey}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }

        return this.Client.GetCarAsync(
            url,
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, onDecoded);
    }

    public Task<Result<Success>> DownloadRecordAsync(string collection, ATDid repo, string rkey, Cid? commit = default,
        string? path = default, string? filename = default,
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
            path ?? Directory.GetCurrentDirectory(), filename,
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

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

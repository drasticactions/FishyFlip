// <copyright file="ATProtoSync.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoSync
{
    private ATProtocol proto;

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

    public Task<Result<UploadBlobResponse>> UploadBlobAsync(StreamContent content, CancellationToken cancellationToken = default)
    {
        return
            this.Client
                .Post<UploadBlobResponse>(
                    Constants.Urls.ATProtoRepo.UploadBlob, this.Options.JsonSerializerOptions, content,
                    cancellationToken, this.Options.Logger);
    }

    public Task<Result<Head?>> GetHeadAsync(ATDid did, CancellationToken cancellationToken = default)
        => this.Client.Get<Head>($"{Constants.Urls.ATProtoSync.GetHead}?did={did}", this.Options.JsonSerializerOptions,
            cancellationToken, this.Options.Logger);
    
    public Task<Result<Success?>> NotifyOfUpdateAsync(string hostname, CancellationToken cancellationToken = default)
        => this.Client.Get<Success>($"{Constants.Urls.ATProtoSync.NotifyOfUpdate}?hostname={hostname}", this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    
    public Task<Result<Success?>> RequestCrawlAsync(string hostname, CancellationToken cancellationToken = default)
        => this.Client.Get<Success>($"{Constants.Urls.ATProtoSync.RequestCrawl}?hostname={hostname}", this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    public Task<Result<Dictionary<Cid, byte[]>?>> GetRepoAsync(ATDid repo,
        CancellationToken cancellationToken = default)
        => this.Client.GetCarAsync($"{Constants.Urls.ATProtoSync.GetRepo}?did={repo}",
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

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
    
    public Task<Result<Dictionary<Cid, byte[]>?>> GetBlocksAsync(ATDid did, Cid[] commits,
        CancellationToken cancellationToken = default)
    {
        var commitList = string.Join(",", commits.Select(n => n.ToString()));
        var url = $"{Constants.Urls.ATProtoSync.GetBlocks}?did={did}&commits={commitList}";

        return this.Client.GetCarAsync(url,
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
    
    public Task<Result<Dictionary<Cid, byte[]>?>> GetCheckoutAsync(ATDid did, Cid? commit = default,
        CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetCheckout}?did={did}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }
        
        return this.Client.GetCarAsync(url,
            this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
    
    public Task<Result<Dictionary<Cid, byte[]>?>> GetRecordAsync(string collection, ATDid repo, string rkey, Cid? commit = default,
        CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoSync.GetRecord}?collection={collection}&did={repo}&rkey={rkey}";
        if (commit is not null)
        {
            url += $"&commit={commit}";
        }
        
        return this.Client.GetCarAsync(url,
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

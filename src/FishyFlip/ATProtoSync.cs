// <copyright file="ATProtoSync.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoSync
{
    private ATProtocol proto;

    public ATProtoSync(ATProtocol proto)
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
}

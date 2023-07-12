// <copyright file="ATProtoIdentity.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoIdentity
{
    private ATProtocol proto;

    public ATProtoIdentity(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    public async Task<Result<HandleResolution?>> ResolveHandleAsync(ATHandle handler, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.ATProtoIdentity.ResolveHandle}?handle={handler}";
        return await this.Client.Get<HandleResolution>(url, this.Options.JsonSerializerOptions, cancellationToken);
    }
}

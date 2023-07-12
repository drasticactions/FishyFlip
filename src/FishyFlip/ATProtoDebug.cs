// <copyright file="ATProtoDebug.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoDebug
{
    private ATProtocol proto;

    internal ATProtoDebug(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    public Task<Result<dynamic?>> GetAsync(string path, CancellationToken cancellationToken = default)
        => this.Client.Get<dynamic?>(path, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    public Task<Result<dynamic?>> PostAsync(string path, CancellationToken cancellationToken = default)
        => this.Client.Post<dynamic?>(path, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    public Task<Result<dynamic?>> PostAsync(string path, StreamContent content, CancellationToken cancellationToken = default)
        => this.Client.Post<dynamic?>(path, this.Options.JsonSerializerOptions, content, cancellationToken, this.Options.Logger);
}

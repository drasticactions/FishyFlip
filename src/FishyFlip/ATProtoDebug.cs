// <copyright file="ATProtoDebug.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Debug methods for accessing AT Protocol directly.
/// With this, you can pass in your own endpoints and get the raw data back.
/// </summary>
public sealed class ATProtoDebug
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtoDebug"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal ATProtoDebug(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Get a raw response from the AT Protocol.
    /// </summary>
    /// <param name="path">ATProtocol Endpoint.</param>
    /// <param name="cancellationToken">Cancellation Token. Defaults to null.</param>
    /// <returns>Result of dynamic JSON.</returns>
    public Task<Result<dynamic?>> GetAsync(
        string path,
        CancellationToken cancellationToken = default)
        => this.Client.Get<dynamic?>(path, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    /// <summary>
    /// Get a raw response as a CAR file from the AT Protocol.
    /// </summary>
    /// <param name="path">ATProtocol Endpoint.</param>
    /// <param name="decode">Delegate to decode the CAR response.</param>
    /// <param name="cancellationToken">Cancellation Token. Defaults to null.</param>
    /// <returns>Result of <see cref="Success"/>. Use <see cref="OnCarDecoded"/> to read response.</returns>
    public Task<Result<Success?>> GetCarAsync(
        string path,
        OnCarDecoded decode,
        CancellationToken cancellationToken = default)
        => this.Client.GetCarAsync(path, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, decode);

    /// <summary>
    /// Post a raw response to the AT Protocol.
    /// </summary>
    /// <param name="path">ATProtocol Endpoint.</param>
    /// <param name="cancellationToken">Cancellation Token. Defaults to null.</param>
    /// <returns>Result of dynamic JSON.</returns>
    public Task<Result<dynamic?>> PostAsync(
        string path,
        CancellationToken cancellationToken = default)
        => this.Client.Post<dynamic?>(path, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);

    /// <summary>
    /// Post a raw response to the AT Protocol.
    /// </summary>
    /// <param name="path">ATProtocol Endpoint.</param>
    /// <param name="content"><see cref="StreamContent"/> to post.</param>
    /// <param name="cancellationToken">Cancellation Token. Defaults to null.</param>
    /// <returns>Result of dynamic JSON.</returns>
    public Task<Result<dynamic?>> PostAsync(
        string path,
        StreamContent content,
        CancellationToken cancellationToken = default)
        => this.Client.Post<dynamic?>(path, this.Options.JsonSerializerOptions, content, cancellationToken, this.Options.Logger);
}

// <copyright file="ATProtoIdentity.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// AT Proto Identity.
/// </summary>
public sealed class ATProtoIdentity
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtoIdentity"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    public ATProtoIdentity(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Resolves a users <see cref="ATHandle"/> to a <see cref="ATDid"/>.
    /// </summary>
    /// <param name="handler">Users Handle.</param>
    /// <param name="cancellationToken">Cancellation Token, defaults to none.</param>
    /// <returns>Result of <see cref="HandleResolution"/>.</returns>
    public async Task<Result<HandleResolution?>> ResolveHandleAsync(ATHandle handler, CancellationToken cancellationToken = default)
    {
        string url = $"{Constants.Urls.ATProtoIdentity.ResolveHandle}?handle={handler}";
        return await this.Client.Get<HandleResolution>(url, this.Options.SourceGenerationContext.HandleResolution, this.Options.JsonSerializerOptions, cancellationToken);
    }

    /// <summary>
    /// Updates a users handle.
    /// </summary>
    /// <param name="handle">Handle to update to.</param>
    /// <param name="cancellationToken">Cancellation Token, defaults to none.</param>
    /// <returns>Result of <see cref="Success"/>.</returns>
    public async Task<Result<Success>> UpdateHandleAsync(ATHandle handle, CancellationToken cancellationToken = default)
    {
        return await this.Client.Post<UpdateHandle, Success>(Constants.Urls.ATProtoIdentity.UpdateHandle, this.Options.SourceGenerationContext.UpdateHandle, this.Options.SourceGenerationContext.Success, this.Options.JsonSerializerOptions, new UpdateHandle(handle.Handle.ToString()), cancellationToken);
    }
}

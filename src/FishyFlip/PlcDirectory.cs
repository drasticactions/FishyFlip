// <copyright file="PlcDirectory.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Methods and Endpoints for https://plc.directory.
/// </summary>
public sealed class PlcDirectory : IDisposable
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlcDirectory"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal PlcDirectory(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    /// <summary>
    /// Asynchronously retrieves the <see cref="DidDoc"/> for a given <see cref="ATDid"/>.
    /// </summary>
    /// <param name="identifier">The ATDid of the repository to describe.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the description of the repository, or null if the repository was not found.</returns>
    public async Task<Result<DidDoc?>> GetDidDocAsync(ATDid identifier, CancellationToken cancellationToken = default)
    {
        // Url: https://plc.directory/{identifier}
        var url = new Uri(this.Options.PlcDirectoryUrl, $"/{identifier}");
        return await this.proto.XrpcClient.Client.Get<DidDoc?>(url.ToString(), this.Options.SourceGenerationContext.DidDoc!, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Disposes code.
    /// </summary>
    public void Dispose()
    {
    }
}

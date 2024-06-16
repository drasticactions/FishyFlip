// <copyright file="ATProtoExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization.Metadata;

namespace FishyFlip.Tools;

/// <summary>
/// Provides extension methods for ATProto.
/// </summary>
public static class ATProtoExtensions
{
    /// <summary>
    /// Asynchronously generates a client from an ATIdentifier.
    /// </summary>
    /// <param name="socialProto">The ATProtocol instance.</param>
    /// <param name="identifier">The ATIdentifier instance.</param>
    /// <param name="token">An optional cancellation token. Defaults to null.</param>
    /// <param name="logger">An optional ILogger instance. Defaults to null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a tuple with the ATProtocol and ATDid instances.</returns>
    public static async Task<(ATProtocol Proto, ATDid? Did, bool UsingCurrentProto)> GenerateClientFromATIdentifierAsync(this ATProtocol socialProto, ATIdentifier identifier, CancellationToken? token = default, ILogger? logger = null)
    {
        if (ShouldUseCurrentPDS(socialProto, identifier, logger))
        {
            return (socialProto, identifier as ATDid, true);
        }

        var (repo, atError) = await socialProto.Repo.DescribeRepoAsync(identifier, cancellationToken: token ?? CancellationToken.None);
        if (atError is not null)
        {
            logger?.LogError($"ATError: {atError.StatusCode} {atError.Detail?.Error} {atError.Detail?.Message}");
            throw new ATNetworkErrorException(atError);
        }

        var uri = new Uri(repo!.DidDoc.Service[0].ServiceEndpoint);
        var protocolBuilder = new ATProtocolBuilder().WithInstanceUrl(uri);
        return (protocolBuilder.Build(), repo.Did!, false);
    }

    private static bool ShouldUseCurrentPDS(ATProtocol proto, ATIdentifier identifier, ILogger? logger)
    {
        if ((proto.Client.BaseAddress?.ToString() ?? string.Empty).Contains(Constants.Urls.ATProtoServer.SocialApi))
        {
            logger?.LogDebug("Using current PDS, as we are already on the Social API.");
            return true;
        }

        if (identifier is ATDid did)
        {
            logger?.LogDebug($"Checking if identifier {did} is the same as the current session Did.");
            return did == proto.SessionManager?.Session?.Did;
        }

        if (identifier is ATHandle handle)
        {
            logger?.LogDebug($"Checking if handle {handle} is the same as the current session Handle.");
            return handle == proto.SessionManager?.Session?.Handle;
        }

        logger?.LogDebug("Could not determine if we should use the current PDS. Defaulting to false.");
        return false;
    }
}
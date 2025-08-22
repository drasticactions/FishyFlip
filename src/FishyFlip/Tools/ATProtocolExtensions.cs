// <copyright file="ATProtocolExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// ATProtocol Extensions.
/// </summary>
public static class ATProtocolExtensions
{
    private static async Task<string> ResolveHostUri(this ATProtocol protocol, string pathAndQueryString, string? ndid = null)
    {
        var logger = protocol.Options.Logger;
        string? host = null;

        // Find repo name in pathAndQueryString
        if (pathAndQueryString.Contains("repo"))
        {
            // repo name is a query string value of "repo"
            var repoName = pathAndQueryString.Split('&').FirstOrDefault(x => x.Contains("repo="));
            if (!string.IsNullOrEmpty(repoName))
            {
                var repo = repoName.Split('=')[1];
                if (ATDid.TryCreate(repo, out ATDid? did))
                {
                    (host, _) = await protocol.ResolveATDidHostAsync(did!);
                }
                else if (ATHandle.TryCreate(repo, out ATHandle? handle))
                {
                    (host, _) = await protocol.ResolveATHandleHostAsync(handle!);
                }
            }
        }
        else if (pathAndQueryString.Contains("did"))
        {
            // did is a query string value of "did"
            var didName = pathAndQueryString.Split('&').FirstOrDefault(x => x.Contains("did="));
            if (!string.IsNullOrEmpty(didName))
            {
                var did = didName.Split('=')[1];
                if (ATDid.TryCreate(did, out ATDid? atdid))
                {
                    (host, _) = await protocol.ResolveATDidHostAsync(atdid!);
                }
            }
        }

        if (string.IsNullOrEmpty(host))
        {
            if (protocol.IsAuthenticated)
            {
                host = protocol.Session?.DidDoc?.GetPDSEndpointUrl()?.ToString() ?? throw new InvalidOperationException("Session did doc is required.");
                logger?.LogDebug($"Using PDS host {host}");
            }
            else
            {
                host = protocol.Options.Url.ToString();
                if (host.Contains(Constants.Urls.ATProtoServer.PublicApi) && pathAndQueryString.Contains(FishyFlip.Lexicon.Com.Atproto.Server.ServerEndpoints.CreateSession))
                {
                    host = Constants.Urls.ATProtoServer.SocialApi;
                    logger?.LogDebug($"Trying to authenticate with PublicAPI endpoint, switching to Social: {host}");
                }
                else
                {
                    logger?.LogDebug($"Host was empty, using default host {host}");
                }
            }
        }

        if (string.IsNullOrEmpty(host))
        {
            throw new InvalidOperationException("Host is required.");
        }

        if (host!.EndsWith("/") && pathAndQueryString.StartsWith("/"))
        {
            host = host.Substring(0, host.Length - 1);
        }
        else if (!host.EndsWith("/") && !pathAndQueryString.StartsWith("/"))
        {
            host += "/";
        }

        var result = $"{host}{pathAndQueryString}";
        logger?.LogDebug($"Resolved Host Uri: {result}");
        return result;
    }
}


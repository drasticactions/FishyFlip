// <copyright file="ATProtocolExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// ATProtocol Extensions.
/// </summary>
public static class ATProtocolExtensions
{
    /// <summary>
    /// Sends a GET request to the specified Uri as an asynchronous operation and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The type of the response body.</typeparam>
    /// <param name="protocol">The instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    public static async Task<Result<T?>> Get<T>(
        this ATProtocol protocol,
        string url,
        JsonTypeInfo<T> type,
        CancellationToken cancellationToken,
        Dictionary<string, string>? headers = default)
    {
        var result = await protocol.ResolveHostUri(url);
        return await protocol.Client.Get<T>(result, type, protocol.Options.JsonSerializerOptions, cancellationToken, protocol.Options.Logger, headers);
    }

    /// <summary>
    /// Sends a GET request to the specified Uri and downloads the response as a CAR (Content-Addressable Archive) file.
    /// </summary>
    /// <param name="protocol">The instance.</param>
    /// <param name="identifier">The user to download.</param>
    /// <param name="outputStream">The stream to write the CAR file to.</param>
    /// <param name="since">The revision ('rev') of the repo to create a diff from.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Success response message as the result.</returns>
    public static async Task<Result<Success?>> DownloadRepoAsync(
        this ATProtocol protocol,
        ATIdentifier identifier,
        Stream outputStream,
        string? since = default,
        CancellationToken cancellationToken = default)
    {
        var (result, error) = await protocol.ResolveATIdentifierToHostAddressAsync(identifier);
        if (error is not null)
        {
            return error;
        }

        ATDid did;
        if (identifier is ATDid atDid)
        {
            did = atDid;
        }
        else if (identifier is ATHandle atHandle)
        {
            var (didResult, didError) = await protocol.ResolveATDidAsync(atHandle);
            if (didError is not null)
            {
                return didError;
            }

            did = didResult!;
        }
        else
        {
            throw new InvalidOperationException("Identifier must be a Did or Handle.");
        }

        var endpointUrl = $"{result![0..^1]}{FishyFlip.Lexicon.Com.Atproto.Sync.SyncEndpoints.GetRepo.ToString()}";
        endpointUrl += "?";
        List<string> queryStrings = new();
        queryStrings.Add("did=" + did);

        if (since != null)
        {
            queryStrings.Add("since=" + since);
        }

        endpointUrl += string.Join("&", queryStrings);
        return await protocol.Client.DownloadCarAsync(endpointUrl, outputStream, protocol.Options.JsonSerializerOptions, cancellationToken, protocol.Options.Logger);
    }

    /// <summary>
    /// Sends a GET request to the specified Uri and decodes the response as a CAR (Content-Addressable Archive).
    /// </summary>
    /// <param name="protocol">The instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="progress">The progress reporter for the decoding process. This is optional and defaults to null.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Success response message as the result.</returns>
    public static async Task<Result<CarResponse>> GetCarAsync(
            this ATProtocol protocol,
            string url,
            CancellationToken cancellationToken,
            OnCarDecoded? progress = null)
    {
        var result = await protocol.ResolveHostUri(url);
        return await protocol.Client.GetCarAsync(result, protocol.Options.JsonSerializerOptions, cancellationToken, protocol.Options.Logger, progress);
    }

    /// <summary>
    /// Sends a GET request to the specified Uri and retrieves the response as a Blob.
    /// </summary>
    /// <param name="protocol">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Blob response message as the result.</returns>
    public static async Task<Result<byte[]?>> GetBlob(
       this ATProtocol protocol,
       string url,
       CancellationToken cancellationToken)
    {
        var result = await protocol.ResolveHostUri(url);
        return await protocol.Client.GetBlob(result, protocol.Options.JsonSerializerOptions, cancellationToken, protocol.Options.Logger);
    }

    /// <summary>
    /// Sends a POST request with a StreamContent body to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="protocol">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    public static async Task<Result<TK>> Post<TK>(
        this ATProtocol protocol,
        string url,
        JsonTypeInfo<TK> type,
        CancellationToken cancellationToken,
        Dictionary<string, string>? headers = null)
    {
        var result = await protocol.ResolveHostUri(url);
        return await protocol.Client.Post<TK>(result, type, protocol.Options.JsonSerializerOptions, cancellationToken, protocol.Options.Logger, headers);
    }

    /// <summary>
    /// Sends a POST request with a StreamContent body to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="protocol">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="body">The StreamContent request body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    public static async Task<Result<TK>> Post<TK>(
       this ATProtocol protocol,
       string url,
       JsonTypeInfo<TK> type,
       StreamContent body,
       CancellationToken cancellationToken,
       Dictionary<string, string>? headers = default)
    {
        var result = await protocol.ResolveHostUri(url);
        if (headers is not null)
        {
            foreach (var header in headers)
            {
                body.Headers.Add(header.Key, header.Value);
            }
        }

        return await protocol.Client.Post<TK>(result, type, protocol.Options.JsonSerializerOptions, body, cancellationToken, protocol.Options.Logger);
    }

    /// <summary>
    /// Sends a POST request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="T">The type of the request body.</typeparam>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="protocol">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="typeT">The JsonTypeInfo of the request body.</param>
    /// <param name="typeTK">The JsonTypeInfo of the response body.</param>
    /// <param name="body">The request body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    public static async Task<Result<TK>> Post<T, TK>(
       this ATProtocol protocol,
       string url,
       JsonTypeInfo<T> typeT,
       JsonTypeInfo<TK> typeTK,
       T body,
       CancellationToken cancellationToken,
       Dictionary<string, string>? headers = default)
    {
        var result = await protocol.ResolveHostUri(url);
        return await protocol.Client.Post<T, TK>(result, typeT, typeTK, protocol.Options.JsonSerializerOptions, body, cancellationToken, protocol.Options.Logger, headers);
    }

    /// <summary>
    /// Tries to fetch the proxy for the given URI from the ATProxyCache.
    /// </summary>
    /// <param name="atp">The ATProtocol instance.</param>
    /// <param name="uri">The URI to fetch the proxy for.</param>
    /// <param name="proxy">The proxy URI if found, otherwise null.</param>
    /// <returns>True if the proxy was found, otherwise false.</returns>
    /// <remarks>
    /// This method checks the ATProxyCache for a proxy associated with the given URI.
    /// If found, it sets the proxy parameter and returns true. Otherwise, it sets proxy to null and returns false.
    /// </remarks>
    internal static bool TryFetchProxy(this ATProtocol atp, string uri, out string? proxy)
    {
        if (atp.Options.ATProxyCache.TryGetValue(uri, out proxy))
        {
            return true;
        }

        proxy = atp.Options.ATProxyCache
            .FirstOrDefault(x => uri.StartsWith(x.Key, StringComparison.OrdinalIgnoreCase))
            .Value;

        return proxy != null;
    }

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
                host = protocol.SessionManager.Session?.DidDoc?.GetPDSEndpointUrl()?.ToString() ?? throw new InvalidOperationException("Session did doc is required.");
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
            host = host[0..^1];
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


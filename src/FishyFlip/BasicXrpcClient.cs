// <copyright file="BasicXrpcClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.Concurrent;
using System.Net;
using FishyFlip.Lexicon.Com.Atproto.Identity;

namespace FishyFlip;

/// <summary>
/// A basic XRPC client.
/// </summary>
public abstract class BasicXrpcClient : IXrpcClient
{
    private Uri baseUri;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicXrpcClient"/> class.
    /// </summary>
    /// <param name="options"><see cref="ATProtocolOptions"/>.</param>
    public BasicXrpcClient(ATProtocolOptions options)
    {
        this.Client = new HttpClient(new BasicXrpcDelegatingHandler(options.Logger));
        this.OzoneProxyHeader = options.OzoneProxyHeader;
        this.Logger = options.Logger;
        this.LabelParameters = options.LabelParameters;
        this.baseUri = options.Url;
        this.PlcDirectoryUrl = options.PlcDirectoryUrl;
    }

    /// <inheritdoc/>
    public HashSet<LabelParameter> LabelParameters { get; } = new HashSet<LabelParameter>();

    /// <inheritdoc/>
    public HttpClient Client { get; }

    /// <inheritdoc/>
    public string OzoneProxyHeader { get; private set; } = string.Empty;

    /// <inheritdoc/>
    public Session? Session { get; internal set; }

    /// <inheritdoc/>
    public ILogger? Logger { get; }

    /// <summary>
    /// Gets the PLC Directory URL.
    /// </summary>
    public Uri PlcDirectoryUrl { get; }

    /// <inheritdoc/>
    public virtual bool IsAuthenticated => this.Session is not null;

    /// <summary>
    /// Gets the ATProxy Cache.
    /// </summary>
    internal ConcurrentDictionary<string, string> ATProxyCache { get; } = new();

    /// <summary>
    /// Gets the Did Cache.
    /// </summary>
    internal ConcurrentDictionary<string, string> DidCache { get; } = new();

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Client.Dispose();
    }

    /// <inheritdoc/>
    public async Task<Result<byte[]?>> GetBlobAsync(string url, CancellationToken cancellationToken)
    {
        url = await this.ResolveHostUri(url, this.OzoneProxyHeader).ConfigureAwait(false);
        this.Logger?.LogDebug($"GET {url}");
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        using var message = await this.Client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await this.CreateError(message!, SourceGenerationContext.Default.Options, cancellationToken, this.Logger);
            return atError!;
        }

#if NETSTANDARD
        var blob = await message.Content.ReadAsByteArrayAsync();
        string response = await message.Content.ReadAsStringAsync();
#else
        var blob = await message.Content.ReadAsByteArrayAsync(cancellationToken);
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
#endif

        this.Logger?.LogDebug($"GET BLOB {url}: {response}");
        return blob;
    }

    /// <inheritdoc/>
    public async Task<Result<CarResponse>> GetCarAsync(string url, CancellationToken cancellationToken, OnCarDecoded? progress = null)
    {
        url = await this.ResolveHostUri(url, this.OzoneProxyHeader).ConfigureAwait(false);
        this.Logger?.LogDebug($"GET {url}");
        var message = await this.Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await this.CreateError(message!, SourceGenerationContext.Default.Options, cancellationToken, this.Logger);
            return atError!;
        }

        var carResponse = new CarResponse(message);

        if (progress != null)
        {
            await foreach (var item in carResponse)
            {
                progress(new CarProgressStatusEvent(item.Cid, item.Bytes));
            }
        }

        return new Result<CarResponse>(carResponse);
    }

    /// <inheritdoc/>
    public async Task<Result<TK>> ProcedureAsync<TK>(string url, JsonTypeInfo<TK> type, CancellationToken cancellationToken, Dictionary<string, string>? headers = null)
    {
        url = await this.ResolveHostUri(url, this.OzoneProxyHeader).ConfigureAwait(false);
        this.Logger?.LogDebug($"POST {url}");
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var message = await this.Client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await this.CreateError(message!, SourceGenerationContext.Default.Options, cancellationToken, this.Logger);
            return atError!;
        }

#if NETSTANDARD
        string response = await message.Content.ReadAsStringAsync();
#else
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
#endif
        if (response.IsNullOrEmpty() && message.IsSuccessStatusCode)
        {
            response = "{ }";
        }

        this.Logger?.LogDebug($"POST {this.Client.BaseAddress}{url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, type);
        return result!;
    }

    /// <inheritdoc/>
    public async Task<Result<TK>> ProcedureAsync<TK>(string url, JsonTypeInfo<TK> type, StreamContent body, CancellationToken cancellationToken, Dictionary<string, string>? headers = null)
    {
        url = await this.ResolveHostUri(url, this.OzoneProxyHeader).ConfigureAwait(false);
        this.Logger?.LogDebug($"POST STREAM {url}: {body.Headers.ContentType}");
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = body;
        using var message = await this.Client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await this.CreateError(message!, SourceGenerationContext.Default.Options, cancellationToken, this.Logger);
            return atError!;
        }

#if NETSTANDARD
        string response = await message.Content.ReadAsStringAsync();
#else
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
#endif
        if (response.IsNullOrEmpty() && message.IsSuccessStatusCode)
        {
            response = "{ }";
        }

        this.Logger?.LogDebug($"POST {this.Client.BaseAddress}{url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, type);
        return result!;
    }

    /// <inheritdoc/>
    public async Task<Result<TK>> ProcedureAsync<T, TK>(string url, JsonTypeInfo<T> typeT, JsonTypeInfo<TK> typeTK, T body, CancellationToken cancellationToken, Dictionary<string, string>? headers = null)
    {
        url = await this.ResolveHostUri(url, this.OzoneProxyHeader).ConfigureAwait(false);
        var jsonContent = JsonSerializer.Serialize(body, typeT)!;
        StringContent content = new(jsonContent, Encoding.UTF8, "application/json");
        if (headers != null)
        {
            foreach (var header in headers)
            {
                content.Headers.Add(header.Key, header.Value);
            }
        }

        this.Logger?.LogDebug($"POST {url}: {jsonContent}");
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = content;
        using var message = await this.Client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await this.CreateError(message!, SourceGenerationContext.Default.Options, cancellationToken, this.Logger);
            return atError!;
        }

#if NETSTANDARD
        string response = await message.Content.ReadAsStringAsync();
#else
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
#endif
        if (response.IsNullOrEmpty() && message.IsSuccessStatusCode)
        {
            response = "{ }";
        }

        this.Logger?.LogDebug($"POST {url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, typeTK);
        return result!;
    }

    /// <inheritdoc/>
    public async Task<Result<TK>> ProcedureAsync<TK>(string url, JsonTypeInfo<TK> type, StreamContent body, CancellationToken cancellationToken)
    {
        url = await this.ResolveHostUri(url, this.OzoneProxyHeader).ConfigureAwait(false);
        this.Logger?.LogDebug($"POST STREAM {url}: {body.Headers.ContentType}");
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = body;
        using var message = await this.Client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await this.CreateError(message!, SourceGenerationContext.Default.Options, cancellationToken, this.Logger);
            return atError!;
        }

#if NETSTANDARD
        string response = await message.Content.ReadAsStringAsync();
#else
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
#endif
        if (response.IsNullOrEmpty() && message.IsSuccessStatusCode)
        {
            response = "{ }";
        }

        this.Logger?.LogDebug($"POST {url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, type);
        return result!;
    }

    /// <inheritdoc/>
    public async Task<Result<T>> QueryAsync<T>(string url, JsonTypeInfo<T> type, CancellationToken cancellationToken, Dictionary<string, string>? headers = null)
    {
        url = await this.ResolveHostUri(url, this.OzoneProxyHeader).ConfigureAwait(false);
        this.Logger?.LogDebug($"GET {url}");
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var message = await this.Client.SendAsync(request, cancellationToken);

        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await this.CreateError(message!, SourceGenerationContext.Default.Options, cancellationToken, this.Logger);
            return atError!;
        }

#if NETSTANDARD
        string response = await message.Content.ReadAsStringAsync();
#else
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
#endif
        if (response.IsNullOrEmpty() && message.IsSuccessStatusCode)
        {
            response = "{ }";
        }

        // BUG: Sometimes, ATProtocol does not set $type as the first property in the JSON response.
        // That causes the deserialization to fail. This is a workaround to reorder the $type property.
        response = JsonTypeReorderer.ReorderTypeProperty(response);
        this.Logger?.LogDebug($"GET {this.Client.BaseAddress}{url}: {response}");
        return JsonSerializer.Deserialize<T>(response, type)!;
    }

    /// <inheritdoc/>
    public void SetOzoneProxy(ATDid did)
    {
        this.OzoneProxyHeader = $"{did}{Constants.AtLabeler}";
    }

    /// <inheritdoc/>
    public bool TryFetchProxy(string uri, out string? proxy)
    {
        if (this.ATProxyCache.TryGetValue(uri, out proxy))
        {
            return true;
        }

        proxy = this.ATProxyCache
            .FirstOrDefault(x => uri.StartsWith(x.Key, StringComparison.OrdinalIgnoreCase))
            .Value;

        return proxy != null;
    }

    /// <summary>
    /// Resolves an ATDid to a host address.
    /// </summary>
    /// <param name="did"><see cref="ATDid"/>.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>String of Host URI if it could be resolved, null if it could not.</returns>
    public async Task<Result<string?>> ResolveATDidHostAsync(ATDid did, CancellationToken? token = default)
    {
        if (this.DidCache.TryGetValue(did.ToString(), out var host) && !string.IsNullOrEmpty(host))
        {
            this.Logger?.LogDebug($"Resolved DID from cache: {did} to {host}");
            return host;
        }

        try
        {
            switch (did.Type)
            {
                case "plc":
                    (host, var error) = await this.ResolvePlcDidAsync(did, token);
                    if (error is not null)
                    {
                        return error;
                    }

                    break;
                case "web":
                    host = await this.ResolveWebDidAsync(did, token);
                    break;
                default:
                    this.Logger?.LogError($"DID type could not be resolved: {did}");
                    break;
            }

            if (!string.IsNullOrEmpty(host))
            {
                this.DidCache[did.ToString()] = host!;
                this.Logger?.LogDebug($"Resolved DID: {did} to {host}, adding to cache.");
            }
        }
        catch (Exception ex)
        {
            this.Logger?.LogError($"Failed to resolve DID: {did}. {ex.Message}");
        }

        return host;
    }

    /// <summary>
    /// Resolves an ATHandle to a host address.
    /// </summary>
    /// <param name="handle"><see cref="ATHandle"/>.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>String of Host URI if it could be resolved, null if it could not.</returns>
    public async Task<Result<string?>> ResolveATHandleHostAsync(ATHandle handle, CancellationToken? token = default)
    {
        if (this.DidCache.TryGetValue(handle.ToString(), out var host) && !string.IsNullOrEmpty(host))
        {
            this.Logger?.LogDebug($"Resolved handle from cache: {handle} to {host}");
            return host;
        }

        if (this.IsAuthenticated && this.Session?.Handle.ToString() == handle.ToString())
        {
            host = this.Session?.DidDoc?.GetPDSEndpointUrl(this.Logger)?.ToString();
            if (!string.IsNullOrEmpty(host))
            {
                this.DidCache[handle.ToString()] = host!;
                this.Logger?.LogDebug($"Resolved handle: {handle} to {host}, adding to cache.");
                return host;
            }

            this.Logger?.LogError($"Failed to resolve Self User handle: {handle}, missing Service Handle.");
        }

        try
        {
            var endpointUrl = $"{Constants.Urls.ATProtoServer.PublicApi}{IdentityEndpoints.ResolveHandle}?handle={handle}";
            var result = await this.Client.GetAsync(endpointUrl, token ?? CancellationToken.None);
            if (result.IsSuccessStatusCode)
            {
                var resolveHandle = JsonSerializer.Deserialize<ResolveHandleOutput>(
                    await result.Content.ReadAsStringAsync(),
                    SourceGenerationContext.Default.ComAtprotoIdentityResolveHandleOutput);
                if (resolveHandle?.Did is not null)
                {
                    (host, var error) = await this.ResolveATDidHostAsync(resolveHandle.Did, token);
                    if (!string.IsNullOrEmpty(host))
                    {
                        this.DidCache[handle.ToString()] = host!;
                        this.Logger?.LogDebug($"Resolved handle: {handle} to {host}, adding to cache.");
                    }
                    else
                    {
                        this.Logger?.LogError($"Failed to resolve Handle: {handle}, missing Service Handle.");
                        return error;
                    }
                }
                else
                {
                    this.Logger?.LogError($"Failed to resolve Handle: {handle}. Missing DID.");
                }
            }
            else
            {
                var resolveError = JsonSerializer.Deserialize<ATError>(
                    await result.Content.ReadAsStringAsync(),
                    SourceGenerationContext.Default.ATError);
                if (resolveError is not null)
                {
                    this.Logger?.LogError($"Failed to resolve Handle: {handle}. {resolveError}");
                    return resolveError;
                }

                this.Logger?.LogError($"Failed to resolve Handle: {handle}. {result.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            this.Logger?.LogError($"Failed to resolve Handle: {handle}. {ex.Message}");
        }

        return host;
    }

    /// <summary>
    /// Creates an ATError from an HttpResponseMessage.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="options">Options.</param>
    /// <param name="cancellationToken">CancellationToken.</param>
    /// <param name="logger">Logger.</param>
    /// <returns>ATError.</returns>
    internal async Task<ATError> CreateError(HttpResponseMessage message, JsonSerializerOptions options, CancellationToken cancellationToken, ILogger? logger = default)
    {
#if NETSTANDARD
        string response = await message.Content.ReadAsStringAsync();
#else
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
#endif
        ATError atError;
        ErrorDetail? detail = default;
        if (string.IsNullOrEmpty(response))
        {
            detail = new ErrorDetail("HTTP Error", message.ReasonPhrase ?? string.Empty);
            atError = new ATError((int)message.StatusCode, detail);
        }
        else
        {
            try
            {
                detail = JsonSerializer.Deserialize<ErrorDetail>(response, ((SourceGenerationContext)options.TypeInfoResolver!).ErrorDetail) ??
                         new ErrorDetail("UnknownError", response);

                atError = FishyFlip.Lexicon.ATErrorGenerator.Generate((int)message.StatusCode, detail);
            }
            catch (Exception)
            {
                atError = new ATError((int)message.StatusCode, null);
            }
        }

        logger?.LogError($"ATError: {atError.StatusCode} {atError.Detail?.Error} {atError.Detail?.Message}");
        return atError;
    }

    private async Task<string> ResolveHostUri(string pathAndQueryString, string? ndid = null)
    {
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
                    (host, _) = await this.ResolveATDidHostAsync(did!);
                }
                else if (ATHandle.TryCreate(repo, out ATHandle? handle))
                {
                    (host, _) = await this.ResolveATHandleHostAsync(handle!);
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
                    (host, _) = await this.ResolveATDidHostAsync(atdid!);
                }
            }
        }

        if (string.IsNullOrEmpty(host))
        {
            if (this.IsAuthenticated)
            {
                host = this.Session?.DidDoc?.GetPDSEndpointUrl()?.ToString() ?? throw new InvalidOperationException("Session did doc is required.");
                this.Logger?.LogDebug($"Using PDS host {host}");
            }
            else
            {
                host = this.baseUri.ToString();
                if (host.Contains(Constants.Urls.ATProtoServer.PublicApi) && pathAndQueryString.Contains(FishyFlip.Lexicon.Com.Atproto.Server.ServerEndpoints.CreateSession))
                {
                    host = Constants.Urls.ATProtoServer.SocialApi;
                    this.Logger?.LogDebug($"Trying to authenticate with PublicAPI endpoint, switching to Social: {host}");
                }
                else
                {
                    this.Logger?.LogDebug($"Host was empty, using default host {host}");
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
        this.Logger?.LogDebug($"Resolved Host Uri: {result}");
        return result;
    }

    private async Task<Result<string?>> ResolvePlcDidAsync(ATDid did, CancellationToken? token)
    {
        string? host = null;
        if (this.IsAuthenticated && this.Session?.Did.ToString() == did.ToString())
        {
            host = this.Session?.DidDoc?.GetPDSEndpointUrl(this.Logger)?.ToString();
            if (!string.IsNullOrEmpty(host))
            {
                this.DidCache[did.ToString()] = host!;
                this.Logger?.LogDebug($"Resolved DID: {did} to {host}, adding to cache.");
            }
            else
            {
                this.Logger?.LogError($"Failed to resolve Self User DID: {did}, missing Service Handle.");
            }
        }

        var url = new Uri(this.PlcDirectoryUrl, $"/{did}");
        var (resolveHandle, error) = await this.Client.Get<DidDoc?>(url.ToString(), SourceGenerationContext.Default.DidDoc!, SourceGenerationContext.Default.Options, token ?? CancellationToken.None, this.Logger);
        if (resolveHandle is not null)
        {
            host = resolveHandle.GetPDSEndpointUrl(this.Logger)?.ToString();
            if (string.IsNullOrEmpty(host))
            {
                this.Logger?.LogError($"Failed to resolve DID: {did}, missing Service Handle.");
            }
        }
        else
        {
            this.Logger?.LogError($"Failed to resolve plc DID: {did}. {error?.ToString()}");
            return error;
        }

        return host;
    }

    private async Task<string?> ResolveWebDidAsync(ATDid did, CancellationToken? token)
    {
        string? host = null;
        var baseUri = did.ToString().Split(':').Last();
        if (!baseUri.Contains("http"))
        {
            baseUri = $"https://{baseUri}";
        }

        if (Uri.TryCreate(baseUri, UriKind.Absolute, out var uri))
        {
            var result = await this.Client.GetAsync($"{uri}{Constants.DidJson}", token ?? CancellationToken.None);
            if (result.IsSuccessStatusCode)
            {
                var didDoc = JsonSerializer.Deserialize<DidDoc>(await result.Content.ReadAsStringAsync(), SourceGenerationContext.Default.DidDoc);
                if (didDoc is not null)
                {
                    host = didDoc.GetPDSEndpointUrl(this.Logger)?.ToString();
                    if (string.IsNullOrEmpty(host))
                    {
                        this.Logger?.LogError($"Failed to resolve DID: {did}, missing Service Handle.");
                    }
                }
                else
                {
                    this.Logger?.LogError($"Failed to resolve DID: {did}, missing DID Doc.");
                }
            }
            else
            {
                this.Logger?.LogError($"Failed to resolve web DID: {did}.");
            }
        }
        else
        {
            this.Logger?.LogError($"Failed to resolve web DID: {did}.");
        }

        return host;
    }

    /// <summary>
    /// XRPC Delegating Handler.
    /// </summary>
    private class BasicXrpcDelegatingHandler
    : DelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicXrpcDelegatingHandler"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/>.</param>
        public BasicXrpcDelegatingHandler(ILogger? logger)
        {
            var handle = new HttpClientHandler { MaxRequestContentBufferSize = int.MaxValue };
            if (handle.SupportsAutomaticDecompression)
            {
                logger?.LogDebug("Enabling GZip and Deflate decompression.");
                handle.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            else
            {
                logger?.LogDebug("Automatic decompression is not supported, disabled...");
            }

            this.InnerHandler = handle;
        }
    }
}
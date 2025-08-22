// <copyright file="IXrpcClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// The XRPC client.
/// </summary>
public interface IXrpcClient : IDisposable
{
    /// <summary>
    /// Gets the label parameters.
    /// </summary>
    HashSet<LabelParameter> LabelParameters { get; }

    /// <summary>
    /// Gets the HttpClient.
    /// </summary>
    HttpClient Client { get; }

    /// <summary>
    /// Gets the Ozone Proxy header.
    /// </summary>
    string OzoneProxyHeader { get; }

    /// <summary>
    /// Gets the current session.
    /// </summary>
    Session? Session { get; }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    ILogger? Logger { get; }

    /// <summary>
    /// Gets a value indicating whether the client is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Tries to fetch the proxy for the given URI from the ATProxyCache.
    /// </summary>
    /// <param name="uri">The URI to fetch the proxy for.</param>
    /// <param name="proxy">The proxy URI if found, otherwise null.</param>
    /// <returns>True if the proxy was found, otherwise false.</returns>
    /// <remarks>
    /// This method checks the ATProxyCache for a proxy associated with the given URI.
    /// If found, it sets the proxy parameter and returns true. Otherwise, it sets proxy to null and returns false.
    /// </remarks>
    bool TryFetchProxy(string uri, out string? proxy);

    /// <summary>
    /// Queries a PDS.
    /// This is an HTTP GET request.
    /// </summary>
    /// <typeparam name="T">The type of the response body.</typeparam>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    Task<Result<T>> QueryAsync<T>(
        string url,
        JsonTypeInfo<T> type,
        CancellationToken cancellationToken,
        Dictionary<string, string>? headers = default);

    /// <summary>
    /// Executes a procedure on a PDS.
    /// Sends a POST request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    Task<Result<TK>> ProcedureAsync<TK>(
        string url,
        JsonTypeInfo<TK> type,
        CancellationToken cancellationToken,
        Dictionary<string, string>? headers = default);

    /// <summary>
    /// Executes a procedure on a PDS.
    /// Sends a POST request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="body">The StreamContent request body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    Task<Result<TK>> ProcedureAsync<TK>(
        string url,
        JsonTypeInfo<TK> type,
        StreamContent body,
        CancellationToken cancellationToken,
        Dictionary<string, string>? headers = default);

    /// <summary>
    /// Executes a procedure on a PDS.
    /// Sends a POST request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="T">The type of the request body.</typeparam>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="typeT">The JsonTypeInfo of the request body.</param>
    /// <param name="typeTK">The JsonTypeInfo of the response body.</param>
    /// <param name="body">The request body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    Task<Result<TK>> ProcedureAsync<T, TK>(
        string url,
        JsonTypeInfo<T> typeT,
        JsonTypeInfo<TK> typeTK,
        T body,
        CancellationToken cancellationToken,
        Dictionary<string, string>? headers = default);

    /// <summary>
    /// Executes a procedure on a PDS.
    /// Sends a POST request with a StreamContent body to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="body">The StreamContent request body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    Task<Result<TK>> ProcedureAsync<TK>(
        string url,
        JsonTypeInfo<TK> type,
        StreamContent body,
        CancellationToken cancellationToken);

    /// <summary>
    /// Sends a GET request to the specified Uri and decodes the response as a CAR (Content-Addressable Archive).
    /// </summary>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="progress">The progress reporter for the decoding process. This is optional and defaults to null.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Success response message as the result.</returns>
    Task<Result<CarResponse>> GetCarAsync(
        string url,
        CancellationToken cancellationToken,
        OnCarDecoded? progress = null);

    /// <summary>
    /// Sends a GET request to the specified Uri and retrieves the response as a Blob.
    /// </summary>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Blob response message as the result.</returns>
    public Task<Result<byte[]?>> GetBlobAsync(
       string url,
       CancellationToken cancellationToken);
}
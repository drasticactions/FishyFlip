// <copyright file="HttpClientExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization.Metadata;
using Microsoft.IdentityModel.Tokens;

namespace FishyFlip.Tools;

/// <summary>
/// Provides extension methods for HttpClient.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Sends a POST request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="T">The type of the request body.</typeparam>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="client">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="typeT">The JsonTypeInfo of the request body.</param>
    /// <param name="typeTK">The JsonTypeInfo of the response body.</param>
    /// <param name="options">The JsonSerializerOptions for the request.</param>
    /// <param name="body">The request body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="logger">The logger to use. This is optional and defaults to null.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    public static async Task<Result<TK>> Post<T, TK>(
       this HttpClient client,
       string url,
       JsonTypeInfo<T> typeT,
       JsonTypeInfo<TK> typeTK,
       JsonSerializerOptions options,
       T body,
       CancellationToken cancellationToken,
       ILogger? logger = default,
       Dictionary<string, string>? headers = default)
    {
        var jsonContent = JsonSerializer.Serialize(body, typeT);
        StringContent content = new(jsonContent, Encoding.UTF8, "application/json");
        if (headers != null)
        {
            foreach (var header in headers)
            {
                content.Headers.Add(header.Key, header.Value);
            }
        }

        logger?.LogDebug($"POST {client.BaseAddress}{url}: {jsonContent}");
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = content;
        using var message = await client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await CreateError(message!, options, cancellationToken, logger);
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

        logger?.LogDebug($"POST {client.BaseAddress}{url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, typeTK);
        return result!;
    }

    /// <summary>
    /// Sends a POST request with a StreamContent body to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="client">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="options">The JsonSerializerOptions for the request.</param>
    /// <param name="body">The StreamContent request body.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="logger">The logger to use. This is optional and defaults to null.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    public static async Task<Result<TK>> Post<TK>(
       this HttpClient client,
       string url,
       JsonTypeInfo<TK> type,
       JsonSerializerOptions options,
       StreamContent body,
       CancellationToken cancellationToken,
       ILogger? logger = default)
    {
        logger?.LogDebug($"POST STREAM {client.BaseAddress}{url}: {body.Headers.ContentType}");
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = body;
        using var message = await client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await CreateError(message!, options, cancellationToken, logger);
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

        logger?.LogDebug($"POST {client.BaseAddress}{url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, type);
        return result!;
    }

    /// <summary>
    /// Sends a POST request with a StreamContent body to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TK">The type of the response body.</typeparam>
    /// <param name="client">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="options">The JsonSerializerOptions for the request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="logger">The logger to use. This is optional and defaults to null.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    public static async Task<Result<TK>> Post<TK>(
        this HttpClient client,
        string url,
        JsonTypeInfo<TK> type,
        JsonSerializerOptions options,
        CancellationToken cancellationToken,
        ILogger? logger = default,
        Dictionary<string, string>? headers = default)
    {
        logger?.LogDebug($"POST {client.BaseAddress}{url}");
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var message = await client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await CreateError(message!, options, cancellationToken, logger);
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

        logger?.LogDebug($"POST {client.BaseAddress}{url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, type);
        return result!;
    }

    /// <summary>
    /// Sends a GET request to the specified Uri and retrieves the response as a Blob.
    /// </summary>
    /// <param name="client">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="options">The JsonSerializerOptions for the request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="logger">The logger to use. This is optional and defaults to null.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Blob response message as the result.</returns>
    public static async Task<Result<byte[]?>> GetBlob(
       this HttpClient client,
       string url,
       JsonSerializerOptions options,
       CancellationToken cancellationToken,
       ILogger? logger = default)
    {
        logger?.LogDebug($"GET {client.BaseAddress}{url}");
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        using var message = await client.SendAsync(request, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await CreateError(message!, options, cancellationToken, logger);
            return atError!;
        }

#if NETSTANDARD
        var blob = await message.Content.ReadAsByteArrayAsync();
        string response = await message.Content.ReadAsStringAsync();
#else
        var blob = await message.Content.ReadAsByteArrayAsync(cancellationToken);
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
#endif

        logger?.LogDebug($"GET BLOB {client.BaseAddress}{url}: {response}");
        return blob;
    }

    /// <summary>
    /// Sends a GET request to the specified Uri and decodes the response as a CAR (Content-Addressable Archive).
    /// </summary>
    /// <param name="client">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="options">The JsonSerializerOptions for the request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="logger">The logger to use. This is optional and defaults to null.</param>
    /// <param name="progress">The progress reporter for the decoding process. This is optional and defaults to null.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Success response message as the result.</returns>
    public static async Task<Result<Success?>> GetCarAsync(
        this HttpClient client,
        string url,
        JsonSerializerOptions options,
        CancellationToken cancellationToken,
        ILogger? logger = default,
        OnCarDecoded? progress = null)
    {
        logger?.LogDebug($"GET {client.BaseAddress}{url}");
        using var message = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await CreateError(message!, options, cancellationToken, logger);
            return atError!;
        }

#if NETSTANDARD
        using var stream = await message.Content.ReadAsStreamAsync();
#else
        await using var stream = await message.Content.ReadAsStreamAsync(cancellationToken);
#endif
        await CarDecoder.DecodeCarAsync(stream, progress);
        return new Success();
    }

    /// <summary>
    /// Sends a GET request to the specified Uri and downloads the response as a CAR (Content-Addressable Archive) file.
    /// </summary>
    /// <param name="client">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="filePath">The path where the file should be saved.</param>
    /// <param name="fileName">The name of the file to be saved.</param>
    /// <param name="options">The JsonSerializerOptions for the request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="logger">The logger to use. This is optional and defaults to null.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Success response message as the result.</returns>
    public static async Task<Result<Success?>> DownloadCarAsync(
        this HttpClient client,
        string url,
        string filePath,
        string fileName,
        JsonSerializerOptions options,
        CancellationToken cancellationToken,
        ILogger? logger = default)
    {
        logger?.LogDebug($"GET {client.BaseAddress}{url}");

        using var message = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await CreateError(message!, options, cancellationToken, logger);
            return atError!;
        }

        var fileDownload = Path.Combine(filePath, StringExtensions.GenerateValidFilename(fileName));
#if NETSTANDARD
        using var content = File.Create(fileDownload);
        using var stream = await message.Content.ReadAsStreamAsync();
        await stream.CopyToAsync(content);
#else
        await using (FileStream content = File.Create(fileDownload))
        {
            await using var stream = await message.Content.ReadAsStreamAsync(cancellationToken);
            await stream.CopyToAsync(content, cancellationToken);
        }
#endif

        return new Success();
    }

    /// <summary>
    /// Sends a GET request to the specified Uri as an asynchronous operation and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The type of the response body.</typeparam>
    /// <param name="client">The HttpClient instance.</param>
    /// <param name="url">The Uri the request is sent to.</param>
    /// <param name="type">The JsonTypeInfo of the response body.</param>
    /// <param name="options">The JsonSerializerOptions for the request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <param name="logger">The logger to use. This is optional and defaults to null.</param>
    /// <param name="headers">Custom headers to include with the request.</param>
    /// <returns>The Task that represents the asynchronous operation. The value of the TResult parameter contains the Http response message as the result.</returns>
    public static async Task<Result<T?>> Get<T>(
        this HttpClient client,
        string url,
        JsonTypeInfo<T> type,
        JsonSerializerOptions options,
        CancellationToken cancellationToken,
        ILogger? logger = default,
        Dictionary<string, string>? headers = default)
    {
        logger?.LogDebug($"GET {client.BaseAddress}{url}");
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var message = await client.SendAsync(request, cancellationToken);

        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await CreateError(message!, options, cancellationToken, logger);
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
        logger?.LogDebug($"GET {client.BaseAddress}{url}: {response}");
        return JsonSerializer.Deserialize<T>(response, type);
    }

    /// <summary>
    /// Gets the DID Document for the given DID.
    /// </summary>
    /// <param name="client">HttpClient.</param>
    /// <param name="did">ATDid.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <param name="logger">Logger.</param>
    /// <returns>Result of DidDoc.</returns>
    /// <exception cref="NotImplementedException">Thrown if DID is not supported.</exception>
    public static async Task<Result<DidDoc?>> GetDidDocAsync(this HttpClient client, ATDid did, CancellationToken? cancellationToken = default, ILogger? logger = default)
    {
        var token = cancellationToken ??= CancellationToken.None;
        string url = did.Type switch
        {
            "plc" => $"https://plc.directory/{did}",
            "web" => $"https://{did.ToString().Split(':').Last()}/.well-known/did.json",
            _ => throw new NotImplementedException(),
        };

        logger?.LogDebug($"GET {url}");
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        using var message = await client.SendAsync(request, cancellationToken: token);
        if (!message.IsSuccessStatusCode)
        {
            ATError atError = await CreateError(message!, new JsonSerializerOptions(), token, logger);
            return atError!;
        }

        var result = await message.Content.ReadAsStringAsync();
        logger?.LogDebug($"GET {url}: {result}");
        return JsonSerializer.Deserialize<DidDoc>(result, SourceGenerationContext.Default.DidDoc);
    }

    /// <summary>
    /// Creates an ATError from an HttpResponseMessage.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="options">Options.</param>
    /// <param name="cancellationToken">CancellationToken.</param>
    /// <param name="logger">Logger.</param>
    /// <returns>ATError.</returns>
    internal static async Task<ATError> CreateError(HttpResponseMessage message, JsonSerializerOptions options, CancellationToken cancellationToken, ILogger? logger = default)
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
}

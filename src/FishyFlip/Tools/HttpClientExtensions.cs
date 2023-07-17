// <copyright file="HttpClientExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// HttpClient Extensions.
/// </summary>
internal static class HttpClientExtensions
{
    internal static async Task<Result<TK>> Post<T, TK>(
       this HttpClient client,
       string url,
       JsonSerializerOptions options,
       T body,
       CancellationToken cancellationToken,
       ILogger? logger = default)
    {
        var jsonContent = JsonSerializer.Serialize(body, options);
        StringContent content = new(jsonContent, Encoding.UTF8, "application/json");
        logger?.LogDebug($"POST {url}: {jsonContent}");
        using var message = await client.PostAsync(url, content, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            Error error = await CreateError(message!, options, cancellationToken, logger);
            return error!;
        }

        string response = await message.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrEmpty(response))
        {
            logger?.LogDebug($"POST {url}: No Response");
            return Activator.CreateInstance<TK>();
        }

        logger?.LogDebug($"POST {url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, options);
        return result!;
    }

    internal static async Task<Result<TK>> Post<TK>(
       this HttpClient client,
       string url,
       JsonSerializerOptions options,
       StreamContent body,
       CancellationToken cancellationToken,
       ILogger? logger = default)
    {
        logger?.LogDebug($"POST STREAM {url}: {body.Headers.ContentType}");
        using var message = await client.PostAsync(url, body, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            Error error = await CreateError(message!, options, cancellationToken, logger);
            return error!;
        }

        string response = await message.Content.ReadAsStringAsync(cancellationToken);
        logger?.LogDebug($"POST {url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, options);
        return result!;
    }

    internal static async Task<Result<TK>> Post<TK>(
        this HttpClient client,
        string url,
        JsonSerializerOptions options,
        CancellationToken cancellationToken,
        ILogger? logger = default)
    {
        logger?.LogDebug($"POST {url}");
        using var message = await client.PostAsync(url, null, cancellationToken: cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            Error error = await CreateError(message!, options, cancellationToken, logger);
            return error!;
        }

        string response = await message.Content.ReadAsStringAsync(cancellationToken);
        logger?.LogDebug($"POST {url}: {response}");
        TK? result = JsonSerializer.Deserialize<TK>(response, options);
        return result!;
    }

    internal static async Task<Result<Blob?>> GetBlob(
       this HttpClient client,
       string url,
       JsonSerializerOptions options,
       CancellationToken cancellationToken,
       ILogger? logger = default)
    {
        logger?.LogDebug($"GET {url}");
        using var message = await client.GetAsync(url, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            Error error = await CreateError(message!, options, cancellationToken, logger);
            return error!;
        }

        var blob = await message.Content.ReadAsByteArrayAsync(cancellationToken);
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
        logger?.LogDebug($"GET BLOB {url}: {response}");
        return new Blob(blob);
    }

    internal static async Task<Result<Success>> GetCarAsync(
        this HttpClient client,
        string url,
        JsonSerializerOptions options,
        CancellationToken cancellationToken,
        ILogger? logger = default,
        OnCarDecoded? progress = null)
    {
        logger?.LogDebug($"GET {url}");
        using var message = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            Error error = await CreateError(message!, options, cancellationToken, logger);
            return error!;
        }

        using var stream = await message.Content.ReadAsStreamAsync(cancellationToken);
        await CarDecoder.DecodeCarAsync(stream, progress);
        return new Success();
    }

    internal static async Task<Result<Success>> DownloadCarAsync(
        this HttpClient client,
        string url,
        string filePath,
        string fileName,
        JsonSerializerOptions options,
        CancellationToken cancellationToken,
        ILogger? logger = default)
    {
        logger?.LogDebug($"GET {url}");

        using var message = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            Error error = await CreateError(message!, options, cancellationToken, logger);
            return error!;
        }

        var fileDownload = Path.Combine(filePath, StringExtensions.GenerateValidFilename(fileName));
        using (var content = File.Create(fileDownload))
        {
            using var stream = await message.Content.ReadAsStreamAsync(cancellationToken);
            await stream.CopyToAsync(content, cancellationToken);
        }

        return new Success();
    }

    internal static async Task<Result<T?>> Get<T>(
        this HttpClient client,
        string url,
        JsonSerializerOptions options,
        CancellationToken cancellationToken,
        ILogger? logger = default)
    {
        logger?.LogDebug($"GET {url}");
        using var message = await client.GetAsync(url, cancellationToken);
        if (!message.IsSuccessStatusCode)
        {
            Error error = await CreateError(message!, options, cancellationToken, logger);
            return error!;
        }

        string response = await message.Content.ReadAsStringAsync(cancellationToken);
        logger?.LogDebug($"GET {url}: {response}");
        return JsonSerializer.Deserialize<T>(response, options);
    }

    private static async Task<Error> CreateError(HttpResponseMessage message, JsonSerializerOptions options, CancellationToken cancellationToken, ILogger? logger = default)
    {
        string response = await message.Content.ReadAsStringAsync(cancellationToken);
        Error error;
        ErrorDetail? detail = default;
        if (string.IsNullOrEmpty(response))
        {
            error = new Error((int)message.StatusCode, detail);
        }
        else
        {
            try
            {
                detail = JsonSerializer.Deserialize<ErrorDetail>(response, options);
                error = new Error((int)message.StatusCode, detail);
            }
            catch (Exception)
            {
                error = new Error((int)message.StatusCode, null);
            }
        }

        logger?.LogError($"Error: {error.StatusCode} {error.Detail?.Error} {error.Detail?.Message}");
        return error;
    }
}

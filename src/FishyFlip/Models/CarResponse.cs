// <copyright file="CarResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a response from a CAR file.
/// This can only be used once.
/// </summary>
public class CarResponse : IAsyncEnumerable<FrameEvent>, IDisposable
{
    private readonly HttpResponseMessage response;

    private bool disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="CarResponse"/> class.
    /// </summary>
    /// <param name="response">HttpResponseMessage.</param>
    internal CarResponse(HttpResponseMessage response)
    {
        this.response = response;
    }

    /// <summary>
    /// Fetch Repo Items from a given CarResponse.
    /// </summary>
    /// <returns><see cref="RepoResponse"/>.</returns>
    public RepoResponse FetchRepo()
    {
        if (this.disposedValue)
        {
            throw new ObjectDisposedException(nameof(CarResponse));
        }

        return RepoResponse.FromCarResponse(this);
    }

    /// <summary>
    /// Disposes the response.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.disposedValue = true;
        this.response.Dispose();
    }

    /// <inheritdoc/>
    public async IAsyncEnumerator<FrameEvent> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (this.disposedValue)
        {
            throw new ObjectDisposedException(nameof(CarResponse));
        }

#if NETSTANDARD
        var stream = await this.response.Content.ReadAsStreamAsync();
#else
        var stream = await this.response.Content.ReadAsStreamAsync(cancellationToken);
#endif

        await foreach (var item in CarDecoder.DecodeCarAsync(stream))
        {
            yield return item;
        }

        this.response.Dispose();
    }
}
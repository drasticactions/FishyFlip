// <copyright file="RepoResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.Com.Atproto.Sync;

namespace FishyFlip.Models;

/// <summary>
/// Wraps a <see cref="CarResponse"/> and returns <see cref="ATObject"/>s.
/// This can only be used once.
/// </summary>
public class RepoResponse : IAsyncEnumerable<ATObject>, IDisposable
{
    private readonly CarResponse carResponse;
    private bool disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepoResponse"/> class.
    /// </summary>
    /// <param name="carResponse">CarResponse.</param>
    internal RepoResponse(CarResponse carResponse)
    {
        this.carResponse = carResponse;
    }

    /// <summary>
    /// Creates a new <see cref="RepoResponse"/> from a <see cref="CarResponse"/>.
    /// </summary>
    /// <param name="carResponse"><see cref="CarResponse"/>.</param>
    /// <returns><see cref="RepoResponse"/>.</returns>
    public static RepoResponse FromCarResponse(CarResponse carResponse)
    {
        return new RepoResponse(carResponse);
    }

    /// <summary>
    /// Disposes the response.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.disposedValue = true;
        this.carResponse.Dispose();
    }

    /// <inheritdoc/>
    public async IAsyncEnumerator<ATObject> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (this.disposedValue)
        {
            throw new ObjectDisposedException(nameof(RepoResponse));
        }

        await foreach (var item in this.carResponse)
        {
            using var blockStream = new MemoryStream(item.Bytes);
            var blockObj = CBORObject.Read(blockStream);
            if (blockObj.IsATObject())
            {
                yield return blockObj.ToATObject();
            }
        }

        this.carResponse.Dispose();
    }
}
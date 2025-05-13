// <copyright file="DidDocInMemoryAsyncCache.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// In Memory DidDoc Async Cache.
/// </summary>
public class DidDocInMemoryAsyncCache : IAsyncCache<ATDid, DidDoc>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DidDocInMemoryAsyncCache"/> class.
    /// </summary>
    public DidDocInMemoryAsyncCache()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DidDocInMemoryAsyncCache"/> class.
    /// </summary>
    /// <param name="didDocCache">Original Cache.</param>
    public DidDocInMemoryAsyncCache(Dictionary<ATDid, DidDoc> didDocCache)
    {
        foreach (var kvp in didDocCache)
        {
            this.DidDocCache.TryAdd(kvp.Key, kvp.Value);
        }
    }

    /// <summary>
    /// Gets the in-memory cache for DidDocs.
    /// </summary>
    public ConcurrentDictionary<ATDid, DidDoc> DidDocCache { get; } = new();

    /// <inheritdoc/>
    public Task ClearAsync(CancellationToken cancellationToken = default)
    {
        this.DidDocCache.Clear();
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<long>(this.DidDocCache.Count);
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        this.DidDocCache.Clear();

#if NETSTANDARD
        return new ValueTask(Task.CompletedTask);
#else
        return ValueTask.CompletedTask;
#endif
    }

    /// <inheritdoc/>
    public Task<DidDoc?> GetAsync(ATDid key, CancellationToken cancellationToken = default)
    {
        if (this.DidDocCache.TryGetValue(key, out DidDoc? didDoc))
        {
            return Task.FromResult<DidDoc?>(didDoc);
        }

        return Task.FromResult<DidDoc?>(null);
    }

    /// <inheritdoc/>
    public Task<bool> RemoveAsync(ATDid key, CancellationToken cancellationToken = default)
    {
        if (this.DidDocCache.TryRemove(key, out _))
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    /// <inheritdoc/>
    public Task<bool> SetAsync(ATDid key, DidDoc value, CancellationToken cancellationToken = default)
    {
        if (this.DidDocCache.TryAdd(key, value))
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    /// <inheritdoc/>
    public Task<bool> TryGetAsync(ATDid key, out DidDoc? value, CancellationToken cancellationToken = default)
    {
        if (this.DidDocCache.TryGetValue(key, out DidDoc? didDoc))
        {
            value = didDoc;
            return Task.FromResult(true);
        }

        value = null;
        return Task.FromResult(false);
    }
}
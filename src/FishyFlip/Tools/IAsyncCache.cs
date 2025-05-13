// <copyright file="IAsyncCache.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// IAsyncCache interface.
/// This interface is used to define a cache that can store and retrieve data asynchronously.
/// It is a generic interface that takes two type parameters.
/// </summary>
/// <typeparam name="T1">Type 1.</typeparam>
/// <typeparam name="T2">Type 2.</typeparam>
public interface IAsyncCache<T1, T2> : IAsyncDisposable
{
    /// <summary>
    /// Asynchronously counts the number of items in the cache.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for cancellation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of items in the cache.</returns>
    Task<long> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key to look up.</param>
    /// <param name="cancellationToken">Cancellation token for cancellation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the value associated with the specified key.</returns>
    Task<T2?> GetAsync(T1 key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously sets the value for the specified key.
    /// </summary>
    /// <param name="key">The key to set.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="cancellationToken">Cancellation token for cancellation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the value if the value could be set.</returns>
    Task<bool> SetAsync(T1 key, T2 value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously tries to get the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key to look up.</param>
    /// <param name="value">The value.</param>
    /// <param name="cancellationToken">Cancellation token for cancellation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<bool> TryGetAsync(T1 key, out T2? value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously removes the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    /// <param name="cancellationToken">Cancellation token for cancellation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the value was removed, false otherwise.</returns>
    Task<bool> RemoveAsync(T1 key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously clears the cache.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for cancellation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ClearAsync(CancellationToken cancellationToken = default);
}
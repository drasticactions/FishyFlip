// <copyright file="IATIdentifierCacheResolver.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.Concurrent;

namespace FishyFlip.Tools;

/// <summary>
/// Interface for resolving AT identifiers with caching.
/// </summary>
public interface IATIdentifierCacheResolver : IATIdentifierResolver
{
    /// <summary>
    /// Clears the cache.
    /// </summary>
    /// <returns>The task representing the asynchronous operation.</returns>
    Task ClearAsync();

    /// <summary>
    /// Adds an entry to the cache.
    /// </summary>
    /// <param name="key">The key to add.</param>
    /// <param name="value">The value to add.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    Task AddAsync(ATIdentifier key, string value);

    /// <summary>
    /// Removes an entry from the cache.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    Task RemoveAsync(ATIdentifier key);
}

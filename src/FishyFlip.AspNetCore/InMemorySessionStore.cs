// <copyright file="InMemorySessionStore.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FishyFlip.AspNetCore;

/// <summary>
/// In-memory implementation of ISessionStore using IMemoryCache.
/// </summary>
public class InMemorySessionStore : ISessionStore
{
    private readonly IMemoryCache cache;
    private readonly TimeSpan defaultExpiration;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemorySessionStore"/> class.
    /// </summary>
    /// <param name="cache">The memory cache instance.</param>
    /// <param name="defaultExpiration">Default session expiration time. Defaults to 24 hours.</param>
    public InMemorySessionStore(IMemoryCache cache, TimeSpan? defaultExpiration = null)
    {
        this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        this.defaultExpiration = defaultExpiration ?? TimeSpan.FromHours(24);
    }

    /// <inheritdoc/>
    public Task StoreSessionAsync(string userId, AuthSession authSession, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentNullException.ThrowIfNull(authSession);

        var key = GetCacheKey(userId);
        var expiration = authSession.Session.ExpiresIn > DateTime.UtcNow
            ? authSession.Session.ExpiresIn.Subtract(DateTime.UtcNow)
            : this.defaultExpiration;

        this.cache.Set(key, authSession, expiration);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<AuthSession?> GetSessionAsync(string userId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);

        var key = GetCacheKey(userId);
        var session = this.cache.Get<AuthSession>(key);
        return Task.FromResult(session);
    }

    /// <inheritdoc/>
    public Task RemoveSessionAsync(string userId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);

        var key = GetCacheKey(userId);
        this.cache.Remove(key);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task UpdateSessionAsync(string userId, AuthSession authSession, CancellationToken cancellationToken = default)
    {
        return this.StoreSessionAsync(userId, authSession, cancellationToken);
    }

    private static string GetCacheKey(string userId)
    {
        return $"fishyflip:session:{userId}";
    }
}
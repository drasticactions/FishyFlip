// <copyright file="OAuthFlowManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FishyFlip.AspNetCore.Services;

/// <summary>
/// Implementation of IOAuthFlowManager using in-memory storage.
/// </summary>
public class OAuthFlowManager : IOAuthFlowManager
{
    private static readonly TimeSpan StateExpiration = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan CodeVerifierExpiration = TimeSpan.FromMinutes(10);

    private readonly IMemoryCache cache;
    private readonly ILogger<OAuthFlowManager> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthFlowManager"/> class.
    /// </summary>
    /// <param name="cache">The memory cache.</param>
    /// <param name="logger">The logger.</param>
    public OAuthFlowManager(IMemoryCache cache, ILogger<OAuthFlowManager> logger)
    {
        this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public Task StoreOAuthStateAsync(string state, string userId, string clientId, string redirectUrl, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(state);
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        ArgumentException.ThrowIfNullOrEmpty(redirectUrl);

        var stateData = new OAuthStateData(userId, clientId, redirectUrl);
        var key = GetStateKey(state);

        this.cache.Set(key, stateData, StateExpiration);
        this.logger.LogDebug("Stored OAuth state {State} for user {UserId}", state, userId);

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<(string UserId, string ClientId, string RedirectUrl)?> ConsumeOAuthStateAsync(string state, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(state);

        var key = GetStateKey(state);
        if (this.cache.TryGetValue(key, out OAuthStateData? stateData))
        {
            this.cache.Remove(key);
            this.logger.LogDebug("Consumed OAuth state {State} for user {UserId}", state, stateData!.UserId);
            return Task.FromResult<(string, string, string)?>((stateData.UserId, stateData.ClientId, stateData.RedirectUrl));
        }

        this.logger.LogWarning("OAuth state {State} not found or expired", state);
        return Task.FromResult<(string, string, string)?>(null);
    }

    /// <inheritdoc/>
    public Task StoreCodeVerifierAsync(string userId, string codeVerifier, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(codeVerifier);

        var key = GetCodeVerifierKey(userId);
        this.cache.Set(key, codeVerifier, CodeVerifierExpiration);
        this.logger.LogDebug("Stored code verifier for user {UserId}", userId);

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<string?> ConsumeCodeVerifierAsync(string userId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);

        var key = GetCodeVerifierKey(userId);
        if (this.cache.TryGetValue(key, out string? codeVerifier))
        {
            this.cache.Remove(key);
            this.logger.LogDebug("Consumed code verifier for user {UserId}", userId);
            return Task.FromResult<string?>(codeVerifier);
        }

        this.logger.LogWarning("Code verifier for user {UserId} not found or expired", userId);
        return Task.FromResult<string?>(null);
    }

    private static string GetStateKey(string state) => $"oauth_state:{state}";

    private static string GetCodeVerifierKey(string userId) => $"code_verifier:{userId}";

    private record OAuthStateData(string UserId, string ClientId, string RedirectUrl);
}
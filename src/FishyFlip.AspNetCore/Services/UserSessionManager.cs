// <copyright file="UserSessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.Concurrent;
using FishyFlip.Lexicon.Com.Atproto.Server;
using FishyFlip.Models;
using Microsoft.Extensions.Logging;

namespace FishyFlip.AspNetCore.Services;

/// <summary>
/// Implementation of IUserSessionManager for managing multiple user sessions.
/// </summary>
public class UserSessionManager : IUserSessionManager
{
    private readonly ISessionStore sessionStore;
    private readonly FishyFlipOptions options;
    private readonly ILogger<UserSessionManager> logger;
    private readonly ConcurrentDictionary<string, OAuth2SessionManager> oauthManagers = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSessionManager"/> class.
    /// </summary>
    /// <param name="sessionStore">The session store.</param>
    /// <param name="options">FishyFlip options.</param>
    /// <param name="logger">The logger.</param>
    public UserSessionManager(
        ISessionStore sessionStore,
        FishyFlipOptions options,
        ILogger<UserSessionManager> logger)
    {
        this.sessionStore = sessionStore ?? throw new ArgumentNullException(nameof(sessionStore));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public async Task<(string SessionId, AuthSession AuthSession)?> CreatePasswordSessionAsync(
        string identifier,
        string password,
        string? instanceUrl = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(identifier);
        ArgumentException.ThrowIfNullOrEmpty(password);

        instanceUrl ??= this.options.InstanceUrl;

        try
        {
            using var passwordManager = new PasswordSessionManager(this.logger);
            var result = await passwordManager.CreateSessionAsync(identifier, password, instanceUrl, cancellationToken);

            if (result.IsT0 && result.AsT0 != null)
            {
                var sessionId = GenerateSessionId();
                var authSession = new AuthSession(result.AsT0);

                await this.sessionStore.StoreSessionAsync(sessionId, authSession, cancellationToken);

                this.logger.LogInformation("Created password session for user {Identifier}", identifier);
                return (sessionId, authSession);
            }

            this.logger.LogWarning("Failed to create password session for user {Identifier}: {Error}", identifier, result.AsT1?.Detail);
            return null;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error creating password session for user {Identifier}", identifier);
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<string?> StartOAuthFlowAsync(
        string userId,
        string clientId,
        string redirectUrl,
        string state,
        IEnumerable<string> scopes,
        string? loginHint = null,
        string? instanceUrl = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        ArgumentException.ThrowIfNullOrEmpty(redirectUrl);

        instanceUrl ??= this.options.InstanceUrl;

        try
        {
            var oauthManager = new OAuth2SessionManager(this.logger);
            this.oauthManagers[userId] = oauthManager;

            var result = await oauthManager.StartAuthorizationAsync(
                clientId,
                redirectUrl,
                scopes,
                loginHint,
                instanceUrl,
                state,
                cancellationToken);

            if (result.IsT0)
            {
                this.logger.LogInformation("Started OAuth flow for user {UserId}", userId);
                return result.AsT0;
            }

            this.logger.LogWarning("Failed to start OAuth flow for user {UserId}: {Error}", userId, result.AsT1?.Detail);
            this.oauthManagers.TryRemove(userId, out _);
            return null;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error starting OAuth flow for user {UserId}", userId);
            this.oauthManagers.TryRemove(userId, out _);
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<(string SessionId, AuthSession AuthSession)?> CompleteOAuthFlowAsync(
        string userId,
        string callbackData,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        ArgumentException.ThrowIfNullOrEmpty(callbackData);

        if (!this.oauthManagers.TryGetValue(userId, out var oauthManager))
        {
            this.logger.LogWarning("No OAuth manager found for user {UserId}", userId);
            return null;
        }

        try
        {
            var result = await oauthManager.CompleteAuthorizationAsync(callbackData, cancellationToken);

            if (result.IsT0 && result.AsT0 != null)
            {
                var sessionId = GenerateSessionId();
                var authSession = new AuthSession(result.AsT0, oauthManager.OAuthSession?.ProofKey ?? string.Empty);
                await this.sessionStore.StoreSessionAsync(sessionId, authSession, cancellationToken);
                this.logger.LogInformation("Completed OAuth flow for user {UserId}", userId);
                return (sessionId, authSession);
            }

            this.logger.LogWarning("Failed to complete OAuth flow for user {UserId}: {Error}", userId, result.AsT1?.Detail);
            return null;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error completing OAuth flow for user {UserId}", userId);
            return null;
        }
        finally
        {
            this.oauthManagers.TryRemove(userId, out _);
            oauthManager?.Dispose();
        }
    }

    /// <inheritdoc/>
    public async Task<AuthSession?> GetSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(sessionId);

        try
        {
            return await this.sessionStore.GetSessionAsync(sessionId, cancellationToken);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error retrieving session {SessionId}", sessionId);
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<AuthSession?> RefreshSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(sessionId);

        try
        {
            var authSession = await this.sessionStore.GetSessionAsync(sessionId, cancellationToken);
            if (authSession == null)
            {
                return null;
            }

            ISessionManager sessionManager = authSession.ProofKey != null
                ? new OAuth2SessionManager(this.logger)
                : new PasswordSessionManager(this.logger);

            Result<RefreshSessionOutput?> refreshResult;

            if (sessionManager is OAuth2SessionManager oauthManager)
            {
                var startResult = await oauthManager.StartSessionAsync(
                    authSession,
                    this.options.ClientId ?? string.Empty,
                    this.options.InstanceUrl,
                    cancellationToken);

                if (startResult.IsT1 || startResult.AsT0 == null)
                {
                    return null;
                }

                refreshResult = await oauthManager.RefreshSessionAsync(cancellationToken);
            }
            else if (sessionManager is PasswordSessionManager passwordManager)
            {
                passwordManager.SetSession(authSession.Session);
                refreshResult = await passwordManager.RefreshSessionAsync(cancellationToken);
            }
            else
            {
                return null;
            }

            if (refreshResult.IsT0 && refreshResult.AsT0 != null)
            {
                var newSession = new Session(
                    authSession.Session.Did,
                    authSession.Session.DidDoc,
                    authSession.Session.Handle,
                    authSession.Session.Email,
                    refreshResult.AsT0.AccessJwt,
                    refreshResult.AsT0.RefreshJwt,
                    DateTime.UtcNow.AddSeconds(3600));

                var updatedAuthSession = new AuthSession(newSession, authSession.ProofKey!);
                await this.sessionStore.UpdateSessionAsync(sessionId, updatedAuthSession, cancellationToken);

                this.logger.LogInformation("Refreshed session {SessionId}", sessionId);
                return updatedAuthSession;
            }

            this.logger.LogWarning("Failed to refresh session {SessionId}", sessionId);
            return null;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error refreshing session {SessionId}", sessionId);
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task RemoveSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(sessionId);

        try
        {
            await this.sessionStore.RemoveSessionAsync(sessionId, cancellationToken);
            this.logger.LogInformation("Removed session {SessionId}", sessionId);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error removing session {SessionId}", sessionId);
        }
    }

    /// <inheritdoc/>
    public async Task<ATProtocol?> GetATProtocolForSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(sessionId);

        try
        {
            var authSession = await this.GetSessionAsync(sessionId, cancellationToken);
            if (authSession == null)
            {
                return null;
            }

            if (authSession.Session.ExpiresIn <= DateTime.UtcNow.Add(this.options.RefreshThreshold))
            {
                authSession = await this.RefreshSessionAsync(sessionId, cancellationToken);
                if (authSession == null)
                {
                    return null;
                }
            }

            ISessionManager sessionManager = !string.IsNullOrEmpty(authSession.ProofKey)
                ? new OAuth2SessionManager(this.logger)
                : new PasswordSessionManager(this.logger);

            if (sessionManager is OAuth2SessionManager oauthManager)
            {
                var result = await oauthManager.StartSessionAsync(
                    authSession,
                    this.options.ClientId ?? string.Empty,
                    this.options.InstanceUrl,
                    cancellationToken);

                if (result.IsT1 || result.AsT0 == null)
                {
                    return null;
                }

                return oauthManager.Protocol;
            }
            else if (sessionManager is PasswordSessionManager passwordManager)
            {
                passwordManager.SetSession(authSession.Session);
                return passwordManager.Protocol;
            }

            return null;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error getting ATProtocol for session {SessionId}", sessionId);
            return null;
        }
    }

    private static string GenerateSessionId()
    {
        return Guid.NewGuid().ToString("N");
    }
}
// <copyright file="IUserSessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;

namespace FishyFlip.AspNetCore.Services;

/// <summary>
/// Interface for managing user sessions in multi-tenant server environments.
/// </summary>
public interface IUserSessionManager
{
    /// <summary>
    /// Creates a new session for a user using password authentication.
    /// </summary>
    /// <param name="identifier">The user identifier (handle or email).</param>
    /// <param name="password">The user's password.</param>
    /// <param name="instanceUrl">The ATProtocol instance URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The session ID and AuthSession if successful.</returns>
    Task<(string SessionId, AuthSession AuthSession)?> CreatePasswordSessionAsync(
        string identifier,
        string password,
        string? instanceUrl = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts an OAuth authorization flow for a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="clientId">The OAuth client ID.</param>
    /// <param name="redirectUrl">The OAuth redirect URL.</param>
    /// <param name="state">The state parameter for CSRF protection.</param>
    /// <param name="scopes">The requested OAuth scopes.</param>
    /// <param name="loginHint">Optional login hint.</param>
    /// <param name="instanceUrl">The ATProtocol instance URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authorization URL if successful.</returns>
    Task<string?> StartOAuthFlowAsync(
        string userId,
        string clientId,
        string redirectUrl,
        string state,
        IEnumerable<string> scopes,
        string? loginHint = null,
        string? instanceUrl = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Completes an OAuth authorization flow and creates a session.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="callbackData">The OAuth callback data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The session ID and AuthSession if successful.</returns>
    Task<(string SessionId, AuthSession AuthSession)?> CompleteOAuthFlowAsync(
        string userId,
        string callbackData,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a stored session for a user.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The AuthSession if found.</returns>
    Task<AuthSession?> GetSessionAsync(string sessionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Refreshes a user's session tokens.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated AuthSession if successful.</returns>
    Task<AuthSession?> RefreshSessionAsync(string sessionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a user's session.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the async operation.</returns>
    Task RemoveSessionAsync(string sessionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an ATProtocol instance configured with the user's session.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An ATProtocol instance if session is valid.</returns>
    Task<ATProtocol?> GetATProtocolForSessionAsync(string sessionId, CancellationToken cancellationToken = default);
}
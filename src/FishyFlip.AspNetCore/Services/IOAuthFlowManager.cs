// <copyright file="IOAuthFlowManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.AspNetCore.Services;

/// <summary>
/// Interface for managing OAuth flows in server environments.
/// </summary>
public interface IOAuthFlowManager
{
    /// <summary>
    /// Stores OAuth state for CSRF protection.
    /// </summary>
    /// <param name="state">The OAuth state parameter.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="clientId">The OAuth client ID.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the async operation.</returns>
    Task StoreOAuthStateAsync(string state, string userId, string clientId, string redirectUrl, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves and removes OAuth state information.
    /// </summary>
    /// <param name="state">The OAuth state parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The OAuth state information if found.</returns>
    Task<(string UserId, string ClientId, string RedirectUrl)?> ConsumeOAuthStateAsync(string state, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores OAuth code verifier for PKCE flow.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="codeVerifier">The code verifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the async operation.</returns>
    Task StoreCodeVerifierAsync(string userId, string codeVerifier, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves and removes OAuth code verifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The code verifier if found.</returns>
    Task<string?> ConsumeCodeVerifierAsync(string userId, CancellationToken cancellationToken = default);
}
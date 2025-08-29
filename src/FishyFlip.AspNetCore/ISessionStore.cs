// <copyright file="ISessionStore.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;

namespace FishyFlip.AspNetCore;

/// <summary>
/// Interface for storing and retrieving user sessions in server environments.
/// </summary>
public interface ISessionStore
{
    /// <summary>
    /// Stores an AuthSession for a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="authSession">The AuthSession to store.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the async operation.</returns>
    Task StoreSessionAsync(string userId, AuthSession authSession, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an AuthSession for a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The stored AuthSession, or null if not found.</returns>
    Task<AuthSession?> GetSessionAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a stored session for a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the async operation.</returns>
    Task RemoveSessionAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing session for a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="authSession">The updated AuthSession.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the async operation.</returns>
    Task UpdateSessionAsync(string userId, AuthSession authSession, CancellationToken cancellationToken = default);
}
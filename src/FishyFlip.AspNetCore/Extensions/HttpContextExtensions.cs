// <copyright file="HttpContextExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FishyFlip.AspNetCore.Extensions;

/// <summary>
/// Extension methods for HttpContext to work with FishyFlip sessions.
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// Gets an ATProtocol instance for the current authenticated user.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>ATProtocol instance if user is authenticated.</returns>
    public static async Task<ATProtocol?> GetUserATProtocolAsync(this HttpContext context, CancellationToken cancellationToken = default)
    {
        var sessionId = context.User?.FindFirst("bluesky_session_id")?.Value;
        if (string.IsNullOrEmpty(sessionId))
        {
            return null;
        }

        var userSessionManager = context.RequestServices.GetRequiredService<IUserSessionManager>();
        return await userSessionManager.GetATProtocolForSessionAsync(sessionId, cancellationToken);
    }

    /// <summary>
    /// Gets the current user's Bluesky DID.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The user's DID if authenticated.</returns>
    public static string? GetBlueskyDid(this HttpContext context)
    {
        return context.User?.FindFirst("bluesky_did")?.Value;
    }

    /// <summary>
    /// Gets the current user's Bluesky handle.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The user's handle if authenticated.</returns>
    public static string? GetBlueskyHandle(this HttpContext context)
    {
        return context.User?.FindFirst("bluesky_handle")?.Value;
    }

    /// <summary>
    /// Gets the current user's email.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The user's email if available.</returns>
    public static string? GetBlueskyEmail(this HttpContext context)
    {
        return context.User?.FindFirst("bluesky_email")?.Value;
    }
}
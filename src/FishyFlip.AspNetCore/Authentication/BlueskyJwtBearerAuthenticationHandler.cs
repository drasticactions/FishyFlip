// <copyright file="BlueskyJwtBearerAuthenticationHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Security.Claims;
using System.Text.Encodings.Web;
using FishyFlip.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FishyFlip.AspNetCore.Authentication;

/// <summary>
/// Authentication handler for Bluesky JWT bearer token authentication.
/// </summary>
public class BlueskyJwtBearerAuthenticationHandler : AuthenticationHandler<BlueskyJwtBearerSchemeOptions>
{
    private readonly IUserSessionManager userSessionManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlueskyJwtBearerAuthenticationHandler"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="encoder">The URL encoder.</param>
    /// <param name="userSessionManager">The user session manager.</param>
    public BlueskyJwtBearerAuthenticationHandler(
        IOptionsMonitor<BlueskyJwtBearerSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IUserSessionManager userSessionManager)
        : base(options, logger, encoder)
    {
        this.userSessionManager = userSessionManager ?? throw new ArgumentNullException(nameof(userSessionManager));
    }

    /// <inheritdoc/>
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = this.Context.Request.Headers.Authorization.FirstOrDefault();
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return AuthenticateResult.NoResult();
        }

        if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Invalid authorization header format");
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        if (string.IsNullOrEmpty(token))
        {
            return AuthenticateResult.Fail("Missing bearer token");
        }

        try
        {
            var sessionId = ExtractSessionIdFromToken(token);
            if (string.IsNullOrEmpty(sessionId))
            {
                return AuthenticateResult.Fail("Invalid token format");
            }

            var authSession = await this.userSessionManager.GetSessionAsync(sessionId, this.Context.RequestAborted);
            if (authSession == null)
            {
                return AuthenticateResult.Fail("Invalid session");
            }

            if (authSession.Session.AccessJwt != token)
            {
                return AuthenticateResult.Fail("Token mismatch");
            }

            if (authSession.Session.ExpiresIn <= DateTime.UtcNow)
            {
                var refreshed = await this.userSessionManager.RefreshSessionAsync(sessionId, this.Context.RequestAborted);
                if (refreshed == null)
                {
                    await this.userSessionManager.RemoveSessionAsync(sessionId, this.Context.RequestAborted);
                    return AuthenticateResult.Fail("Session expired and refresh failed");
                }

                authSession = refreshed;
            }

            var claims = new List<Claim>
            {
                new(this.Options.DidClaimType, authSession.Session.Did.ToString()),
                new(this.Options.HandleClaimType, authSession.Session.Handle.ToString()),
                new(this.Options.SessionIdClaimType, sessionId),
                new(ClaimTypes.NameIdentifier, authSession.Session.Did.ToString()),
                new(ClaimTypes.Name, authSession.Session.Handle.ToString()),
            };

            if (!string.IsNullOrEmpty(authSession.Session.Email))
            {
                claims.Add(new Claim(this.Options.EmailClaimType, authSession.Session.Email));
                claims.Add(new Claim(ClaimTypes.Email, authSession.Session.Email));
            }

            var identity = new ClaimsIdentity(claims, this.Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, "Error during JWT bearer authentication");
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    private static string? ExtractSessionIdFromToken(string token)
    {
        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                return null;
            }

            var payload = parts[1];
            payload = payload.PadRight(payload.Length + ((4 - (payload.Length % 4)) % 4), '=');
            var payloadBytes = Convert.FromBase64String(payload);
            var payloadJson = System.Text.Encoding.UTF8.GetString(payloadBytes);

            var tokenData = System.Text.Json.JsonDocument.Parse(payloadJson);
            return tokenData.RootElement.TryGetProperty("sub", out var subElement) ? subElement.GetString() : null;
        }
        catch
        {
            return null;
        }
    }
}
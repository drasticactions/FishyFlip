// <copyright file="BlueskyAuthenticationHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Security.Claims;
using System.Text.Encodings.Web;
using FishyFlip.AspNetCore.Services;
using FishyFlip.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FishyFlip.AspNetCore.Authentication;

/// <summary>
/// Authentication handler for Bluesky cookie-based authentication.
/// </summary>
public class BlueskyAuthenticationHandler : AuthenticationHandler<BlueskyAuthenticationSchemeOptions>
{
    private readonly IUserSessionManager userSessionManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlueskyAuthenticationHandler"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="encoder">The URL encoder.</param>
    /// <param name="userSessionManager">The user session manager.</param>
    public BlueskyAuthenticationHandler(
        IOptionsMonitor<BlueskyAuthenticationSchemeOptions> options,
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
        var sessionId = this.Context.Request.Cookies["fishyflip_session_id"];
        if (string.IsNullOrEmpty(sessionId))
        {
            return AuthenticateResult.NoResult();
        }

        try
        {
            var authSession = await this.userSessionManager.GetSessionAsync(sessionId, this.Context.RequestAborted);
            if (authSession == null)
            {
                this.Context.Response.Cookies.Delete("fishyflip_session_id");
                return AuthenticateResult.Fail("Invalid session");
            }

            if (authSession.Session.ExpiresIn <= DateTime.UtcNow)
            {
                var refreshed = await this.userSessionManager.RefreshSessionAsync(sessionId, this.Context.RequestAborted);
                if (refreshed == null)
                {
                    await this.userSessionManager.RemoveSessionAsync(sessionId, this.Context.RequestAborted);
                    this.Context.Response.Cookies.Delete("fishyflip_session_id");
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
            this.Logger.LogError(ex, "Error during Bluesky authentication");
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    /// <inheritdoc/>
    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        var redirectUrl = properties.RedirectUri ?? "/";
        var loginUrl = $"/auth/bluesky/login?returnUrl={Uri.EscapeDataString(redirectUrl)}";
        this.Context.Response.Redirect(loginUrl);
        return Task.CompletedTask;
    }
}
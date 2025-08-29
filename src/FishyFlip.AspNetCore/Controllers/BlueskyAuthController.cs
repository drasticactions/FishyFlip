// <copyright file="BlueskyAuthController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FishyFlip.AspNetCore.Controllers;

/// <summary>
/// Controller for handling Bluesky authentication flows.
/// </summary>
[Route("auth/bluesky")]
[ApiController]
public class BlueskyAuthController : ControllerBase
{
    private readonly IUserSessionManager userSessionManager;
    private readonly IOAuthFlowManager oauthFlowManager;
    private readonly FishyFlipOptions options;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlueskyAuthController"/> class.
    /// </summary>
    /// <param name="userSessionManager">The user session manager.</param>
    /// <param name="oauthFlowManager">The OAuth flow manager.</param>
    /// <param name="options">FishyFlip options.</param>
    public BlueskyAuthController(
        IUserSessionManager userSessionManager,
        IOAuthFlowManager oauthFlowManager,
        FishyFlipOptions options)
    {
        this.userSessionManager = userSessionManager ?? throw new ArgumentNullException(nameof(userSessionManager));
        this.oauthFlowManager = oauthFlowManager ?? throw new ArgumentNullException(nameof(oauthFlowManager));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Initiates password-based login.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Login result.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(request.Identifier) || string.IsNullOrEmpty(request.Password))
        {
            return this.BadRequest("Identifier and password are required");
        }

        var result = await this.userSessionManager.CreatePasswordSessionAsync(
            request.Identifier,
            request.Password,
            request.InstanceUrl ?? this.options.InstanceUrl,
            cancellationToken);

        if (result == null)
        {
            return this.Unauthorized("Invalid credentials");
        }

        var (sessionId, authSession) = result.Value;

        var claims = new List<Claim>
        {
            new("bluesky_did", authSession.Session.Did.ToString()),
            new("bluesky_handle", authSession.Session.Handle.ToString()),
            new("bluesky_session_id", sessionId),
            new(ClaimTypes.NameIdentifier, authSession.Session.Did.ToString()),
            new(ClaimTypes.Name, authSession.Session.Handle.ToString()),
        };

        if (!string.IsNullOrEmpty(authSession.Session.Email))
        {
            claims.Add(new Claim("bluesky_email", authSession.Session.Email));
            claims.Add(new Claim(ClaimTypes.Email, authSession.Session.Email));
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        this.HttpContext.Response.Cookies.Append("fishyflip_session_id", sessionId, new CookieOptions
        {
            HttpOnly = true,
            Secure = this.HttpContext.Request.IsHttps,
            SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax,
            Expires = authSession.Session.ExpiresIn,
        });

        return this.Ok(new LoginResponse(sessionId, authSession.Session.Did.ToString(), authSession.Session.Handle.ToString()));
    }

    /// <summary>
    /// Initiates OAuth authorization flow.
    /// </summary>
    /// <param name="loginHint">Optional login hint.</param>
    /// <param name="returnUrl">Return URL after authentication.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Redirect to OAuth authorization URL.</returns>
    [HttpGet("oauth/authorize")]
    public async Task<IActionResult> AuthorizeAsync(string? loginHint = null, string? returnUrl = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(this.options.ClientId) || string.IsNullOrEmpty(this.options.RedirectUri))
        {
            return this.BadRequest("OAuth not configured");
        }

        var userId = Guid.NewGuid().ToString("N");
        var state = Guid.NewGuid().ToString("N");

        await this.oauthFlowManager.StoreOAuthStateAsync(
            state,
            userId,
            this.options.ClientId,
            this.options.RedirectUri,
            cancellationToken);

        var authUrl = await this.userSessionManager.StartOAuthFlowAsync(
            userId,
            this.options.ClientId,
            this.options.RedirectUri,
            state,
            this.options.Scopes,
            loginHint,
            this.options.InstanceUrl,
            cancellationToken);

        if (string.IsNullOrEmpty(authUrl))
        {
            return this.BadRequest("Failed to initiate OAuth flow");
        }

        var finalAuthUrl = $"{authUrl}&state={state}";
        if (!string.IsNullOrEmpty(returnUrl))
        {
            this.HttpContext.Session.SetString("return_url", returnUrl);
        }

        return this.Redirect(finalAuthUrl);
    }

    /// <summary>
    /// Handles OAuth callback.
    /// </summary>
    /// <param name="code">Authorization code.</param>
    /// <param name="state">OAuth state.</param>
    /// <param name="error">Error parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Redirect result.</returns>
    [HttpGet("oauth/callback")]
    public async Task<IActionResult> CallbackAsync(string? code = null, string? state = null, string? error = null, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(error))
        {
            return this.BadRequest($"OAuth error: {error}");
        }

        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
        {
            return this.BadRequest("Missing code or state parameter");
        }

        var stateData = await this.oauthFlowManager.ConsumeOAuthStateAsync(state, cancellationToken);
        if (stateData == null)
        {
            return this.BadRequest("Invalid or expired state");
        }

        var (userId, clientId, redirectUrl) = stateData.Value;

        var callbackData = this.HttpContext.Request.QueryString.ToString();
        var result = await this.userSessionManager.CompleteOAuthFlowAsync(userId, callbackData, cancellationToken);

        if (result == null)
        {
            return this.BadRequest("Failed to complete OAuth flow");
        }

        var (sessionId, authSession) = result.Value;

        var claims = new List<Claim>
        {
            new("bluesky_did", authSession.Session.Did.ToString()),
            new("bluesky_handle", authSession.Session.Handle.ToString()),
            new("bluesky_session_id", sessionId),
            new(ClaimTypes.NameIdentifier, authSession.Session.Did.ToString()),
            new(ClaimTypes.Name, authSession.Session.Handle.ToString()),
        };

        if (!string.IsNullOrEmpty(authSession.Session.Email))
        {
            claims.Add(new Claim("bluesky_email", authSession.Session.Email));
            claims.Add(new Claim(ClaimTypes.Email, authSession.Session.Email));
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        this.HttpContext.Response.Cookies.Append("fishyflip_session_id", sessionId, new CookieOptions
        {
            HttpOnly = true,
            Secure = this.HttpContext.Request.IsHttps,
            SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax,
            Expires = authSession.Session.ExpiresIn,
        });

        var returnUrl = this.HttpContext.Session.GetString("return_url") ?? "/";
        this.HttpContext.Session.Remove("return_url");

        return this.Redirect(returnUrl);
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Logout result.</returns>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync(CancellationToken cancellationToken = default)
    {
        var sessionId = this.User.FindFirst("bluesky_session_id")?.Value;
        if (!string.IsNullOrEmpty(sessionId))
        {
            await this.userSessionManager.RemoveSessionAsync(sessionId, cancellationToken);
        }

        await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        this.HttpContext.Response.Cookies.Delete("fishyflip_session_id");

        return this.Ok();
    }

    /// <summary>
    /// Gets the current user's session information.
    /// </summary>
    /// <returns>Session information.</returns>
    [HttpGet("session")]
    [Authorize]
    public IActionResult GetSession()
    {
        var did = this.User.FindFirst("bluesky_did")?.Value;
        var handle = this.User.FindFirst("bluesky_handle")?.Value;
        var email = this.User.FindFirst("bluesky_email")?.Value;

        return this.Ok(new SessionInfo(did, handle, email));
    }
}

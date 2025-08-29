// <copyright file="AccountController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore;
using FishyFlip.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetOAuthSample.Controllers;

public class AccountController : Controller
{
    private readonly IUserSessionManager userSessionManager;
    private readonly IOAuthFlowManager oauthFlowManager;
    private readonly FishyFlipOptions options;
    private readonly ILogger<AccountController> logger;

    public AccountController(
        IUserSessionManager userSessionManager,
        IOAuthFlowManager oauthFlowManager,
        FishyFlipOptions options,
        ILogger<AccountController> logger)
    {
        this.userSessionManager = userSessionManager;
        this.oauthFlowManager = oauthFlowManager;
        this.options = options;
        this.logger = logger;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (this.User.Identity?.IsAuthenticated == true)
        {
            return this.RedirectToLocal(returnUrl);
        }

        this.ViewData["ReturnUrl"] = returnUrl;
        this.ViewData["ClientId"] = this.options.ClientId;
        this.ViewData["RedirectUri"] = this.options.RedirectUri;

        return this.View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartOAuth(string? returnUrl = null, string? loginHint = null)
    {
        if (string.IsNullOrEmpty(this.options.ClientId) || string.IsNullOrEmpty(this.options.RedirectUri))
        {
            this.ModelState.AddModelError(string.Empty, "OAuth is not properly configured. Please check your client ID and redirect URI.");
            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View("Login");
        }

        try
        {
            var userId = Guid.NewGuid().ToString("N");
            var state = Guid.NewGuid().ToString("N");

            // Store return URL in session for after OAuth completion
            if (!string.IsNullOrEmpty(returnUrl))
            {
                this.HttpContext.Session.SetString($"return_url_{state}", returnUrl);
            }

            await this.oauthFlowManager.StoreOAuthStateAsync(
                state,
                userId,
                this.options.ClientId,
                this.options.RedirectUri);

            var authUrl = await this.userSessionManager.StartOAuthFlowAsync(
                userId,
                this.options.ClientId,
                this.options.RedirectUri,
                state,
                this.options.Scopes,
                loginHint,
                this.options.InstanceUrl);

            if (string.IsNullOrEmpty(authUrl))
            {
                this.ModelState.AddModelError(string.Empty, "Failed to initiate OAuth flow. Please try again.");
                this.ViewData["ReturnUrl"] = returnUrl;
                return this.View("Login");
            }

            // Add state parameter to auth URL
            var finalAuthUrl = $"{authUrl}&state={state}";

            this.logger.LogInformation("Redirecting to OAuth authorization URL for user {UserId}", userId);
            return this.Redirect(finalAuthUrl);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error starting OAuth flow");
            this.ModelState.AddModelError(string.Empty, "An error occurred while starting the OAuth flow. Please try again.");
            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View("Login");
        }
    }

    [HttpGet]
    public async Task<IActionResult> OAuthCallback(string? code = null, string? state = null, string? error = null)
    {
        if (!string.IsNullOrEmpty(error))
        {
            this.logger.LogWarning("OAuth callback received error: {Error}", error);
            this.TempData["Error"] = $"Authentication failed: {error}";
            return this.RedirectToAction("Login");
        }

        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
        {
            this.logger.LogWarning("OAuth callback missing required parameters");
            this.TempData["Error"] = "Invalid OAuth callback. Missing code or state parameter.";
            return this.RedirectToAction("Login");
        }

        try
        {
            var stateData = await this.oauthFlowManager.ConsumeOAuthStateAsync(state);
            if (stateData == null)
            {
                this.logger.LogWarning("Invalid OAuth state: {State}", state);
                this.TempData["Error"] = "Invalid or expired OAuth state. Please try logging in again.";
                return this.RedirectToAction("Login");
            }

            var (userId, clientId, redirectUrl) = stateData.Value;

            var callbackData = this.HttpContext.Request.QueryString.ToString();
            var result = await this.userSessionManager.CompleteOAuthFlowAsync(userId, callbackData);

            if (result == null)
            {
                this.logger.LogWarning("Failed to complete OAuth flow for user {UserId}", userId);
                this.TempData["Error"] = "Failed to complete authentication. Please try again.";
                return this.RedirectToAction("Login");
            }

            var (sessionId, authSession) = result.Value;

            // Create claims for the authenticated user
            var claims = new List<System.Security.Claims.Claim>
            {
                new("bluesky_did", authSession.Session.Did.ToString()),
                new("bluesky_handle", authSession.Session.Handle.ToString()),
                new("bluesky_session_id", sessionId),
                new(System.Security.Claims.ClaimTypes.NameIdentifier, authSession.Session.Did.ToString()),
                new(System.Security.Claims.ClaimTypes.Name, authSession.Session.Handle.ToString()),
            };

            if (!string.IsNullOrEmpty(authSession.Session.Email))
            {
                claims.Add(new System.Security.Claims.Claim("bluesky_email", authSession.Session.Email));
                claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, authSession.Session.Email));
            }

            var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Set session cookie
            this.HttpContext.Response.Cookies.Append("fishyflip_session_id", sessionId, new CookieOptions
            {
                HttpOnly = true,
                Secure = this.HttpContext.Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Expires = authSession.Session.ExpiresIn,
            });

            this.logger.LogInformation("User {Handle} successfully authenticated via OAuth", authSession.Session.Handle);

            // Check for stored return URL
            var returnUrl = this.HttpContext.Session.GetString($"return_url_{state}");
            if (!string.IsNullOrEmpty(returnUrl))
            {
                this.HttpContext.Session.Remove($"return_url_{state}");
            }

            return this.RedirectToLocal(returnUrl);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error during OAuth callback processing");
            this.TempData["Error"] = "An error occurred during authentication. Please try again.";
            return this.RedirectToAction("Login");
        }
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        var sessionId = this.User.FindFirst("bluesky_session_id")?.Value;
        if (!string.IsNullOrEmpty(sessionId))
        {
            await this.userSessionManager.RemoveSessionAsync(sessionId);
        }

        await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        this.HttpContext.Response.Cookies.Delete("fishyflip_session_id");

        this.logger.LogInformation("User logged out");

        return this.RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return this.View();
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (this.Url.IsLocalUrl(returnUrl))
        {
            return this.Redirect(returnUrl);
        }
        else
        {
            return this.RedirectToAction("Index", "Home");
        }
    }
}
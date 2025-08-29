// <copyright file="AccountController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetMvcSample.Controllers;

/// <summary>
/// Account Controller.
/// </summary>
public class AccountController : Controller
{
    private readonly IUserSessionManager userSessionManager;
    private readonly ILogger<AccountController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="userSessionManager">User session manager.</param>
    /// <param name="logger">Logger.</param>
    public AccountController(IUserSessionManager userSessionManager, ILogger<AccountController> logger)
    {
        this.userSessionManager = userSessionManager;
        this.logger = logger;
    }

    /// <summary>
    /// Login Action.
    /// </summary>
    /// <param name="returnUrl">Return URL.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        this.ViewData["ReturnUrl"] = returnUrl;
        return this.View();
    }

    /// <summary>
    /// Login Action.
    /// </summary>
    /// <param name="model">Login model.</param>
    /// <param name="returnUrl">Return URL.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        this.ViewData["ReturnUrl"] = returnUrl;

        if (!this.ModelState.IsValid)
        {
            return this.View(model);
        }

        try
        {
            var result = await this.userSessionManager.CreatePasswordSessionAsync(
                model.Identifier,
                model.Password,
                model.InstanceUrl);

            if (result == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return this.View(model);
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
                SameSite = SameSiteMode.Lax,
                Expires = authSession.Session.ExpiresIn,
            });

            this.logger.LogInformation("User {Handle} logged in successfully", authSession.Session.Handle);

            return this.RedirectToLocal(returnUrl);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error during login for user {Identifier}", model.Identifier);
            this.ModelState.AddModelError(string.Empty, "An error occurred during login. Please try again.");
            return this.View(model);
        }
    }

    /// <summary>
    /// Logout Action.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
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

        return this.RedirectToAction(nameof(HomeController.Index), "Home");
    }

    /// <summary>
    /// Access Denied Action.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
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
            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}

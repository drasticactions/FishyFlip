// <copyright file="HomeController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore.Extensions;
using FishyFlip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetMvcSample.Controllers;

/// <summary>
/// Home Controller.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Index Action.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    public IActionResult Index()
    {
        return this.View();
    }

    /// <summary>
    /// Profile Action.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var atProtocol = await this.HttpContext.GetUserATProtocolAsync();
        if (atProtocol == null)
        {
            return this.RedirectToAction("Login", "Account");
        }

        var did = this.HttpContext.GetBlueskyDid();
        if (string.IsNullOrEmpty(did))
        {
            return this.RedirectToAction("Login", "Account");
        }

        try
        {
            var profileResult = await atProtocol.Actor.GetProfileAsync(ATDid.Create(did)!);
            if (profileResult.IsT0)
            {
                this.ViewBag.Profile = profileResult.AsT0;
                this.ViewBag.Handle = this.HttpContext.GetBlueskyHandle();
                this.ViewBag.Email = this.HttpContext.GetBlueskyEmail();
                return this.View();
            }
            else
            {
                this.ViewBag.Error = "Failed to load profile";
                return this.View();
            }
        }
        catch (Exception ex)
        {
            this.ViewBag.Error = $"Error loading profile: {ex.Message}";
            return this.View();
        }
    }

    /// <summary>
    /// Timeline Action.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    [Authorize]
    public async Task<IActionResult> Timeline()
    {
        var atProtocol = await this.HttpContext.GetUserATProtocolAsync();
        if (atProtocol == null)
        {
            return this.RedirectToAction("Login", "Account");
        }

        try
        {
            var timelineResult = await atProtocol.Feed.GetTimelineAsync(limit: 20);
            if (timelineResult.IsT0)
            {
                this.ViewBag.Timeline = timelineResult.AsT0;
                return this.View();
            }
            else
            {
                this.ViewBag.Error = "Failed to load timeline";
                return this.View();
            }
        }
        catch (Exception ex)
        {
            this.ViewBag.Error = $"Error loading timeline: {ex.Message}";
            return this.View();
        }
    }

    /// <summary>
    /// Error Action.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}

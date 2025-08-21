// <copyright file="HomeController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Diagnostics;
using FishyFlip;
using FishyFlip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SandboxWebMvc.Models;

namespace SandboxWebMvc.Controllers;

/// <summary>
/// Home controller.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private ATProtocol protocol;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    public HomeController(ILogger<HomeController> logger)
    {
        this.logger = logger;
        var atProtocolBuilder = new ATProtocolBuilder();
        this.protocol = atProtocolBuilder.Build();
    }

    /// <summary>
    /// Returns the Index view.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    public IActionResult Index()
    {
        return this.View();
    }

    /// <summary>
    /// Returns the Auth view.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    [Authorize]
    public IActionResult Auth()
    {
        return this.View();
    }

    /// <summary>
    /// Returns the Privacy view.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    public IActionResult Privacy()
    {
        return this.View();
    }

    /// <summary>
    /// Handles errors.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}

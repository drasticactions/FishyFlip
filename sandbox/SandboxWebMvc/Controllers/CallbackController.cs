// <copyright file="CallbackController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SandboxWebMvc.Models;

namespace SandboxWebMvc.Controllers;

/// <summary>
/// Home controller.
/// </summary>
public class CallbackController : Controller
{
    private readonly ILogger<CallbackController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CallbackController"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    public CallbackController(ILogger<CallbackController> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Returns the Index view.
    /// </summary>
    /// <returns><see cref="IActionResult"/>.</returns>
    public IActionResult Index()
    {
        return this.Ok();
    }
}

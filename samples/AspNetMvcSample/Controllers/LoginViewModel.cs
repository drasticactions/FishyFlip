// <copyright file="LoginViewModel.cs" company="Drastic Actions">
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
/// Login View Model.
/// </summary>
public class LoginViewModel
{
    /// <summary>
    /// Gets or sets the identifier (username or email).
    /// </summary>
    public string Identifier { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the instance URL.
    /// </summary>
    public string InstanceUrl { get; set; } = "https://bsky.social";

    /// <summary>
    /// Gets or sets a value indicating whether to remember the user.
    /// </summary>
    public bool RememberMe { get; set; }
}
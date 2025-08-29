// <copyright file="ErrorViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore.Extensions;
using FishyFlip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetMvcSample.Controllers;

/// <summary>
/// Error View Model.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Gets or sets the request identifier.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Gets a value indicating whether to show the request identifier.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
}
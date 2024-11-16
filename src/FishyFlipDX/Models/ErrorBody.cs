// <copyright file="ErrorBody.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an atError response body.
/// </summary>
public class ErrorBody
{
    /// <summary>
    /// Gets or sets the atError code.
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Gets or sets the atError message.
    /// </summary>
    public string? Message { get; set; }
}
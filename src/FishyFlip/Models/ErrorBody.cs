// <copyright file="ErrorBody.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an error response body.
/// </summary>
public class ErrorBody
{
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string? Message { get; set; }
}
// <copyright file="ErrorDetail.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an atError response body.
/// </summary>
public class ErrorDetail
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorDetail"/> class.
    /// </summary>
    public ErrorDetail()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorDetail"/> class.
    /// </summary>
    /// <param name="error">The error type.</param>
    /// <param name="message">The error message.</param>
    public ErrorDetail(string error, string message)
    {
        this.Error = error;
        this.Message = message;
    }

    /// <summary>
    /// Gets or sets the atError code.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Gets or sets the atError message.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
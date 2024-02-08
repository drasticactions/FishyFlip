// <copyright file="FrameError.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame error.
/// </summary>
public class FrameError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameError"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the error information.</param>
    public FrameError(CBORObject obj)
    {
        this.Error = obj["error"].AsString();
        this.Message = obj["message"].AsString();
    }

    /// <summary>
    /// Gets the error.
    /// </summary>
    public string? Error { get; }

    /// <summary>
    /// Gets the error.
    /// </summary>
    public string? Message { get; }
}
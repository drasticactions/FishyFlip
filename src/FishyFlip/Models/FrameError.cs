// <copyright file="FrameError.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame atError.
/// </summary>
public class FrameError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameError"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the atError information.</param>
    public FrameError(CBORObject obj)
    {
        try
        {
            this.Error = obj["atError"].AsString();
            this.Message = obj["message"].AsString();
        }
        catch
        {
        }
    }

    /// <summary>
    /// Gets the atError.
    /// </summary>
    public string? Error { get; }

    /// <summary>
    /// Gets the atError.
    /// </summary>
    public string? Message { get; }
}
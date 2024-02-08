// <copyright file="FrameInfo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents information about a frame.
/// </summary>
public class FrameInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameInfo"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the frame information.</param>
    public FrameInfo(CBORObject obj)
    {
        this.Name = obj["name"]?.AsString();
        this.Message = obj["message"]?.AsString();
    }

    /// <summary>
    /// Gets the name of the frame.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the message associated with the frame.
    /// </summary>
    public string? Message { get; }
}
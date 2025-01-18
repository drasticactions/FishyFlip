// <copyright file="FrameHeader.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame header.
/// </summary>
public class FrameHeader
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameHeader"/> class.
    /// </summary>
    /// <param name="obj">The CBOR Object.</param>
    public FrameHeader(CBORObject obj)
    {
        if (obj.Count <= 0)
        {
            return;
        }

        this.Operation = (FrameHeaderOperation)(obj["op"]?.AsInt32() ?? 0);
        this.Type = obj["t"]?.AsString();
    }

    /// <summary>
    /// Gets or sets the operation type.
    /// </summary>
    public FrameHeaderOperation Operation { get; set; }

    /// <summary>
    /// Gets or sets the type of header.
    /// </summary>
    public string? Type { get; set; }
}
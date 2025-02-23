// <copyright file="FrameEvent.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an event that contains the progress status of a car.
/// </summary>
public class FrameEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameEvent"/> class.
    /// </summary>
    /// <param name="cid">The car ID.</param>
    /// <param name="bytes">The progress status bytes.</param>
    public FrameEvent(ATCid cid, byte[] bytes)
    {
        this.Cid = cid;
        this.Bytes = bytes;
    }

    /// <summary>
    /// Gets the progress status bytes.
    /// </summary>
    public byte[] Bytes { get; }

    /// <summary>
    /// Gets the car ID.
    /// </summary>
    public ATCid Cid { get; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{this.Cid} - {this.Bytes.Length}";
    }
}
// <copyright file="CarProgressStatusEvent.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Represents an event that contains the progress status of a car.
/// </summary>
public class CarProgressStatusEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarProgressStatusEvent"/> class.
    /// </summary>
    /// <param name="cid">The car ID.</param>
    /// <param name="bytes">The progress status bytes.</param>
    public CarProgressStatusEvent(ATCid cid, byte[] bytes)
    {
        this.FrameEvent = new FrameEvent(cid, bytes);
    }

    /// <summary>
    /// Gets the progress status bytes.
    /// </summary>
    public byte[] Bytes => this.FrameEvent.Bytes;

    /// <summary>
    /// Gets the car ID.
    /// </summary>
    public ATCid Cid => this.FrameEvent.Cid;

    /// <summary>
    /// Gets the frame event.
    /// </summary>
    public FrameEvent FrameEvent { get; }
}
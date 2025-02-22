// <copyright file="FrameHandle.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a handle for a frame.
/// </summary>
public class FrameHandle : ICBOREncodable<FrameHandle>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameHandle"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the frame handle data.</param>
    public FrameHandle(CBORObject obj)
    {
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Seq = obj["seq"].AsInt64Value();
        this.Handle = obj["handle"]?.AsString();
        this.Time = obj["time"] is not null ? obj["time"].ToDateTime() : null;
    }

    /// <summary>
    /// Gets the DID (Decentralized Identifier) associated with the frame handle.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the sequence number of the frame handle.
    /// </summary>
    public long Seq { get; }

    /// <summary>
    /// Gets the handle string of the frame handle.
    /// </summary>
    public string? Handle { get; }

    /// <summary>
    /// Gets the timestamp of the frame handle.
    /// </summary>
    public DateTime? Time { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="FrameHandle"/> class from a CBORObject.
    /// </summary>
    /// <param name="obj">The CBORObject to convert into a FrameHandle instance.</param>
    /// <returns>A new instance of the <see cref="FrameHandle"/> class.</returns>
    public static FrameHandle FromCBORObject(CBORObject obj)
    {
        return new FrameHandle(obj);
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}

// <copyright file="FrameIdentity.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame Identity.
/// </summary>
public class FrameIdentity : ICBOREncodable<FrameIdentity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameIdentity"/> class.
    /// </
    /// <param name="obj">The CBOR object containing the identity information.</param>
    public FrameIdentity(CBORObject obj)
    {
        this.Seq = obj["seq"].AsInt64Value();
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Time = obj["time"]?.ToDateTime();
        this.Handle = obj["handle"] is not null ? ATHandle.Create(obj["handle"].AsString()) : null;
    }

    /// <summary>
    /// Gets the did.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the sequence.
    /// </summary>
    public long Seq { get; }

    /// <summary>
    /// Gets the date time of the frame.
    /// </summary>
    public DateTime? Time { get; }

    /// <summary>
    /// Gets the handle.
    /// </summary>
    public ATHandle? Handle { get; }

    /// <inheritdoc/>
    public static FrameIdentity FromCBORObject(CBORObject obj)
    {
        return new FrameIdentity(obj);
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}
// <copyright file="FrameTombstone.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a tombstone object for a frame.
/// </summary>
public class FrameTombstone : ICBOREncodable<FrameTombstone>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameTombstone"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the tombstone data.</param>
    public FrameTombstone(CBORObject obj)
    {
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Seq = obj["seq"].AsInt64Value();
        this.Time = obj["time"] is not null ? obj["time"].ToDateTime() : null;
    }

    /// <summary>
    /// Gets the DID (Decentralized Identifier) associated with the tombstone.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the sequence number of the frame.
    /// </summary>
    public long Seq { get; }

    /// <summary>
    /// Gets the timestamp of the tombstone.
    /// </summary>
    public DateTime? Time { get; }

    /// <inheritdoc/>
    public static FrameTombstone FromCBORObject(CBORObject obj)
    {
        return new FrameTombstone(obj);
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}
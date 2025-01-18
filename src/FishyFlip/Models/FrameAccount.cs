// <copyright file="FrameAccount.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame Account.
/// </summary>
public class FrameAccount
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameAccount"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the account information.</param>
    public FrameAccount(CBORObject obj)
    {
        this.Seq = obj["seq"].AsInt64Value();
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Time = obj["time"]?.ToDateTime();
        this.Active = obj["active"]?.AsBoolean() ?? false;
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
    /// Gets a value indicating whether the account is active.
    /// </summary>
    public bool Active { get; }
}
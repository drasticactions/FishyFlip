// <copyright file="ThreadGateRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an actor record.
/// </summary>
public class ThreadGateRecord : ATFeedTypeAPI
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadGateRecord"/> class.
    /// </summary>
    /// <param name="value">The thread gate.</param>
    /// <param name="uri">The ATUri of the actor.</param>
    /// <param name="cid">The ATCid of the action.</param>
    [JsonConstructor]
    public ThreadGateRecord(ThreadGate? value, ATUri? uri, ATCid? cid)
        : base(uri, cid)
    {
        this.Value = value;
    }

    /// <summary>
    /// Gets the profile value of the actor record.
    /// </summary>
    public ThreadGate? Value { get; }
}
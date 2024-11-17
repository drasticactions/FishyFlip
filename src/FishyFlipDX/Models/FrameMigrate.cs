// <copyright file="FrameMigrate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a frame migration object.
/// </summary>
public class FrameMigrate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameMigrate"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the frame migration data.</param>
    public FrameMigrate(CBORObject obj)
    {
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Seq = obj["seq"].ToObject<long>();
        this.MigrateTo = obj["migrateTo"]?.AsString();
        this.Time = obj["time"] is not null ? obj["time"].ToDateTime() : null;
    }

    /// <summary>
    /// Gets the DID (Decentralized Identifier) associated with the frame migration.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the sequence number of the frame migration.
    /// </summary>
    public long Seq { get; }

    /// <summary>
    /// Gets the migration destination of the frame.
    /// </summary>
    public string? MigrateTo { get; }

    /// <summary>
    /// Gets the timestamp of the frame migration.
    /// </summary>
    public DateTime? Time { get; }
}
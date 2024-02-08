// <copyright file="BlockRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a block record.
/// </summary>
public class BlockRecord : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockRecord"/> class.
    /// </summary>
    /// <param name="subject">The Subject of the Block.</param>
    /// <param name="createdAt">When the block was created. Otherwise set to DateTime.UtcNow.</param>
    [JsonConstructor]
    public BlockRecord(ATDid? subject, DateTime? createdAt)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.UtcNow;
        this.Type = Constants.GraphTypes.Block;
    }

    /// <summary>
    /// Gets the subject of the block record.
    /// </summary>
    public ATDid? Subject { get; }

    /// <summary>
    /// Gets the creation date and time of the block record.
    /// </summary>
    public DateTime CreatedAt { get; }
}
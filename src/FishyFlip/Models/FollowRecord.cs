// <copyright file="FollowRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a follow record.
/// </summary>
public class FollowRecord : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FollowRecord"/> class.
    /// </summary>
    /// <param name="subject">The subject of the follow record.</param>
    /// <param name="createdAt">The creation date and time of the follow record.</param>
    [JsonConstructor]
    public FollowRecord(ATDid? subject, DateTime? createdAt)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.GraphTypes.Follow;
    }

    /// <summary>
    /// Gets the subject of the follow record.
    /// </summary>
    public ATDid? Subject { get; }

    /// <summary>
    /// Gets the creation date and time of the follow record.
    /// </summary>
    public DateTime CreatedAt { get; }
}
// <copyright file="LikeRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record of a like action.
/// </summary>
public class LikeRecord : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LikeRecord"/> class.
    /// </summary>
    /// <param name="subject">The subject of the like action.</param>
    /// <param name="createdAt">The date and time when the like action was created.</param>
    [JsonConstructor]
    public LikeRecord(Subject? subject, DateTime? createdAt)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.FeedType.Like;
    }

    /// <summary>
    /// Gets the subject of the like action.
    /// </summary>
    public Subject? Subject { get; }

    /// <summary>
    /// Gets the date and time when the like action was created.
    /// </summary>
    public DateTime CreatedAt { get; }
}

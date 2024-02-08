// <copyright file="RepostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record of a repost in the application.
/// </summary>
public class RepostRecord : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RepostRecord"/> class.
    /// </summary>
    /// <param name="subject">The subject of the repost.</param>
    /// <param name="createdAt">The creation date and time of the repost.</param>
    [JsonConstructor]
    public RepostRecord(Subject? subject, DateTime? createdAt)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.FeedType.Repost;
    }

    /// <summary>
    /// Gets the subject of the repost.
    /// </summary>
    public Subject? Subject { get; }

    /// <summary>
    /// Gets the creation date and time of the repost.
    /// </summary>
    public DateTime CreatedAt { get; }
}

// <copyright file="ModerationRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Represents a moderation record.
/// </summary>
public class ModerationRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModerationRecord"/> class.
    /// </summary>
    /// <param name="subject">The subject of the moderation record.</param>
    /// <param name="createdAt">The creation date and time of the moderation record.</param>
    /// <param name="id">The ID of the moderation record.</param>
    /// <param name="reason">The reason for the moderation.</param>
    /// <param name="reportedBy">The ID of the user who reported the moderation.</param>
    [JsonConstructor]
    public ModerationRecord(ModerationSubject? subject, DateTime? createdAt, int id, string? reason, ATDid? reportedBy)
    {
        this.Id = id;
        this.Reason = reason;
        this.ReportedBy = reportedBy;
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
    }

    /// <summary>
    /// Gets the ID of the moderation record.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the reason for the moderation.
    /// </summary>
    public string? Reason { get; }

    /// <summary>
    /// Gets the ID of the user who reported the moderation.
    /// </summary>
    public ATDid? ReportedBy { get; }

    /// <summary>
    /// Gets the subject of the moderation record.
    /// </summary>
    public ModerationSubject? Subject { get; }

    /// <summary>
    /// Gets the creation date and time of the moderation record.
    /// </summary>
    public DateTime? CreatedAt { get; }
}

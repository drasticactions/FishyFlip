// <copyright file="ReasonRepost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reason for reposting.
/// </summary>
public class ReasonRepost : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReasonRepost"/> class.
    /// </summary>
    /// <param name="by">The actor profile who reposted.</param>
    /// <param name="indexedAt">The date and time when the repost was indexed.</param>
    /// <param name="type">The type of the reason.</param>
    [JsonConstructor]
    public ReasonRepost(ActorProfile? by, DateTime? indexedAt, string? type)
        : base(type)
    {
        this.By = by;
        this.IndexedAt = indexedAt;
    }

    /// <summary>
    /// Gets the actor profile who reposted.
    /// </summary>
    public ActorProfile? By { get; }

    /// <summary>
    /// Gets the date and time when the repost was indexed.
    /// </summary>
    public DateTime? IndexedAt { get; }
}
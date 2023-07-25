// <copyright file="ReasonRepost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ReasonRepost : ATRecord
{
    [JsonConstructor]
    public ReasonRepost(ActorProfile? by, DateTime? indexedAt, string? type)
        : base(type)
    {
        this.By = by;
        this.IndexedAt = indexedAt;
    }

    public ActorProfile? By { get; }

    public DateTime? IndexedAt { get; }
}
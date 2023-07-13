// <copyright file="FollowRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class FollowRecord : ATRecord
{
    [JsonConstructor]
    public FollowRecord(ATDid? subject, DateTime? createdAt)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.GraphTypes.Block;
    }

    public ATDid? Subject { get; }

    public DateTime CreatedAt { get; }
}
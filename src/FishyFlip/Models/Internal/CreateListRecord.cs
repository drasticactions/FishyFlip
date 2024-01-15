// <copyright file="CreateListRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using static FishyFlip.Constants;

namespace FishyFlip.Models.Internal;

internal record CreateListRecord(string Collection, string Repo, ListRecordInternal Record, string? Rkey = null, string? SwapCommit = null);

internal class ListRecordInternal : ATRecord
{
    [JsonConstructor]
    public ListRecordInternal(string name, string description, string purpose, DateTime? createdAt = default)
    {
        this.Name = name;
        this.Description = description;
        this.Type = GraphTypes.List;
        this.CreatedAt = createdAt ?? DateTime.UtcNow;
        this.Purpose = purpose;
    }

    public DateTime CreatedAt { get; }

    public string Name { get; }

    public string Description { get; }

    public string Purpose { get; }
}
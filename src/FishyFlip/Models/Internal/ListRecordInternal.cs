// <copyright file="ListRecordInternal.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using static FishyFlip.Constants;

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents an internal list record.
/// </summary>
internal class ListRecordInternal : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListRecordInternal"/> class.
    /// </summary>
    /// <param name="name">The name of the list.</param>
    /// <param name="description">The description of the list.</param>
    /// <param name="purpose">The purpose of the list.</param>
    /// <param name="createdAt">The creation date of the list. Defaults to the current UTC date and time if not specified.</param>
    [JsonConstructor]
    public ListRecordInternal(string name, string description, string purpose, DateTime? createdAt = default)
    {
        this.Name = name;
        this.Description = description;
        this.Type = GraphTypes.List;
        this.CreatedAt = createdAt ?? DateTime.UtcNow;
        this.Purpose = purpose;
    }

    /// <summary>
    /// Gets the creation date of the list.
    /// </summary>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the name of the list.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the description of the list.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the purpose of the list.
    /// </summary>
    public string Purpose { get; }
}
// <copyright file="ListItemRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record for a list item.
/// </summary>
public class ListItemRecord : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListItemRecord"/> class.
    /// </summary>
    /// <param name="subject">The subject of the list item.</param>
    /// <param name="list">The URI of the list.</param>
    /// <param name="createdAt">The creation date and time of the list item.</param>
    [JsonConstructor]
    public ListItemRecord(ATDid subject, ATUri list, DateTime? createdAt)
        : base(Constants.GraphTypes.ListItem)
    {
        this.Subject = subject;
        this.List = list;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.GraphTypes.ListItem;
    }

    /// <summary>
    /// Gets the subject of the list item.
    /// </summary>
    public ATDid Subject { get; }

    /// <summary>
    /// Gets the URI of the list.
    /// </summary>
    public ATUri List { get; }

    /// <summary>
    /// Gets the creation date and time of the list item.
    /// </summary>
    public DateTime CreatedAt { get; }
}
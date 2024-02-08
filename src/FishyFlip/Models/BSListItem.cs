// <copyright file="BSListItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a list item.
/// </summary>
public class BSListItem : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BSListItem"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the list item data.</param>
    public BSListItem(CBORObject obj)
    {
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.GraphTypes.ListItem;
        this.Subject = obj["subject"]?.AsString();
        this.Name = obj["name"]?.AsString();
    }

    /// <summary>
    /// Gets the name of the list item.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the subject of the list item.
    /// </summary>
    public string? Subject { get; }

    /// <summary>
    /// Gets the creation date and time of the list item.
    /// </summary>
    public DateTime? CreatedAt { get; }
}

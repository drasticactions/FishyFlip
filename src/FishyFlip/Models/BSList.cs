// <copyright file="BSList.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a BSList object.
/// </summary>
public class BSList : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BSList"/> class.
    /// </summary>
    /// <param name="obj">The CBORObject containing the BSList data.</param>
    public BSList(CBORObject obj)
    {
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.GraphTypes.List;
        this.Description = obj["description"]?.AsString();
        this.Purpose = obj["purpose"]?.AsString();
        this.Name = obj["name"]?.AsString();
    }

    /// <summary>
    /// Gets the name of the BSList.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the purpose of the BSList.
    /// </summary>
    public string? Purpose { get; }

    /// <summary>
    /// Gets the description of the BSList.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the creation date and time of the BSList.
    /// </summary>
    public DateTime? CreatedAt { get; }
}
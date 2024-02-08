// <copyright file="FeedGenerator.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a feed generator.
/// </summary>
public class FeedGenerator : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedGenerator"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the feed generator data.</param>
    public FeedGenerator(CBORObject obj)
    {
        this.Type = Constants.FeedType.Generator;
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Avatar = obj["avatar"] is not null ? new Image(obj["avatar"]) : null;
        this.CreatedAt = obj["createdAt"] is not null ? obj["createdAt"].ToDateTime() : null;
        this.Description = obj["description"]?.AsString();
        this.DisplayName = obj["displayName"]?.AsString();
    }

    /// <summary>
    /// Gets the DID (Decentralized Identifier) associated with the feed generator.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the avatar image of the feed generator.
    /// </summary>
    public Image? Avatar { get; }

    /// <summary>
    /// Gets the creation date and time of the feed generator.
    /// </summary>
    public DateTime? CreatedAt { get; }

    /// <summary>
    /// Gets the description of the feed generator.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the display name of the feed generator.
    /// </summary>
    public string? DisplayName { get; }
}
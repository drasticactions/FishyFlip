// <copyright file="FeedGenerator.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class FeedGenerator : ATRecord
{
    public FeedGenerator(CBORObject obj)
    {
        this.Type = Constants.FeedType.Generator;
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Avatar = obj["avatar"] is not null ? new Image(obj["avatar"]) : null;
        this.CreatedAt = obj["createdAt"] is not null ? obj["createdAt"].ToDateTime() : null;
        this.Description = obj["description"]?.AsString();
        this.DisplayName = obj["displayName"]?.AsString();
    }

    public ATDid? Did { get; }

    public Image? Avatar { get; }

    public DateTime? CreatedAt { get; }

    public string? Description { get; }

    public string? DisplayName { get; }
}
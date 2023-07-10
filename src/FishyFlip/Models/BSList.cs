// <copyright file="BSList.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class BSList : ATRecord
{
    public BSList(CBORObject obj)
    {
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.GraphTypes.List;
        this.Description = obj["description"]?.AsString();
        this.Purpose = obj["purpose"]?.AsString();
        this.Name = obj["name"]?.AsString();
    }

    public string? Name { get; }

    public string? Purpose { get; }

    public string? Description { get; }

    public DateTime? CreatedAt { get; }
}
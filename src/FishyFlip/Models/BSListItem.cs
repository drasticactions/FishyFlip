// <copyright file="BSListItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class BSListItem : ATRecord
{
    public BSListItem(CBORObject obj)
    {
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.GraphTypes.ListItem;
        this.Subject = obj["subject"]?.AsString();
        this.Name = obj["name"]?.AsString();
    }

    public string? Name { get; }

    public string? Subject { get; }

    public DateTime? CreatedAt { get; }
}

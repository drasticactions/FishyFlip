// <copyright file="ListItemRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;

public class ListItemRecord : ATRecord
{
    [JsonConstructor]
    public ListItemRecord(ATDid subject, ATUri list, DateTime? createdAt)
    {
        this.Subject = subject;
        this.List = list;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.GraphTypes.ListItem;
    }

    public ATDid Subject { get; }

    public ATUri List { get; }

    public DateTime CreatedAt { get; }
}
// <copyright file="RecordViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class RecordViewEmbed : Embed
{
    public RecordViewEmbed(PostViewEmbed record)
    {
        this.Record = record;
        this.Type = Constants.EmbedTypes.RecordView;
    }

    public PostViewEmbed Record { get; }
}
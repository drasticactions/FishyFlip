// <copyright file="RecordWithMediaViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Represents an embed with a record and media view.
/// </summary>
public class RecordWithMediaViewEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecordWithMediaViewEmbed"/> class.
    /// </summary>
    /// <param name="record">The record view embed.</param>
    /// <param name="embed">The media embed.</param>
    [JsonConstructor]
    public RecordWithMediaViewEmbed(RecordViewEmbed? record, Embed? embed)
    {
        this.Record = record;
        this.Embed = embed;
        this.Type = Constants.EmbedTypes.RecordWithMedia;
    }

    /// <summary>
    /// Gets the record view embed.
    /// </summary>
    public RecordViewEmbed? Record { get; }

    /// <summary>
    /// Gets the image view embed.
    /// </summary>
    public Embed? Embed { get; }
}
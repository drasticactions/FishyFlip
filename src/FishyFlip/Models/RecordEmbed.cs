// <copyright file="RecordEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an embed for a record.
/// </summary>
public class RecordEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecordEmbed"/> class.
    /// </summary>
    /// <param name="record">The record object.</param>
    /// <param name="type">The type of the embed.</param>
    [JsonConstructor]
    public RecordEmbed(Record? record, string? type)
        : base(type)
    {
        this.Record = record;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RecordEmbed"/> class from a CBORObject.
    /// </summary>
    /// <param name="obj">The CBORObject representing the record.</param>
    public RecordEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Record;
        this.Record = new Record(obj);
    }

    /// <summary>
    /// Gets the record associated with the embed.
    /// </summary>
    public Record? Record { get; }
}

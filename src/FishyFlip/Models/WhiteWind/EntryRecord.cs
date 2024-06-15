// <copyright file="EntryRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.WhiteWind;

/// <summary>
/// Represents a record for an entry.
/// </summary>
public class EntryRecord : ATFeedTypeAPI
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntryRecord"/> class.
    /// </summary>
    /// <param name="value">The entry value.</param>
    /// <param name="uri">The URI of the post.</param>
    /// <param name="cid">The CID of the post.</param>
    [JsonConstructor]
    public EntryRecord(Entry? value, ATUri? uri, ATCid? cid)
        : base(uri, cid)
    {
        this.Value = value;
    }

    /// <summary>
    /// Gets the entry value.
    /// </summary>
    public Entry? Value { get; }
}
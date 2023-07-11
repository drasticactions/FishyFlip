// <copyright file="ATRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// AT Record.
/// </summary>
public abstract class ATRecord
{
    [JsonConstructor]
    public ATRecord(string? type)
    {
        this.Type = type;
    }

    public ATRecord()
    {
    }

    /// <summary>
    /// Gets the Record Type.
    /// </summary>
    [JsonPropertyName("$type")]
    public string? Type { get; internal set; }
}

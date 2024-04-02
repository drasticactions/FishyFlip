// <copyright file="ThreadGateReason.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// ThreadGate Reason.
/// </summary>
public class ThreadGateReason
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadGateReason"/> class.
    /// </summary>
    /// <param name="list">List value.</param>
    /// <param name="type">The Type.</param>
    [JsonConstructor]
    public ThreadGateReason(string? list, string? type)
    {
        this.List = list;
        this.Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadGateReason"/> class.
    /// </summary>
    /// <param name="obj">CBorObject.</param>
    public ThreadGateReason(CBORObject obj)
    {
        this.Type = obj["$type"].AsString();
    }

    /// <summary>
    /// Gets the Record Type.
    /// </summary>
    [JsonPropertyName("list")]
    public string? List { get; internal set; }

    /// <summary>
    /// Gets the Record Type.
    /// </summary>
    [JsonPropertyName("$type")]
    public string? Type { get; internal set; }
}
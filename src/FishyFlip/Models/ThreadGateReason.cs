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
    /// <param name="type">The type.</param>
    [JsonConstructor]
    public ThreadGateReason(string? type)
    {
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
    [JsonPropertyName("$type")]
    public string? Type { get; internal set; }
}
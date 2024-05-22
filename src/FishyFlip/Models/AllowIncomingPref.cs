// <copyright file="AllowIncomingPref.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Adult Content Preference.
/// </summary>
public class AllowIncomingPref : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllowIncomingPref"/> class.
    /// </summary>
    /// <param name="allowIncoming">Type of chat messages to allow.</param>
    /// <param name="type">ATRecord Type.</param>
    [JsonConstructor]
    public AllowIncomingPref(string allowIncoming, string? type = default)
        : base(type)
    {
        this.AllowIncoming = allowIncoming;
        this.Type = type ?? Constants.DeclarationTypes.AllowIncomingPref;
    }

    /// <summary>
    /// Gets a value the type of chat messages to allow.
    /// </summary>
    public string AllowIncoming { get; }
}
// <copyright file="AdultContentPref.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Adult Content Preference.
/// </summary>
public class AdultContentPref : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdultContentPref"/> class.
    /// </summary>
    /// <param name="enabled">Is Enabled.</param>
    /// <param name="type">ATRecord Type.</param>
    [JsonConstructor]
    public AdultContentPref(bool enabled, string? type = default)
        : base(type)
    {
        this.Enabled = enabled;
        this.Type = type ?? Constants.ActorTypes.AdultContentPref;
    }

    /// <summary>
    /// Gets a value indicating whether the adult content is enabled.
    /// </summary>
    public bool Enabled { get; }
}
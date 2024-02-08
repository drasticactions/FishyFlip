// <copyright file="HandleResolution.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a user handle resolution.
/// </summary>
public class HandleResolution
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HandleResolution"/> class.
    /// </summary>
    /// <param name="did">The ATDid value.</param>
    [JsonConstructor]
    public HandleResolution(ATDid? did)
    {
        this.Did = did;
    }

    /// <summary>
    /// Gets the ATDid value.
    /// </summary>
    public ATDid? Did { get; }
}

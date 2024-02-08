// <copyright file="ATIdentifier.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an abstract class for an identifier.
/// </summary>
public abstract class ATIdentifier
{
    /// <summary>
    /// Creates an instance of ATIdentifier based on the provided ID.
    /// </summary>
    /// <param name="id">The ID to create the ATIdentifier from.</param>
    /// <returns>An instance of ATIdentifier if the ID is valid, otherwise null.</returns>
    public static ATIdentifier? Create(string id)
    {
        if (ATDid.IsValid(id))
        {
            return ATDid.Create(id);
        }

        if (ATHandle.IsValid(id))
        {
            return ATHandle.Create(id);
        }

        return null;
    }
}

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

    /// <summary>
    /// Creates a new instance of the <see cref="ATIdentifier"/> class.
    /// </summary>
    /// <param name="uri">String based URI.</param>
    /// <param name="atIdentifier">A new instance of the <see cref="ATIdentifier"/> class.</param>
    /// <returns>Bool if ATIdentifier is valid.</returns>
    public static bool TryCreate(string uri, out ATIdentifier? atIdentifier)
    {
        try
        {
            if (ATDid.IsValid(uri))
            {
                atIdentifier = ATDid.Create(uri);
                return true;
            }

            if (ATHandle.IsValid(uri))
            {
                atIdentifier = ATHandle.Create(uri);
                return true;
            }

            atIdentifier = null;
            return false;
        }
        catch (Exception)
        {
            atIdentifier = null;
            return false;
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            ATDid atDid => this.ToString() == atDid.ToString(),
            ATHandle atHandle => this.ToString() == atHandle.ToString(),
            _ => obj?.ToString() == this.ToString(),
        };
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return this.ToString()?.GetHashCode() ?? 0;
    }
}

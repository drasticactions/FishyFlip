// <copyright file="ATIdentifier.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

#nullable enable annotations
#nullable disable warnings

using System.Diagnostics.CodeAnalysis;

namespace FishyFlip.Models;

/// <summary>
/// Represents an abstract class for an identifier.
/// </summary>
public abstract class ATIdentifier : IParsable<ATIdentifier>
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

    /// <inheritdoc/>
    public static ATIdentifier Parse(string s, IFormatProvider? provider)
    {
        var atIdentifier = Create(s);
        if (atIdentifier == null)
        {
            throw new FormatException($"Invalid ATIdentifier format: {s}");
        }

        return atIdentifier;
    }

    /// <inheritdoc/>
    public static bool TryParse(string? s, IFormatProvider? provider, out ATIdentifier result)
    {
        result = Create(s);
        return result != null;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ATIdentifier"/> class.
    /// </summary>
    /// <param name="uri">String based URI.</param>
    /// <param name="atIdentifier">A new instance of the <see cref="ATIdentifier"/> class.</param>
    /// <returns>Bool if ATIdentifier is valid.</returns>
    public static bool TryCreate(string uri, out ATIdentifier atIdentifier)
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
        var test = this.ToString()?.GetHashCode();
        return this.ToString()?.GetHashCode() ?? 0;
    }
}

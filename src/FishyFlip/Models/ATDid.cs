// <copyright file="ATDid.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// ATDid.
/// </summary>
public class ATDid : ATIdentifier
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATDid"/> class.
    /// </summary>
    /// <param name="handler">Actor Handler.</param>
    [JsonConstructor]
    internal ATDid(string handler)
    {
        this.Handler = handler;
    }

    /// <summary>
    /// Gets the Actor Handler.
    /// </summary>
    public string Handler { get; }

    /// <summary>
    /// Create a new ATDid.
    /// </summary>
    /// <param name="uri">Uri.</param>
    /// <returns>ATDid.</returns>
    /// <exception cref="ArgumentNullException">Thrown if URI is null or empty.</exception>
    public static new ATDid? Create(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = DIDValidator.EnsureValidDid(uri);
        if (!valid)
        {
            return null;
        }

        return new ATDid(uri);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ATDid"/> class.
    /// </summary>
    /// <param name="uri">String based URI.</param>
    /// <param name="atDid">A new instance of the <see cref="ATDid"/> class.</param>
    /// <returns>Bool if ATDid is valid.</returns>
    public static bool TryCreate(string uri, out ATDid? atDid)
    {
        try
        {
            atDid = new ATDid(uri);
            return true;
        }
        catch (Exception)
        {
            atDid = null;
            return false;
        }
    }

    /// <summary>
    /// Validate if a given string is a valid ATDid.
    /// </summary>
    /// <param name="uri">Uri.</param>
    /// <returns>Bool.</returns>
    /// <exception cref="ArgumentNullException">Thrown if uri is null or empty.</exception>
    public static bool IsValid(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        return DIDValidator.EnsureValidDid(uri);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.Handler.ToString();
    }

    /// <summary>
    /// Create a new ATDid.
    /// </summary>
    /// <param name="uri">ATUri.</param>
    /// <returns>ATDid.</returns>
    /// <exception cref="ArgumentNullException">Thrown if uri is null or empty.</exception>
    internal static ATDid? Create(ATUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = DIDValidator.EnsureValidDid(uri.Hostname);
        if (valid)
        {
            return new ATDid(uri.Hostname);
        }

        return null;
    }
}

// <copyright file="ATHandle.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// ATHandle.
/// </summary>
public class ATHandle : ATIdentifier
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATHandle"/> class.
    /// </summary>
    /// <param name="handle">Handle.</param>
    [JsonConstructor]
    public ATHandle(string handle)
    {
        this.Handle = handle;
    }

    /// <summary>
    /// Gets the handle.
    /// </summary>
    public string Handle { get; }

    /// <summary>
    /// Creates an ATHandle from a string.
    /// </summary>
    /// <param name="uri">The URI as string.</param>
    /// <returns>ATHandle.</returns>
    /// <exception cref="ArgumentNullException">Thrown if uri is null or empty.</exception>
    public static new ATHandle? Create(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = HandleValidator.EnsureValidHandle(uri);
        if (valid)
        {
            return new ATHandle(uri);
        }

        return null;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ATHandle"/> class.
    /// </summary>
    /// <param name="uri">String based URI.</param>
    /// <param name="atHandle">A new instance of the <see cref="ATHandle"/> class.</param>
    /// <returns>Bool if ATHandle is valid.</returns>
    public static bool TryCreate(string uri, out ATHandle? atHandle)
    {
        try
        {
            atHandle = new ATHandle(uri);
            return true;
        }
        catch (Exception)
        {
            atHandle = null;
            return false;
        }
    }

    /// <summary>
    /// Is the given string a valid ATHandle.
    /// </summary>
    /// <param name="uri">The uri as string.</param>
    /// <returns>Bool.</returns>
    /// <exception cref="ArgumentNullException">Thrown if uri is null or empty.</exception>
    public static bool IsValid(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        return HandleValidator.EnsureValidHandle(uri);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.Handle.ToString();
    }

    /// <summary>
    /// Create an ATHandle from a ATURI.
    /// </summary>
    /// <param name="uri">ATUri.</param>
    /// <returns>ATHandle.</returns>
    /// <exception cref="ArgumentNullException">Thrown if uri is null.</exception>
    internal static ATHandle? Create(ATUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = HandleValidator.EnsureValidHandle(uri.Hostname);
        if (valid)
        {
            return new ATHandle(uri.Hostname);
        }

        return null;
    }
}

// <copyright file="ATHandle.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

#nullable enable annotations
#nullable disable warnings

using System.Diagnostics.CodeAnalysis;

namespace FishyFlip.Models;

/// <summary>
/// ATHandle.
/// </summary>
public class ATHandle : ATIdentifier, IParsable<ATHandle>
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
    public static bool TryCreate(string uri, out ATHandle atHandle)
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
    /// Gets the handles from a post text.
    /// </summary>
    /// <param name="post">Text of the post.</param>
    /// <returns>Array of ATHandle.</returns>
    public static ATHandle[] FromPostText(string post)
    {
        var handles = new List<ATHandle>();
        var matches = Regex.Matches(post, @"@(?!http)[a-zA-Z0-9][-a-zA-Z0-9_.]{1,}");
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                var handle = match.Value;
                if (handle.StartsWith("@"))
                {
                    handle = handle.Substring(1);
                }

                if (HandleValidator.EnsureValidHandle(handle))
                {
                    if (!handles.Any(n => n.Handle == handle))
                    {
                        handles.Add(new ATHandle(handle));
                    }
                }
            }
        }

        return handles.ToArray();
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

    /// <summary>
    /// TryParse method for ATHandle.
    /// </summary>
    /// <param name="s">String to parse.</param>
    /// <param name="provider">Format provider.</param>
    /// <param name="result">Parsed ATHandle.</param>
    /// <returns>True if parsed successfully, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if s is null or empty.</exception>
    public static bool TryParse(string? s, IFormatProvider? provider, out ATHandle result)
    {
        result = Create(s);
        return result != null;
    }

#if NET
    /// <summary>
    /// Parse method for ATHandle.
    /// </summary>
    /// <param name="s">String to parse.</param>
    /// <param name="provider">Format provider.</param>
    /// <returns>Parsed ATHandle.</returns>
    /// <exception cref="ArgumentNullException">Thrown if s is null or empty.</exception>
    /// <exception cref="FormatException">Thrown if the format is invalid.</exception>
    static ATHandle IParsable<ATHandle>.Parse(string s, IFormatProvider? provider)
    {
        var atHandle = Create(s);
        if (atHandle == null)
        {
            throw new FormatException($"Invalid ATHandle format: {s}");
        }

        return atHandle;
    }
#endif

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

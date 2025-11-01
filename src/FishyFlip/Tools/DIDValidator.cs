// <copyright file="DIDValidator.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Validates a DID.
/// </summary>
internal static class DIDValidator
{
    /// <summary>
    /// Ensures that the provided DID is valid.
    /// </summary>
    /// <param name="did">The DID to validate.</param>
    /// <param name="logger">An optional logger to log validation errors.</param>
    /// <returns><c>true</c> if the DID is valid; otherwise, <c>false</c>.</returns>
    internal static bool EnsureValidDid(string did, ILogger? logger = default)
    {
        if (!Regex.IsMatch(did, "^[a-zA-Z0-9._:%-]*$"))
        {
            logger?.LogError("Disallowed characters in DID (ASCII letters, digits, and a couple other characters only)");
            return false;
        }

        if (!did.StartsWith("did:", StringComparison.Ordinal))
        {
            logger?.LogError("DID requires \"did:\" prefix");
            return false;
        }

        var secondColon = did.IndexOf(':', 4);

        if (secondColon == -1)
        {
            logger?.LogError("DID requires prefix, method, and method-specific content");
            return false;
        }

#if NETSTANDARD
        var method = did.Substring(4, secondColon - 4);
#else
        var method = did.AsSpan(4, secondColon - 4);
#endif

        if (!Regex.IsMatch(method, "^[a-z]+$"))
        {
            logger?.LogError("DID method must be lower-case letters");
            return false;
        }

        if (did.EndsWith(":") || did.EndsWith("%"))
        {
            logger?.LogError("DID can not end with \":\" or \"%\"");
            return false;
        }

        if (did.Length > 2 * 1024)
        {
            logger?.LogError("DID is too long (2048 chars max)");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Ensures that the provided DID matches the regular expression pattern for DIDs.
    /// </summary>
    /// <param name="did">The DID to validate.</param>
    /// <param name="logger">An optional logger to log validation errors.</param>
    /// <returns><c>true</c> if the DID matches the regular expression pattern; otherwise, <c>false</c>.</returns>
    internal static bool EnsureValidDidRegex(string did, ILogger? logger = default)
    {
        if (!Regex.IsMatch(did, "^did:[a-z]+:[a-zA-Z0-9._:%-]*[a-zA-Z0-9._-]$"))
        {
            logger?.LogError("DID didn't validate via regex");
            return false;
        }

        if (did.Length > 2 * 1024)
        {
            logger?.LogError("DID is too long (2048 chars max)");
            return false;
        }

        return true;
    }
}
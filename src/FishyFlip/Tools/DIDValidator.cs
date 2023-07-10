// <copyright file="DIDValidator.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

internal static class DIDValidator
{
    internal static bool EnsureValidDid(string did, ILogger? logger = default)
    {
        if (!Regex.IsMatch(did, "^[a-zA-Z0-9._:%-]*$"))
        {
            logger?.LogError("Disallowed characters in DID (ASCII letters, digits, and a couple other characters only)");
            return false;
        }

        string[] parts = did.Split(':');
        if (parts.Length < 3)
        {
            logger?.LogError("DID requires prefix, method, and method-specific content");
            return false;
        }

        if (parts[0] != "did")
        {
            logger?.LogError("DID requires \"did:\" prefix");
            return false;
        }

        if (!Regex.IsMatch(parts[1], "^[a-z]+$"))
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
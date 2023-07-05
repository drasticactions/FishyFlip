// <copyright file="DIDValidator.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FishyFlip.Exceptions;

namespace FishyFlip.Tools;

internal static class DIDValidator
{
    internal static void EnsureValidDid(string did)
    {
        if (!Regex.IsMatch(did, "^[a-zA-Z0-9._:%-]*$"))
        {
            throw new InvalidDidError("Disallowed characters in DID (ASCII letters, digits, and a couple other characters only)");
        }

        string[] parts = did.Split(':');
        if (parts.Length < 3)
        {
            throw new InvalidDidError("DID requires prefix, method, and method-specific content");
        }

        if (parts[0] != "did")
        {
            throw new InvalidDidError("DID requires \"did:\" prefix");
        }

        if (!Regex.IsMatch(parts[1], "^[a-z]+$"))
        {
            throw new InvalidDidError("DID method must be lower-case letters");
        }

        if (did.EndsWith(":") || did.EndsWith("%"))
        {
            throw new InvalidDidError("DID can not end with \":\" or \"%\"");
        }

        if (did.Length > 2 * 1024)
        {
            throw new InvalidDidError("DID is too long (2048 chars max)");
        }
    }

    internal static void EnsureValidDidRegex(string did)
    {
        if (!Regex.IsMatch(did, "^did:[a-z]+:[a-zA-Z0-9._:%-]*[a-zA-Z0-9._-]$"))
        {
            throw new InvalidDidError("DID didn't validate via regex");
        }

        if (did.Length > 2 * 1024)
        {
            throw new InvalidDidError("DID is too long (2048 chars max)");
        }
    }
}
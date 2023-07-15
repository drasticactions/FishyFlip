// <copyright file="HandleValidator.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Handle Validator.
/// </summary>
internal static class HandleValidator
{
    /// <summary>
    /// Ensure Valid Handle.
    /// </summary>
    /// <param name="handle">String handle.</param>
    /// <param name="logger">Optional Logger for why a handle was not valid.</param>
    /// <returns>Returns a bool indicating if the handle is valid.</returns>
    internal static bool EnsureValidHandle(string handle, ILogger? logger = default)
    {
        if (!Regex.IsMatch(handle, "^[a-zA-Z0-9.-]*$"))
        {
            logger?.LogError("Disallowed characters in handle (ASCII letters, digits, dashes, periods only)");
            return false;
        }

        if (handle.Length > 253)
        {
            logger?.LogError("Handle is too long (253 chars max)");
            return false;
        }

        string[] labels = handle.Split('.');
        if (labels.Length < 2)
        {
            logger?.LogError("Handle domain needs at least two parts");
            return false;
        }

        for (int i = 0; i < labels.Length; i++)
        {
            string l = labels[i];

            if (l.Length < 1)
            {
                logger?.LogError("Handle parts can not be empty");
                return false;
            }

            if (l.Length > 63)
            {
                logger?.LogError("Handle part too long (max 63 chars)");
                return false;
            }

            if (l.EndsWith('-') || l.StartsWith('-'))
            {
                logger?.LogError("Handle parts can not start or end with hyphens");
                return false;
            }

            if (i + 1 == labels.Length && !Regex.IsMatch(l, "^[a-zA-Z]"))
            {
                logger?.LogError("Handle final component (TLD) must start with ASCII letter");
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Ensure Valid Handle Regex.
    /// </summary>
    /// <param name="handle">String handle.</param>
    /// <param name="logger">Optional Logger for why a handle was not valid.</param>
    /// <returns>Returns a bool indicating if the handle is valid.</returns>
    internal static bool EnsureValidHandleRegex(string handle, ILogger? logger = default)
    {
        if (!Regex.IsMatch(handle, "^([a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\\.)+[a-zA-Z]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?$"))
        {
            logger?.LogError("Handle didn't validate via regex");
        }

        if (handle.Length > 253)
        {
            logger?.LogError("Handle is too long (253 chars max)");
        }

        return true;
    }
}

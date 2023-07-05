using FishyFlip.Exceptions;
using System.Text.RegularExpressions;

namespace FishyFlip.Tools;

internal static class HandleValidator
{
    internal static void EnsureValidHandle(string handle)
    {
        if (!Regex.IsMatch(handle, "^[a-zA-Z0-9.-]*$"))
        {
            throw new InvalidHandleError("Disallowed characters in handle (ASCII letters, digits, dashes, periods only)");
        }

        if (handle.Length > 253)
        {
            throw new InvalidHandleError("Handle is too long (253 chars max)");
        }

        string[] labels = handle.Split('.');
        if (labels.Length < 2)
        {
            throw new InvalidHandleError("Handle domain needs at least two parts");
        }

        for (int i = 0; i < labels.Length; i++)
        {
            string l = labels[i];

            if (l.Length < 1)
            {
                throw new InvalidHandleError("Handle parts can not be empty");
            }

            if (l.Length > 63)
            {
                throw new InvalidHandleError("Handle part too long (max 63 chars)");
            }

            if (l.EndsWith('-') || l.StartsWith('-'))
            {
                throw new InvalidHandleError("Handle parts can not start or end with hyphens");
            }

            if (i + 1 == labels.Length && !Regex.IsMatch(l, "^[a-zA-Z]"))
            {
                throw new InvalidHandleError("Handle final component (TLD) must start with ASCII letter");
            }
        }
    }

    internal static void EnsureValidHandleRegex(string handle)
    {
        if (!Regex.IsMatch(handle, "^([a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\\.)+[a-zA-Z]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?$"))
        {
            throw new InvalidHandleError("Handle didn't validate via regex");
        }

        if (handle.Length > 253)
        {
            throw new InvalidHandleError("Handle is too long (253 chars max)");
        }
    }
}

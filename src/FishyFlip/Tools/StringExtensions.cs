// <copyright file="StringExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

internal static class StringExtensions
{
    internal static string FormatValue<T>(T value) => $"{typeof(T).FullName}: {value?.ToString()}";

    internal static string GenerateValidFilename(string input)
    {
        // Remove invalid characters from the input string
        string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
        string invalidRegEx = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
        string sanitizedInput = Regex.Replace(input, invalidRegEx, "_");

        // Truncate the filename if it exceeds the maximum length allowed
        int maxFileNameLength = 260; // Maximum length for a filename in Windows
        if (sanitizedInput.Length > maxFileNameLength)
        {
            sanitizedInput = sanitizedInput.Substring(0, maxFileNameLength);
        }

        return sanitizedInput;
    }
}
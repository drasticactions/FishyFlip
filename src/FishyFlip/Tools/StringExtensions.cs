// <copyright file="StringExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Provides extension methods for the <see cref="string"/> class.
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// Formats the value of the specified type into a string.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to format.</param>
    /// <returns>A string that represents the value and its type.</returns>
    internal static string FormatValue<T>(T value) => $"{typeof(T).FullName}: {value?.ToString()}";

    /// <summary>
    /// Generates a valid filename from the specified string.
    /// </summary>
    /// <param name="input">The string to generate a filename from.</param>
    /// <returns>A valid filename.</returns>
    /// <remarks>
    /// This method removes invalid characters from the input string and truncates it if it exceeds the maximum length allowed for a filename in Windows.
    /// </remarks>
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

    /// <summary>
    /// IsNullOrEmpty extension method for strings.
    /// </summary>
    /// <param name="input">String.</param>
    /// <returns>If null or empty.</returns>
    internal static bool IsNullOrEmpty(this string input)
        => string.IsNullOrEmpty(input);
}
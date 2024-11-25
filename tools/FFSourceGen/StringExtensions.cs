// <copyright file="StringExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Text;

namespace FFSourceGen;

/// <summary>
/// String Extensions.
/// </summary>
internal static class StringExtensions
{
    public static string ToPreventClassPropertyNameClash(this string input, string className)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        if (input == className)
        {
            return input + "Value";
        }

        return input;
    }

    /// <summary>
    /// Convert a string to a class safe name.
    /// </summary>
    /// <param name="input">Input string.</param>
    /// <returns>String with class safe name.</returns>
    public static string ToClassSafe(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c))
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
}
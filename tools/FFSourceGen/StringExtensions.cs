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
    /// <summary>
    /// Convert a string to Pascal Case.
    /// </summary>
    /// <param name="input">String.</param>
    /// <returns>String with Pascal Case.</returns>
    public static string ToPascalCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        StringBuilder result = new StringBuilder();
        bool nextUpper = true;

        for (int i = 0; i < input.Length; i++)
        {
            if (i > 0 && char.IsUpper(input[i]))
            {
                result.Append(input[i]);
            }
            else if (nextUpper)
            {
                result.Append(char.ToUpper(input[i]));
                nextUpper = false;
            }
            else
            {
                result.Append(input[i]);
            }
        }

        return result.ToString();
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
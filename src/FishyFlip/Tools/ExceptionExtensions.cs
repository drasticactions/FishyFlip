// <copyright file="ExceptionExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Extension Methods for Exceptions.
/// </summary>
internal static class ExceptionExtensions
{
    internal static T ThrowIfNull<T>(this T? t)
    {
        ArgumentNullException.ThrowIfNull(t);
        return t;
    }
}
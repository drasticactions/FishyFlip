// <copyright file="StringExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

internal static class StringExtensions
{
    internal static string FormatValue<T>(T value) => $"{typeof(T).FullName}: {value?.ToString()}";
}
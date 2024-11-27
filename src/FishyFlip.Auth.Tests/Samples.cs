﻿// <copyright file="Samples.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tests;

/// <summary>
/// Samples.
/// </summary>
internal static class Samples
{
    /// <summary>
    /// Gets a base64 image.
    /// </summary>
    public static string Base64Image => @"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";
}

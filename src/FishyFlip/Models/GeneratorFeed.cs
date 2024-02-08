// <copyright file="GeneratorFeed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a generator feed.
/// </summary>
/// <param name="Feeds">The array of generator views.</param>
/// <param name="Cursor">The cursor for pagination.</param>
public record GeneratorFeed(GeneratorView[] Feeds, string? Cursor);

// <copyright file="ListFeed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a list feed containing an array of <see cref="FeedViewPost"/> and a cursor.
/// </summary>
public record ListFeed(FeedViewPost[] Feed, string? Cursor);

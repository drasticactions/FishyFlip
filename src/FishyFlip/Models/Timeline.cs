// <copyright file="Timeline.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a timeline containing feed posts and a cursor.
/// </summary>
public record Timeline(FeedViewPost[] Feed, string? Cursor);
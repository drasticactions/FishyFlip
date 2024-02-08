// <copyright file="SkeletonFeed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a skeleton feed.
/// </summary>
public record SkeletonFeed(SkeletonFeedPost[] Feed, string? Cursor);

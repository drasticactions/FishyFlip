// <copyright file="LikesFeed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a feed of likes.
/// </summary>
public record LikesFeed(Like[] Likes, string? Cursor);
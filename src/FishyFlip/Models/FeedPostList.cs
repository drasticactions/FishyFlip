// <copyright file="FeedPostList.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a list of feed posts.
/// </summary>
public record FeedPostList(FeedViewPost[] Feed, string? Cursor);

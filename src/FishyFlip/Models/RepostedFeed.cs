// <copyright file="RepostedFeed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reposted feed.
/// </summary>
public record RepostedFeed(FeedProfile[] RepostedBy, string? Cursor);
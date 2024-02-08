// <copyright file="ActorFollowers.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the followers of an actor in a feed.
/// </summary>
public record ActorFollowers(FeedProfile? Subject, FeedProfile[]? Followers, string? Cursor);
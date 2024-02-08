// <copyright file="ActorFollows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the relationship between an actor and the profiles they follow.
/// </summary>
public record ActorFollows(FeedProfile? Subject, FeedProfile[]? Follows, string? Cursor);
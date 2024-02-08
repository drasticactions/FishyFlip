// <copyright file="FeedResultList.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a list of feed results.
/// </summary>
public record FeedResultList(FeedRecord[] Feeds, string? Cursor);

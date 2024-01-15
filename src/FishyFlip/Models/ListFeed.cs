// <copyright file="ListFeed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record ListFeed(FeedViewPost[] Feed, string? Cursor);

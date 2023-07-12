// <copyright file="Timeline.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record Timeline(FeedViewPost[] Feed, string? Cursor);
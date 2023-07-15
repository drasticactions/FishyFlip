// <copyright file="ActorFollows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record ActorFollows(FeedProfile? Subject, FeedProfile[]? Follows, string? Cursor);
// <copyright file="ActorFollowers.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record ActorFollowers(FeedProfile? Subject, FeedProfile[]? Followers, string? Cursor);
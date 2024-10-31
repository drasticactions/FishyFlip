﻿// <copyright file="FeedProfile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a feed profile.
/// </summary>
public record FeedProfile(ATDid Did,
    string Handle,
    string DisplayName,
    string Description,
    string Avatar,
    string Banner,
    int FollowsCount,
    int FollowersCount,
    int PostsCount,
    PinnedPost? PinnedPost,
    DateTime IndexedAt,
    Viewer Viewer,
    IReadOnlyList<Label> Labels);

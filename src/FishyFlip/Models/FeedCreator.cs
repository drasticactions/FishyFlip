// <copyright file="FeedCreator.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a feed creator.
/// </summary>
public record FeedCreator(string Did,
    string Handle,
    string DisplayName,
    string Avatar,
    Viewer Viewer,
    IReadOnlyList<Label> Labels);

// <copyright file="FeedViewPostReply.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reply to a post in a feed view.
/// </summary>
public record FeedViewPostReply(PostView? Root, PostView? Parent);
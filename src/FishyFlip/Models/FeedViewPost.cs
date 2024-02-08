// <copyright file="FeedViewPost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a post in a feed view.
/// </summary>
public record FeedViewPost(PostView Post, FeedViewPostReply? Reply, ReplyRef? Ref, ReasonRepost? Reason);

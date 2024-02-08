// <copyright file="SkeletonFeedPost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a skeleton feed post.
/// </summary>
/// <param name="Post">The ATUri of the post.</param>
/// <param name="Reason">The reason for reposting the skeleton feed post.</param>
public record SkeletonFeedPost(ATUri Post, SkeletonReasonRepost? Reason);

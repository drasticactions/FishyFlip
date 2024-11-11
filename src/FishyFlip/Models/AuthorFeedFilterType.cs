// <copyright file="AuthorFeedFilterType.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the types of filtering that can be passed to <see cref="BlueskyFeed.GetActorFeedsAsync(ATIdentifier, int, string?, AuthorFeedFilterType, CancellationToken)"/>.
/// </summary>
public enum AuthorFeedFilterType
{
    /// <summary>
    /// Includes posts and replies.
    /// </summary>
    PostsWithReplies,

    /// <summary>
    /// Includes posts only, no replies.
    /// </summary>
    PostsNoReplies,

    /// <summary>
    /// Includes only posts with media attachments.
    /// </summary>
    PostsWithMedia,

    /// <summary>
    /// Includes top-level posts, and threads where the only author is the specified actor.
    /// </summary>
    PostsAndAuthorThreads,
}

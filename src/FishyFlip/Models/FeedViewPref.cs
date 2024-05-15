// <copyright file="FeedViewPref.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Feed View Preference.
/// </summary>
public class FeedViewPref : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedViewPref"/> class.
    /// </summary>
    /// <param name="hideReplies">A boolean value indicating whether to hide replies or not.</param>
    /// <param name="hideReposts">A boolean value indicating whether to hide reposts or not.</param>
    /// <param name="hideQuotePosts">A boolean value indicating whether to hide quote posts or not.</param>
    /// <param name="hideRepliesByLikeCount">An integer value indicating the like count threshold for hiding replies.</param>
    /// <param name="hideRepliesByUnfollowed">A boolean value indicating whether to hide replies by unfollowed users or not.</param>
    /// <param name="feed">A string representing the feed.</param>
    /// <param name="type">An optional string representing the type of the feed view preference.</param>
    public FeedViewPref(bool hideReplies, bool hideReposts, bool hideQuotePosts, int hideRepliesByLikeCount, bool hideRepliesByUnfollowed, string feed, string? type = default)
        : base(type)
    {
        this.HideReplies = hideReplies;
        this.HideReposts = hideReposts;
        this.HideQuotePosts = hideQuotePosts;
        this.HideRepliesByLikeCount = hideRepliesByLikeCount;
        this.HideRepliesByUnfollowed = hideRepliesByUnfollowed;
        this.Feed = feed;
    }

    /// <summary>
    /// Gets a value indicating whether replies should be hidden.
    /// </summary>
    public bool HideReplies { get; }

    /// <summary>
    /// Gets a value indicating whether reposts should be hidden.
    /// </summary>
    public bool HideReposts { get; }

    /// <summary>
    /// Gets a value indicating whether quote posts should be hidden.
    /// </summary>
    public bool HideQuotePosts { get; }

    /// <summary>
    /// Gets the like count threshold for hiding replies.
    /// </summary>
    public int HideRepliesByLikeCount { get; }

    /// <summary>
    /// Gets a value indicating whether replies by unfollowed users should be hidden.
    /// </summary>
    public bool HideRepliesByUnfollowed { get; }

    /// <summary>
    /// Gets the feed.
    /// </summary>
    public string Feed { get; }
}
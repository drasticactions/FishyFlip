// <copyright file="SkeletonFeedPost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Skeleton Feed Post.
/// </summary>
public class SkeletonFeedPost
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SkeletonFeedPost"/> class.
    /// </summary>
    /// <param name="post">ATPost Did.</param>
    /// <param name="reason">Optional Reason for post.</param>
    [JsonConstructor]
    public SkeletonFeedPost(string post, SkeletonReasonRepost? reason = null)
    {
        this.Post = post;
        this.Reason = reason;
    }

    /// <summary>
    /// Gets the ATUri of the post.
    /// </summary>
    [JsonPropertyName("post")]
    public string Post { get; }

    /// <summary>
    /// Gets the reason for reposting the skeleton feed post.
    /// </summary>
    [JsonPropertyName("reason")]
    public SkeletonReasonRepost? Reason { get; }

    /// <summary>
    /// Creates a new SkeletonFeedPost instance.
    /// </summary>
    /// <param name="post">The ATUri of the post.</param>
    /// <param name="reason">The reason for reposting the skeleton feed post.</param>
    /// <returns>A new SkeletonFeedPost instance.</returns>
    public static SkeletonFeedPost Create(ATUri post, SkeletonReasonRepost? reason = null)
    {
        return new SkeletonFeedPost(post.ToString(), reason);
    }
}

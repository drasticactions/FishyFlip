// <copyright file="ThreadView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a view of a thread, including the main post and its replies.
/// </summary>
public class ThreadView : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadView"/> class.
    /// </summary>
    /// <param name="post">The main post of the thread.</param>
    /// <param name="replies">The replies to the main post.</param>
    /// <param name="type">The type of the thread.</param>
    [JsonConstructor]
    public ThreadView(PostView? post, ThreadView[] replies, string type)
    {
        this.Post = post;
        this.Replies = replies;
        this.Type = type;
    }

    /// <summary>
    /// Gets the main post of the thread.
    /// </summary>
    public PostView? Post { get; }

    /// <summary>
    /// Gets the replies to the main post.
    /// </summary>
    public ThreadView[]? Replies { get; }
}
// <copyright file="PinnedPost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a pinned post.
/// </summary>
public class PinnedPost
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PinnedPost"/> class.
    /// </summary>
    /// <param name="uri">The Post URI.</param>
    /// <param name="cid">The reference to the post.</param>
    [JsonConstructor]
    public PinnedPost(ATUri? uri, ATCid? cid)
    {
        this.Cid = cid;
        this.Uri = uri;
    }

    /// <summary>
    /// Gets the ATCid of the created post.
    /// </summary>
    public ATCid? Cid { get; }

    /// <summary>
    /// Gets the Uri of the created post.
    /// </summary>
    public ATUri? Uri { get; }
}
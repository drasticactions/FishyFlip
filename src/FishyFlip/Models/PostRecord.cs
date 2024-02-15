// <copyright file="PostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record for a post.
/// </summary>
public class PostRecord : ATFeedTypeAPI
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PostRecord"/> class.
    /// </summary>
    /// <param name="value">The post value.</param>
    /// <param name="uri">The URI of the post.</param>
    /// <param name="cid">The CID of the post.</param>
    [JsonConstructor]
    public PostRecord(Post? value, ATUri? uri, ATCid? cid)
        : base(uri, cid)
    {
        this.Value = value;
    }

    /// <summary>
    /// Gets the post value.
    /// </summary>
    public Post? Value { get; }
}

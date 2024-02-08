// <copyright file="CreatePostResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the response object for creating a post.
/// </summary>
public class CreatePostResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePostResponse"/> class.
    /// </summary>
    /// <param name="uri">The Post URI.</param>
    /// <param name="cid">The reference to the post.</param>
    [JsonConstructor]
    public CreatePostResponse(ATUri? uri, Cid? cid)
    {
        this.Cid = cid;
        this.Uri = uri;
    }

    /// <summary>
    /// Gets the Cid of the created post.
    /// </summary>
    public Cid? Cid { get; }

    /// <summary>
    /// Gets the Uri of the created post.
    /// </summary>
    public ATUri? Uri { get; }
}
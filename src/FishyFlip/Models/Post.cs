// <copyright file="Post.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Org.BouncyCastle.Ocsp;

namespace FishyFlip.Models;

/// <summary>
/// Represents a post.
/// </summary>
public class Post : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Post"/> class.
    /// </summary>
    /// <param name="embed">The embed associated with the post.</param>
    /// <param name="facets">The facets of the post.</param>
    /// <param name="createdAt">The creation date of the post.</param>
    /// <param name="reply">The reply associated with the post.</param>
    /// <param name="text">The text content of the post.</param>
    /// <param name="langs">The languages associated with the post.</param>
    /// <param name="type">The type of the post.</param>
    [JsonConstructor]
    public Post(Embed? embed, Facet[]? facets, DateTime? createdAt, Reply? reply, string? text, string[]? langs, string? type)
        : base(type)
    {
        this.Embed = embed;
        this.Facets = facets;
        this.CreatedAt = createdAt;
        this.Reply = reply;
        this.Text = text;
        this.Langs = langs;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Post"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the post.</param>
    public Post(CBORObject obj)
    {
        this.Type = Constants.FeedType.Post;
        if (!obj["embed"]?.IsNull ?? false)
        {
            this.Embed = obj["embed"].ToEmbed();
#if DEBUG
            if (this.Embed is null)
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
        }

        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Text = obj["text"].AsString();
        this.Langs = obj["langs"]?.Values.Select(n => n.AsString()).ToArray();
        this.Facets = obj["facets"]?.Values.Select(n => new Facet(n)).ToArray();
        this.Reply = obj["reply"]?.IsNull ?? false ? null : obj["reply"].ToReply();
    }

    /// <summary>
    /// Gets the embed associated with the post.
    /// </summary>
    public Embed? Embed { get; }

    /// <summary>
    /// Gets the facets of the post.
    /// </summary>
    public Facet[]? Facets { get; }

    /// <summary>
    /// Gets the creation date of the post.
    /// </summary>
    public DateTime? CreatedAt { get; }

    /// <summary>
    /// Gets or sets the reply associated with the post.
    /// </summary>
    public Reply? Reply { get; set; }

    /// <summary>
    /// Gets the text content of the post.
    /// </summary>
    public string? Text { get; }

    /// <summary>
    /// Gets the languages associated with the post.
    /// </summary>
    public string[]? Langs { get; }
}
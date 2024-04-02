// <copyright file="ThreadGate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// ATProtocol ThreadGate.
/// Record defining interaction gating rules for a thread (aka, reply controls).
/// The record key (rkey) of the thread gate record must match the record key of the thread's root post, and that record must be in the same repository.
/// </summary>
public class ThreadGate : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadGate"/> class.
    /// </summary>
    /// <param name="post">The URI of the post. Can be null.</param>
    /// <param name="allow">Array of allowed gate types. Can be null.</param>
    /// <param name="createdAt">The date and time when the ThreadGate was created. Can be null.</param>
    [JsonConstructor]
    public ThreadGate(ATUri? post, ThreadGateReason[]? allow, DateTime? createdAt)
    {
        this.Post = post;
        this.Allow = allow ?? [];
        this.CreatedAt = createdAt;
        this.Type = Constants.FeedType.ThreadGate;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadGate"/> class.
    /// </summary>
    /// <param name="obj">CBorObject.</param>
    public ThreadGate(CBORObject obj)
    {
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.FeedType.ThreadGate;
        this.Post = new ATUri(obj["post"].AsString());
        this.Allow = obj["allow"].Values.Select(n => new ThreadGateReason(n)).ToArray()!;
    }

    /// <summary>
    /// Gets the URI of the repost.
    /// </summary>
    public ATUri? Post { get; }

    /// <summary>
    /// Gets the allowed gate types.
    /// </summary>
    public ThreadGateReason[] Allow { get; }

    /// <summary>
    /// Gets the Created At Date.
    /// </summary>
    public DateTime? CreatedAt { get; }
}
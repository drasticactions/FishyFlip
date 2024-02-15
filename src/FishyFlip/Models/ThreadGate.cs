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
    [JsonConstructor]
    public ThreadGate()
    {
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
        this.AllowedTypes = obj["allow"].Values.Select(n => new ThreadGateReason(n.AsString())).ToArray()!;
    }

    /// <summary>
    /// Gets the URI of the repost.
    /// </summary>
    [JsonPropertyName("post")]
    public ATUri? Post { get; }

    /// <summary>
    /// Gets the allowed gate types.
    /// </summary>
    [JsonPropertyName("allow")]
    public ThreadGateReason[] AllowedTypes { get; } = [];

    /// <summary>
    /// Gets the Created At Date.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; }
}
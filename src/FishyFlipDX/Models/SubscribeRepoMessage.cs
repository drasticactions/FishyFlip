// <copyright file="SubscribeRepoMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a message for subscribing to a repository.
/// </summary>
public class SubscribeRepoMessage
{
    /// <summary>
    /// Gets the header of the message.
    /// </summary>
    public FrameHeader? Header { get; internal set; }

    /// <summary>
    /// Gets the commit of the message.
    /// </summary>
    public FrameCommit? Commit { get; internal set; }

    /// <summary>
    /// Gets the handle of the message.
    /// </summary>
    public FrameHandle? Handle { get; internal set; }

    /// <summary>
    /// Gets the footer of the message.
    /// </summary>
    public FrameFooter? Footer { get; internal set; }

    /// <summary>
    /// Gets the record of the message.
    /// </summary>
    public ATObject? Record { get; internal set; }

    /// <summary>
    /// Gets the atError of the message.
    /// </summary>
    public FrameError? Error { get; internal set; }

    /// <summary>
    /// Gets the info of the message.
    /// </summary>
    public FrameInfo? Info { get; internal set; }

    /// <summary>
    /// Gets the migrate of the message.
    /// </summary>
    public FrameMigrate? Migrate { get; internal set; }

    /// <summary>
    /// Gets the repository operation of the message.
    /// </summary>
    public FrameRepoOp? RepoOp { get; internal set; }

    /// <summary>
    /// Gets the tombstone of the message.
    /// </summary>
    public FrameTombstone? Tombstone { get; internal set; }

    /// <summary>
    /// Gets the list of nodes in the message.
    /// </summary>
    public List<FrameNode> Nodes { get; } = new List<FrameNode>();
}

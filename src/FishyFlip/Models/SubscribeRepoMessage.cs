// <copyright file="SubscribeRepoMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class SubscribeRepoMessage
{
    public FrameHeader? Header { get; internal set; }

    public FrameCommit? Commit { get; internal set; }

    public FrameHandle? Handle { get; internal set; }

    public FrameFooter? Footer { get; internal set; }

    public ATRecord? Record { get; internal set; }

    public FrameError? Error { get; internal set; }

    public FrameInfo? Info { get; internal set; }

    public FrameMigrate? Migrate { get; internal set; }

    public FrameRepoOp? RepoOp { get; internal set; }

    public FrameTombstone? Tombstone { get; internal set; }

    public List<FrameNode> Nodes { get; } = new List<FrameNode>();
}

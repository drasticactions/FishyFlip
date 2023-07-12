// <copyright file="FrameRepoOp.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame Repo Op.
/// </summary>
public class FrameRepoOp
{
    public FrameRepoOp(CBORObject obj)
    {
        this.Cid = obj["cid"] is not null ? Cid.Decode(obj["cid"].AsString()) : null;
        this.Path = obj["path"]?.AsString();
        this.Action = obj["action"]?.AsString();
    }

    public Cid? Cid { get; }

    public string? Path { get; }

    public string? Action { get; }
}
// <copyright file="FrameCommit.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class FrameCommit
{
    public FrameCommit(CBORObject obj)
    {
        this.Ops = obj["ops"]?.Values.Select(n => new Ops(n)).ToArray();
        this.Seq = obj["seq"].AsInt32();
        this.Blocks = obj["blocks"]?.GetByteString();
#pragma warning disable CS0618
        this.Prev = obj["prev"]?.ToCid();
#pragma warning restore CS0618
        this.Rev = obj["rev"]?.AsString();
        this.Since = obj["since"]?.AsString();
        this.Commit = obj["commit"].ToCid();
        this.Repo = obj["repo"] is not null ? ATDid.Create(obj["repo"].AsString()) : null;
        this.Handle = obj["handle"] is not null ? ATHandle.Create(obj["handle"].AsString()) : null;
        this.Rebase = obj["rebase"]?.AsBoolean() ?? false;
        this.TooBig = obj["tooBig"]?.AsBoolean() ?? false;
        var time = obj["time"]?.AsString();
        if (!string.IsNullOrEmpty(time))
        {
            this.Time = DateTime.Parse(time);
        }
    }

    /// <summary>
    /// Gets the list of operations.
    /// </summary>
    public Ops[]? Ops { get; }

    /// <summary>
    /// Gets the sequence.
    /// </summary>
    public int Seq { get; }

    /// <summary>
    /// Gets the handle.
    /// </summary>
    public ATHandle? Handle { get; }

    [Obsolete("Obsolete in the V3 Spec, use Rev instead.")]
    /// <summary>
    /// Gets the previous id.
    /// </summary>
    public Cid? Prev { get; }

    /// <summary>
    /// Gets the rev.
    /// </summary>
    public string? Rev { get; }

    /// <summary>
    /// Gets the previous rev.
    /// </summary>
    public string? Since { get; }

    /// <summary>
    /// Gets the repo.
    /// </summary>
    public ATDid? Repo { get; }

    /// <summary>
    /// Gets the date time of the frame.
    /// </summary>
    public DateTime? Time { get; }

    /// <summary>
    /// Gets the CBOR blocks.
    /// </summary>
    public byte[]? Blocks { get; }

    /// <summary>
    /// Gets the commit.
    /// </summary>
    public Cid? Commit { get; }

    /// <summary>
    /// Gets a value indicating whether this is a rebase.
    /// </summary>
    public bool Rebase { get; }

    /// <summary>
    /// Gets a value indicating whether this is too big.
    /// I have no idea what that means.
    /// </summary>
    public bool TooBig { get; }
}
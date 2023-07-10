// <copyright file="FrameBody.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Cbor;

public class FrameBody
{
    /// <summary>
    /// Gets or sets the list of operations.
    /// </summary>
    public Ops[]? Ops { get; set; }

    /// <summary>
    /// Gets or sets the sequence.
    /// </summary>
    public int Seq { get; set; }

    /// <summary>
    /// Gets or sets the previous id.
    /// </summary>
    public string? Prev { get; set; }

    /// <summary>
    /// Gets or sets the repo.
    /// </summary>
    public string? Repo { get; set; }

    /// <summary>
    /// Gets or sets the date time of the frame.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the CBOR blocks.
    /// </summary>
    public byte[]? Blocks { get; set; }

    /// <summary>
    /// Gets or sets the commit.
    /// </summary>
    public string? Commit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a rebase.
    /// </summary>
    public bool Rebase { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is too big.
    /// I have no idea what that means.
    /// </summary>
    public bool TooBig { get; set; }
}
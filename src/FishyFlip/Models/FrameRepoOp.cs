// <copyright file="FrameRepoOp.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an operation on a frame repository.
/// </summary>
public class FrameRepoOp
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameRepoOp"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the operation data.</param>
    public FrameRepoOp(CBORObject obj)
    {
        this.Cid = obj["cid"] is not null ? Ipfs.Cid.Decode(obj["cid"].AsString()).ToString() : null;
        this.Path = obj["path"]?.AsString();
        this.Action = obj["action"]?.AsString();
    }

    /// <summary>
    /// Gets the CID associated with the operation.
    /// </summary>
    public ATCid? Cid { get; }

    /// <summary>
    /// Gets the path associated with the operation.
    /// </summary>
    public string? Path { get; }

    /// <summary>
    /// Gets the action associated with the operation.
    /// </summary>
    public string? Action { get; }
}
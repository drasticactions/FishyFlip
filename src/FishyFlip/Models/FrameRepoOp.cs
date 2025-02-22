// <copyright file="FrameRepoOp.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an operation on a frame repository.
/// </summary>
public class FrameRepoOp : ICBOREncodable<FrameRepoOp>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameRepoOp"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the operation data.</param>
    public FrameRepoOp(CBORObject obj)
    {
        this.Cid = obj["cid"] is not null ? Ipfs.Cid.Decode(obj["cid"].AsString()) : null;
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

    /// <summary>
    /// Creates a new instance of the <see cref="FrameRepoOp"/> class from a CBORObject.
    /// </summary>
    /// <param name="obj">The CBORObject to convert into a FrameRepoOp instance.</param>
    /// <returns>A new instance of the <see cref="FrameRepoOp"/> class.</returns>
    public static FrameRepoOp FromCBORObject(CBORObject obj)
    {
        return new FrameRepoOp(obj);
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}
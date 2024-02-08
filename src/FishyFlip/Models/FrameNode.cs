// <copyright file="FrameNode.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a node in a frame, containing data.
/// </summary>
public class FrameNode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameNode"/> class with the specified CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object to initialize the node with.</param>
    public FrameNode(CBORObject obj)
    {
        this.Data = obj?.EncodeToBytes();
    }

    /// <summary>
    /// Gets the data contained in the node.
    /// </summary>
    public byte[]? Data { get; }
}

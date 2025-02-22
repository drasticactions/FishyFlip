// <copyright file="FrameNode.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a node in a frame, containing data.
/// https://atproto.com/ja/specs/repository.
/// </summary>
public class FrameNode : ICBOREncodable<FrameNode>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameNode"/> class with the specified CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object to initialize the node with.</param>
    public FrameNode(CBORObject obj)
    {
        this.Left = obj["l"]?.ToATCid();
        this.Entries = obj["e"]?.Values.Select(n => new FrameEntry(n)).ToArray();
    }

    /// <summary>
    /// Gets the link to sub-tree Node on a lower level and with all keys sorting before keys at this node.
    /// </summary>
    public ATCid? Left { get; }

    /// <summary>
    /// Gets an ordered list of TreeEntry objects.
    /// </summary>
    public FrameEntry[]? Entries { get; }

    /// <inheritdoc/>
    public static FrameNode FromCBORObject(CBORObject obj)
    {
        return new FrameNode(obj);
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}

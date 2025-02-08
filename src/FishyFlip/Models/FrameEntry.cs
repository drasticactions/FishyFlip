// <copyright file="FrameEntry.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon;

namespace FishyFlip.Models;

/// <summary>
/// Represents a frame entry.
/// </summary>
public class FrameEntry
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameEntry"/> class.
    /// </
    /// <param name="obj">The CBOR object.</param>
    public FrameEntry(CBORObject obj)
    {
        this.PrefixLength = obj["p"].AsInt64Value();
        this.KeySuffix = obj["k"].EncodeToBytes();
        this.Value = obj["v"].ToATCid()!;
        this.Tree = obj["t"]?.ToATCid();
    }

    /// <summary>
    /// Gets the count of bytes shared with previous TreeEntry in this Node (if any).
    /// </summary>
    public long PrefixLength { get; }

    /// <summary>
    /// Gets the remainder of key for this TreeEntry, after "prefixlen" have been removed.
    /// </summary>
    public byte[] KeySuffix { get; }

    /// <summary>
    /// Gets the link to the record data (CBOR) for this entry.
    /// </summary>
    public ATCid Value { get; }

    /// <summary>
    /// Gets the link to a sub-tree Node at a lower level which has keys sorting after this TreeEntry's key (to the "right"), but before the next TreeEntry's key in this Node (if any).
    /// </summary>
    public ATCid? Tree { get; }
}
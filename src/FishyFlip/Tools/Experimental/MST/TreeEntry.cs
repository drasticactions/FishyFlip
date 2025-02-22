// <copyright file="TreeEntry.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Represents a tree entry in a Merkle Patricia Trie.
/// </summary>
public struct TreeEntry : ICBOREncodable<TreeEntry>
{
    /// <summary>
    /// Prefix count of ascii chars that this key shares with the prev key.
    /// </summary>
    public int PrefixCount;

    /// <summary>
    /// The rest of the key outside the shared prefix.
    /// </summary>
    public byte[] Key;

    /// <summary>
    /// Gets the value associated with the key.
    /// </summary>
    public ATCid Value;

    /// <summary>
    /// Next subtree (to the right of the leaf).
    /// </summary>
    public ATCid? Tree;

    /// <summary>
    /// Gets the rest of the key outside the shared prefix as a string.
    /// </summary>
    public string KeyString => Encoding.ASCII.GetString(this.Key);

    /// <summary>
    /// Creates a new instance of the <see cref="TreeEntry"/> struct from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBORObject.</param>
    /// <returns><see cref="TreeEntry"/>.</returns>
    public static TreeEntry FromCBORObject(CBORObject obj)
    {
        var prefixCount = obj["p"].AsInt32();
        var key = obj["k"].GetByteString();
        var value = obj["v"].ToATCid();
        Cid? tree = obj.ContainsKey("t") && !obj["t"].IsNull ? obj["t"].ToATCid() : null;
        return new TreeEntry
        {
            PrefixCount = prefixCount,
            Key = key,
            Value = value!,
            Tree = tree,
        };
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}

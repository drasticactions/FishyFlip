// <copyright file="Leaf.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Represents a leaf node in a Merkle Patricia Trie.
/// </summary>
public class Leaf : INodeEntry
{
    /// <summary>
    /// The key associated with the leaf node.
    /// </summary>
    public readonly string Key;

    /// <summary>
    /// The value associated with the leaf node.
    /// </summary>
    public readonly ATCid Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Leaf"/> class.
    /// </summary>
    /// <param name="key">The key associated with the leaf node.</param>
    /// <param name="value">The value associated with the leaf node.</param>
    public Leaf(string key, Cid value)
    {
        this.Key = key;
        this.Value = value;
    }
}

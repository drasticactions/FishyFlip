// <copyright file="NodeData.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Represents the data of a node in a Merkle Patricia Trie.
/// </summary>
public struct NodeData : ICBOREncodable<NodeData>
{
    /// <summary>
    /// Left-most subtree.
    /// </summary>
    public ATCid? Left;

    /// <summary>
    /// Entries.
    /// </summary>
    public List<TreeEntry> Entries;

    /// <summary>
    /// Creates a new instance of the <see cref="NodeData"/> struct from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBORObject.</param>
    /// <returns><see cref="NodeData"/>.</returns>
    public static NodeData FromCBORObject(CBORObject obj)
    {
        ATCid? left = obj.ContainsKey("l") && !obj["l"].IsNull ? obj["l"].ToATCid() : null;
        var entries = obj["e"].Values.Select(TreeEntry.FromCBORObject).ToList();
        return new NodeData
        {
            Left = left,
            Entries = entries,
        };
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}

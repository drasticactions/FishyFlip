// <copyright file="DataDiff.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// DataDiff class.
/// </summary>
public class DataDiff
{
    /// <summary>
    /// Gets or sets the adds.
    /// </summary>
    [JsonPropertyName("adds")]
    public Dictionary<string, DataAdd> Adds { get; set; } = new();

    /// <summary>
    /// Gets or sets the updates.
    /// </summary>
    [JsonPropertyName("updates")]
    public Dictionary<string, DataUpdate> Updates { get; set; } = new();

    /// <summary>
    /// Gets or sets the deletes.
    /// </summary>
    [JsonPropertyName("deletes")]
    public Dictionary<string, DataDelete> Deletes { get; set; } = new();

    /// <summary>
    /// Gets or sets the new MST blocks.
    /// </summary>
    [JsonPropertyName("newMstBlocks")]
    public BlockMap NewMstBlocks { get; set; } = new();

    /// <summary>
    /// Gets or sets the new leaf CIDs.
    /// </summary>
    [JsonPropertyName("newLeafCids")]
    public CidSet NewLeafCids { get; set; } = new();

    /// <summary>
    /// Gets or sets the removed CIDs.
    /// </summary>
    [JsonPropertyName("removedCids")]
    public CidSet RemovedCids { get; set; } = new();

    /// <summary>
    /// Diffs the specified current and previous MST.
    /// </summary>
    /// <param name="curr">Current MST.</param>
    /// <param name="prev">Previous MST.</param>
    /// <returns>Task of diff.</returns>
    public static async Task<DataDiff> Of(MST curr, MST? prev)
    {
        return await MSTDiff.Diff(curr, prev);
    }

    /// <summary>
    /// Adds a node.
    /// </summary>
    /// <param name="entry">Node.</param>
    /// <returns>Task.</returns>
    public async Task NodeAdd(INodeEntry entry)
    {
        if (entry is Leaf leaf)
        {
            this.LeafAdd(leaf.Key, leaf.Value);
        }
        else if (entry is MST tree)
        {
            var (cid, bytes) = await tree.Serialize();
            this.TreeAdd(cid, bytes);
        }
    }

    /// <summary>
    /// Deletes a node.
    /// </summary>
    /// <param name="entry">Node to delete.</param>
    /// <returns>Task.</returns>
    public async Task NodeDelete(INodeEntry entry)
    {
        if (entry is Leaf leaf)
        {
            this.Deletes[leaf.Key] = new DataDelete(leaf.Key, leaf.Value);
            this.RemovedCids.Add(leaf.Value);
        }
        else if (entry is MST tree)
        {
            var cid = await tree.GetPointer();
            this.TreeDelete(cid);
        }
        else
        {
            throw new Exception("Unknown node type");
        }
    }

    /// <summary>
    /// Adds a leaf.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="cid">Cid.</param>
    public void LeafAdd(string key, Cid cid)
    {
        this.Adds[key] = new DataAdd(key, cid);
        if (this.RemovedCids.Has(cid))
        {
            this.RemovedCids.Delete(cid);
        }
        else
        {
            this.NewLeafCids.Add(cid);
        }
    }

    /// <summary>
    /// Updates a leaf.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="prev">Previous CID.</param>
    /// <param name="cid">CID.</param>
    public void LeafUpdate(string key, Cid prev, Cid cid)
    {
        if (prev.Equals(cid))
        {
            return;
        }

        this.Updates[key] = new DataUpdate(key, prev, cid);
        this.RemovedCids.Add(cid);
        this.NewLeafCids.Add(cid);
    }

    /// <summary>
    /// Deletes a leaf.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="cid">ATCid.</param>
    public void LeafDelete(string key, Cid cid)
    {
        this.Deletes[key] = new DataDelete(key, cid);
        if (this.NewLeafCids.Has(cid))
        {
            this.NewLeafCids.Delete(cid);
        }
        else
        {
            this.RemovedCids.Add(cid);
        }
    }

    /// <summary>
    /// Adds a tree.
    /// </summary>
    /// <param name="cid">CID.</param>
    /// <param name="bytes">Bytes.</param>
    public void TreeAdd(Cid cid, byte[] bytes)
    {
        if (this.RemovedCids.Has(cid))
        {
            this.RemovedCids.Delete(cid);
        }
        else
        {
            this.NewMstBlocks.Set(cid, bytes);
        }
    }

    /// <summary>
    /// Deletes a tree.
    /// </summary>
    /// <param name="cid">ATCid.</param>
    public void TreeDelete(Cid cid)
    {
        if (this.NewMstBlocks.Has(cid))
        {
            this.NewMstBlocks.Delete(cid);
        }
        else
        {
            this.RemovedCids.Add(cid);
        }
    }
}